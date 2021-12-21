using System;
namespace bramkominatorMobile.Models
{
    public class Node
    {
        private LogicGateway gateway;
        public LogicGateway Gateway { get => gateway; set => gateway = value; }

        private Node next;
        public Node Next { get => next; set => next = value; }

        private Node previous;
        public Node Previous { get => previous; set => previous = value; }

        public Node(LogicGateway gateway)
        {
            Gateway = gateway;
        }
    }
}

