using System;
using SQLite;

namespace bramkominatorMobile.Models
{
    public class Node
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ElementId { get; set; }
        public int CircutId { get; set; }

        [Ignore]
        public CircutElement Content { get; set; }

        public int ContentId { get; set; }

        [Ignore]
        public Node Next { get; set; }
        public int NextId { get; set; }

        [Ignore]
        public Node Left { get; set; }
        public int LeftId { get; set; }

        [Ignore]
        public Node Right { get; set; }
        public int RightId { get; set; }

        public Node() { }

        public Node(CircutElement element)
        {
            Content = element;
        }
    }
}

