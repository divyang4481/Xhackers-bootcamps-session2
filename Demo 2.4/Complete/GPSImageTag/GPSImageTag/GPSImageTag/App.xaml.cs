using GPSImageTag.Core.Helpers;
using GPSImageTag.Core.Interfaces;

using Xamarin.Forms;

namespace GPSImageTag
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new GPSImageTag.CameraPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
