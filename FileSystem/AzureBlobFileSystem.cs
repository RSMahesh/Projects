using System.IO;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FileSystem
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
            this._storageConnectionString = StorageConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="container"></param>
        public void CopyFile(string sourceFile, string destinationFile)
        {
            this.PraseIt(destinationFile);

            var blob = this.GetBlockBlobFromFileName();

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
            this.PraseIt(file);
            var blob = this.GetBlockBlobFromFileName();
            blob.DeleteIfExists();
        }

        private CloudBlockBlob GetBlockBlobFromFileName()
        {
            return this.GetContainerReference().GetBlockBlobReference(this._curentFile);
        }
        private CloudBlobContainer GetContainerReference()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(this._storageConnectionString);
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(this._currentContainer);
            blobContainer.CreateIfNotExists();
            return blobContainer;
        }

        private void PraseIt(string path)
        {
            path = path.Replace("\\", "/");
            this._currentContainer = path.Split('/')[0];
            this._curentFile = path.Substring(path.IndexOf('/') + 1, (path.Length - this._currentContainer.Length) - 1);
        }
    }
}
