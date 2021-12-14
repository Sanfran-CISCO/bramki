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
                        BorderColor = Color.Orange,
                        Content = new Label
                        {
                            TextColor = Color.White,
                            Text = $"{row}, {column}",
                            BackgroundColor = Color.Transparent
                        }


                        /*GestureRecognizers =
                        {
                            new DropGestureRecognizer
                            {
                                AllowDrop = true,
                                DropCommand = MainPageViewModel.DropItem(),
                                Dr
                                DropCommandParameter = this
                            }
                        }*/
                    };

                    BoardGrid.Children.Add(frame, column, row);
                }
            }

        }

        void DragGestureRecognizer_DragStarting(System.Object sender, Xamarin.Forms.DragStartingEventArgs e)
        {
            var boxview = (sender as Element).Parent as BoxView;
            e.Data.Properties.Add("BoxView", boxview);
            MyFrame = (sender as Element).Parent.Parent as Frame;
        }

        void DropGestureRecognizer_Drop(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {
            var box = e.Data.Properties["BoxView"] as BoxView;
            var frame = (sender as Element).Parent as Frame;
            frame.Content = box;
        }

        void DragGestureRecognizer_DropCompleted(System.Object sender, Xamarin.Forms.DropCompletedEventArgs e)
        {
            MyFrame.Content = new BoxView
            {
                WidthRequest=50,
                HeightRequest=50,
                BackgroundColor=Color.Transparent
            };
        }
    }
}