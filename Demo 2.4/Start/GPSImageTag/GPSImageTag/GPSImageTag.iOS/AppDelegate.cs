using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using GPSImageTag.Core.Helpers;
using GPSImageTag.Core.Interfaces;
using GPSImageTag.Core.Services;
using System.Threading.Tasks;

namespace GPSImageTag.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            RegisterServices();
            Task task = new Task(RegisterServices);
            task.Start();
            task.Wait();
            return base.FinishedLaunching(app, options);
        }

        private async void RegisterServices()
        {
            ServiceManager.Register<IDialogService>(new DialogService());
            ServiceManager.Register<IPhotoService>(new PhotoService());
            await ServiceManager.GetObject<IPhotoService>().InitCamera();
            ServiceManager.Register<IAzureStorageService>(new AzureStorageService());
            ServiceManager.Register<IAzureService>(new AzureService());
        }
    }
}
