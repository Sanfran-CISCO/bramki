using System;
using NUnit.Framework;
using bramkominatorMobile.Models;
using bramkominatorMobile.Services;

namespace NUnitTests
{
    [TestFixture]
    public class LogicGatewaysTests
    {
        [Test]
        public void NotGatewayTest()
        {
            LogicGateway not = new LogicGateway(GatewayType.Not);
            not.InputA = true;

            Assert.AreEqual(false, not.Output);

            not.InputA = false;

            Assert.AreEqual(true, not.Output);
        }

        [Test]
        public void AndGatewayTest()
        {
            LogicGateway and = new LogicGateway(GatewayType.And, true, true);

            Assert.AreEqual(true, and.Output);

            and.InputA = false;

            Assert.AreEqual(false, and.Output);
        }

        [Test]
        public void OrGatewayTest()
        {
            LogicGateway or = new LogicGateway(GatewayType.Or, true, true);

            Assert.AreEqual(true, or.Output);

            or.InputA = false;

            Assert.AreEqual(true, or.Output);

            or.InputB = false;

            Assert.AreEqual(false, or.Output);
        }

        [Test]
        public void NandGatewayTest()
        {
            LogicGateway nand = new LogicGateway(GatewayType.Nand, false, false);

            Assert.AreEqual(true, nand.Output);

            nand.InputA = true;

            Assert.AreEqual(true, nand.Output);

            nand.InputB = true;

            Assert.AreEqual(false, nand.Output);
        }

        [Test]
        public void NorGatewayTest()
        {
            LogicGateway nor = new LogicGateway(GatewayType.Nor, false, false);

            Assert.AreEqual(true, nor.Output);

            nor.InputA = true;

            Assert.AreEqual(false, nor.Output);

            nor.InputB = true;

            Assert.AreEqual(false, nor.Output);
        }

        [Test]
        public void XorGatewayTest()
        {
            LogicGateway xor = new LogicGateway(GatewayType.Xor, false, false);

            Assert.AreEqual(false, xor.Output);

            xor.InputA = true;

            Assert.AreEqual(true, xor.Output);

            xor.InputB = true;

            Assert.AreEqual(false, xor.Output);
        }

        [Test]
        public void XnorGatewayTest()
        {
            LogicGateway xnor = new LogicGateway(GatewayType.Xnor, false, false);

            Assert.AreEqual(true, xnor.Output);

            xnor.InputA = true;

            Assert.AreEqual(false, xnor.Output);

            xnor.InputB = true;

            Assert.AreEqual(true, xnor.Output);
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

            Assert.AreEqual(true, customGate.Output);
        }
    }
}
