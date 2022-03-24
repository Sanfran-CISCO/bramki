//using bramkominatorMobile.Services;
//using bramkominatorMobile.Models;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Autofac.Extras.Moq;
//using System.Linq;
//using Moq;

//namespace NUnitTests
//{
//    [TestFixture]
//    public class GatewaysDbServiceTests
//    {
//        [Test]
//        public void AddGatewayTest()
//        {
//            using (var mock = AutoMock.GetLoose())
//            {
//                var circut = new LogicCircut();

//                var input1 = new InputElement(true, new Position());
//                var input2 = new InputElement(false, new Position(0, 1));

//                var gate = new LogicGateway(GatewayType.And, new Position(1,1), "MyTestAnd");

//                circut.Connect(input1, gate, 1);
//                circut.Connect(input2, gate, 2);

//                mock.Mock<IGatewaysDbService>()
//                    .Setup(x => x.AddGateway(gate))
//                    .Returns(GetBasicGatesSample);

//                var cls = mock.Create<IGatewaysDbService>();

//                cls.AddGateway(gate);

//                mock.Mock<IGatewaysDbService>()
//                    .Verify(x => x.AddGateway(gate), Times.Once());
//            }
//        }

//        [Test]
//        public void RemoveGatewayTest()
//        {
//            using (var mock = AutoMock.GetLoose())
//            {
//                var circut = new LogicCircut();

//                var input1 = new InputElement(true, new Position());
//                var input2 = new InputElement(false, new Position(0, 1));

//                var gate = new LogicGateway(GatewayType.And, new Position(1, 1), "MyTestAnd");

//                circut.Connect(input1, gate, 1);
//                circut.Connect(input2, gate, 2);

//                mock.Mock<IGatewaysDbService>()
//                    .Setup(x => x.RemoveGateway(gate))
//                    .Returns(GetBasicGatesSample);

//                var cls = mock.Create<IGatewaysDbService>();

//                cls.RemoveGateway(gate);

//                mock.Mock<IGatewaysDbService>()
//                    .Verify(x => x.RemoveGateway(gate), Times.Once());
//            }
//        }

//        [Test]
//        public void GetAllGatesTest()
//        {
//            using (var mock = AutoMock.GetLoose())
//            {
//                mock.Mock<IGatewaysDbService>()
//                    .Setup(x => x.GetAllGates())
//                    .Returns(GetAllGatesSample());

//                var cls = mock.Create<IGatewaysDbService>();

//                var expeted = GetAllGatesSample().Result;
//                var actual = cls.GetAllGates().Result;

//                for (int i = 0; i < expeted.LongCount(); i++)
//                {
//                    Assert.AreEqual(expeted.ElementAt(i).Id, actual.ElementAt(i).Id);
//                    Assert.AreEqual(expeted.ElementAt(i).Type, actual.ElementAt(i).Type);
//                }
//            }
//        }

//        [Test]
//        public void GetBasicGatesTest()
//        {
//            using (var mock = AutoMock.GetLoose())
//            {
//                mock.Mock<IGatewaysDbService>()
//                    .Setup(x => x.GetBasicGates())
//                    .Returns(GetBasicGatesSample);

//                var cls = mock.Create<IGatewaysDbService>();

//                var expeted = GetBasicGatesSample().Result;
//                var actual = cls.GetBasicGates().Result;

//                for (int i = 0; i < expeted.LongCount(); i++)
//                {
//                    Assert.AreEqual(expeted.ElementAt(i).Id, actual.ElementAt(i).Id);
//                    Assert.AreEqual(expeted.ElementAt(i).Type, actual.ElementAt(i).Type);
//                }
//            }
//        }

//        [Test]
//        public void GetCustomGatesTest()
//        {
//            using (var mock = AutoMock.GetLoose())
//            {
//                mock.Mock<IGatewaysDbService>()
//                    .Setup(x => x.GetCustomGates())
//                    .Returns(GetCustomGatesSample());

//                var cls = mock.Create<IGatewaysDbService>();

//                var expected = GetCustomGatesSample().Result;
//                var actual = cls.GetCustomGates().Result;

//                Assert.AreEqual(expected.LongCount(), actual.LongCount());

//                for (int i = 0; i < expected.LongCount(); i++)
//                {
//                    Assert.AreEqual(expected.ElementAt(i).Id, actual.ElementAt(i).Id);
//                    Assert.AreEqual(expected.ElementAt(i).Type, actual.ElementAt(i).Type);
//                }
//            }
//        }


