using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSImageTag.Core.Interfaces
{
    public interface IPhotoService
    {
        bool IsPhotoAccessEnabled
        {
            get;
            set;
        }
        Task<byte[]> PickPhoto();
        Task<byte[]> TakePhoto();
    }
}
