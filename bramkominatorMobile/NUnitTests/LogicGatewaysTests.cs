using NUnit.Framework;
using bramkominatorMobile.Models;
using bramkominatorMobile.Services;

namespace NUnitTests
{
    [TestFixture]
    public class LogicGatewaysTests
    {
        Position pos = new Position(1, 1);

        [Test]
        public void NotGatewayTest()
        {
            var input = new InputElement(true, new Position());

            var not = new LogicGateway(GatewayType.Not, new Position(1,1));

            var circut = new LogicCircut();
            circut.Connect(input, not, 1);

            Assert.AreEqual(false, not.Output);

            input.Clicked();

            Assert.AreEqual(true, not.Output);
;
            Assert.AreEqual(pos, not.Position);
        }

        [Test]
        public void AndGatewayTest()
        {
            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(true, new Position(0, 1));

            LogicGateway and = new LogicGateway(GatewayType.And, new Position(1,1));

            var circut = new LogicCircut();
            circut.Connect(input1, and, 1);
            circut.Connect(input2, and, 2);

            Assert.AreEqual(true, and.Output);

            input1.Clicked();

            Assert.AreEqual(false, and.Output);

            Assert.AreEqual(pos, and.Position);
        }

        [Test]
        public void OrGatewayTest()
        {
            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(true, new Position(0, 1));

            LogicGateway or = new LogicGateway(GatewayType.Or, new Position(1,1));

            var circut = new LogicCircut();
            circut.Connect(input1, or, 1);
            circut.Connect(input2, or, 2);

            Assert.AreEqual(true, or.Output);

            input1.Clicked();

            Assert.AreEqual(true, or.Output);

            input2.Clicked();

            Assert.AreEqual(false, or.Output);

            Assert.AreEqual(pos, or.Position);
        }

        [Test]
        public void NandGatewayTest()
        { 
            var circut = new LogicCircut();

            var input1 = new InputElement(false, new Position());
            var input2 = new InputElement(false, new Position(0, 1));

            LogicGateway nand = new LogicGateway(GatewayType.Nand, new Position(1,1));

            circut.Connect(input1, nand, 1);
            circut.Connect(input2, nand, 2);

            Assert.AreEqual(true, nand.Output);

            input1.Clicked();

            Assert.AreEqual(true, nand.Output);

            input2.Clicked();

            Assert.AreEqual(false, nand.Output);

            Assert.AreEqual(pos, nand.Position);
        }

        [Test]
        public void NorGatewayTest()
        {
            var circut = new LogicCircut();

            var input1 = new InputElement(false, new Position());
            var input2 = new InputElement(false, new Position(0, 1));

            LogicGateway nor = new LogicGateway(GatewayType.Nor, new Position(1,1));

            circut.Connect(input1, nor, 1);
            circut.Connect(input2, nor, 2);

            Assert.AreEqual(true, nor.Output);

            input1.Clicked();

            Assert.AreEqual(false, nor.Output);

            input2.Clicked();

            Assert.AreEqual(false, nor.Output);

            Assert.AreEqual(pos, nor.Position);
        }

        [Test]
        public void XorGatewayTest()
        {
            var circut = new LogicCircut();

            var input1 = new InputElement(false, new Position());
            var input2 = new InputElement(false, new Position(0, 1));

            LogicGateway xor = new LogicGateway(GatewayType.Xor, new Position(1,1));

            circut.Connect(input1, xor, 1);
            circut.Connect(input2, xor, 2);

            Assert.AreEqual(false, xor.Output);

            input1.Clicked();

            Assert.AreEqual(true, xor.Output);

            input2.Clicked();

            Assert.AreEqual(false, xor.Output);

            Assert.AreEqual(pos, xor.Position);
        }

        [Test]
        public void XnorGatewayTest()
        {
            var circut = new LogicCircut();

            var input1 = new InputElement(false, new Position());
            var input2 = new InputElement(false, new Position(0, 1));

            LogicGateway xnor = new LogicGateway(GatewayType.Xnor, new Position(1,1));

            circut.Connect(input1, xnor, 1);
            circut.Connect(input2, xnor, 2);

            Assert.AreEqual(true, xnor.Output);

            input1.Clicked();

            Assert.AreEqual(false, xnor.Output);

            input2.Clicked();

            Assert.AreEqual(true, xnor.Output);

            Assert.AreEqual(pos, xnor.Position);
        }

        [Test]
        public void CustomGatewayTest()
        {
            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(false, new Position(0, 1));
            var input3 = new InputElement(false, new Position(0, 2));
            var input4 = new InputElement(true, new Position(0, 3));
            var input5 = new InputElement(false, new Position(0, 4));

            LogicCircut circut = new LogicCircut();

            LogicGateway and = new LogicGateway(GatewayType.And, new Position(1,1), "MyAnd");
            LogicGateway or = new LogicGateway(GatewayType.Or, new Position(1,2), "MyOr");
            LogicGateway not = new LogicGateway(GatewayType.Not,new Position(1,3), "MyNot");
            LogicGateway xor = new LogicGateway(GatewayType.Xor, new Position(1, 4), "MyXor");
            LogicGateway nand = new LogicGateway(GatewayType.Nand, new Position(1, 5), "MyNand");

            circut.Connect(input1, and, 1);
            circut.Connect(input2, and, 2);

            circut.Connect(input3, nand, 1);
            circut.Connect(input4, nand, 2);

            circut.Connect(input5, not, 1);

            circut.Connect(nand, xor, 1);
            circut.Connect(not, xor, 2);

            circut.Connect(and, or, 1);
            circut.Connect(xor, or, 2);

            Assert.AreEqual(false, circut.Parent.Content.Output);

            LogicGateway customGate = new LogicGateway(GatewayType.Custom, new Position(1, 6), "MyCustomGate", circut);

            Assert.AreEqual(false, customGate.Output);

            Assert.AreEqual(new Position(1, 6), customGate.Position);
        }
    }
}
