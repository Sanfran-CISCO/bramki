using bramkominatorMobile.Services;
using bramkominatorMobile.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using System.Linq;
using Moq;

namespace NUnitTests
{
    [TestFixture]
    public class GatewaysDbServiceTests
    {
        [Test]
        public void AddGatewayTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var gate = new LogicGateway(GatewayType.And, true, false, "MyTestAnd");

                mock.Mock<IGatewaysDbService>()
                    .Setup(x => x.AddGateway(gate))
                    .Returns(GetBasicGatesSample);

                var cls = mock.Create<IGatewaysDbService>();

                cls.AddGateway(gate);

                mock.Mock<IGatewaysDbService>()
                    .Verify(x => x.AddGateway(gate), Times.Once());
            }
        }

        [Test]
        public void RemoveGatewayTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var gate = new LogicGateway(GatewayType.And, true, false, "MyTestAnd");

                mock.Mock<IGatewaysDbService>()
                    .Setup(x => x.RemoveGateway(gate))
                    .Returns(GetBasicGatesSample);

                var cls = mock.Create<IGatewaysDbService>();

                cls.RemoveGateway(gate);

                mock.Mock<IGatewaysDbService>()
                    .Verify(x => x.RemoveGateway(gate), Times.Once());
            }
        }

        [Test]
        public void GetAllGatesTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IGatewaysDbService>()
                    .Setup(x => x.GetAllGates())
                    .Returns(GetAllGatesSample());

                var cls = mock.Create<IGatewaysDbService>();

                var expeted = GetAllGatesSample().Result;
                var actual = cls.GetAllGates().Result;

                for (int i = 0; i < expeted.LongCount(); i++)
                {
                    Assert.AreEqual(expeted.ElementAt(i).Id, actual.ElementAt(i).Id);
                    Assert.AreEqual(expeted.ElementAt(i).Type, actual.ElementAt(i).Type);
                }
            }
        }

        [Test]
        public void GetBasicGatesTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IGatewaysDbService>()
                    .Setup(x => x.GetBasicGates())
                    .Returns(GetBasicGatesSample);

                var cls = mock.Create<IGatewaysDbService>();

                var expeted = GetBasicGatesSample().Result;
                var actual = cls.GetBasicGates().Result;

                for (int i = 0; i < expeted.LongCount(); i++)
                {
                    Assert.AreEqual(expeted.ElementAt(i).Id, actual.ElementAt(i).Id);
                    Assert.AreEqual(expeted.ElementAt(i).Type, actual.ElementAt(i).Type);
                }
            }
        }

        [Test]
        public void GetCustomGatesTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IGatewaysDbService>()
                    .Setup(x => x.GetCustomGates())
                    .Returns(GetCustomGatesSample());

                var cls = mock.Create<IGatewaysDbService>();

                var expeted = GetCustomGatesSample().Result;
                var actual = cls.GetCustomGates().Result;

                for (int i = 0; i < expeted.LongCount(); i++)
                {
                    Assert.AreEqual(expeted.ElementAt(i).Id, actual.ElementAt(i).Id);
                    Assert.AreEqual(expeted.ElementAt(i).Type, actual.ElementAt(i).Type);
                }
            }
        }


        private LogicCircut CreateCircut()
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

            return circut;
        }

        private async Task<IEnumerable<LogicGateway>> GetAllGatesSample()
        {
            List<LogicGateway> basicGates = new List<LogicGateway>
            {
                new LogicGateway(GatewayType.Not, "Not"),
                new LogicGateway(GatewayType.And, "And"),
                new LogicGateway(GatewayType.Or, "Or"),
                new LogicGateway(GatewayType.Nor, "Nor"),
                new LogicGateway(GatewayType.Xor, "Xor"),
                new LogicGateway(GatewayType.Xnor, "Xnor"),
                new LogicGateway(GatewayType.Nand, "Nand"),
                new LogicGateway(GatewayType.Custom, "Custom1", CreateCircut()),
                new LogicGateway(GatewayType.Custom, "Custom2", CreateCircut()),
                new LogicGateway(GatewayType.Custom, "Custom3", CreateCircut())
            };

            await Task.Delay(1);

            return basicGates;
        }

        private async Task<IEnumerable<LogicGateway>> GetBasicGatesSample()
        {
            List<LogicGateway> basicGates = new List<LogicGateway>
            {
                new LogicGateway(GatewayType.Not, "Not"),
                new LogicGateway(GatewayType.And, "And"),
                new LogicGateway(GatewayType.Or, "Or"),
                new LogicGateway(GatewayType.Nor, "Nor"),
                new LogicGateway(GatewayType.Xor, "Xor"),
                new LogicGateway(GatewayType.Xnor, "Xnor"),
                new LogicGateway(GatewayType.Nand, "Nand"),
            };

            await Task.Delay(1);

            return basicGates;
        }

        private async Task<IEnumerable<LogicGateway>> GetCustomGatesSample()
        {
            List<LogicGateway> customGates = new List<LogicGateway>
            {
                new LogicGateway(GatewayType.Custom, "Custom1", CreateCircut()),
                new LogicGateway(GatewayType.Custom, "Custom2", CreateCircut()),
                new LogicGateway(GatewayType.Custom, "Custom3", CreateCircut()),
                new LogicGateway(GatewayType.Custom, "Custom4", CreateCircut()),
                new LogicGateway(GatewayType.Custom, "Custom5", CreateCircut())
            };

            await Task.Delay(1);

            return customGates;
        }
    }
}
