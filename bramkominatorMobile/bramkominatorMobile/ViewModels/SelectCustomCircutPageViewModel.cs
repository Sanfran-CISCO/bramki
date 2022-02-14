using System;
using bramkominatorMobile.Services;
using bramkominatorMobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using MvvmHelpers.Commands;

namespace bramkominatorMobile.ViewModels
{
    public class SelectCustomCircutPageViewModel : ViewModelBase
    {
        private CircutsDbService _dbService;

        public IEnumerable<LogicCircut> Circuts { get; set; }

        public AsyncCommand RefreshCommand { get; set; }

        public SelectCustomCircutPageViewModel()
        {
            Title = "Saved Circuts:";

            _dbService = new CircutsDbService();

            _ = Init();

            RefreshCommand = new AsyncCommand(Init);
        }

        private async Task Init()
        {
            Circuts = await GetAllCircutsSample();
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

            for (int i=0; i<5; i++)
            {
                circuts[i].Name = $"Circut{i}";
            }

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
    }
}
