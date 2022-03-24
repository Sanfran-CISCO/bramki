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
        //[Test]
        //public async Task AddCircutTest()
        //{
        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        var circut = new LogicCircut();

        //        var not = new LogicGateway(GatewayType.Not, new Position(1,1));
        //        var and = new LogicGateway(GatewayType.And, new Position(1,2));

        //        var input1 = new InputElement(false, new Position());
        //        var input2 = new InputElement(true, new Position(0, 1));

        //        circut.Connect(input1, and, 1);
        //        circut.Connect(input2, and, 2);

        //        circut.Connect(and, not, 1);

        //        mock.Mock<ICircutsDbService>()
        //            .Setup(x => x.AddCircut(circut));

        //        var cls = mock.Create<ICircutsDbService>();

        //        await cls.AddCircut(circut);

        //        mock.Mock<ICircutsDbService>()
        //            .Verify(x => x.AddCircut(circut), Times.Once());
        //    }
        //}

        //[Test]
        //public async Task RemoveCircutTest()
        //{
        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        var circut = new LogicCircut();

        //        var input1 = new InputElement(false, new Position());
        //        var input2 = new InputElement(true, new Position(0, 1));

        //        var not = new LogicGateway(GatewayType.Not, new Position(1,0));

        //        var and = new LogicGateway(GatewayType.And, new Position(2,0));

        //        circut.Connect(input1, not, 1);
        //        circut.Connect(input2, and, 2);
        //        circut.Connect(and, not, 1);

        //        mock.Mock<ICircutsDbService>()
        //            .Setup(x => x.AddCircut(circut));

        //        mock.Mock<ICircutsDbService>()
        //            .Setup(x => x.RemoveCircut(circut));

        //        var cls = mock.Create<ICircutsDbService>();

        //        await cls.AddCircut(circut);

        //        mock.Mock<ICircutsDbService>()
        //            .Verify(x => x.AddCircut(circut), Times.Once());

        //        await cls.RemoveCircut(circut);

        //        mock.Mock<ICircutsDbService>()
        //            .Verify(x => x.RemoveCircut(circut), Times.Once());
        //    }
        //}

        //[Test]
        //public async Task UpdateCircutTest()
        //{
        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        var circut = new LogicCircut();

        //        var input1 = new InputElement(false, new Position());
        //        var input2 = new InputElement(true, new Position(0,1));

        //        var not = new LogicGateway(GatewayType.Not, new Position(1,0));
        //        var and = new LogicGateway(GatewayType.And, new Position(2,0));

        //        circut.Connect(input1, not, 1);
        //        circut.Connect(input2, and, 2);
        //        circut.Connect(not, and, 1);

        //        mock.Mock<ICircutsDbService>()
        //            .Setup(x => x.AddCircut(circut));

        //        mock.Mock<ICircutsDbService>()
        //            .Setup(x => x.UpdateCircut(circut));

        //        var cls = mock.Create<ICircutsDbService>();

        //        await cls.AddCircut(circut);

        //        mock.Mock<ICircutsDbService>()
        //            .Verify(x => x.AddCircut(circut), Times.Once());

        //        and.InputB = false;

        //        await cls.UpdateCircut(circut);

        //        mock.Mock<ICircutsDbService>()
        //            .Verify(x => x.UpdateCircut(circut), Times.Once());

        //        var expected = GetUpdatedCircutSample();

        //        Assert.AreEqual(expected.Parent.Content.Output, circut.Parent.Content.Output);
        //    }
        //}

        //[Test]
        //public void GetAllCircutsTest()
        //{
        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        mock.Mock<ICircutsDbService>()
        //            .Setup(x => x.GetAllCircuts())
        //            .Returns(GetAllCircutsSample());

        //        var cls = mock.Create<ICircutsDbService>();

        //        var actual = cls.GetAllCircuts().Result;

        //        var expected = GetAllCircutsSample().Result;

        //        Assert.AreEqual(expected.LongCount(), actual.LongCount());

        //        for (int i=0; i<expected.LongCount(); i++)
        //        {
        //            Assert.AreEqual(expected.ElementAt(i).Parent.Content.Output, actual.ElementAt(i).Parent.Content.Output);
        //            Assert.AreEqual(expected.ElementAt(i).Size, actual.ElementAt(i).Size);
        //        }
        //    }
        //}

        //[Test]
        //public async Task GetCircutTest()
        //{
        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        mock.Mock<ICircutsDbService>()
        //            .Setup(x => x.GetCircut(1))
        //            .Returns(GetSingleCircutSample());

        //        var cls = mock.Create<ICircutsDbService>();

        //        var actual = await cls.GetCircut(1);

        //        var expected = GetSingleCircutSample().Result;

        //        Assert.AreEqual(1, actual.Id);
        //        Assert.AreEqual(expected.Parent.Content.Output, actual.Parent.Content.Output);
        //        //Assert.AreEqual(expected.Parent.Content.Type, actual.Parent.Content.Type);
        //    }
        //}

        private LogicCircut GetUpdatedCircutSample()
        {
            var circut = new LogicCircut();

            var input1 = new InputElement(false, new Position());
            var input2 = new InputElement(true, new Position(0, 1));

            var not = new LogicGateway(GatewayType.Not, new Position(1, 0));
            var and = new LogicGateway(GatewayType.And, new Position(2, 0));

            circut.Connect(input1, not, 1);
            circut.Connect(input2, and, 2);
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

            var input1 = new InputElement(true, new Position());
            var input2 = new InputElement(true, new Position(0, 1));
            var input3 = new InputElement(true, new Position(0, 2));
            var input4 = new InputElement(false, new Position(0, 3));
            var input5 = new InputElement(false, new Position(0, 4));
            var input6 = new InputElement(true, new Position(0, 5));
            var input7 = new InputElement(false, new Position(0, 6));

            LogicGateway and = new LogicGateway(GatewayType.And, new Position(1, 1), "MyAnd");
            LogicGateway or = new LogicGateway(GatewayType.Or, new Position(1, 2), "MyOr");
            LogicGateway not = new LogicGateway(GatewayType.Not, new Position(1, 3), "MyNot");
            LogicGateway xor = new LogicGateway(GatewayType.Xor, new Position(1, 4), "MyXor");
            LogicGateway nand = new LogicGateway(GatewayType.Nand, new Position(1, 5), "MyNand");

            circut.Connect(input1, and, 1);
            circut.Connect(input2, and, 2);

            circut.Connect(input3, or, 1);
            circut.Connect(input4, or, 2);

            circut.Connect(input5, nand, 1);
            circut.Connect(input6, nand, 2);

            circut.Connect(input7, not, 1);


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
