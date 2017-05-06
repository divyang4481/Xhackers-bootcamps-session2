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
        public const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=xtestgpstagimagestorage;AccountKey=YhMaw0awem2A0TL1nzgOrtMl5qEIdISRzuP0v67j4eONWVRPGEUhfRZXT3gaI0mg3PkIlCsHZUB9ecMftYuI5Q==;";
        public const string StorageContainerName = "photos";
    }
}
