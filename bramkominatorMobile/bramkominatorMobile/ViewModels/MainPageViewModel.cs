using System;
using System.Collections.Generic;
using System.Text;
using bramkominatorMobile.Models;
using MvvmHelpers;

namespace bramkominatorMobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableRangeCollection<LogicGateway> Gateways { get; }


        public MainPageViewModel()
        {
            Title = "Bramki testy";

            Gateways = new ObservableRangeCollection<LogicGateway>
            {
                new LogicGateway(GatewayType.And, true, false),
                new LogicGateway(GatewayType.Or, false, false),
                new LogicGateway(GatewayType.Xnor, true, false)
            };
        }
    }
}
