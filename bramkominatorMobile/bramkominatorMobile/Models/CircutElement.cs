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
                    Content = new Image
                    {
                        Source = "xnor.png",
                        WidthRequest = 100,
                        HeightRequest = 100,
                        GestureRecognizers = {
                            dragRecognizer
                        },
                    },
                    BackgroundColor = Color.Transparent,
                    Padding = 0
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
                var image = (sender as Element).Parent as Image;
                e.Data.Properties.Add("Gate", image);
                Frame = image.Parent as Frame;
            }

            void DropCompleted(Object sender, DropCompletedEventArgs e)
            {
                Frame.Content = new BoxView
                {
                    WidthRequest = 200,
                    HeightRequest = 200,
                    BackgroundColor = Color.Transparent
                };

                //ConnectElements();
            }
        }

        private static CircutElement[,] _matrix;
        private DragHandler _dragHandler;

        public string Name { get; set; }
        public Position Position { get; set; }

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
