using System.IO;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FileSystem
{

//    PUT https://api.github.com/repos/MaheshBailwal/StatusMakerLastestApp/contents/index3.html HTTP/1.1
//Host: api.github.com
//User-Agent: Fiddler
//Authorization: basic bWFoZXNoYmFpbHdhbDptYkAyNDgwMDE =
//Content - Length: 85

//{
//	"path": "index.html",
//	"message": "test",
//	"content": "dGVzdGluZyBodG1sIHYy"
//}
public class GitFileSystem : IFileSystem
    {
        private string _storageConnectionString = string.Empty;
        private string _currentContainer;
        private string _curentFile;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StorageConnectionString"></param>
        public GitFileSystem(string StorageConnectionString)
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
