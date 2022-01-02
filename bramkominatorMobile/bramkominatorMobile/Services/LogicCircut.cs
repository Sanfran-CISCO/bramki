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

        private Node parent;
        public Node Parent { get => parent; }

        private Node inputNode;
        public Node InputNode { get => inputNode; }

        public int Size { get; private set; }

        public LogicCircut()
        {}

        public void Connect(LogicGateway from, LogicGateway to, int inputNumber)
        {
            Node fromNode = new Node(from);
            Node toNode = new Node(to);

            if (parent == null)
            {
                SetParent(to);
            }

            if (inputNode == null)
            {
                inputNode = fromNode;
            }

            if (parent.Gateway.Type == to.Type && parent.Gateway.Position == to.Position)
            {
                toNode = parent;
            }

            DisconnectFromCurrentNextGate(fromNode);

            switch(inputNumber)
            {
                case 1:
                    fromNode.Next = toNode;
                    toNode.Left = fromNode;
                    toNode.Gateway.InputA = fromNode.Gateway.Output;
                    break;

                case 2:
                    fromNode.Next = toNode;
                    toNode.Right = fromNode;
                    toNode.Gateway.InputB = fromNode.Gateway.Output;
                    break;

                default:
                throw new BadGatewayInputException("Bad gateway input selected!");
            }

            Size++;

            if (toNode.Left == parent.Next || toNode.Right == parent.Next)
                parent = toNode;
        }

        private void SetParent(LogicGateway gateway)
        {
            Node newNode = new Node(gateway);

            if (parent == null)
                Size++;

            parent = newNode;
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
        }

        public bool Disconnect(LogicGateway gate, string direction)
        {
            Node node = FindNode(parent, gate);

            if (node is null)
                return false;

            switch(direction.ToLower())
            {
                case "next":
                    DisconnectFromCurrentNextGate(node);
                    break;

                case "left":
                    if (node.Left != null)
                    {
                        if (node.Left == inputNode)
                        {
                            if (node.Right != null)
                                inputNode = node.Right;
                            else
                                inputNode = node;
                        }

                        node.Left.Next = null;
                        node.Left = null;
                    }
                    break;

                case "right":
                    if (node.Right != null)
                    {
                        if (node.Right == inputNode)
                        {
                            if (node.Left != null)
                                inputNode = node.Left;
                            else
                                inputNode = node;
                        }

                        node.Right.Next = null;
                        node.Right = null;
                    }
                    break;
            }

            return true;
        }

        private Node FindNode(Node node, LogicGateway gate)
        {
            if (node.Left is null && node.Right is null)
            {
                return node;
            }

            else
            {
                Node root = node;

                if (root.Gateway.Type == gate.Type && root.Gateway.Position == gate.Position)
                {
                    return root;
                }
                else
                {
                    root = FindNode(root.Left, gate);
                    if (root is null)
                    {
                        root = FindNode(root.Right, gate);
                    }
                    else
                    {
                        return root;
                    }
                }

                return null;
            }
        }

        public bool Remove(LogicGateway gate)
        {
            Node node = FindNode(parent, gate);

            if (node == inputNode)
            {
                inputNode = node.Next;
            }

            if (node is null)
            {
                return false;
            }
            else if (Disconnect(gate, "next") && Disconnect(gate, "left") && Disconnect(gate, "right"))
            {
                node = null;
                Size--;
                return true;
            }

            return false;
        }

        public bool IsConnected(LogicGateway gate)
        {
            Node node = FindNode(parent, gate);

            if (node is null)
                throw new GatewayNotFoundException();

            if (node.Next != null || node.Left != null || node.Right != null)
                return true;

            return false;
        }

        public IEnumerator<Node> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

