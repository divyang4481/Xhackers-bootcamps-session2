using GPSImageTag.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSImageTag.Core.Interfaces
{
    public interface IAzureService
    {
        Task Initialize();
        Task<IEnumerable<Photo>> GetPhotos();

        Task<Photo> AddPhoto(byte[] image, string name, string desc);

        Task SyncPhotos();
    }
}
