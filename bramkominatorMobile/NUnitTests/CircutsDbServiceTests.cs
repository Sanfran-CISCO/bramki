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
    public class CircutsDbServiceTests
    {
        [Test]
        public async Task AddCircutTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var not = new LogicGateway(GatewayType.Not);
                not.InputA = false;

                var and = new LogicGateway(GatewayType.And);
                and.InputB = true;

                var circut = new LogicCircut();

                circut.Connect(and, not, 1);

                mock.Mock<ICircutsDbService>()
                    .Setup(x => x.AddCircut(circut));

                var cls = mock.Create<ICircutsDbService>();

                await cls.AddCircut(circut);

                mock.Mock<ICircutsDbService>()
                    .Verify(x => x.AddCircut(circut), Times.Once());
            }
        }

        [Test]
        public async Task RemoveCircutTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var not = new LogicGateway(GatewayType.Not);
                not.InputA = false;

                var and = new LogicGateway(GatewayType.And);
                and.InputB = true;

                var circut = new LogicCircut();

                circut.Connect(and, not, 1);

                mock.Mock<ICircutsDbService>()
                    .Setup(x => x.AddCircut(circut));

                mock.Mock<ICircutsDbService>()
                    .Setup(x => x.RemoveCircut(circut));

                var cls = mock.Create<ICircutsDbService>();

                await cls.AddCircut(circut);

                mock.Mock<ICircutsDbService>()
                    .Verify(x => x.AddCircut(circut), Times.Once());

                await cls.RemoveCircut(circut);

                mock.Mock<ICircutsDbService>()
                    .Verify(x => x.RemoveCircut(circut), Times.Once());
            }
        }

        [Test]
        public async Task UpdateCircutTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var not = new LogicGateway(GatewayType.Not);
                not.InputA = false;

                var and = new LogicGateway(GatewayType.And);
                and.InputB = true;

                var circut = new LogicCircut();

                circut.Connect(not, and, 1);

                mock.Mock<ICircutsDbService>()
                    .Setup(x => x.AddCircut(circut));

                mock.Mock<ICircutsDbService>()
                    .Setup(x => x.UpdateCircut(circut));

                var cls = mock.Create<ICircutsDbService>();

                await cls.AddCircut(circut);

                mock.Mock<ICircutsDbService>()
                    .Verify(x => x.AddCircut(circut), Times.Once());

                and.InputB = false;

                await cls.UpdateCircut(circut);

                mock.Mock<ICircutsDbService>()
                    .Verify(x => x.UpdateCircut(circut), Times.Once());

                var expected = GetUpdatedCircutSample();

                Assert.AreEqual(expected.Parent.Gateway.Type, circut.Parent.Gateway.Type);
                Assert.AreEqual(expected.Parent.Gateway.Output, circut.Parent.Gateway.Output);
            }
        }

        [Test]
        public void GetAllCircutsTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ICircutsDbService>()
                    .Setup(x => x.GetAllCircuts())
                    .Returns(GetAllCircutsSample());

                var cls = mock.Create<ICircutsDbService>();

                var actual = cls.GetAllCircuts().Result;

                var expected = GetAllCircutsSample().Result;

                Assert.AreEqual(expected.LongCount(), actual.LongCount());

                for (int i=0; i<expected.LongCount(); i++)
                {
                    Assert.AreEqual(expected.ElementAt(i).Parent.Gateway.Output, actual.ElementAt(i).Parent.Gateway.Output);
                    Assert.AreEqual(expected.ElementAt(i).Parent.Gateway.Type, actual.ElementAt(i).Parent.Gateway.Type);
                    Assert.AreEqual(expected.ElementAt(i).Size, actual.ElementAt(i).Size);
                }
            }
        }

        [Test]
        public async Task GetCircutTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ICircutsDbService>()
                    .Setup(x => x.GetCircut(1))
                    .Returns(GetSingleCircutSample());

                var cls = mock.Create<ICircutsDbService>();

                var actual = await cls.GetCircut(1);

                var expected = GetSingleCircutSample().Result;

                Assert.AreEqual(1, actual.Id);
                Assert.AreEqual(expected.Parent.Gateway.Output, actual.Parent.Gateway.Output);
                Assert.AreEqual(expected.Parent.Gateway.Type, actual.Parent.Gateway.Type);
            }
        }

        private LogicCircut GetUpdatedCircutSample()
        {
            var not = new LogicGateway(GatewayType.Not);
            not.InputA = false;

            var and = new LogicGateway(GatewayType.And);
            and.InputB = false;

            var circut = new LogicCircut();

            circut.Connect(not, and, 1);

            return circut;
        }

        private async Task<IEnumerable<LogicCircut>> GetAllCircutsSample()
        {
            List<LogicCircut> circuts = new List<LogicCircut>
            {
                CreateCircut(),
                CreateCircut(),
                CreateCircut(),
                CreateCircut(),
                CreateCircut()
            };

            await Task.Delay(100);

            return circuts;
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

        private async Task<LogicCircut> GetSingleCircutSample()
        {
            var circut = CreateCircut();

            circut.Id = 1;

            await Task.Delay(100);

            return circut;
        }
    }
}
