namespace bramkominatorMobile.Models
{
    public class InputElement : CircutElement
    {
        public override bool Output { get => base.InputA; set => base.InputA = value; }

        public InputElement() : base()
        {
            Name = "Input";
            Output = false;
            Image = "inputOff.png";
            Position = new Position();
        }

        public InputElement(Position position, string name=null) : base()
        {
            if (name is null)
                Name = "Input";
            else
                Name = name;

            Output = false;
            Image = "inputOff.png";
            Position = new Position(position.Column, position.Row);
        }

        public InputElement(bool input, Position position, string name=null) : base()
        {
            if (name is null)
                Name = "Input";
            else
                Name = name;

            Output = input;

            if (Output)
                Image = "inputOn.png";
            else
                Image = "inputOff.png";

            Position = new Position(position.Column, position.Row);
        }

        public void Clicked()
        {
            if (Output)
            {
                Output = false;
                Image = "inputOff.png";
            }
            else
            {
                Output = true;
                Image = "inputOn.png";
            }
        }
    }
}
