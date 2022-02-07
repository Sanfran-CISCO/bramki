using Xamarin.Forms;
using bramkominatorMobile.Services;
using SQLite;

namespace bramkominatorMobile.Models
{
    public class LogicGateway : CircutElement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private LogicCircut circut;

        public GatewayType Type { get; set; }
        public string Image { get; set; }
        public Color Color { get; set; }

        public override bool InputA {
            get {
                if (Type == GatewayType.Custom)
                {
                    if (circut.InputNode.Content is null)
                        return false;
                    else
                        return circut.InputNode.Content.InputA;
                }
                else
                {
                    if (Node.Left.Content is null)
                        return false;
                    else
                        return Node.Left.Content.Output;
                }
            }
        }

        public override bool InputB {
            get {
                if (Type == GatewayType.Custom)
                {
                    if (circut.InputNode.Content is null)
                        return false;
                    else
                        return circut.InputNode.Content.InputB;
                }
                else
                {
                    if (Node.Right.Content is null)
                        return false;
                    else
                        return Node.Right.Content.Output;
                }
            }
        }

        public override bool Output { get => GetOutput(); }


        public LogicGateway() : base() { }

        public LogicGateway(GatewayType type, Position position, string name="", LogicCircut circut=null) : base()
        {
            Type = type;

            if (name == "")
            {
                Name = type.ToString();
            }
            else
            {
                Name = name;
            }

            if (circut != null)
                this.circut = circut;


            SetImage();

            Position = new Position(position.Column, position.Row);
        }

        public LogicGateway(GatewayType type, Position position) : base()
        {
            Type = type;

            Name = type.ToString();

            SetImage();

            Position = new Position(position.Column, position.Row);
        }

        private void SetImage()
        {
            switch (Type)
            {
                case GatewayType.Not:
                    Image = "not.png";
                    break;
                case GatewayType.And:
                    Image = "and.png";
                    break;
                case GatewayType.Or:
                    Image = "or.png";
                    break;
                case GatewayType.Nand:
                    Image = "nand.png";
                    break;
                case GatewayType.Nor:
                    Image = "nor.png";
                    break;
                case GatewayType.Xnor:
                    Image = "xnor.png";
                    break;
                case GatewayType.Xor:
                    Image = "xor.png";
                    break;
                case GatewayType.Custom:
                    Image = "custom.png";
                    break;
            }
        }

        private bool GetOutput()
        {
            bool output = false;

            switch (Type)
            {
                case GatewayType.Not:
                    output = !InputA;
                    break;
                case GatewayType.And:
                    output = InputA && InputB;
                    break;
                case GatewayType.Or:
                    output = InputA || InputB;
                    break;
                case GatewayType.Nand:
                    output = !InputA || !InputB;
                    break;
                case GatewayType.Nor:
                    output = !InputA && !InputB;
                    break;
                case GatewayType.Xnor:
                    output = InputA == InputB;
                    break;
                case GatewayType.Xor:
                    output = InputA != InputB;
                    break;
                case GatewayType.Custom:
                    output = circut.Parent.Content.Output;
                    break;
            }
            return output;
        }
    }
}
