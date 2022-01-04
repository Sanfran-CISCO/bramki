namespace bramkominatorMobile.Models
{
    public class OutputElement
    {
        private LogicGateway connectedGate;

        public bool Input { get => connectedGate.Output; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public string Image { get; private set; }

        public OutputElement()
        {
            Name = "DefaultOutput";
            Position.Set(0, 0);
            Image = "outputOff.png";
            connectedGate = null;
        }

        public OutputElement(string name, LogicGateway gate=null, Position position=null)
        {
            if (name != null)
                Name = name;
            else
                Name = "DefaultOutput";

            Image = "outputOff.png";

            if (gate != null)
            {
                connectedGate = gate;

                if (Input)
                    Image = "outputOn.png";
            }
            else
                connectedGate = null;
            

            if (position != null)
                Position = position;
            else
                Position.Set(0, 0);
        }

        public bool ConnectGate(LogicGateway gate)
        {
            if (gate is null)
                return false;
            else
            {
                connectedGate = gate;
                return true;
            }
        }

        public void Disconnect()
        {
            connectedGate = null;
        }

        public bool IsConnected()
        {
            return connectedGate != null;
        }
    }
}
