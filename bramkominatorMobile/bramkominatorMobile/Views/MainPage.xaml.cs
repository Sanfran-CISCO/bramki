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
    public partial class MainPage : ContentPage
    {
        private int R;
        private int C;

        private CircutElement[,] _matrix;

        private CableService _service;
        private CircutsDbService _circutsDbService;
        private GatewaysDbService _gatewaysDbService;

        private LogicCircut _circut;

        public MainPage()
        {
            InitializeComponent();

            R = C = 10;

            _matrix = new CircutElement[R, C];
            _service = new CableService(ref _matrix, R, C);

            CircutElement.InitDragHandler(ref _matrix);

            GetDefaultBoardTemplate();

            var input1Pos = new Position();
            var input2Pos = new Position(0, 1);
            var input3Pos = new Position(0, 3);

            var input1 = new InputElement(input1Pos);
            var input2 = new InputElement(true, input2Pos);
            var input3 = new InputElement(input3Pos);

            _matrix[input1Pos.Row, input1Pos.Column] = input1;
            _matrix[input2Pos.Row, input2Pos.Column] = input2;
            _matrix[input3Pos.Row, input3Pos.Column] = input3;

            //input1.GetFrame().BackgroundColor = Color.Yellow;
            //input2.GetFrame().BackgroundColor = Color.Red;
            //input3.GetFrame().BackgroundColor = Color.MediumBlue;

            BoardGrid.Children.Add(input1.GetFrame(), input1Pos.Column, input1Pos.Row);
            BoardGrid.Children.Add(input2.GetFrame(), input2Pos.Column, input2Pos.Row);
            BoardGrid.Children.Add(input3.GetFrame(), input3Pos.Column, input3Pos.Row);

            //_circut.Connect(input1, gate1, 1);
            //_circut.Connect(input2, gate1, 2);

            //_circut.Connect(input3, gate2, 2);

            //ConnectElements(input1, gate1);
            //ConnectElements(input2, gate1);

            //ConnectElements(input3, gate2);

            //var gridFrame = BoardGrid.Children.FirstOrDefault(x => Grid.GetColumn(x) == start.Column && Grid.GetRow(x) == start.Row);
            //gridFrame.BackgroundColor = Color.Yellow;
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

            BoardGrid.Children.Add(gate1.GetFrame(), start.Column, start.Row);
            BoardGrid.Children.Add(gate2.GetFrame(), target.Column, target.Row);
        }

        private async void GetCustomBoardTemplate(int id)
        {
            _circutsDbService = new CircutsDbService();

            _circut = await _circutsDbService.GetCircut(id);

            // TODO --> Iterate through circut and add elements to BoardGrid
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
    }
}