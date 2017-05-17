using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class AzureBlobFileSystem : IFileSystem
    {
        private string _storageConnectionString = string.Empty;
        private string _currentContainer;
        private string _curentFile;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StorageConnectionString"></param>
        public AzureBlobFileSystem(string StorageConnectionString)
        {
            _storageConnectionString = StorageConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="container"></param>
        public void CopyFile(string sourceFile, string destinationFile)
        {
            PraseIt(destinationFile);

            var blob = GetBlockBlobFromFileName();

            using (var sourceStream = new FileStream(
               sourceFile,
               FileMode.Open,
               FileAccess.Read,
               FileShare.ReadWrite))
            {
                blob.UploadFromStream(sourceStream);
            }
        }


        public void CreateDirectoryIfNotExist(string directoryPath)
        {
            //we need no to do anththing here
        }

        public void DeleteFileIfExist(string file)
        {
            PraseIt(file);
            var blob = GetBlockBlobFromFileName();
            blob.DeleteIfExists();
        }

        private CloudBlockBlob GetBlockBlobFromFileName()
        {
            return GetContainerReference().GetBlockBlobReference(_curentFile);
        }
        private CloudBlobContainer GetContainerReference()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(_currentContainer);
            blobContainer.CreateIfNotExists();
            return blobContainer;
        }

        private void PraseIt(string path)
        {
            path = path.Replace("\\", "/");
            _currentContainer = path.Split('/')[0];
            _curentFile = path.Substring(path.IndexOf('/') + 1, (path.Length - _currentContainer.Length) - 1);
        }
    }
}
