using bramkominatorMobile.Models;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class InputElementTests
    {
        [Test]
        public void CreateInputElementDefaultConstructorTest()
        {
            InputElement input1 = new InputElement();

            Assert.AreEqual("DefaultInput", input1.Name);
            Assert.AreEqual(false, input1.Output);
            Assert.AreEqual("inputOff.png", input1.Image);
            Assert.AreEqual(new Position(), input1.Position);
        }

        [Test]
        public void CreateInputElementTest()
        {
            var name = "customInputNameForTest";
            var inputState = true;
            var position = new Position(6, 9);
            var gate = new LogicGateway(GatewayType.And, true, true);

            InputElement input = new InputElement(name, inputState, gate, 1, position);

            Assert.AreEqual(name, input.Name);
            Assert.AreEqual(true, input.Output);
            Assert.AreEqual("inputOn.png", input.Image);
            Assert.AreEqual(position, input.Position);
        }

        [Test]
        public void CreateInputElementOnlyWithName()
        {
            var input = new InputElement("test");

            Assert.AreEqual("test", input.Name);
            Assert.AreEqual(false, input.Output);
            Assert.AreEqual(new Position(), input.Position);
        }

        [Test]
        public void CreateInputElementWithInvalidGatewayData()
        {
            var name = "customInputNameForTest";
            var gateway = new LogicGateway(GatewayType.And, true, true);

            InputElement input = new InputElement(name, gate: gateway, inputNumber: 12);

            Assert.AreEqual(name, input.Name);
            Assert.AreEqual("inputOff.png", input.Image);
            Assert.AreEqual(new Position(), input.Position);

            //because and gateway have 2 inputs not 12
            Assert.AreEqual(true, gateway.InputA);
            Assert.AreEqual(true, gateway.InputB);
            Assert.AreEqual(true, gateway.Output);
        }

        [Test]
        public void CreateInputElementWithValidGatewayData()
        {
            var name = "customInputNameForTest";
            var gateway = new LogicGateway(GatewayType.And, true, false);

            Assert.AreEqual(false, gateway.Output);

            InputElement input = new InputElement(name, input: true, gate: gateway, inputNumber: 2);

            Assert.AreEqual(name, input.Name);
            Assert.AreEqual("inputOn.png", input.Image);
            Assert.AreEqual(new Position(), input.Position);

            Assert.AreEqual(true, gateway.InputB);
            Assert.AreEqual(true, gateway.Output);
        }

        [Test]
        public void ConnectInputElementTest()
        {
            var not = new LogicGateway(GatewayType.Not);

            var name = "test";
            var position = new Position(4, 20);

            var input = new InputElement(name, position: position);

            var result = input.Connect(not, 1);

            Assert.AreEqual(true, result);
            Assert.AreEqual(name, input.Name);
            Assert.AreEqual(position, input.Position);
            Assert.AreEqual(true, not.Output);
        }
    }
}
