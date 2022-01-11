namespace bramkominatorMobile.Models
{
    public class EmptyElement : CircutElement
    {
        public string Name { get; set; }
        public Position Position;
        public bool Output { get; }

        public EmptyElement()
        {
        }

        public EmptyElement(Position position, string name = null)
        {
            if (name != null)
                Name = name;

            if (position != null)
            {
                Position = position;
            }
            else
                Position = new Position();
        }

        public EmptyElement(int row, int column)
        {
            Position = new Position(column, row);
        }

        public override Position GetPosition()
        {
            return Position;
        }
    }
}
