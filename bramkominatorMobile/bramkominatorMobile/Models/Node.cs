using System;
namespace bramkominatorMobile.Models
{
    public class Node
    {
        private LogicGateway gateway;
        public LogicGateway Gateway { get => gateway; set => gateway = value; }

        private Node next;
        public Node Next { get => next; set => next = value; }

        private Node left;
        public Node Left { get => left; set => left = value; }

        private Node right;
        public Node Right { get => right; set => right = value; }

        public Node(LogicGateway gateway)
        {
            Gateway = gateway;
        }
    }
}

