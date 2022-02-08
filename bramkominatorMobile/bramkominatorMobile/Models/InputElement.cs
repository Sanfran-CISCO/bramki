namespace bramkominatorMobile.Models
{
    public class InputElement : CircutElement
    {
        public override bool Output { get => base.InputA; set => base.InputA = value; }

        public InputElement() : base()
        {
            Output = false;
            //Image = "inputOff.png";
            Image = "nand.png";
            Position = new Position();
        }

        public InputElement(Position position) : base()
        {
            Output = false;
            //Image = "inputOff.png";
            Image = "nand.png";
            Position = new Position(position.Column, position.Row);
        }

        public InputElement(bool input, Position position) : base()
        {
            Output = input;

            if (Output)
                //Image = "inputOn.png";
                Image = "nor.png";
            else
                //Image = "inputOff.png";
                Image = "nand.png";

            Position = new Position(position.Column, position.Row);
        }

        public void Clicked()
        {
            if (Output)
            {
                Output = false;
                //Image = "inputOff.png";
                Image = "nand.png";
            }
            else
            {
                Output = true;
                //Image = "inputOn.png";
                Image = "nor.png";
            }
        }

        public bool Connect(LogicGateway gate, int inputNumber)
        {
            bool isConnected = false;

            if (gate is null)
                isConnected = false;

            switch (inputNumber)
            {
                case 1:
                    gate.InputA = Output;
                    isConnected = true;
                    break;
                case 2:
                    gate.InputB = Output;
                    isConnected = true;
                    break;
                default:
                    isConnected = false;
                    break;
            }

            return isConnected;
        }
    }
}
