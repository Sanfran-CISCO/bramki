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

namespace bramkominatorMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private int R;
        private int C;

        private CircutElement[,] _matrix;

        private CableService _service;

        private LogicCircut _circut;

        public MainPage()
        {
            InitializeComponent();
            CircutElement.InitDragHandler(ref _matrix);

            R = C = 10;

            _matrix = new CircutElement[R, C];
            _service = new CableService(ref _matrix, R, C);
            _circut = new LogicCircut();

            

            for (int row=0; row<R; row++)
            {
                for (int column=0; column<C; column++)
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

            var start = new Position(0, 0);
            var target = new Position(4, 3);

            _matrix[start.Row, start.Column] = new LogicGateway(GatewayType.Xnor, position: start);
            _matrix[target.Row, target.Column] = new LogicGateway(GatewayType.Xnor, position: target);

            BoardGrid.Children.Add(_matrix[start.Row, start.Column].GetFrame(), start.Column, start.Row);
            BoardGrid.Children.Add(_matrix[target.Row, target.Column].GetFrame(), target.Column, target.Row);

            //InputElement input = new InputElement("input1", input: true,
            //    gate: _matrix[start.Row, start.Column] as LogicGateway, inputNumber: 1,
            //    new Position(0, 1));

            //(_matrix[start.Row, start.Column] as LogicGateway).InputB = true;

            _circut.Connect(_matrix[start.Row, start.Column], _matrix[target.Row, target.Column], 1);

            ConnectElements(start, target);

            //var gridFrame = BoardGrid.Children.FirstOrDefault(x => Grid.GetColumn(x) == start.Column && Grid.GetRow(x) == start.Row);
            //gridFrame.BackgroundColor = Color.Yellow;
        }

        

        private void ConnectElements(Position start, Position target)
        {
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

        private void Drop(Object sender, DropEventArgs e)
        {
            var image = e.Data.Properties["Gate"] as Image;
            var frame = (sender as Element).Parent as Frame;
            frame.Padding = 0;
            frame.Content = image;

            var newRow = Grid.GetRow(frame);
            var newCol = Grid.GetColumn(frame);

            _matrix[newRow, newCol] = _matrix[0, 0];
            _matrix[0, 0] = new EmptyElement(0, 0);
            _matrix[newRow, newCol].Position.Set(newCol, newRow);

            //Debug.WriteLine("<--------------->");
            //Debug.WriteLine($"\tDROP --> Image: {e.Data.Properties["Gate"]}");
            //Debug.WriteLine($"\tDROP --> Sender Parent: {(sender as Element).Parent}");
            //Debug.WriteLine($"\t{image.Width}, {image.Height}");
            //Debug.WriteLine($"\tmatrix[0,0] --> {_matrix[0, 0]}");
            //Debug.WriteLine($"\tmatrix[{newRow},{newCol}] --> {_matrix[newRow, newCol]}");
            //Debug.WriteLine($"\tmatrix[{newRow},{newCol}].X --> {_matrix[newRow, newCol].Position.Column}");
            //Debug.WriteLine($"\tmatrix[{newRow},{newCol}].Y --> {_matrix[newRow, newCol].Position.Row}");
            //Debug.WriteLine("<--------------->");
        }
    }
}