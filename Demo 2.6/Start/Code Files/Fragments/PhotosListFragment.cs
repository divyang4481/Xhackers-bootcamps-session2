using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using GPSImageTag.ViewModels;
using GPSImageTag.Core.Models;
using GPSImageTag.DroidNative.Adapters;

namespace GPSImageTag.DroidNative.Fragments
{
    public class PhotosListFragment : Fragment
    {

        private Button syncPhotosButton;
        ProgressBar progressBar;
        ListView listView;


        public PhotosPageViewModel ViewModel { get; set; }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
       }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            ViewModel = new PhotosPageViewModel();
            FindViews();
            HandleEvents();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.PhotosListFragment, container, false);

        }

        private void FindViews()
        {
            syncPhotosButton = this.View.FindViewById<Button>(Resource.Id.syncphotosbutton);
            progressBar = this.View.FindViewById<ProgressBar>(Resource.Id.progressBar);
            listView = this.View.FindViewById<ListView>(Resource.Id.listView);
            progressBar.Visibility = ViewStates.Gone;
        }

        public async void syncPhotosButton_OnClick(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            await ViewModel.GetPhotos();
            var myList = new List<Photo>(ViewModel.Photos);
            listView.Adapter = new CusotmListAdapter(this.Activity, myList);
            progressBar.Visibility = ViewStates.Gone;
        }

        private void HandleEvents()
        {
            syncPhotosButton.Click += syncPhotosButton_OnClick;
        }
    }
}