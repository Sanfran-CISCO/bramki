using System;
using System.Collections;
using System.Collections.Generic;
using SQLite;
using bramkominatorMobile.Exceptions;
using bramkominatorMobile.Models;

namespace bramkominatorMobile.Services
{
    public class LogicCircut : ILogicCircut
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        private Node parent;
        public Node Parent { get => parent; }

        private Node inputNode;
        public Node InputNode { get => inputNode; }

        public int Size { get; private set; }

        public LogicCircut()
        { }

        public void Connect(CircutElement from, CircutElement to, int inputNumber)
        {
            if (parent == null)
            {
                SetParent(to);
            }

            if (inputNode == null)
            {
                inputNode = from.Node;
            }

            if (parent.Content.GetType() == to.GetType() && parent.Content.Position == to.Position)
            {
               to.Node = parent;
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

            if (from.Node == parent)
                SetParent(to);
        }

        private void SetParent(CircutElement element)
        {
            if (parent == null)
                Size++;

            parent = element.Node;
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
                            if (element.Node.Left == inputNode)
                            {
                                if (element.Node.Right != null)
                                    inputNode = element.Node.Right;
                                else
                                    inputNode = element.Node;
                            }
                            element.Node.Left.Next = null;
                            element.Node.Left = null;
                        }
                        break;

                    case "right":
                        if (element.Node.Right != null)
                        {
                            if (element.Node.Right == inputNode)
                            {
                                if (element.Node.Left != null)
                                    inputNode = element.Node.Left;
                                else
                                    inputNode = element.Node;
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
            if (element.Position == inputNode.Content.Position)
            {
                inputNode = element.Node.Next;
            }

            if (element.Node is null)
            {
                return false;
            }
            else if (Disconnect(element, "next") && Disconnect(element, "left") && Disconnect(element, "right"))
            {
                element.Node = null;
                Size--;
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

        public IEnumerator<Node> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

