using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace bramkominatorMobile.Models
{
    public class CircutElement
    {
        class FrameManager
        {
            private CircutElement[,] _matrix;
            public Frame Frame { get; set; }

            public FrameManager(ref CircutElement[,] matrix)
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

                //Debug.WriteLine("<--------------->");
                //Debug.WriteLine($"\tDRAG-START --> Frame Content: {Frame.Content}");
                //Debug.WriteLine($"\tDRAG-START --> Sender Parent: {(sender as Element).Parent}");
                //Debug.WriteLine($"\tDRAG-START --> Sender Grandparent: {image.Parent}");
                //Debug.WriteLine("<--------------->");
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

                //Debug.WriteLine("<--------------->");
                //Debug.WriteLine($"\tDROP-COMP --> Frame Content: {Frame.Content}");
                //Debug.WriteLine($"\tDROP-COMP --> Frame Content: {Frame.Content.Width}, {Frame.Content.Height}");
                //Debug.WriteLine("<--------------->");
            }
        }

        private static CircutElement[,] _matrix;
        private FrameManager _frameManager;

        public Position Position { get; set; }

        protected CircutElement()
        {
            _frameManager = new FrameManager(ref _matrix);
        }

        public Frame GetFrame()
        {
            return _frameManager.Frame;
        }

        public virtual Position GetPosition()
        {
            return Position;
        }

        public void SetPosition(int col, int row)
        {
            Position.Set(col, row);
        }

        public static void InitFrameManager(ref CircutElement[,] matrix)
        {
            _matrix = matrix;
        }
    }
}
