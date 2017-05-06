using GPSImageTag.Core.Helpers;
using GPSImageTag.Core.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using GPSImageTag.Core.Services;
using System.Diagnostics;

namespace GPSImageTag.ViewModels
{
    public class CameraPageViewModel : BaseViewModel
    {

        private IDialogService dialogService;
        private IPhotoService photoService;
        private IAzureService mobileService;

        private byte[] photo;
        public byte[] Photo
        {
            get
            {
                return photo;
            }

        }

        private string imageName;

        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value; OnPropertyChanged("ImageName");
            }
        }

        private string imageDesc;

        public string ImageDesc
        {
            get { return imageDesc; }
            set
            {
                imageDesc = value; OnPropertyChanged("imageDesc");
            }
        }

        public Command TakePhotoCommand { get; set; }
        public Command PickPhotoCommand { get; set; }
        public Command UploadPhotoCommand { get; set; }


        public CameraPageViewModel()
        {
            Title = "Upload Photo";
            TakePhotoCommand = new Command(
                    async () => await TakePhoto(),
                    () => !IsBusy);

            PickPhotoCommand = new Command(
                  async () => await PickPhoto(),
                   () => !IsBusy);

            UploadPhotoCommand = new Command(
               async () => await UploadPhoto(),
                () => !IsBusy);

            photo = null;

            dialogService = ServiceManager.GetObject<IDialogService>();
            photoService = ServiceManager.GetObject<IPhotoService>();
            mobileService = ServiceManager.GetObject<IAzureService>();
        }

        public async Task UploadPhoto()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {

                IsBusy = true;

                var name = imageName;
                var desc = imageDesc;

                if (string.IsNullOrEmpty(name))
                {
                    dialogService.ShowError("Enter a photo name before uploading photo");
                    IsBusy = false;
                    return;
                }
                await mobileService.AddPhoto(photo, name, desc);
                dialogService.ShowSuccess("File has been successfully uploaded!");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task PickPhoto()
        {
            try
            {
                if (photoService.IsPhotoAccessEnabled)
                {
                    photo = await photoService.PickPhoto();
                    OnPropertyChanged("Photo");
                }
                else
                {
                    dialogService.ShowError("Permission not granted to photos.");
                }

            }
            catch (Exception ex)
            {
                dialogService.ShowError("Unable to pick a photo: " + ex);
            }

            return;
        }

        public async Task TakePhoto()
        {
            try
            {
                if (photoService.IsPhotoAccessEnabled)
                {
                    photo = await photoService.TakePhoto();
                    OnPropertyChanged("Photo");
                }
                else
                {
                    dialogService.ShowError("No camera avaialble.");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowError("Unable to camera capabilities: " + ex);
            }

            return;
        }
    }
}
