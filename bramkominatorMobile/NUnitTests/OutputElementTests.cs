using NUnit.Framework;
using bramkominatorMobile.Models;
using bramkominatorMobile.Services;
using bramkominatorMobile.Exceptions;

namespace NUnitTests
{
    [TestFixture]
    public class OutputElementTests
    {
        [Test]
        public void CreateOutputElementDefaultConstructorTest()
        {
            var output = new OutputElement(new Position());

            Assert.AreEqual(new Position(), output.Position);
            Assert.AreEqual("outputOff.png", output.Image);
        }

        [Test]
        public void CreateOutputElementTest()
        {
            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(true, new Position(0, 1));

            var gate = new LogicGateway(GatewayType.And, new Position(1,1));

            var circut = new LogicCircut();
            circut.Connect(input1, gate, 1);
            circut.Connect(input2, gate, 2);

            var position = new Position(4, 20);

            var output = new OutputElement(position);
            circut.Connect(gate, output, 1);

            Assert.AreEqual(gate.Output, output.Output);
            Assert.AreEqual(position, output.Position);
        }

        [Test]
        public void CreateOutputElementWithInvalidPositionTest()
        {
            try
            {
                var output = new OutputElement(null);
            }
            catch (BadElementInputException e)
            {
                string msg = "Position cannot be null!";
                Assert.AreEqual(msg, e.Message);
            }
        }

        [Test]
        public void ConnectGateToOutputTest()
        {
            var circut = new LogicCircut();

            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(true, new Position(0, 1));

            var gate = new LogicGateway(GatewayType.And, new Position(1, 1));
            var output = new OutputElement(new Position(2, 1));

            circut.Connect(input1, gate, 1);
            circut.Connect(input2, gate, 2);
            circut.Connect(gate, output, 1);

            Assert.AreEqual(gate.Type, (output.Node.Left.Content as LogicGateway).Type);
            Assert.AreEqual(gate.Output, output.Output);
        }

        [Test]
        public void IsConnectedTest()
        {

            var circut = new LogicCircut();

            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(true, new Position(0, 1));

            var gate = new LogicGateway(GatewayType.And, new Position(1, 1));
            var output = new OutputElement(new Position(2, 1));

            circut.Connect(input1, gate, 1);
            circut.Connect(input2, gate, 2);
            circut.Connect(gate, output, 1);

            var actual = circut.IsConnected(output);

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void DisconnectOutputTest()
        {
            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(true, new Position(0,1));

            var gate = new LogicGateway(GatewayType.And, new Position(1,1));

            var output = new OutputElement(new Position(3, 3));

            var circut = new LogicCircut();

            circut.Connect(input1, gate, 1);
            circut.Connect(input2, gate, 2);
            circut.Connect(gate, output, 1);

            var result = output.Output;

            Assert.AreEqual(true, result);

            var isConnected = circut.IsConnected(output);

            Assert.AreEqual(true, isConnected);

            var isDisconnected = circut.Disconnect(output, "left");
            isConnected = circut.IsConnected(output);

            Assert.AreEqual(false, isConnected);
            Assert.AreEqual(true, isDisconnected);
        }
    }
}
