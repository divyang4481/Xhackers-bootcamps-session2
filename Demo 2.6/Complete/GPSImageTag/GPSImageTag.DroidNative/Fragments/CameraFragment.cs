using System;
using System.Linq;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GPSImageTag.ViewModels;
using Android.Graphics;
using System.Threading.Tasks;

namespace GPSImageTag.DroidNative.Fragments
{
    public class CameraFragment : Fragment
    {
        private Button takePhotoButton;
        private Button pickPhotoButton;
        private Button uploadPhotoButton;
        private ImageView pictureImageView;
        private EditText photoName;
        private EditText photoDesc;
        private ProgressBar progressBar;

        public CameraPageViewModel ViewModel { get; set; }
        public static string documents { get; private set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            ViewModel = new CameraPageViewModel();
            FindViews();
            HandleEvents();
        }

        private void HandleEvents()
        {
            takePhotoButton.Click += takePhotoButton_OnClick;
            pickPhotoButton.Click += pickPhotoButton_OnClick;
            uploadPhotoButton.Click += uploadPhotoButton_OnClick;
        }

        private async void uploadPhotoButton_OnClick(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            uploadPhotoButton.Enabled = false;
            ViewModel.ImageName = photoName.Text;
            ViewModel.ImageDesc = photoDesc.Text;
            await ViewModel.UploadPhoto();
            uploadPhotoButton.Enabled = true;
            progressBar.Visibility = ViewStates.Gone;
        }

        private async void takePhotoButton_OnClick(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            takePhotoButton.Enabled = false;
            pickPhotoButton.Enabled = false;

            try
            {
                await ViewModel.TakePhoto();
                if (ViewModel.Photo != null)
                {
                    Activity.RunOnUiThread(async () => pictureImageView.SetImageBitmap(await bytesToUIImage(ViewModel.Photo)));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
            }

            progressBar.Visibility = ViewStates.Gone;
            takePhotoButton.Enabled = true;
            pickPhotoButton.Enabled = true;

        }

        private async void pickPhotoButton_OnClick(object sender, EventArgs e)
        {

            progressBar.Visibility = ViewStates.Visible;
            pickPhotoButton.Enabled = false;
            takePhotoButton.Enabled = false;


            try
            {
                await ViewModel.PickPhoto();
                if (ViewModel.Photo != null)
                {
                    this.Activity.RunOnUiThread(async () => pictureImageView.SetImageBitmap(await bytesToUIImage(ViewModel.Photo)));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
            }

            progressBar.Visibility = ViewStates.Gone;
            pickPhotoButton.Enabled = true;
            takePhotoButton.Enabled = true;
        }

        private void FindViews()
        {
            takePhotoButton = this.View.FindViewById<Button>(Resource.Id.takephotobutton);
            pickPhotoButton = this.View.FindViewById<Button>(Resource.Id.pickphotobutton);
            uploadPhotoButton = this.View.FindViewById<Button>(Resource.Id.uploadphotobutton);
            progressBar = this.View.FindViewById<ProgressBar>(Resource.Id.progressBar);
            pictureImageView = this.View.FindViewById<ImageView>(Resource.Id.pictureImageView);
            photoName = this.View.FindViewById<EditText>(Resource.Id.photoname);
            photoDesc = this.View.FindViewById<EditText>(Resource.Id.photodesc);
            progressBar.Visibility = ViewStates.Gone;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.CameraFragment, container, false);
        }

        /// Loads a Bitmap from a byte array
        public async Task<Bitmap> bytesToUIImage(byte[] bytes)
        {
            if (bytes == null)
                return null;

            Bitmap bitmap;

            bitmap = await BitmapFactory.DecodeByteArrayAsync(bytes, 0, bytes.Length);

            return bitmap;
        }
    }
}