//        private LogicCircut CreateCircut()
//        {
//            LogicCircut circut = new LogicCircut();

//            var input1 = new InputElement(true, new Position());
//            var input2 = new InputElement(true, new Position(0, 1));
//            var input3 = new InputElement(true, new Position(0, 2));
//            var input4 = new InputElement(false, new Position(0, 3));
//            var input5 = new InputElement(false, new Position(0, 4));
//            var input6 = new InputElement(true, new Position(0, 5));
//            var input7 = new InputElement(false, new Position(0, 6));

//            LogicGateway and = new LogicGateway(GatewayType.And, new Position(0, 1), "MyAnd");
//            LogicGateway or = new LogicGateway(GatewayType.Or, new Position(0, 2), "MyOr");
//            LogicGateway not = new LogicGateway(GatewayType.Not, new Position(0, 3), "MyNot");
//            LogicGateway xor = new LogicGateway(GatewayType.Xor, new Position(0, 4), "MyXor");
//            LogicGateway nand = new LogicGateway(GatewayType.Nand, new Position(0, 5), "MyNand");

//            circut.Connect(input1, and, 1);
//            circut.Connect(input2, and, 2);

//            circut.Connect(input3, or, 1);
//            circut.Connect(input4, or, 2);

//            circut.Connect(input5, nand, 1);
//            circut.Connect(input6, nand, 2);

//            circut.Connect(input7, not, 1);

//            circut.Connect(nand, xor, 1);
//            circut.Connect(not, xor, 2);

//            circut.Connect(and, or, 1);
//            circut.Connect(xor, or, 2);

//            return circut;
//        }

//        private async Task<IEnumerable<LogicGateway>> GetAllGatesSample()
//        {
//            List<LogicGateway> basicGates = new List<LogicGateway>
//            {
//                new LogicGateway(GatewayType.Not, new Position(), "Not"),
//                new LogicGateway(GatewayType.And, new Position(0,1), "And"),
//                new LogicGateway(GatewayType.Or, new Position(0,2), "Or"),
//                new LogicGateway(GatewayType.Nor, new Position(0,3), "Nor"),
//                new LogicGateway(GatewayType.Xor, new Position(0,4), "Xor"),
//                new LogicGateway(GatewayType.Xnor, new Position(0,5), "Xnor"),
//                new LogicGateway(GatewayType.Nand, new Position(0,6), "Nand"),
//                new LogicGateway(GatewayType.Custom, new Position(0,7), "Custom1", CreateCircut()),
//                new LogicGateway(GatewayType.Custom, new Position(0,8), "Custom2", CreateCircut()),
//                new LogicGateway(GatewayType.Custom, new Position(0,9), "Custom3", CreateCircut())
//            };

//            await Task.Delay(1);

//            return basicGates;
//        }

//        private async Task<IEnumerable<LogicGateway>> GetBasicGatesSample()
//        {
//            List<LogicGateway> basicGates = new List<LogicGateway>
//            {
//                new LogicGateway(GatewayType.Not, new Position(0, 1), "Not"),
//                new LogicGateway(GatewayType.And, new Position(0, 2), "And"),
//                new LogicGateway(GatewayType.Or, new Position(0, 3), "Or"),
//                new LogicGateway(GatewayType.Nor, new Position(0, 4), "Nor"),
//                new LogicGateway(GatewayType.Xor, new Position(0, 5), "Xor"),
//                new LogicGateway(GatewayType.Xnor, new Position(0, 6), "Xnor"),
//                new LogicGateway(GatewayType.Nand, new Position(0, 7), "Nand"),
//            };

//            await Task.Delay(1);

//            return basicGates;
//        }

//        private async Task<IEnumerable<LogicGateway>> GetCustomGatesSample()
//        {
//            List<LogicGateway> customGates = new List<LogicGateway>
//            {
//                new LogicGateway(GatewayType.Custom, new Position(0, 1), "Custom1", CreateCircut()),
//                new LogicGateway(GatewayType.Custom, new Position(0, 2), "Custom2", CreateCircut()),
//                new LogicGateway(GatewayType.Custom, new Position(0, 3), "Custom3", CreateCircut()),
//                new LogicGateway(GatewayType.Custom, new Position(0, 4), "Custom4", CreateCircut()),
//                new LogicGateway(GatewayType.Custom, new Position(0, 5), "Custom5", CreateCircut())
//            };

//            await Task.Delay(1);

//            return customGates;
//        }
//    }
//}
