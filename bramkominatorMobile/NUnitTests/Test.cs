using bramkominatorMobile.Services;
using bramkominatorMobile.Models;
using NUnit.Framework;
using System;

namespace NUnitTests
{
    [TestFixture()]
    public class Test
    {
        private LogicGateway and = new LogicGateway(GatewayType.And, "MyAnd");

        //true
        private LogicGateway or = new LogicGateway(GatewayType.Or, true, false, "MyOr");

        //true
        private LogicGateway not = new LogicGateway(GatewayType.Not, "MyNot");

        [Test]
        public void CreateSimpleCircutTest()
        {
            LogicCircut circut = new LogicCircut();

            not.InputA = false;

            circut.Connect(or, and, 1);
            circut.Connect(not, and, 2);

            Assert.AreEqual(true, circut.Parent.Gateway.Output);
        }

        [Test]
        public void CreateCircutFromNotebookTest()
        {
            LogicCircut circut = new LogicCircut();

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
        }

        [Test]
        public void DisconnectGatesTest()
        {
            LogicCircut circut = new LogicCircut();

            LogicGateway and = new LogicGateway(GatewayType.And, true, true, "and2");
            LogicGateway not = new LogicGateway(GatewayType.Not, "not2");

            circut.Connect(and, not, 1);

            Assert.AreEqual("not2", circut.Parent.Gateway.Name);

            Assert.AreEqual(true, circut.Disconnect(and, "next"));

            Assert.AreEqual(null, circut.Parent.Left);
        }

        [Test]
        public void RemoveGatewayTest()
        {
            LogicCircut circut = new LogicCircut();

            LogicGateway and = new LogicGateway(GatewayType.And, true, true);
            LogicGateway or = new LogicGateway(GatewayType.Or);
            or.InputB = false;

            circut.Connect(and, or, 1);

            Assert.AreEqual(true, circut.Parent.Gateway.Output);
            Assert.AreEqual(GatewayType.Or, circut.Parent.Gateway.Type);

            Assert.AreEqual(true, circut.Remove(and));
        }
    }
}
