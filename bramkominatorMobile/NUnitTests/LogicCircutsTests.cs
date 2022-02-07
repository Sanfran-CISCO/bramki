using bramkominatorMobile.Services;
using bramkominatorMobile.Models;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture()]
    public class LogicCircutsTests
    {

        [Test]
        public void CreateSimpleCircutTest()
        {
            LogicCircut circut = new LogicCircut();

            var input1 = new InputElement(false, new Position());
            var input2 = new InputElement(true, new Position(0, 1));
            var input3 = new InputElement(false, new Position(0, 2));

            var not = new LogicGateway(GatewayType.Not, new Position(1, 0));
            var and = new LogicGateway(GatewayType.And, new Position(1, 1));
            var or = new LogicGateway(GatewayType.Or, new Position(1, 2));

            circut.Connect(input1, not, 1);

            circut.Connect(input2, or, 1);
            circut.Connect(input3, or, 2);

            circut.Connect(or, and, 1);
            circut.Connect(not, and, 2);

            Assert.AreEqual(true, circut.Parent.Content.Output);

            input2.Clicked();
            //Assert.AreEqual(false, circut.Parent.Content.Output);

            Assert.AreEqual(GatewayType.And, (circut.Parent.Content as LogicGateway).Type);
        }

        [Test]
        public void CreateCircutFromNotebookTest()
        {
            LogicCircut circut = new LogicCircut();

            var and = new LogicGateway(GatewayType.And, new Position(1,0));
            var not = new LogicGateway(GatewayType.Not, new Position(1,1));
            var or = new LogicGateway(GatewayType.Or,  new Position(1,2));
            var nand = new LogicGateway(GatewayType.Nand, new Position(1,3));
            var xor = new LogicGateway(GatewayType.Xor, new Position(2,2));

            var input1 = new InputElement(true, new Position(0, 0));
            var input2 = new InputElement(true, new Position(0, 1));
            var input3 = new InputElement(false, new Position(0, 2));
            var input4 = new InputElement(true, new Position(0, 3));
            var input5 = new InputElement(false, new Position(0, 4));

            circut.Connect(input1, and, 1);
            circut.Connect(input2, and, 2);
            circut.Connect(input3, nand, 1);
            circut.Connect(input4, nand, 2);
            circut.Connect(input5, not, 1);

            circut.Connect(nand, xor, 1);
            circut.Connect(not, xor, 2);

            circut.Connect(and, or, 1);
            circut.Connect(xor, or, 2);

            Assert.AreEqual(true, circut.Parent.Content.Output);
        }

        [Test]
        public void AddInputElementToCircutTest()
        {
            var circut = new LogicCircut();

            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(true, new Position(0, 1));

            var and = new LogicGateway(GatewayType.And, new Position(1, 1));

            circut.Connect(input1, and, 1);
            circut.Connect(input2, and, 2);

            var expected = true;
            var actual = circut.IsConnected(input1);

            Assert.AreEqual(expected, actual);

            actual = circut.IsConnected(input2);

            Assert.AreEqual(expected, actual);

            actual = circut.IsConnected(and);
            Assert.AreEqual(expected, actual);

            actual = and.Output;
            Assert.AreEqual(expected, actual);

            input1.Clicked();

            Assert.AreEqual(false, circut.Parent.Content.Output);
            Assert.AreEqual(false, and.Output);
        }

        [Test]
        public void DisconnectGatesTest()
        {
            LogicCircut circut = new LogicCircut();

            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(true, new Position(0, 1));

            var and = new LogicGateway(GatewayType.And, new Position(1,1));
            var not = new LogicGateway(GatewayType.Not, new Position(1,3));

            circut.Connect(input1, and, 1);
            circut.Connect(input2, and, 2);

            circut.Connect(and, not, 1);

            Assert.AreEqual("Not", circut.Parent.Content.Name);

            var isDisconnected = circut.Disconnect(and, "next");

            Assert.AreEqual(true, isDisconnected);

            Assert.AreEqual(null, circut.Parent.Left);
        }

        [Test]
        public void RemoveGatewayTest()
        {
            LogicCircut circut = new LogicCircut();

            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(true, new Position(0, 1));
            var input3 = new InputElement(true, new Position(0, 2));

            var and = new LogicGateway(GatewayType.And, new Position(1,1));
            var or = new LogicGateway(GatewayType.Or, new Position(1,3));

            circut.Connect(input1, and, 1);
            circut.Connect(input2, and, 2);

            circut.Connect(input3, or, 1);

            circut.Connect(and, or, 1);

            Assert.AreEqual(true, or.Output);
            Assert.AreEqual(GatewayType.Or, (circut.Parent.Content as LogicGateway).Type);

            var isRemoved = circut.Remove(and);

            Assert.AreEqual(true, isRemoved);
            Assert.AreEqual(null, circut.Parent.Left);
        }

        [Test]
        public void IsConnectedTest()
        {
            LogicCircut circut = new LogicCircut();

            var input = new InputElement(false, new Position());

            var and = new LogicGateway(GatewayType.And, new Position(1,1));
            var or = new LogicGateway(GatewayType.Or, new Position(1,1));

            circut.Connect(input, or, 2);

            circut.Connect(and, or, 1);

            var isConnected = circut.IsConnected(and);

            Assert.AreEqual(true, isConnected);

            var isDisconnected = circut.Disconnect(and, "next");

            isConnected = circut.IsConnected(and);

            Assert.AreEqual(true, isDisconnected);
            Assert.AreEqual(false, isConnected);
        }
    }
}
