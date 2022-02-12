using System;
using System.Diagnostics;
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
                Frame = GenerateFrame(GetRecognizer());
            }

            private Frame GenerateFrame(DragGestureRecognizer dragRecognizer)
            {
                var frame = new Frame
                {
                    Content = new StackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = "Default"
                            }
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

                return frame;
            }

            private DragGestureRecognizer GetRecognizer()
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
