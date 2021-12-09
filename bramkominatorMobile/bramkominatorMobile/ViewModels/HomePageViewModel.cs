using System;
using System.Collections.Generic;
using System.Text;
using bramkominatorMobile.Models;
using MvvmHelpers;

namespace bramkominatorMobile.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public LogicGateway Gateway { get; }
        public HomePageViewModel()
        {
            Title = "Bramkominator";
            Gateway = new LogicGateway(GatewayType.Or, true, false);
        }
    }
}
