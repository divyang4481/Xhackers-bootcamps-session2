using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GPSImageTag.Core.Helpers;
using GPSImageTag.Core.Interfaces;
using GPSImageTag.Core.Services;
using Acr.UserDialogs;
using GPSImageTag.DroidNative.Fragments;

namespace GPSImageTag.DroidNative
{
    [Activity(Label = "GPSImageTag.DroidNative", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            RegisterServices();
            UserDialogs.Init(this);
            await ServiceManager.GetObject<IPhotoService>().InitCamera();
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab("Photo List", new PhotosListFragment());
            AddTab("Upload Photo", new CameraFragment());
        }

        private void AddTab(string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
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