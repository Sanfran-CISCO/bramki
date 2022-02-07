using System;
namespace bramkominatorMobile.Models
{
    public class Node
    {
        private CircutElement content;
        public CircutElement Content { get => content; set => content = value; }

        private Node next;
        public Node Next { get => next; set => next = value; }

        private Node left;
        public Node Left { get => left; set => left = value; }

        private Node right;
        public Node Right { get => right; set => right = value; }

        public Node(CircutElement element)
        {
            Content = element;
        }
    }
}

