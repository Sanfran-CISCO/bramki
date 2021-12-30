using NUnit.Framework;
using bramkominatorMobile.Models;
using bramkominatorMobile.Services;

namespace NUnitTests
{
    [TestFixture]
    public class LogicGatewaysTests
    {
        Position pos = new Position(1, 0);

        [Test]
        public void NotGatewayTest()
        {
            LogicGateway not = new LogicGateway(GatewayType.Not);
            not.InputA = true;
            not.Position.Set(1, 0);

            Assert.AreEqual(false, not.Output);

            not.InputA = false;

            Assert.AreEqual(true, not.Output);
;
            Assert.AreEqual(pos, not.Position);
        }

        [Test]
        public void AndGatewayTest()
        {
            LogicGateway and = new LogicGateway(GatewayType.And, true, true);
            and.Position.Set(1, 0);

            Assert.AreEqual(true, and.Output);

            and.InputA = false;

            Assert.AreEqual(false, and.Output);

            Assert.AreEqual(pos, and.Position);
        }

        [Test]
        public void OrGatewayTest()
        {
            LogicGateway or = new LogicGateway(GatewayType.Or, true, true);
            or.Position.Set(1, 0);

            Assert.AreEqual(true, or.Output);

            or.InputA = false;

            Assert.AreEqual(true, or.Output);

            or.InputB = false;

            Assert.AreEqual(false, or.Output);

            Assert.AreEqual(pos, or.Position);
        }

        [Test]
        public void NandGatewayTest()
        {
            LogicGateway nand = new LogicGateway(GatewayType.Nand, false, false);
            nand.Position.Set(1, 0);

            Assert.AreEqual(true, nand.Output);

            nand.InputA = true;

            Assert.AreEqual(true, nand.Output);

            nand.InputB = true;

            Assert.AreEqual(false, nand.Output);

            Assert.AreEqual(pos, nand.Position);
        }

        [Test]
        public void NorGatewayTest()
        {
            LogicGateway nor = new LogicGateway(GatewayType.Nor, false, false);
            nor.Position.Set(1, 0);

            Assert.AreEqual(true, nor.Output);

            nor.InputA = true;

            Assert.AreEqual(false, nor.Output);

            nor.InputB = true;

            Assert.AreEqual(false, nor.Output);

            Assert.AreEqual(pos, nor.Position);
        }

        [Test]
        public void XorGatewayTest()
        {
            LogicGateway xor = new LogicGateway(GatewayType.Xor, false, false);
            xor.Position.Set(1, 0);

            Assert.AreEqual(false, xor.Output);

            xor.InputA = true;

            Assert.AreEqual(true, xor.Output);

            xor.InputB = true;

            Assert.AreEqual(false, xor.Output);

            Assert.AreEqual(pos, xor.Position);
        }

        [Test]
        public void XnorGatewayTest()
        {
            LogicGateway xnor = new LogicGateway(GatewayType.Xnor, false, false);
            xnor.Position.Set(1, 0);

            Assert.AreEqual(true, xnor.Output);

            xnor.InputA = true;

            Assert.AreEqual(false, xnor.Output);

            xnor.InputB = true;

            Assert.AreEqual(true, xnor.Output);

            Assert.AreEqual(pos, xnor.Position);
        }

        [Test]
        public void CustomGatewayTest()
        {
            LogicCircut circut = new LogicCircut();

            LogicGateway and = new LogicGateway(GatewayType.And, "MyAnd");
            LogicGateway or = new LogicGateway(GatewayType.Or, true, false, "MyOr");
            LogicGateway not = new LogicGateway(GatewayType.Not, "MyNot");
            LogicGateway xor = new LogicGateway(GatewayType.Xor, "MyXor");
            LogicGateway nand = new LogicGateway(GatewayType.Nand, "MyNand");

            //true
            and.InputA = true;
            and.InputB = true;

            //true
            nand.InputA = false;
            nand.InputB = true;

            //true
            not.InputA = false;

            circut.Connect(nand, xor, 1);
            circut.Connect(not, xor, 2);

            circut.Connect(and, or, 1);
            circut.Connect(xor, or, 2);

            Assert.AreEqual(true, circut.Parent.Gateway.Output);

            LogicGateway customGate = new LogicGateway(GatewayType.Custom, "MyCustomGate", circut);
            customGate.Position.Set(1, 0);

            Assert.AreEqual(true, customGate.Output);

            Assert.AreEqual(pos, customGate.Position);
        }
    }
}
