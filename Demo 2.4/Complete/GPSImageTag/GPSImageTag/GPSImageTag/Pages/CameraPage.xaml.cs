using GPSImageTag.ViewModels;
using Xamarin.Forms;

namespace GPSImageTag
{
    public partial class CameraPage : ContentPage
    {
        CameraPageViewModel vm;

        public CameraPage()
        {

            InitializeComponent();

            vm = new CameraPageViewModel();

            BindingContext = vm;


        }
    }
}
