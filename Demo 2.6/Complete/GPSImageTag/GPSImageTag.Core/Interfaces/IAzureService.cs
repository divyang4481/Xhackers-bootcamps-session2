using GPSImageTag.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPSImageTag.Core.Interfaces
{
    public interface IAzureService
    {
        Task Initialize();
        Task<IEnumerable<Photo>> GetPhotos();

        Task<Photo> AddPhoto(byte[] image, string name, string desc);

        Task SyncPhotos();

        Task<string> GetAzureStorageSasTokenAsync();

    }
}
