using GPSImageTag.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GPSImageTag
{
    public partial class PhotosPage : ContentPage
    {
        PhotosPageViewModel vm;
        public PhotosPage()
        {
            InitializeComponent();

            vm = new PhotosPageViewModel();

            BindingContext = vm;
        }
    }
}
