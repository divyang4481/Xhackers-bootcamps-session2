using GPSImageTag.Helpers;
using System;
using Xamarin.Forms;

namespace GPSImageTag
{
    public partial class StartPage : TabbedPage
    {
        public StartPage()
        {
            InitializeComponent();

            Title = "Welcome to GPSImageTag";
            Children.Add(new PhotosPage());
            Children.Add(new CameraPage());

            BarBackgroundColor = Colours.TabBackgroundColor;

        }
    }
}
