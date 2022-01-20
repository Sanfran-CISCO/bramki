using System;
using System.IO;
using bramkominatorMobile.Models;
using bramkominatorMobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FFImageLoading.Svg;
using FFImageLoading.Svg.Forms;

namespace bramkominatorMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private int R;
        private int C;

        private CircutElement[,] _matrix;

        private CableService _service;

        public Frame MyFrame { get; set; }

        public MainPage()
        {
            InitializeComponent();

            R = C = 10;

            _matrix = new CircutElement[R, C];

            _service = new CableService(_matrix, R, C);

            for (int row=0; row<R; row++)
            {
                for (int column=0; column<C; column++)
                {
                    _matrix[row, column] = new EmptyElement(column, row);

                    var frame = new Frame
                    {
                        BackgroundColor = Color.Transparent,
                        BorderColor = Color.Orange,
                        Margin = -2
                    };

                    var dropRecognizer = new DropGestureRecognizer();
                    dropRecognizer.AllowDrop = true;
                    dropRecognizer.Drop += (s, e) => {
                        Drop(s, e);
                    };

                    frame.GestureRecognizers.Add(dropRecognizer);

                    BoardGrid.Children.Add(frame, column, row);
                }
            }

            var start = new Position(0, 1);
            var target = new Position(4, 3);

            _matrix[start.Row, start.Column] = new LogicGateway(GatewayType.Xnor, position: start);
            _matrix[target.Column, target.Row] = new LogicGateway(GatewayType.Xnor, position: target);

            BoardGrid.Children.Add(new Image { Source = "xnor.png" }, start.Column, start.Row);
            BoardGrid.Children.Add(new Image { Source = "xnor.png" }, target.Column, target.Row);

            _matrix[0, 2] = new LogicGateway(GatewayType.Not, position: new Position(2, 0));
            _matrix[0, 3] = new LogicGateway(GatewayType.Not, position: new Position(3, 0));
            BoardGrid.Children.Add(new Image { Source = "not.png" }, 2, 0);
            BoardGrid.Children.Add(new Image { Source = "not.png" }, 3, 0);

            var path = _service.FindPath(_matrix[start.Row, start.Column], _matrix[target.Row, target.Column]);

            if (path.Count == 0)
                Shell.Current.DisplayAlert("Path not found", "Path not found", "OK");
            else
            {
                var cable = new Cable(path, start, target);

                for (int i = 0; i < path.Count-1; i++)
                {
                    BoardGrid.Children.Add(new Frame { BackgroundColor = Color.Orange }, path[i].Column, path[i].Row);
                    BoardGrid.Children.Add(new SvgCachedImage { Source = cable.GetImage(i) }, path[i].Column, path[i].Row);
                }
            }
        }

        void DragStarting(Object sender, DragStartingEventArgs e)
        {
            var boxview = (sender as Element).Parent as BoxView;
            e.Data.Properties.Add("BoxView", boxview);
            MyFrame = (sender as Element).Parent.Parent as Frame;
        }

        void Drop(Object sender, DropEventArgs e)
        {
            var box = e.Data.Properties["BoxView"] as BoxView;
            var frame = (sender as Element).Parent as Frame;
            frame.Content = box;
        }

        void DropCompleted(Object sender, DropCompletedEventArgs e)
        {
            MyFrame.Content = new BoxView
            {
                WidthRequest=50,
                HeightRequest=50,
                BackgroundColor=Color.Transparent
            };

            Random rnd = new Random();

            var box = new BoxView
            {
                WidthRequest = 50,
                HeightRequest = 50,
                //BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))
                Style = (Style) Application.Current.Resources["Box"]
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

        void DropCompletedBasic(System.Object sender, Xamarin.Forms.DropCompletedEventArgs e)
        {
            MyFrame.Content = new BoxView
            {
                WidthRequest = 50,
                HeightRequest = 50,
                BackgroundColor = Color.Transparent
            };

            Random random = new Random();

            BasicFrame.Content = new BoxView
            {
                WidthRequest = 50,
                HeightRequest = 50,
                BackgroundColor = Color.HotPink
            };
        }
    }
}