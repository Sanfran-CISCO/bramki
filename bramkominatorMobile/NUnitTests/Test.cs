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

        [Test()]
        public void AddParentGatewayTest()
        {
            GatewaysList list = new GatewaysList();
            list.SetParent(and);

            Assert.AreEqual(GatewayType.And, list.Parent.Gateway.Type);
        }

        [Test]
        public void CreateSimpleCircutTest()
        {
            GatewaysList list = new GatewaysList();
            list.SetParent(and);

            not.InputA = false;

            list.Connect(or, and, 1);
            list.Connect(not, and, 2);

            Assert.AreEqual(true, list.Parent.Gateway.Output);
        }

        [Test]
        public void CreateCircutFromNotebookTest()
        {
            GatewaysList list = new GatewaysList();

            LogicGateway xor = new LogicGateway(GatewayType.Xor, "MyXor");
            LogicGateway nand = new LogicGateway(GatewayType.Nand, "MyNand");

            list.SetParent(or);

            //true
            and.InputA = true;
            and.InputB = true;

            //true
            nand.InputA = false;
            nand.InputB = true;

            //true
            not.InputA = false;

            list.Connect(nand, xor, 1);
            list.Connect(not, xor, 2);

            list.Connect(and, or, 1);
            list.Connect(xor, or, 2);

            Assert.AreEqual(true, list.Parent.Gateway.Output);
        }

        [Test]
        public void DisconnectGatesTest()
        {
            GatewaysList list = new GatewaysList();

            LogicGateway and = new LogicGateway(GatewayType.And, true, true, "and2");
            LogicGateway not = new LogicGateway(GatewayType.Not, "not2");

            list.SetParent(not);

            list.Connect(and, not, 1);

            Assert.AreEqual("not2", list.Parent.Gateway.Name);

            Assert.AreEqual(true, list.Disconnect(and, "next"));

            Assert.AreEqual(null, list.Parent.Left);
        }
    }
}
