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
        /// Gets a reference to the container for storing the images
        /// </summary>
        /// <returns></returns>
        private CloudBlobContainer GetContainer()
        {
            // Parses the connection string for the WindowS Azure Storage Account
            var account = CloudStorageAccount.Parse(Configuration.StorageConnectionString);
            var client = account.CreateCloudBlobClient();

            // Gets a reference to the images container
            var container = client.GetContainerReference(Configuration.StorageContainerName);

            return container;
        }

        /// <summary>
        /// Uploads a new image to a blob container.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<string> UploadImage(MemoryStream image, string imageName)
        {
            var imageUri = string.Empty;

            try
            {
                var container = GetContainer();

                 // Uses a random name for the new images
                var name = imageName;

                // Uploads the image the blob storage
                var imageBlob = container.GetBlockBlobReference(name);

                //move the pointer to the start of the stream.  Why this is not default behaviour is beyond me!
                //http://stackoverflow.com/questions/27418053/uploading-to-azure-storage-from-memorystream-returning-an-empty-file
                image.Position = 0;

                await imageBlob.UploadFromStreamAsync(image);
                imageUri = imageBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to upload photo to Azure Storage: " + ex);
            }


            return imageUri;
        }

     }
}
