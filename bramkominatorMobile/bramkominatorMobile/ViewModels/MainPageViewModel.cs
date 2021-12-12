using System;
using System.Threading.Tasks;
using bramkominatorMobile.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace bramkominatorMobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableRangeCollection<LogicGateway> GatewaysList { get; set; }
        public ObservableRangeCollection<LogicGateway> StandardGateways { get; }
        public ObservableRangeCollection<LogicGateway> CustomGateways { get; set; }

        public AsyncCommand AddCustomGatewayCommand { get; }
        public Command DisplayGatewaysListCommand { get; }

        private bool displayStandardGateways;

        public MainPageViewModel()
        {
            Title = "Bramki testy";

            displayStandardGateways = true;

            StandardGateways = new ObservableRangeCollection<LogicGateway>
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
            DisplayGatewaysListCommand = new Command(DisplayGatewaysList);

            GatewaysList = new ObservableRangeCollection<LogicGateway>(StandardGateways);
        }

        private async Task AddCustomGateway()
        {
            await Shell.Current.DisplayAlert("Custom Gateway", "Custom Gateway Added!", "OK");
        }

        private void DisplayGatewaysList()
        {
            try
            {
                if (displayStandardGateways)
                {
                    ShowCustomGateways();
                    displayStandardGateways = false;
                }
                else
                {
                    ShowStandardGateways();
                    displayStandardGateways = true;
                }
            } catch (Exception)
            {

            }
        }

        private void ShowStandardGateways()
        {
            IsBusy = true;

            //await Shell.Current.DisplayAlert("Standard Gateways", "Here will be standard Gatewats", "OK");

            GatewaysList.Clear();
            foreach (LogicGateway gateway in StandardGateways)
            {
                GatewaysList.Add(gateway);
            }

            IsBusy = false;
        }

        private void ShowCustomGateways()
        {
            IsBusy = true;

            //await Shell.Current.DisplayAlert("Custom Gateways", "Here will be custom Gatewats", "OK");

            GatewaysList.Clear();
            foreach (LogicGateway gateway in CustomGateways)
            {
                GatewaysList.Add(gateway);
            }

            IsBusy = true;
        }
    }
}
