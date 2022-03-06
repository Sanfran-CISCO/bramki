using System;
using System.Diagnostics;
using System.IO;
using bramkominatorMobile.Models;
using bramkominatorMobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FFImageLoading.Svg;
using FFImageLoading.Svg.Forms;
using System.Linq;
using SQLite;

namespace bramkominatorMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(CircutId), nameof(CircutId))]
    public partial class MainPage : ContentPage
    {
        public string CircutId { get; set; }

        private int R;
        private int C;

        private CircutElement[,] _matrix;

        private CableService _service;

        private LogicCircut _circut;

        public MainPage()
        {
            InitializeComponent();

            //GetDefaultBoardTemplate();

            //var input1Pos = new Position();
            //var input2Pos = new Position(0, 1);
            //var input3Pos = new Position(0, 3);

            //var input1 = new InputElement(input1Pos);
            //var input2 = new InputElement(true, input2Pos);
            //var input3 = new InputElement(input3Pos);

            //_matrix[input1Pos.Row, input1Pos.Column] = input1;
            //_matrix[input2Pos.Row, input2Pos.Column] = input2;
            //_matrix[input3Pos.Row, input3Pos.Column] = input3;

            ////input1.GetFrame().BackgroundColor = Color.Yellow;
            ////input2.GetFrame().BackgroundColor = Color.Red;
            ////input3.GetFrame().BackgroundColor = Color.MediumBlue;

            //BoardGrid.Children.Add(input1.GetFrame(), input1Pos.Column, input1Pos.Row);
            //BoardGrid.Children.Add(input2.GetFrame(), input2Pos.Column, input2Pos.Row);
            //BoardGrid.Children.Add(input3.GetFrame(), input3Pos.Column, input3Pos.Row);

            //_circut.Connect(input1, gate1, 1);
            //_circut.Connect(input2, gate1, 2);

            //_circut.Connect(input3, gate2, 2);

            //ConnectElements(input1, gate1);
            //ConnectElements(input2, gate1);

            //ConnectElements(input3, gate2);

            //var gridFrame = BoardGrid.Children.FirstOrDefault(x => Grid.GetColumn(x) == start.Column && Grid.GetRow(x) == start.Row);
            //gridFrame.BackgroundColor = Color.Yellow;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            R = C = 10;

            _matrix = new CircutElement[R, C];
            _service = new CableService(ref _matrix, R, C);

            CircutElement.InitDragHandler(ref _matrix);

            int.TryParse(CircutId, out var result);

            if (result <= 0)
                GetDefaultBoardTemplate();
            else
                GetCustomBoardTemplate(result);
        }

        private void GetDefaultBoardTemplate()
        {
            _circut = new LogicCircut();

            for (int row = 0; row < R; row++)
            {
                for (int column = 0; column < C; column++)
                {
                    _matrix[row, column] = new EmptyElement(column, row);

                    var frame = new Frame
                    {
                        BackgroundColor = Color.Transparent,
                        BorderColor = Color.DarkGray,
                        Margin = -3,
                        WidthRequest = 100,
                        HeightRequest = 100
                    };

                    var dropRecognizer = new DropGestureRecognizer();
                    dropRecognizer.AllowDrop = true;
                    dropRecognizer.Drop += (s, e) => {
                        Drop(s, e);
                    };

                    frame.GestureRecognizers.Add(dropRecognizer);

                    Grid.SetColumn(frame, column);
                    Grid.SetRow(frame, row);
                    BoardGrid.Children.Add(frame, column, row);
                }
            }

            var start = new Position(2, 1);
            var target = new Position(2, 3);

            var gate1 = new LogicGateway(GatewayType.Not, position: start);
            var gate2 = new LogicGateway(GatewayType.And, position: target);

            _matrix[gate1.Position.Row, gate1.Position.Column] = gate1;
            _matrix[gate2.Position.Row, gate2.Position.Column] = gate2;

            _circut.Connect(gate1, gate2, 1);

            foreach (CircutElement el in _circut.Elements)
            {
                BoardGrid.Children.Add(el.GetFrame(), el.Position.Column, el.Position.Row);
            }

            //var gate1Frame = ((gate1.GetFrame() as Frame).Content as StackLayout).Children[0] as Label;

            //Debug.WriteLine($"Gate 1 content: {gate1Frame.Text}");

            //BoardGrid.Children.Add(gate1.GetFrame(), start.Column, start.Row);
            //BoardGrid.Children.Add(gate2.GetFrame(), target.Column, target.Row);

            //var gridChild = BoardGrid.Children.FirstOrDefault(c => Grid.GetRow(c) == 1 && Grid.GetColumn(c) == 2);
            //Debug.WriteLine($"Grid [2,1] content: {gridChild}");
        }

        private async void GetCustomBoardTemplate(int id)
        {

            _circut = await CircutsDbService.GetCircut(id);

            _circut.InitDragHandlers(ref _matrix);

            foreach (var element in _circut.Elements)
                BoardGrid.Children.Add(element.GetFrame(), element.Position.Column, element.Position.Row);
        }

        private void Drop(Object sender, DropEventArgs e)
        {
            var element = e.Data.Properties["Layout"] as StackLayout;
            var frame = (sender as Element).Parent as Frame;
            frame.Padding = 0;
            frame.Content = element;

            var newRow = Grid.GetRow(frame);
            var newCol = Grid.GetColumn(frame);

            var matrixElement = e.Data.Properties["MatrixElement"] as CircutElement;
            _matrix[newRow, newCol] = matrixElement;
        }

        private void ConnectElements(CircutElement from, CircutElement to)
        {
            var start = from.Position;
            var target = to.Position;

            var path = _service.FindPath(_matrix[start.Row, start.Column], _matrix[target.Row, target.Column]);

            if (path.Count == 0)
                Shell.Current.DisplayAlert("Path not found", "Path not found", "OK");
            else
            {
                var cable = new Cable(path, start, target);

                for (int i = 0; i < path.Count - 1; i++)
                {
                    BoardGrid.Children.Add(
                        new SvgCachedImage
                        {
                            Source = cable.GetImage(i),
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        },
                        path[i].Column, path[i].Row);
                }
            }
        }

        async void AddCustomCircut(System.Object sender, System.EventArgs e)
        {
            var name = await Shell.Current.DisplayPromptAsync("Set name", "Name your circut:", accept: "Save", placeholder: "CustomCircut");

            if (name == null)
                await Shell.Current.DisplayAlert("Empty name", "Name cannot be empty!", "OK");

            else
            {
                _circut.Name = name;

                await CircutsDbService.AddCircut(_circut);
            }
        }

        async void AddCustomGateway(System.Object sender, System.EventArgs e)
        {
            var name = await Shell.Current.DisplayPromptAsync("Set name", "Name your gateway:", accept: "Save",
                 placeholder: "CustomGate", maxLength: 10);

            if (name == null)
                await Shell.Current.DisplayAlert("Empty name", "Name cannot be empty!", "OK");

            else
            {
                foreach (var element in _circut.Elements)
                {
                    var type = element.GetType();
                    if (type == typeof(InputElement) || type == typeof(OutputElement))
                        _circut.Remove(element);
                }

                _circut.Name = name;

                await CircutsDbService.AddCircut(_circut);
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}?CircutId=-1");
            }
        }
    }
}