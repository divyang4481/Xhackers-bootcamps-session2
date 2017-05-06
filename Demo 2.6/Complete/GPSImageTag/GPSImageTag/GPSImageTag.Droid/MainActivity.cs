using Android.App;
using Android.Content.PM;
using Android.OS;
using GPSImageTag.Core.Helpers;
using GPSImageTag.Core.Interfaces;
using GPSImageTag.Core.Services;
using Acr.UserDialogs;

namespace GPSImageTag.Droid
{
    [Activity(Label = "GPSImageTag", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            UserDialogs.Init(this);
            RegisterServices();
            await ServiceManager.GetObject<IPhotoService>().InitCamera();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        private void RegisterServices()
        {
            ServiceManager.Register<IDialogService>(new DialogService());
            ServiceManager.Register<IPhotoService>(new PhotoService());
            ServiceManager.Register<IAzureStorageService>(new AzureStorageService());
            ServiceManager.Register<IAzureService>(new AzureService());
        }
    }
}

