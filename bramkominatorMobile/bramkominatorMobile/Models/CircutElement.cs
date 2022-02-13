using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bramkominatorMobile.Models
{
    public class CircutElement
    {
        class DragHandler
        {
            private CircutElement[,] _matrix;
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

            async void Tap(Object sender, TappedEventArgs e)
            {
                
            }
        }

        private static CircutElement[,] _matrix;
        private DragHandler _dragHandler;

        public string Name { get; set; }
        public Position Position { get; set; }
        public string Image { get; set; }

        public virtual bool Output { get; set; }
        public virtual bool InputA { get; set; }
        public virtual bool InputB { get; set; }
        public virtual Node Node { get; set; }

        protected CircutElement()
        {
            _dragHandler = new DragHandler(ref _matrix);

            Node = new Node(this);
        }

        public Frame GetFrame()
        {
            ((_dragHandler.Frame.Content as StackLayout).Children[0] as Label).Text = Name;

            // TODO If called by LogicGateway --> Set BackgroundColor to gate's color
            //(_dragHandler.Frame.Content as Frame).BackgroundColor = Color;

            return _dragHandler.Frame;
        }

        public virtual Position GetPosition()
        {
            return Position;
        }

        public void SetPosition(int col, int row)
        {
            Position.Set(col, row);
        }

        public static void InitDragHandler(ref CircutElement[,] matrix)
        {
            _matrix = matrix;
        }
    }
}
