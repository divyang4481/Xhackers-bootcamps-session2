using GPSImageTag.Core.Interfaces;
using Plugin.Media;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GPSImageTag.Core.Services
{
    public class PhotoService: IPhotoService
    {
        private bool isPhotoAccessEnabled=false;
        public bool IsPhotoAccessEnabled
        {
            get
            {
                return isPhotoAccessEnabled;
            }
            set {

                isPhotoAccessEnabled = value;
            }

        }

        public PhotoService()
        {
   
        }

        public async Task InitCamera()
        {
            await CrossMedia.Current.Initialize();


            if (CrossMedia.Current.IsCameraAvailable || CrossMedia.Current.IsTakePhotoSupported)
            {
                isPhotoAccessEnabled = true;
            }

        }

        public async Task<byte[]> PickPhoto()
        {
            byte[] imageData = null;
            try
            {

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    isPhotoAccessEnabled = false;
                    return imageData;
                }
      

                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file != null)
                {


                    var stream = file.GetStream();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.Position = 0; // needed for WP (in iOS and Android it also works without it)!!
                        stream.CopyTo(ms);  // was empty without stream.Position = 0;
                        imageData = ms.ToArray();
                    }
                }
                file.Dispose();
            }
            catch (Exception ex)
            {
                isPhotoAccessEnabled = false;
            }

            return imageData;
        }

        public async Task<byte[]> TakePhoto()
        {
            byte[] imageData = null;

            try
            {
           

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    isPhotoAccessEnabled = false;
                    return imageData;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {

                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file != null)
                {
                    var stream = file.GetStream();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.Position = 0; // needed for WP (in iOS and Android it also works without it)!!
                        stream.CopyTo(ms);  // was empty without stream.Position = 0;
                        imageData = ms.ToArray();
                    }
                }
                file.Dispose();
            }
            catch (Exception ex)
            {
                isPhotoAccessEnabled = false;

            }

            return imageData;

        }
     
    }
}
