using System.IO;
using System.Threading.Tasks;

namespace GPSImageTag.Core.Interfaces
{
    public interface IAzureStorageService
    {
        Task<string> UploadImage(MemoryStream image, string imageName);
    }
}
