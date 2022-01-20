using System;
using System.Collections.Generic;
using System.Text;
using bramkominatorMobile.Services;
using SQLite;

namespace bramkominatorMobile.Models
{
    public class LogicGateway : CircutElement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private LogicCircut circut;
        public Position Position;

        public string Name { get; set; }
        public GatewayType Type { get; set; }
        public string Image { get; set; }

        public bool InputA { get; set; }
        public bool InputB { get; set; }
        public bool Output { get => GetOutput(); }


        public LogicGateway() { }

        public LogicGateway(GatewayType type, string name="", LogicCircut circut=null, Position position=null)
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

            if (type == GatewayType.Custom && circut != null)
            {
                InputA = circut.InputNode.Gateway.InputA;
                InputB = circut.InputNode.Gateway.InputB;
                this.circut = circut;
            }


            SetImage();

            if (position is null)
                Position = new Position();
            else
                Position = new Position(position.Column, position.Row);
        }

        public LogicGateway(GatewayType type, bool inputA, bool inputB, string name="", Position position=null)
        {
            Type = type;
            InputA = inputA;
            InputB = inputB;

            if (name == "")
            {
                Name = type.ToString();
            }
            else
            {
                Name = name;
            }

            SetImage();

            if (position is null)
                Position = new Position();
            else
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
                    output = circut.Parent.Gateway.Output;
                    break;
            }
            return output;
        }

        public override Position GetPosition()
        {
            return Position;
        }
    }
}
