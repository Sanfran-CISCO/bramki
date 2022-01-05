using NUnit.Framework;
using bramkominatorMobile.Models;

namespace NUnitTests
{
    [TestFixture]
    public class OutputElementTests
    {
        [Test]
        public void CreateOutputElementDefaultConstructorTest()
        {
            var output = new OutputElement();

            Assert.AreEqual("DefaultOutput", output.Name);
            Assert.AreEqual(new Position(), output.Position);
            Assert.AreEqual("outputOff.png", output.Image);
        }

        [Test]
        public void CreateOutputElementTest()
        {
            var name = "MyCustomOutput";
            var gate = new LogicGateway(GatewayType.And, true, true);
            var position = new Position(4, 20);

            var output = new OutputElement(name, gate: gate, position: position);

            Assert.AreEqual(name, output.Name);
            Assert.AreEqual(gate.Type, output.ConnectedGate.Type);
            Assert.AreEqual(gate.Output, output.ConnectedGate.Output);
            Assert.AreEqual(gate.Output, output.Input);
            Assert.AreEqual(position, output.Position);
        }

        [Test]
        public void CreateOutputElementWithInvalidGatewayTest()
        {
            var name = "test";

            var output = new OutputElement(name, gate: null);

            Assert.AreEqual(name, output.Name);
            Assert.AreEqual(null, output.ConnectedGate);
        }

        [Test]
        public void CreateOutputElementWithInvalidPositionTest()
        {
            var name = "test";

            var output = new OutputElement(name, position: null);

            Assert.AreEqual(name, output.Name);
            Assert.AreEqual(new Position(), output.Position);
        }

        [Test]
        public void ConnectGateToOutputTest()
        {
            var gate = new LogicGateway(GatewayType.And, true, false);

            var output = new OutputElement("test");

            var result = output.ConnectGate(gate);

            Assert.AreEqual(true, result);
            Assert.AreEqual(gate.Type, output.ConnectedGate.Type);
            Assert.AreEqual(gate.Output, output.ConnectedGate.Output);
            Assert.AreEqual(gate.Output, output.Input);
        }

        [Test]
        public void IsConnectedTest()
        {
            var gate = new LogicGateway(GatewayType.And, true, false);

            var output = new OutputElement("test");

            var result = output.ConnectGate(gate);

            Assert.AreEqual(true, result);

            var isConnected = output.IsConnected();

            Assert.AreEqual(true, isConnected);
            Assert.AreEqual(gate, output.ConnectedGate);
        }

        [Test]
        public void DisconnectOutputTest()
        {
            var gate = new LogicGateway(GatewayType.And, true, false);

            var output = new OutputElement("test");

            var result = output.ConnectGate(gate);

            Assert.AreEqual(true, result);

            var isConnected = output.IsConnected();

            Assert.AreEqual(true, isConnected);

            output.Disconnect();

            isConnected = output.IsConnected();

            Assert.AreEqual(false, isConnected);
            Assert.AreEqual(null, output.ConnectedGate);
        }
    }
}
