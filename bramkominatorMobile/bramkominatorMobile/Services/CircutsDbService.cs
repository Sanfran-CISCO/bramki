using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Collections.Generic;
using bramkominatorMobile.Exceptions;
using bramkominatorMobile.Models;
using System.Collections;
using System.Linq;
using bramkominatorMobile.Views;

namespace bramkominatorMobile.Services
{
    public static class CircutsDbService
    {
        private static SQLiteAsyncConnection db;

        public static async Task Init()
        {
            if (db != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "bramkominator_CircutsDB.db");

            db = new SQLiteAsyncConnection(dbPath);

            await db.CreateTableAsync<LogicCircut>();
            await db.CreateTableAsync<CircutElement>();
            await db.CreateTableAsync<LogicGateway>();
            await db.CreateTableAsync<InputElement>();
            await db.CreateTableAsync<OutputElement>();
            await db.CreateTableAsync<Position>();
            await db.CreateTableAsync<Node>();
        }

        public static async Task AddCircut(LogicCircut circut)
        {
            await Init();

            // TODO
            // Delete this in production from this
            var gateways = await db.Table<LogicGateway>().ToListAsync();
            foreach (var gate in gateways)
                await db.DeleteAsync(gate);

            var inputElements = await db.Table<InputElement>().ToListAsync();
            foreach (var input in inputElements)
                await db.DeleteAsync(input);

            var outputElements = await db.Table<OutputElement>().ToListAsync();
            foreach (var output in outputElements)
                await db.DeleteAsync(output);

            var nodeElements = await db.Table<Node>().ToListAsync();
            foreach (var node in nodeElements)
                await db.DeleteAsync(node);

            // TODO
            // To this

            if (circut is null)
                throw new InvalidCircutException("Circut can't be null");

            Node parent = circut.Parent;
            Node inputNode = circut.InputNode;

            var elements = new List<CircutElement>();
            var positions = new List<Position>();
            var nodes = new List<Node>();

            var gates = circut.Elements.Where(x => x.GetType() == typeof(LogicGateway));
            var outputs = circut.Elements.Where(x => x.GetType() == typeof(OutputElement));
            var inputs = circut.Elements.Where(x => x.GetType() == typeof(InputElement));

            foreach(var el in circut.Elements)
                elements.Add(el);

            foreach (var element in elements)
            {
                positions.Add(element.Position);
                nodes.Add(element.Node);

                if (element.GetType() == typeof(LogicGateway))
                {
                    (element as LogicGateway).ColorString = (element as LogicGateway).Color.ToHex();

                }
            }

            var id = await db.InsertAsync(circut);

            foreach (var element in elements)
            {
                var node = element.Node;
                element.CircutId = id;
                node.ContentId = await db.InsertAsync(element);
                element.Node = node;
                nodes.Add(node);
                await db.UpdateAsync(element);
            }

            //var gates = await db.Table<LogicGateway>().Where(x => x.CircutId == id).ToListAsync();
            //var outputs = await db.Table<OutputElement>().Where(x => x.CircutId == id).ToListAsync();
            //var inputs = await db.Table<InputElement>().Where(x => x.CircutId == id).ToListAsync();

            elements = new List<CircutElement>();

            foreach (var el in gates)
                elements.Add(el);

            foreach (var el in outputs)
                elements.Add(el);

            foreach (var el in inputs)
                elements.Add(el);

            var elementsSize = elements.Count;
            for (var i=0; i<elementsSize; i++)
            {
                positions[i].ElementId = elements[i].Id;
                elements[i].Node.ElementId = elements[i].Id;
                var content = elements.Find(x => x.Node.Content == elements[i].Node.Content);
                elements[i].Node.ContentId = content.Id;

                await db.UpdateAsync(positions[i]);
                await db.UpdateAsync(elements[i].Node);
            }

            foreach (var node in nodes)
            {
                var added = false;

                node.CircutId = id;

                if (node == parent)
                {
                    parent.Id = await db.InsertAsync(node);
                    continue;
                }

                if (node == inputNode)
                {
                    inputNode.Id = await db.InsertAsync(node);
                    continue;
                }

                foreach (var nd in nodes)
                {
                    if (node.Left == nd) {
                        node.LeftId = await db.InsertAsync(nd);
                        added = true;
                    }
                    else if (node.Right == nd)
                    {
                        node.RightId = await db.InsertAsync(nd);
                        added = true;
                    }
                    else if (node.Next == nd)
                    {
                        node.NextId = await db.InsertAsync(nd);
                        added = true;
                    }
                    else
                        continue;
                }

                //var content = await db.Table<LogicGateway>().FirstOrDefaultAsync(x => x.Id == node.ContentId);

                //node.Content

                if (!added)
                    _ = db.InsertAsync(node);
            }

            await db.InsertAllAsync(positions);

            nodes = await db.Table<Node>().Where(x => x.CircutId == id).ToListAsync();

            foreach (var node in nodes)
            {
                CircutElement content = new CircutElement();

                if (await db.Table<LogicGateway>().FirstOrDefaultAsync(x => x.NodeId == node.Id) != null)
                {
                    content = await db.Table<LogicGateway>().FirstOrDefaultAsync(x => x.NodeId == node.Id);
                }
                else if (await db.Table<OutputElement>().FirstOrDefaultAsync(x => x.NodeId == node.Id) != null)
                {
                    content = await db.Table<OutputElement>().FirstOrDefaultAsync(x => x.NodeId == node.Id);
                }
                else if (await db.Table<InputElement>().FirstOrDefaultAsync(x => x.NodeId == node.Id) != null)
                {
                    content = await db.Table<InputElement>().FirstOrDefaultAsync(x => x.NodeId == node.Id);
                }

                node.ContentId = content.Id;
            }

            circut = await db.Table<LogicCircut>().FirstOrDefaultAsync(x => x.Id == id);

            await db.UpdateAsync(circut);
            await db.UpdateAllAsync(nodes);

            await Shell.Current.DisplayAlert("Circut saved", $"ID: {circut.Id}", "OK");
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        public static async Task RemoveCircut(LogicCircut circut)
        {
            await Init();

            List<CircutElement> elements = await db.Table<CircutElement>().Where(x => x.CircutId == circut.Id).ToListAsync();

            foreach (var element in elements)
            {
                var position = await db.Table<Position>().FirstOrDefaultAsync(x => x.ElementId == element.Id);
                var node = await db.Table<Node>().FirstOrDefaultAsync(x => x.ElementId == element.Id);

                await db.DeleteAsync<Position>(position);
                await db.DeleteAsync<Node>(node);
                await db.DeleteAsync<LogicGateway>(element as LogicGateway);
                await db.DeleteAsync<OutputElement>(element as OutputElement);
                await db.DeleteAsync<InputElement>(element as InputElement);
            }

            await db.DeleteAsync<LogicCircut>(circut.Id);
        }

        public static async Task UpdateCircut(LogicCircut circut)
        {
            await Init();

            var parent = await db.Table<Node>()
                .FirstOrDefaultAsync(x => x.Left == circut.Parent.Left && x.Right == circut.Parent.Right &&
                x.Next == circut.Parent.Next);

            circut.ParentNodeId = parent.Id;

            var inputNode = await db.Table<Node>()
                .FirstOrDefaultAsync(x => x.Left == circut.InputNode.Left && x.Right == circut.InputNode.Right &&
                x.Next == circut.InputNode.Next);

            circut.InputNodeId = inputNode.Id;

            foreach (var element in circut.Elements)
            {
                await db.UpdateAsync(element.Position);
                await db.UpdateAsync(element.Node);
                await db.UpdateAsync(element);
            }

            await db.UpdateAsync(circut);
        }

        public static async Task<IEnumerable<LogicCircut>> GetAllCircuts()
        {
            await Init();

            var circuts = await db.Table<LogicCircut>().ToListAsync();

            foreach (var circut in circuts)
            {
                var elements = new List<CircutElement>();

                var gates = await db.Table<LogicGateway>().Where(x => x.CircutId == circut.Id).ToListAsync();
                var outputs = await db.Table<OutputElement>().Where(x => x.CircutId == circut.Id).ToListAsync();
                var inputs = await db.Table<InputElement>().Where(x => x.CircutId == circut.Id).ToListAsync();

                foreach (var el in gates)
                    elements.Add(el);

                foreach (var el in outputs)
                    elements.Add(el);

                foreach (var el in inputs)
                    elements.Add(el);

                foreach (var element in elements)
                {
                    var position = await db.Table<Position>().FirstOrDefaultAsync(x => x.ElementId == element.Id);
                    var node = await db.Table<Node>().FirstOrDefaultAsync(x => x.ElementId == element.Id);

                    var nodeContent = await db.Table<CircutElement>()
                        .FirstOrDefaultAsync(x => x.NodeId == node.Id);

                    var nodeRight = await db.Table<Node>().FirstOrDefaultAsync(x => x.Id == node.RightId);
                    var nodeLeft = await db.Table<Node>().FirstOrDefaultAsync(x => x.Id == node.LeftId);
                    var nodeNext = await db.Table<Node>().FirstOrDefaultAsync(x => x.Id == node.NextId);

                    node.Content = nodeContent;

                    node.Right = nodeRight;
                    node.Left = nodeLeft;
                    node.Next = nodeNext;

                    element.Position = position;
                    element.Node = node;

                    if (node.Id == circut.InputNodeId)
                        circut.InputNode = node;

                    if (node.Id == circut.ParentNodeId)
                        circut.Parent = node;

                    if (element.GetType() == typeof(LogicGateway))
                    {
                        (element as LogicGateway).Color = (Color.FromHex((element as LogicGateway).ColorString));
                    }
                }

                circut.Elements = elements;
            }

            return circuts;
        }

        public static async Task<LogicCircut> GetCircut(int id)
        {
            await Init();

            var circut = await db.Table<LogicCircut>().FirstOrDefaultAsync(c => c.Id == id);

            var elements = new List<CircutElement>();

            var gates = await db.Table<LogicGateway>().Where(x => x.CircutId == circut.Id).ToListAsync();
            var outputs = await db.Table<OutputElement>().Where(x => x.CircutId == circut.Id).ToListAsync();
            var inputs = await db.Table<InputElement>().Where(x => x.CircutId == circut.Id).ToListAsync();

            foreach (var el in gates)
                elements.Add(el);

            foreach (var el in outputs)
                elements.Add(el);

            foreach (var el in inputs)
                elements.Add(el);

            foreach (var element in elements)
            {
                var position = await db.Table<Position>().FirstOrDefaultAsync(x => x.ElementId == element.Id);
                var node = await db.Table<Node>().FirstOrDefaultAsync(x => x.ElementId == element.Id);

                var nodeContent = await db.Table<CircutElement>()
                        .FirstOrDefaultAsync(x => x.NodeId == node.Id);

                var nodeRight = await db.Table<Node>().FirstOrDefaultAsync(x => x.Id == node.RightId);
                var nodeLeft = await db.Table<Node>().FirstOrDefaultAsync(x => x.Id == node.LeftId);
                var nodeNext = await db.Table<Node>().FirstOrDefaultAsync(x => x.Id == node.NextId);

                node.Content = nodeContent;

                node.Right = nodeRight;
                node.Left = nodeLeft;
                node.Next = nodeNext;

                element.Position = position;
                element.Node = node;

                if (node.Id == circut.InputNodeId)
                    circut.InputNode = node;

                if (node.Id == circut.ParentNodeId)
                    circut.Parent = node;

                if (element.GetType() == typeof(LogicGateway))
                {
                    (element as LogicGateway).Color = (Color.FromHex((element as LogicGateway).ColorString));
                }
            }

            circut.Elements = elements;

            return circut;
        }

        public static async Task<IEnumerable<LogicCircut>> GetAllCircutsSample()
        {
            List<LogicCircut> circuts = new List<LogicCircut>
            {
                new LogicCircut(),
                new LogicCircut(),
                new LogicCircut(),
                new LogicCircut(),
                new LogicCircut(),
            };

            for (int i = 0; i < 5; i++)
            {
                circuts[i].Name = $"Circut {i}";
            }

            await Task.Delay(100);

            return circuts;
        }
    }
}
