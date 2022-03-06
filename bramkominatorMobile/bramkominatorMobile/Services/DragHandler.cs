using System;
using bramkominatorMobile.Models;
using Xamarin.Forms;

namespace bramkominatorMobile.Services
{
    public class DragHandler
    {
        private static CircutElement[,] _matrix;
        public Frame Frame { get; set; }

        public DragHandler(ref CircutElement[,] matrix)
        {
            _matrix = matrix;
            Frame = GenerateFrame(DragRecognizer(), TapRecognizer());
        }

        private Frame GenerateFrame(DragGestureRecognizer dragRecognizer, TapGestureRecognizer tapRecognizer)
        {
            var frame = new Frame
            {
                Content = new StackLayout
                {
                    Children =
                        {
                            new Label
                            {
                                Text = "Default",
                                FontSize = 20,
                                TextColor = Color.White,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center,
                                GestureRecognizers =
                                {
                                    tapRecognizer
                                }
                            },
                        },
                    Padding = 0,
                    BackgroundColor = Color.DodgerBlue,
                    GestureRecognizers =
                        {
                            dragRecognizer
                        }
                },
                Padding = 0,
                BackgroundColor = Color.Transparent
            };

            frame.Content.GestureRecognizers.Add(dragRecognizer);

            return frame;
        }

        private DragGestureRecognizer DragRecognizer()
        {
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

            return dragRecognizer;
        }

        private TapGestureRecognizer TapRecognizer()
        {
            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += async (s, e) =>
            {
                //await Shell.Current.DisplayAlert("Tapped", "You have tapped", "OK");

                var frame = ((s as Element).Parent.Parent as Frame);

                var row = Grid.GetRow(frame);
                var col = Grid.GetColumn(frame);

                var element = _matrix[row, col];

                if (element.GetType() == typeof(InputElement))
                {
                    await Shell.Current.DisplayAlert("Tapped", $"Element Output: {element.Output}", "OK");

                    (element as InputElement).Clicked();
                }
            };

            return tapRecognizer;
        }

        void DragStarting(Object sender, DragStartingEventArgs e)
        {
            var frame = (sender as Element).Parent as StackLayout;

            var row = Grid.GetRow(frame);
            var col = Grid.GetColumn(frame);

            var element = _matrix[row, col];
            e.Data.Properties.Add("MatrixElement", element);

            e.Data.Properties.Add("Layout", frame);
            var dropRecognizer = new DropGestureRecognizer();
            dropRecognizer.AllowDrop = true;
            dropRecognizer.Drop += (s, p) => Drop(s, p);

            (frame.Parent as Frame).GestureRecognizers.Add(dropRecognizer);

            Frame = frame.Parent as Frame;
        }

        public void Drop(Object sender, DropEventArgs e)
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

        void DropCompleted(Object sender, DropCompletedEventArgs e)
        {
            Frame.Content = new Frame
            {
                WidthRequest = 200,
                HeightRequest = 200,
                BackgroundColor = Color.Transparent
            };

            var col = Grid.GetColumn(Frame);
            var row = Grid.GetRow(Frame);

            _matrix[row, col] = new EmptyElement(col, row);

            //ConnectElements();
        }
    }
}
