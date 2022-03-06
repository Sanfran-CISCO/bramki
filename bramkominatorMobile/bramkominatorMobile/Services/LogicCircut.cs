using System;
using System.Collections;
using System.Collections.Generic;
using SQLite;
using bramkominatorMobile.Exceptions;
using bramkominatorMobile.Models;
using System.Linq;
using MvvmHelpers;

namespace bramkominatorMobile.Services
{
    public class LogicCircut : ILogicCircut
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [Ignore]
        public List<CircutElement> Elements { get; set; }

        [Ignore]
        public Node Parent { get; set; }
        public int ParentNodeId { get; set; }

        [Ignore]
        public Node InputNode { get; set; }
        public int InputNodeId { get; set; }

        public int Size { get; private set; }

        public LogicCircut()
        {
            Elements = new List<CircutElement>();
        }

        public void Connect(CircutElement from, CircutElement to, int inputNumber)
        {
            if (Parent == null)
            {
                SetParent(to);
            }

            if (InputNode == null)
            {
                InputNode = from.Node;
            }

            if (Parent.Content.GetType() == to.GetType() && Parent.Content.Position == to.Position)
            {
               to.Node = Parent;
            }

            DisconnectFromCurrentNextGate(from.Node);

            switch(inputNumber)
            {
                case 1:
                    to.Node.Left = from.Node;
                    from.Node.Next = to.Node;
                    break;

                case 2:
                    to.Node.Right = from.Node;
                    from.Node.Next = to.Node;
                    break;

                default:
                throw new BadElementInputException("Bad element input selected!");
            }

            Size++;

            if (from.Node == Parent)
                SetParent(to);

            if (!Elements.Contains(from))
                Elements.Add(from);

            if (!Elements.Contains(to))
                Elements.Add(to);

            from.CircutId = Id;
            to.CircutId = Id;
        }

        private void SetParent(CircutElement element)
        {
            if (Parent == null)
                Size++;

            Parent = element.Node;
        }

        private void DisconnectFromCurrentNextGate(Node fromNode)
        {
            if (fromNode.Next != null && fromNode.Next.Left == fromNode)
            {
                fromNode.Next.Left = null;
            }
            else
            {
                if (fromNode.Next != null && fromNode.Next.Right == fromNode)
                {
                    fromNode.Next.Right = null;
                }
            }
            fromNode.Next = null;
        }

        public bool Disconnect(CircutElement element, string direction)
        {
            if (element.Node is null)
                return false;

            else
            {
                switch (direction.ToLower())
                {
                    case "next":
                        DisconnectFromCurrentNextGate(element.Node);
                        break;

                    case "left":
                        if (element.Node.Left != null)
                        {
                            if (element.Node.Left == InputNode)
                            {
                                if (element.Node.Right != null)
                                    InputNode = element.Node.Right;
                                else
                                    InputNode = element.Node;
                            }
                            element.Node.Left.Next = null;
                            element.Node.Left = null;
                        }
                        break;

                    case "right":
                        if (element.Node.Right != null)
                        {
                            if (element.Node.Right == InputNode)
                            {
                                if (element.Node.Left != null)
                                    InputNode = element.Node.Left;
                                else
                                    InputNode = element.Node;
                            }
                            element.Node.Right.Next = null;
                            element.Node.Right = null;
                        }
                        break;
                }

                return true;
            }
        }

        public bool Remove(CircutElement element)
        {
            if (element.Position == InputNode.Content.Position)
            {
                InputNode = element.Node.Next;
            }

            if (element.Node is null)
            {
                return false;
            }
            else if (Disconnect(element, "next") && Disconnect(element, "left") && Disconnect(element, "right"))
            {
                element.Node = null;
                Size--;

                var el = Elements.FirstOrDefault(x => x.Position == element.Position && x.Name == element.Name);
                Elements.Remove(el);

                element.CircutId = -1;

                Elements.Remove(element);

                return true;
            }

            return false;
        }

        public bool IsConnected(CircutElement element)
        {
            if (element.Node is null)
                throw new ElementNotFoundException();

            if (element.Node.Next != null || element.Node.Left != null || element.Node.Right != null)
                return true;

            return false;
        }

        public IEnumerator<CircutElement> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            int position = 0;
            foreach (var item in Elements)
            {
                position++;
                yield return position;
            }
        }

        public void InitDragHandlers(ref CircutElement[,] matrix)
        {
            foreach (var element in Elements)
                element.SetDragHandler(ref matrix);
        }
    }
}

