using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bramkominatorMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bramkominatorMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public Frame MyFrame { get; set; }

        public MainPage()
        {
            InitializeComponent();

            for (int row=0; row<10; row++)
            {
                for (int column=0; column<10; column++)
                {
                    var frame = new Frame
                    {
                        BackgroundColor = Color.Gray,
                        BorderColor = Color.Orange
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

        }

        void DragStarting(System.Object sender, Xamarin.Forms.DragStartingEventArgs e)
        {
            var boxview = (sender as Element).Parent as BoxView;
            e.Data.Properties.Add("BoxView", boxview);
            MyFrame = (sender as Element).Parent.Parent as Frame;
        }

        void Drop(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {
            var box = e.Data.Properties["BoxView"] as BoxView;
            var frame = (sender as Element).Parent as Frame;
            frame.Content = box;
        }

        void DropCompleted(System.Object sender, Xamarin.Forms.DropCompletedEventArgs e)
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
                BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))
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