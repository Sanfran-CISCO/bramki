using NUnit.Framework;
using bramkominatorMobile.Models;
using bramkominatorMobile.Services;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using FakeItEasy;

namespace NUnitTests
{
    [TestFixture]
    public class CableServiceTests
    {

        public CableServiceTests()
        {
            var platformServicesFake = A.Fake<IPlatformServices>();
            Device.PlatformServices = platformServicesFake;
        }

        [Test]
        public void CreateCableServiceInstanceTest()
        {
            CircutElement[,] matrix = {
                {new LogicGateway(GatewayType.And, new Position()), new EmptyElement(1,0), new EmptyElement(2,0) },
                {new EmptyElement(0,1), new InputElement(true, new Position(1,1)), new EmptyElement(2,1) },
                {new EmptyElement(0,2), new EmptyElement(1,2), new InputElement(true, new Position(2,2)) }
            };

            CableService service = new CableService(ref matrix, 3, 3);

            Assert.AreEqual(typeof(CableService), service.GetType());

            var actual = matrix[1, 1].GetType();
            var expected = typeof(InputElement);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindPathTest()
        {
            CircutElement[,] matrix = {
                {new LogicGateway(GatewayType.And, new Position()), new EmptyElement(1,0), new EmptyElement(2,0) },
                {new EmptyElement(0,1), new InputElement(true, new Position(1,1)), new EmptyElement(2,1) },
                {new EmptyElement(0,2), new EmptyElement(1,2), new InputElement(true, new Position(2,2)) }
            };

            CableService service = new CableService(ref matrix, 3, 3);

            var path = service.FindPath(matrix[0,0], matrix[0,1]);

            Assert.AreEqual(typeof(List<Position>), path.GetType());
            Assert.AreEqual(1, path.Count);

            var first = path[0];

            Assert.AreEqual(1, first.Column);
            Assert.AreEqual(0, first.Row);

            path = service.FindPath(matrix[0, 0], matrix[2, 0]);

            Assert.AreEqual(typeof(List<Position>), path.GetType());
            Assert.AreEqual(2, path.Count);
        }

        [Test]
        public void FindPathTest2()
        {
            CircutElement[,] matrix = {
                {new LogicGateway(GatewayType.And, new Position()), new EmptyElement(1,0), new EmptyElement(2,0) },
                {new EmptyElement(0,1), new InputElement(true, new Position(1,1)), new EmptyElement(2,1) },
                {new EmptyElement(0,2), new EmptyElement(1,2), new InputElement(true, new Position(2,2)) }
            };

            CableService service = new CableService(ref matrix, 3, 3);

            var path = service.FindPath(matrix[2, 2], matrix[0, 0]);

            var expected = new Position(0, 0);
            var actual = path.ElementAt(path.Count()-1);

            Assert.AreEqual(expected, actual);
        }
    }
}
