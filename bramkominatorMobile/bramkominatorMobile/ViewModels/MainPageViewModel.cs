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
        public Command DragItemCommand { get; }
        public Command DropItemCommand { get; }
        public Command DropEndCommand { get; }

        private bool displayStandardGateways;

        public Frame MyViewmodelFrame { get; set; }
        public Frame BasicFrame { get; set; }

        public MainPageViewModel()
        {
            Title = "Bramki testy";

            displayStandardGateways = true;

            StandardGateways = new ObservableRangeCollection<LogicGateway>
            {
                new LogicGateway(GatewayType.And, new Position()),
                new LogicGateway(GatewayType.Or, new Position()),
                new LogicGateway(GatewayType.Xnor, new Position()),
                new LogicGateway(GatewayType.Not, new Position()),
                new LogicGateway(GatewayType.Nor, new Position()),
                new LogicGateway(GatewayType.Nand, new Position()),
                new LogicGateway(GatewayType.Xor, new Position()),
            };

            CustomGateways = new ObservableRangeCollection<LogicGateway>
            {
                new LogicGateway(GatewayType.Custom, new Position(), "Moj 0"),
                new LogicGateway(GatewayType.Custom, new Position(), "Moj 1"),
                new LogicGateway(GatewayType.Custom, new Position(), "Moj 2"),
                new LogicGateway(GatewayType.Custom, new Position(), "Moj 3"),
                new LogicGateway(GatewayType.Custom, new Position(), "Moj 4"),
                new LogicGateway(GatewayType.Custom, new Position(), "Moj 5")
            };

            AddCustomGatewayCommand = new AsyncCommand(AddCustomGateway);
            DisplayGatewaysListCommand = new Command(DisplayGatewaysList);
            //DragItemCommand = new Command(DragItem);
            //DropItemCommand = new Command(DropItem);
            //DropEndCommand = new Command(DropEnd);

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

            GatewaysList.Clear();
            foreach (LogicGateway gateway in CustomGateways)
            {
                GatewaysList.Add(gateway);
            }

            IsBusy = false;
        }

        private void DragStarting(object sender, DragStartingEventArgs e)
        {
            var boxview = (sender as Element).Parent as BoxView;
            e.Data.Properties.Add("BoxView", boxview);
            MyViewmodelFrame = (sender as Element).Parent.Parent as Frame;
        }

        private void Drop(object sender, DropEventArgs e)
        {
            var box = e.Data.Properties["BoxView"] as BoxView;
            var frame = (sender as Element).Parent as Frame;
            frame.Content = box;
        }

        private void DropCompleted(Object sender, DropCompletedEventArgs e)
        {
            MyViewmodelFrame.Content = new BoxView
            {
                WidthRequest = 50,
                HeightRequest = 50,
                BackgroundColor = Color.Transparent
            };

            Random rnd = new Random();

            var box = new BoxView
            {
                WidthRequest = 50,
                HeightRequest = 50,
                BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))
            };

            var dragRecognizer = new DragGestureRecognizer();
            dragRecognizer.CanDrag = true;
            dragRecognizer.DragStarting += (s, p) =>
            {
                DragStarting(s, p);
            };
            dragRecognizer.DropCompleted += (s, p) =>
            {
                DropCompleted(s, p);
            };

            box.GestureRecognizers.Add(dragRecognizer);

            BasicFrame.Content = box;
        }

    }
}
