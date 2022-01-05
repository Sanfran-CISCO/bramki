namespace bramkominatorMobile.Models
{
    public class OutputElement
    {
        public LogicGateway ConnectedGate { get; private set; }
        public bool Input { get => ConnectedGate.Output; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public string Image { get; private set; }

        public OutputElement()
        {
            Name = "DefaultOutput";
            Position = new Position();
            Image = "outputOff.png";
            ConnectedGate = null;
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
                ConnectedGate = gate;

                if (Input)
                    Image = "outputOn.png";
            }
            else
                ConnectedGate = null;
            

            if (position is null)
                Position = new Position();
            else
                Position = position;
        }

        public bool ConnectGate(LogicGateway gate)
        {
            if (gate is null)
                return false;
            else
            {
                ConnectedGate = gate;
                return true;
            }
        }

        public void Disconnect()
        {
            ConnectedGate = null;
        }

        public bool IsConnected()
        {
            return ConnectedGate != null;
        }
    }
}
