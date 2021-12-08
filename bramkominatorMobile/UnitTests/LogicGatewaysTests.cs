using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using bramkominatorMobile.Models;

namespace UnitTests
{
    [TestClass]
    public class LogicGatewaysTests
    {
        [TestMethod]
        public void NotGatewayTest()
        {
            NotGateway not = new NotGateway(true);

            Assert.AreEqual(false, not.Output);

            not.InputA = false;

            Assert.AreEqual(true, not.Output);
        }

        [TestMethod]
        public void AndGatewayTest()
        {
            AndGateway and = new AndGateway(true, true);

            Assert.AreEqual(true, and.Output);

            and.InputA = false;

            Assert.AreEqual(false, and.Output);
        }

        [TestMethod]
        public void OrGatewayTest()
        {
            OrGateway or = new OrGateway(true, true);

            Assert.AreEqual(true, or.Output);

            or.InputA = false;

            Assert.AreEqual(true, or.Output);

            or.InputB = false;

            Assert.AreEqual(false, or.Output);
        }

        [TestMethod]
        public void NandGatewayTest()
        {
            NandGateway nand = new NandGateway(false, false);

            Assert.AreEqual(true, nand.Output);

            nand.InputA = true;

            Assert.AreEqual(true, nand.Output);

            nand.InputB = true;

            Assert.AreEqual(false, nand.Output);
        }

        [TestMethod]
        public void NorGatewayTest()
        {
            NorGateway nor = new NorGateway(false, false);

            Assert.AreEqual(true, nor.Output);

            nor.InputA = true;

            Assert.AreEqual(false, nor.Output);

            nor.InputB = true;

            Assert.AreEqual(false, nor.Output);
        }

        [TestMethod]
        public void XorGatewayTest()
        {
            XorGateway xor = new XorGateway(false, false);

            Assert.AreEqual(false, xor.Output);

            xor.InputA = true;

            Assert.AreEqual(true, xor.Output);

            xor.InputB = true;

            Assert.AreEqual(false, xor.Output);
        }

        [TestMethod]
        public void XnorGatewayTest()
        {
            XnorGateway xnor = new XnorGateway(false, false);

            Assert.AreEqual(true, xnor.Output);

            xnor.InputA = true;

            Assert.AreEqual(false, xnor.Output);

            xnor.InputB = true;

            Assert.AreEqual(true, xnor.Output);
        }
    }
}
