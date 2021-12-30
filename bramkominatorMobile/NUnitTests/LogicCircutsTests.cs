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
            LogicGateway not = new LogicGateway(GatewayType.Not);
            LogicGateway and = new LogicGateway(GatewayType.And);
            LogicGateway or = new LogicGateway(GatewayType.Or);

            not.InputA = false;

            or.InputA = true;
            or.InputB = false;

            LogicCircut circut = new LogicCircut();

            circut.Connect(or, and, 1);
            circut.Connect(not, and, 2);

            Assert.AreEqual(true, circut.Parent.Gateway.Output);
        }

        [Test]
        public void CreateCircutFromNotebookTest()
        {
            var and = new LogicGateway(GatewayType.And, true, true);
            var not = new LogicGateway(GatewayType.Not);
            var or = new LogicGateway(GatewayType.Or);
            var nand = new LogicGateway(GatewayType.Nand, false, true);
            var xor = new LogicGateway(GatewayType.Xor);

            LogicCircut circut = new LogicCircut();

            //true
            not.InputA = false;

            circut.Connect(nand, xor, 1);
            circut.Connect(not, xor, 2);

            circut.Connect(and, or, 1);
            circut.Connect(xor, or, 2);

            Assert.AreEqual(true, circut.Parent.Gateway.Output);
        }

        [Test]
        public void DisconnectGatesTest()
        {
            var and = new LogicGateway(GatewayType.And, true, true);
            var not = new LogicGateway(GatewayType.Not);

            LogicCircut circut = new LogicCircut();

            circut.Connect(and, not, 1);

            Assert.AreEqual("Not", circut.Parent.Gateway.Name);

            Assert.AreEqual(true, circut.Disconnect(and, "next"));

            Assert.AreEqual(null, circut.Parent.Left);

            Assert.AreEqual(GatewayType.And, circut.InputNode.Gateway.Type);
        }

        [Test]
        public void RemoveGatewayTest()
        {
            LogicCircut circut = new LogicCircut();

            var and = new LogicGateway(GatewayType.And, true, true);
            var or = new LogicGateway(GatewayType.Or);

            or.InputB = false;

            circut.Connect(and, or, 1);

            Assert.AreEqual(true, circut.Parent.Gateway.Output);
            Assert.AreEqual(GatewayType.Or, circut.Parent.Gateway.Type);

            Assert.AreEqual(true, circut.Remove(and));

            Assert.AreEqual(GatewayType.Or, circut.InputNode.Gateway.Type);
        }
    }
}
