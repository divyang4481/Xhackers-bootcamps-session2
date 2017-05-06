using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Diagnostics;
using GPSImageTag.Core.Models;
using GPSImageTag.Core.Helpers;
using GPSImageTag.Core.Interfaces;

namespace GPSImageTag.Core.Services
{

    public class AzureService: IAzureService
    {
        public MobileServiceClient Client { get; set; } = null;
        IMobileServiceSyncTable<Photo> table;

        const string MobileServiceUrl = "https://gpstagimagemobileapp.azurewebsites.net";

        //Create our client
        public AzureService()
        {
            //Create our client
            Client = new MobileServiceClient(MobileServiceUrl);

        }

        public async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            //InitialzeDatabase for path
            var path = "syncstore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            
            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);
                                           
            //Define table
            store.DefineTable<Photo>();


             //Initialize SyncContext
             await Client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            table = Client.GetSyncTable<Photo>();

        }

        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            await Initialize();
            await SyncPhotos();
            return await table.OrderBy(s => s.Name).ToEnumerableAsync();
        }

        public async Task<Photo> AddPhoto(byte[] image, string name, string desc)
        {
            await Initialize();
            var photo = new Photo();
            var storageService = ServiceManager.GetObject<IAzureStorageService>();
            var imageUri= await storageService.UploadImage(new MemoryStream(image), name);
            if (string.IsNullOrEmpty(imageUri))
                return photo;
            photo = new Photo
            {
                Name = name,
                Description = desc,
                Uri = imageUri.ToString()
            };

            //create and insert photo
            await table.InsertAsync(photo);
         
            //Synchronize coffee
            await SyncPhotos();

            return photo;
        }


        public async Task SyncPhotos()
        {
            try
            {
                await Client.SyncContext.PushAsync();
                await table.PullAsync("allPhotos", table.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync Photos, that is alright as we have offline capabilities: " + ex);
            }

        }
    }
}