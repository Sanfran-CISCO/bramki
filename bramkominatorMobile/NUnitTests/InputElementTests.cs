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
            InputElement input = new InputElement(false, new Position());

            Assert.AreEqual(false, input.Output);
            Assert.AreEqual("inputOff.png", input.Image);
            Assert.AreEqual(new Position(), input.Position);
        }

        [Test]
        public void CreateInputElementTest()
        {
            var inputState = true;
            var position = new Position(6, 9);

            InputElement input = new InputElement(inputState, position);

            Assert.AreEqual(true, input.Output);
            Assert.AreEqual("inputOn.png", input.Image);
            Assert.AreEqual(position, input.Position);
        }

        [Test]
        public void CreateInputElementOnlyWithName()
        {
            var input = new InputElement(false, new Position());

            Assert.AreEqual(false, input.Output);
            Assert.AreEqual(new Position(), input.Position);
        }
    }
}
