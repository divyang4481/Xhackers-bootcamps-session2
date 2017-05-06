using GPSImageTag.Core.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;


namespace GPSImageTag
{
    public class AzureStorageService: IAzureStorageService
    {
        /// <summary>
        /// Uploads a new image to a blob container.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<string> UploadImage(MemoryStream image, string sasToken)
        {
            var imageUri = string.Empty;

            try
            {
                var storageUri = new Uri(sasToken);

                var imageBlob = new CloudBlockBlob(storageUri);
                                              
                image.Position = 0;

                await imageBlob.UploadFromStreamAsync(image);
                imageUri = imageBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred breakage: {ex.Message}");

                throw ex;
            }

            return imageUri;
        }

     }
}
