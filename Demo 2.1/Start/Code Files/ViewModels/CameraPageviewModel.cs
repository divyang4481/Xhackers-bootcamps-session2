using GPSImageTag.Core.Helpers;
using GPSImageTag.Core.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using GPSImageTag.Core.Services;

namespace GPSImageTag.ViewModels
{
    public class CameraPageViewModel : BaseViewModel
    {

        private IDialogService dialogService;
        private PhotoService photoService;

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


        public CameraPageViewModel()
        {
            Title = "Upload Photo";
            TakePhotoCommand = new Command(
                    async () => await TakePhoto(),
                    () => !IsBusy);

            PickPhotoCommand = new Command(
                  async () => await PickPhoto(),
                   () => !IsBusy);

            photo = null;

            dialogService = ServiceManager.GetObject<IDialogService>();
            photoService = new PhotoService();
        }

        private async Task PickPhoto()
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

        private async Task TakePhoto()
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
