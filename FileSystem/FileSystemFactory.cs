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
        private readonly string _storageConnectionString;
        public FileSystemFactory(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }
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
                    return new AzureBlobFileSystem(_storageConnectionString);
                    break;
            }

            return new NtfsFileSystem();
        }
    }
}
