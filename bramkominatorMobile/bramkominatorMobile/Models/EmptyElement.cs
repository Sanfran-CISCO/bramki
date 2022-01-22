namespace bramkominatorMobile.Models
{
    public class EmptyElement : CircutElement
    {
        public string Name { get; set; }
        public Position Position;
        public bool Output { get; }
        public string Image { get; private set; }

        public EmptyElement()
        {
            Image = "emptyElement.png";
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

            Image = "emptyElement.png";
        }

        public EmptyElement(int column, int row)
        {
            Position = new Position(column, row);
            Image = "emptyElement.png";
        }

        public override Position GetPosition()
        {
            return Position;
        }
    }
}
