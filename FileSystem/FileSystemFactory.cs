using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    public class FileSystemFactory : IFileSystemFactory
    {
        public IFileSystem GetFileSystem(string fileSystemType)
        {
            if (string.IsNullOrEmpty(fileSystemType))
            {
                return new NtfsFileSystem();
            }

            switch (fileSystemType.ToUpperInvariant())
            {
                case "NTFS":
                    return new NtfsFileSystem();
                    break;

                case "AZUREBLOB":
                    return new AzureBlobFileSystem(ConfigurationManager.AppSettings["StorageConnectionString"]);
                    break;
            }

            return new NtfsFileSystem();
        }
    }
}
