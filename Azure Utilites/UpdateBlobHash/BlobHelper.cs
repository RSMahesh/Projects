using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace MediaProcessor.ServiceLibrary.Common
{
    /// <summary>
    ///     Helper class for blob operations
    /// </summary>
    public static class BlobHelper
    {
        private const int MaximumBlobSizeBeforeTransmittingAsBlocks = 4*1024*1024;

        /// <summary>
        ///     Gets the BLOB client.
        /// </summary>
        /// <param name="cloudStorageAccount">The cloud storage account.</param>
        /// <param name="useHttps">if set to <c>true</c> [use HTTPS].</param>
        /// <returns></returns>
        public static CloudBlobClient GetBlobClient(CloudStorageAccount cloudStorageAccount, bool useHttps)
        {
            var account = new CloudStorageAccount(cloudStorageAccount.Credentials, useHttps);

            CloudBlobClient blobClient = account.CreateCloudBlobClient();

            blobClient.ServerTimeout = new TimeSpan(4, 0, 0);

            blobClient.ParallelOperationThreadCount = 4;

            return blobClient;
        }

        /// <summary>
        ///     Gets the BLOB container.
        /// </summary>
        /// <param name="blobClient">The BLOB client.</param>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="setPublicAccess">if set to <c>true</c> [set public access].</param>
        /// <returns></returns>
        public static CloudBlobContainer GetBlobContainer(CloudBlobClient blobClient, string containerName,
            bool setPublicAccess = true)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();

            BlobContainerPermissions permissions = container.GetPermissions();
            //TODO : need to verify this logic for updating permissions

            //this is a rendition library.  Make it public.
            if (containerName.StartsWith("medialibrary-") && containerName.EndsWith("-r"))
            {
                permissions = new BlobContainerPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                container.SetPermissions(permissions);
            }
            else if (permissions.PublicAccess != BlobContainerPublicAccessType.Container && setPublicAccess)
            {
                permissions.PublicAccess = BlobContainerPublicAccessType.Container | BlobContainerPublicAccessType.Blob;
                container.SetPermissions(permissions);
            }
            return container;
        }

        /// <summary>
        ///     Puts the file to BLOB.
        /// </summary>
        /// <param name="inputFilePath">The input file path.</param>
        /// <param name="blobContainer">The BLOB container.</param>
        /// <param name="blobFilePath">The BLOB file path.</param>
        public static void PutFileToBlob(string inputFilePath, CloudBlobContainer blobContainer, string blobFilePath)
        {
            using (FileStream stream = File.OpenRead(inputFilePath))
            {
                PutFileToBlob(stream, blobContainer, blobFilePath);
            }
        }

        /// <summary>
        ///     Puts the file to BLOB.
        /// </summary>
        /// <param name="inputStream">The input stream.</param>
        /// <param name="blobContainer">The BLOB container.</param>
        /// <param name="blobFilePath">The BLOB file path.</param>
        public static void PutFileToBlob(Stream inputStream, CloudBlobContainer blobContainer, string blobFilePath)
        {
            CloudBlockBlob blockblob = blobContainer.GetBlockBlobReference(blobFilePath);

            inputStream.Seek(0, SeekOrigin.Begin);

            if (inputStream.Length <= MaximumBlobSizeBeforeTransmittingAsBlocks)
            {
                blockblob.UploadFromStream(inputStream);
            }
            else
            {
                ParallelUpload(blockblob, inputStream, inputStream.Length);
            }

            if (inputStream.Length > 256)
            {
                inputStream.Seek(0, SeekOrigin.Begin);
                var buffer = new byte[256];

                if (inputStream.Read(buffer, 0, 256) != 256)
                    return;

                string mimeType = GetMimeFromFile(Path.GetFileName(blobFilePath), new MemoryStream(buffer));
                SetContentType(blobFilePath, mimeType, blobContainer);
            }
        }

        /// <summary>
        ///     Gets the MIME from file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="ms">The ms.</param>
        /// <returns></returns>
        public static string GetMimeFromFile(string filename, Stream ms)
        {
            var buffer = new byte[256];

            if (ms.Length >= 256)
                ms.Read(buffer, 0, 256);
            else
                ms.Read(buffer, 0, (int) ms.Length);

            try
            {
                UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                var mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                return mime;
            }
            catch (Exception e)
            {
                return "unknown/unknown";
            }
        }

        public static IEnumerable<IListBlobItem> GetFilesInBlobFolder(
            string connectionString,
            string blobContainer,
            string userBlobFolder,
            string extension = "")
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(connectionString);

            CloudBlobClient blobClient = GetBlobClient(account, true);

            CloudBlobContainer container = GetBlobContainer(blobClient, blobContainer);

            CloudBlobDirectory dir = container.GetDirectoryReference(userBlobFolder);

            IEnumerable<IListBlobItem> files = dir.ListBlobs();

            return extension == string.Empty ? files : files.Where(f => f.Uri.AbsoluteUri.EndsWith(extension));
        }


        public static void DeleteFiles(IEnumerable<Uri> filesUri, string connectionString, string blobContainer)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(connectionString);

            CloudBlobClient blobClient = GetBlobClient(account, true);

            CloudBlobContainer container = GetBlobContainer(blobClient, blobContainer);

            foreach (Uri uri in filesUri)
            {
                CloudBlockBlob block = container.GetBlockBlobReference(uri.AbsoluteUri);

                if (block != null && block.Exists())
                    block.Delete();
                else
                    throw new Exception(
                        string.Format("The container reference {0} could not be retrieved from storage provider.", uri));
            }
        }


        /// <summary>
        ///     Parallels the upload.
        /// </summary>
        /// <param name="blobRef">The BLOB reference.</param>
        /// <param name="inputStream">The input stream.</param>
        /// <param name="contentLength">Length of the content.</param>
        /// <exception cref="System.ArgumentException">
        ///     Blob Reference must have a valid service client associated with it
        ///     or
        ///     Cannot upload empty stream.
        /// </exception>
        /// <exception cref="System.TimeoutException"></exception>
        private static void ParallelUpload(CloudBlockBlob blobRef, Stream inputStream, long contentLength)
        {
            // Parameter Validation & Locals
            if (null == blobRef.ServiceClient)
            {
                throw new ArgumentException("Blob Reference must have a valid service client associated with it");
            }

            if (inputStream.Length - inputStream.Position == 0)
            {
                throw new ArgumentException("Cannot upload empty stream.");
            }

            IRetryPolicy retry = new ExponentialRetry(new TimeSpan(0, 0, 30), 3);

            var options = new BlobRequestOptions
            {
                ServerTimeout = blobRef.ServiceClient.ServerTimeout,
                RetryPolicy = retry
            };

            bool moreToUpload = true;
            var asyncResults = new List<IAsyncResult>();
            var blockList = new List<string>();

            long blockIdSequenceNumber = GetBlockIdSequenceNumber();

            using (MD5 fullBlobMd5 = MD5.Create())
            {
                do
                {
                    int currentPendingTasks = asyncResults.Count;

                    for (int i = currentPendingTasks;
                        i < blobRef.ServiceClient.ParallelOperationThreadCount && moreToUpload;
                        i++)
                    {
                        // Step 1: Create block streams in a serial order as stream can only be read sequentially
                        string blockId = null;

                        // Dispense Block Stream
                        int blockSize = blobRef.StreamWriteSizeInBytes;
                        int totalCopied = 0, numRead = 0;
                        MemoryStream blockAsStream = null;
                        blockIdSequenceNumber++;

                        var blockBufferSize = (int) Math.Min(blockSize, inputStream.Length - inputStream.Position);
                        var buffer = new byte[blockBufferSize];
                        blockAsStream = new MemoryStream(buffer);

                        do
                        {
                            numRead = inputStream.Read(buffer, totalCopied, blockBufferSize - totalCopied);
                            totalCopied += numRead;
                        } while (numRead != 0 && totalCopied < blockBufferSize);


                        // Update Running MD5 Hashes
                        fullBlobMd5.TransformBlock(buffer, 0, totalCopied, null, 0);
                        blockId = GenerateBase64BlockId(blockIdSequenceNumber);

                        // Step 2: Fire off consumer tasks that may finish on other threads
                        blockList.Add(blockId);
                        IAsyncResult asyncresult = blobRef.PutBlockAsync(blockId, blockAsStream, null);
                        asyncResults.Add(asyncresult);

                        if (contentLength == inputStream.Position)
                        {
                            // No more upload tasks
                            moreToUpload = false;
                        }
                    }

                    // Step 3: Wait for 1 or more put blocks to finish and finish operations
                    if (asyncResults.Count > 0)
                    {
                        int waitTimeout = options.ServerTimeout.HasValue
                            ? (int) Math.Ceiling(options.ServerTimeout.Value.TotalMilliseconds)
                            : Timeout.Infinite;
                        int waitResult =
                            WaitHandle.WaitAny(asyncResults.Select(result => result.AsyncWaitHandle).ToArray(),
                                waitTimeout);

                        if (waitResult == WaitHandle.WaitTimeout)
                        {
                            throw new TimeoutException(String.Format("ParallelUpload Failed with timeout = {0}",
                                options.ServerTimeout.Value));
                        }

                        // Optimize away any other completed operations
                        for (int index = 0; index < asyncResults.Count; index++)
                        {
                            IAsyncResult result = asyncResults[index];
                            if (result.IsCompleted)
                            {
                                // Dispose of memory stream
                                if (result.AsyncState != null)
                                    (result.AsyncState as IDisposable).Dispose();
                                asyncResults.RemoveAt(index);
                                index--;
                            }
                        }
                    }
                } while (moreToUpload || asyncResults.Count != 0);

                // Step 4: Calculate MD5 and do a PutBlockList to commit the blob
                fullBlobMd5.TransformFinalBlock(new byte[0], 0, 0);

                byte[] blobHashBytes = fullBlobMd5.Hash;

                string blobHash = Convert.ToBase64String(blobHashBytes);

                blobRef.Properties.ContentMD5 = blobHash;

                blobRef.PutBlockList(blockList);
            }
        }

        private static string GenerateBase64BlockId(long seqNo)
        {
            // 9 bytes needed since base64 encoding requires 6 bits per character (6*12 = 8*9)
            var tempArray = new byte[9];

            for (int m = 0; m < 9; m++)
            {
                tempArray[8 - m] = (byte) ((seqNo >> (8*m)) & 0xFF);
            }

            return Convert.ToBase64String(tempArray);
        }

        private static long GetBlockIdSequenceNumber()
        {
            var rand = new Random();

            long blockIdSequenceNumber = (long) rand.Next() << 32;

            blockIdSequenceNumber += rand.Next();

            return blockIdSequenceNumber;
        }

        private static void SetContentType(string path, string mimeType, CloudBlobContainer container)
        {
            // Create a reference for the filename
            string uniqueName = container.Uri + "/" + path;
            CloudBlockBlob blob = container.GetBlockBlobReference(uniqueName);
            blob.Properties.ContentType = mimeType;
            blob.SetProperties();
        }

        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private static extern UInt32 FindMimeFromData(
            UInt32 pBC,
            [MarshalAs(UnmanagedType.LPStr)] String pwzUrl,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
            UInt32 cbSize,
            [MarshalAs(UnmanagedType.LPStr)] String pwzMimeProposed,
            UInt32 dwMimeFlags,
            out UInt32 ppwzMimeOut,
            UInt32 dwReserverd
            );
    }
}