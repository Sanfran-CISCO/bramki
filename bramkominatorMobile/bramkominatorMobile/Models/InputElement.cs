namespace bramkominatorMobile.Models
{
    public class InputElement : CircutElement
    {
        public bool Output { get; private set; }

        public string Name { get; set; }
        public string Image { get; private set; }
        public Position Position;

        public InputElement()
        {
            Name = "DefaultInput";
            Output = false;
            Image = "inputOff.png";
            Position = new Position();
        }

        public InputElement(string name, bool input = false, LogicGateway gate=null, int inputNumber=0, Position position=null)
        {
            if (name != null)
                Name = name;
            else
                Name = "DefaultInput";

            Output = input;

            if (gate != null && (inputNumber == 1 || inputNumber == 2))
                _ = Connect(gate, inputNumber);

            if (Output)
                Image = "inputOn.png";
            else
                Image = "inputOff.png";

            if (position is null)
                Position = new Position();
            else
                Position = position;
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

        public override Position GetPosition()
        {
            return Position;
        }
    }
}
