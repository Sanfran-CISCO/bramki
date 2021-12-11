using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using bramkominatorMobile.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace bramkominatorMobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableRangeCollection<LogicGateway> Gateways { get; }
        public ObservableRangeCollection<LogicGateway> CustomGateways { get; }

        public AsyncCommand AddCustomGatewayCommand { get; }
        public AsyncCommand DisplayGatewaysListCommand { get; }

        private bool displayStandardGateways;


        public MainPageViewModel()
        {
            Title = "Bramki testy";

            displayStandardGateways = true;

            Gateways = new ObservableRangeCollection<LogicGateway>
            {
                new LogicGateway(GatewayType.And, true, false),
                new LogicGateway(GatewayType.Or, true, false),
                new LogicGateway(GatewayType.Xnor, true, false)
            };

            CustomGateways = new ObservableRangeCollection<LogicGateway>
            {
                new LogicGateway(GatewayType.Custom, true, false, "Moj 0"),
                new LogicGateway(GatewayType.Custom, true, false, "Moj 1"),
                new LogicGateway(GatewayType.Custom, true, false, "Moj 2"),
                new LogicGateway(GatewayType.Custom, true, false, "Moj 3"),
                new LogicGateway(GatewayType.Custom, true, false, "Moj 4"),
                new LogicGateway(GatewayType.Custom, true, false, "Moj 5")
            };

            AddCustomGatewayCommand = new AsyncCommand(AddCustomGateway);
            DisplayGatewaysListCommand = new AsyncCommand(DisplayGatewaysList);
        }

        private async Task AddCustomGateway()
        {
            await Shell.Current.DisplayAlert("Custom Gateway", "Custom Gateway Added!", "OK");
        }

        private async Task DisplayGatewaysList()
        {
            if (displayStandardGateways)
            {
                await ShowStandardGateways();
                displayStandardGateways = false;
            }
            else
            {
                await ShowCustomGateways();
                displayStandardGateways = true;
            }
        }

        private async Task ShowStandardGateways()
        {
            await Shell.Current.DisplayAlert("Standard Gateways", "Here will be standard Gatewats", "OK");
        }

        private async Task ShowCustomGateways()
        {
            await Shell.Current.DisplayAlert("Custom Gateways", "Here will be custom Gatewats", "OK");
        }
    }
}
