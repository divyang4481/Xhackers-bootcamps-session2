using System;
using System.Collections.Generic;
using System.Text;

namespace GPSImageTag
{
    public static class Configuration
    {
        /// <summary>
        /// Azure Storage Connection String. UseDevelopmentStorage=true points to the storage emulator.
        /// </summary>
        public const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=gpstagimagestorage;AccountKey=y8zzh5OH5l3mAQoP/MVL/3ZSsJT/YomNvO/4zur2LSACCEtyMLCbAw3WEH65ctqjrkk23/jgPh0/5fGSPizbbA==";
        public const string StorageContainerName = "photos";
    }
}
