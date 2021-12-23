using System;
using System.Collections;
using System.Collections.Generic;
using bramkominatorMobile.Exceptions;
using bramkominatorMobile.Models;

namespace bramkominatorMobile.Services
{
    public class GatewaysList : IEnumerable<Node>
    {
        private Node parent;
        public Node Parent { get => parent; }

        public int Size { get; private set; }

        public GatewaysList()
        {}

        public void SetParent(LogicGateway gateway)
        {
            Node newNode = new Node(gateway);

            if (parent == null)
                Size++;

            parent = newNode;
        }

        public void Connect(LogicGateway from, LogicGateway to, int inputNumber)
        {
            Node fromNode = new Node(from);
            Node toNode = new Node(to);

            if (parent.Gateway.Type == to.Type && parent.Gateway.Name == to.Name)
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
                    node.Left.Next = null;
                    node.Left = null;
                    break;

                case "right":
                    node.Right.Next = null;
                    node.Right = null;
                    break;
            }

            return true;
        }

        private Node FindNode(Node node, LogicGateway gate)
        {
            Node root = node;

            if (root.Gateway.Type == gate.Type &&
                    root.Gateway.Name == gate.Name)
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

        //prototype -- NOT TESTED yet
        public bool Remove(LogicGateway gate)
        {
            Node node = FindNode(new Node(gate), gate);

            if (Disconnect(gate, "next") && Disconnect(gate, "left") && Disconnect(gate, "right"))
            {
                node = null;
                Size--;
                return true;
            }

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

