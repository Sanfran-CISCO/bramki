﻿using NUnit.Framework;
using bramkominatorMobile.Models;
using bramkominatorMobile.Services;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTests
{
    [TestFixture]
    public class CableServiceTests
    {
        [Test]
        public void CreateCableServiceInstanceTest()
        {
            var gate = new LogicGateway(GatewayType.And, true, true);

            CircutElement[,] matrix = {
                {new LogicGateway(GatewayType.And), new EmptyElement(1,0), new EmptyElement(2,0) },
                {new EmptyElement(0,1), new InputElement("in1", position:new Position(1,1)), new EmptyElement(2,1) },
                {new EmptyElement(0,2), new EmptyElement(1,2), new InputElement("in2", position:new Position(2,2)) }
            };

            CableService service = new CableService(matrix, 3, 3);

            var actual = matrix[1, 1].GetType();
            var expected = typeof(InputElement);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindPathTest()
        {
            var gate = new LogicGateway(GatewayType.And, true, true);
            var emptyElement = new EmptyElement(1, 0);

            CircutElement[,] matrix = {
                {new LogicGateway(GatewayType.And), new EmptyElement(1,0), new EmptyElement(2,0) },
                {new EmptyElement(0,1), new InputElement("in1", position:new Position(1,1)), new EmptyElement(2,1) },
                {new EmptyElement(0,2), new EmptyElement(1,2), new InputElement("in2", position:new Position(2,2)) }
            };

            CableService service = new CableService(matrix, 3, 3);

            var actual = matrix[1, 1].GetType();
            var expected = typeof(InputElement);

            Assert.AreEqual(expected, actual);

            List<Position> path = service.FindPath(matrix[0,0], matrix[0,1]);

            Assert.AreEqual(typeof(List<Position>), path.GetType());
            Assert.AreEqual(1, path.Count);

            var first = path[0];

            Assert.AreEqual(0, first.Column);
            Assert.AreEqual(1, first.Row);

            path = service.FindPath(matrix[0, 0], matrix[2, 0]);

            Assert.AreEqual(typeof(List<Position>), path.GetType());
            Assert.AreEqual(2, path.Count);
        }

        [Test]
        public void FindPathTest2()
        {
            CircutElement[,] matrix = {
                {new LogicGateway(GatewayType.And), new EmptyElement(1,0), new EmptyElement(2,0) },
                {new EmptyElement(0,1), new InputElement("in1", position:new Position(1,1)), new EmptyElement(2,1) },
                {new EmptyElement(0,2), new EmptyElement(1,2), new InputElement("in2", position:new Position(2,2)) }
            };

            CableService service = new CableService(matrix, 3, 3);

            var path = service.FindPath(matrix[2, 2], matrix[0, 0]);

            var expected = new Position(0, 0);

            var actual = path.ElementAt(path.Count()-1);

            Assert.AreEqual(expected, actual);
        }
    }
}
