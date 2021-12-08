using System;
using System.Collections.Generic;
using System.Text;

namespace bramkominatorMobile.Models
{
    public class LogicGateway
    {
        public GatewayType Type { get; }
        public string Image { get; set; }

        public bool InputA { get; set; }
        public bool InputB { get; set; }

        public bool Output { get => GetOutput(); }


        private LogicGateway() { }

        public LogicGateway(GatewayType type, bool inputA, bool inputB)
        {
            Type = type;
            InputA = inputA;
            InputB = inputB;

            SetImage();
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
            }
            return output;
        }
    }
}
