using System;
using bramkominatorMobile.Models;
using bramkominatorMobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
                    EmptyElement element = new EmptyElement();

                    _matrix[row, column] = new EmptyElement(column, row);

                    var frame = new Frame
                    {
                        BackgroundColor = Color.Transparent,
                        BorderColor = Color.Orange,
                        Margin = -2
                    };

                    var image = new Image
                    {
                        Source = element.Image,
                        HeightRequest = 100,
                        WidthRequest = 100,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };

                    var stackLayout = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                        {
                            image
                        }
                    };

                    frame.Content = image;

                    var dropRecognizer = new DropGestureRecognizer();
                    dropRecognizer.AllowDrop = true;
                    dropRecognizer.Drop += (s, e) => {
                        Drop(s, e);
                    };

                    frame.GestureRecognizers.Add(dropRecognizer);

                    BoardGrid.Children.Add(frame, column, row);
                }
            }

            BoardGrid.Children.Add(new Image { Source = "xnor.png" }, 1, 0);
            BoardGrid.Children.Add(new Image { Source = "xnor.png" }, 3, 3);

            var path = _service.FindPath(_matrix[0, 1], _matrix[3, 3]);

            for (int i=0; i<path.Count-1; i++)
            {
                BoardGrid.Children.Add(new Image { Source = "kabel.png" }, path[i].Column, path[i].Row);
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