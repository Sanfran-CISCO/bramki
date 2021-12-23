using System;
using System.Collections;
using System.Collections.Generic;
using bramkominatorMobile.Models;

namespace bramkominatorMobile.Services
{
    public class GatewayList : IEnumerable<Node>
    {
        private Node head;
        public Node First { get => head; set => head = value; }

        private Node tail;
        public Node Last { get => tail; set => tail = value; }

        public int Size { get; private set; }

        public GatewayList()
        {
        }

        public void Add(LogicGateway gateway)
        {
            Node newNode = new Node(gateway);

            if (tail == null)
            {
                head = newNode;
            }
            else
            {
                newNode.Left = tail;
                tail.Next = newNode;
            }
            tail = newNode;
            Size++;
        }

        public void AddAsFirst(LogicGateway gateway)
        {
            Node newNode = new Node(gateway);

            newNode.Next = head;

            if (head == null)
            {
                tail = newNode;
            }
            else
            {
                head.Left = newNode;
            }
            head = newNode;
            Size++;
        }

        public bool Contains(LogicGateway gateway)
        {
            Node currentNode = head;

            while (currentNode != null)
            {
                if (currentNode.Gateway.Type.Equals(gateway.Type))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Remove(LogicGateway gateway)
        {
            Node currentNode = head;

            while (currentNode != null)
            {
                // Currently it will delete all standard gateways with specified type (all Ands etc.)
                // We need to add Gateway Id or something to find target gateway in list
                if (currentNode.Gateway.Name == gateway.Name &&
                    currentNode.Gateway.Type.Equals(gateway.Type))
                {
                    if (currentNode.Next == null)
                    {
                        tail = currentNode.Left;
                    }
                    else
                    {
                        currentNode.Next.Left = currentNode.Left;
                    }

                    if (currentNode.Left == null)
                    {
                        head = currentNode.Next;
                    }
                    else
                    {
                        currentNode.Left.Next = currentNode.Next;
                    }

                    currentNode = null;
                    Size--;
                    return true;
                }

                currentNode = currentNode.Next;
            }
            return false;
        }

        public void RemoveFirst()
        {
            if (head != null)
            {
                head = head.Next;

                if (head == null)
                {
                    tail = null;
                }

                Size--;
            }
        }

        public void RemoveLast()
        {
            if (tail != null)
            {
                tail = tail.Left;

                if (tail == null)
                {
                    head = null;
                }

                Size--;
            }
        }

        public IEnumerator<Node> GetEnumerator()
        {
            Node currentNode = head;

            while (currentNode != null)
            {
                yield return currentNode;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

