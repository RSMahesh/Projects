using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediaProcessor.ServiceLibrary.Common;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace UpdateBlobHash
{
    class Program
    {
        static void Main(string[] args)
        {

            var StorageConnectionString =
                "DefaultEndpointsProtocol=https;AccountName=mediavaletdevasia;AccountKey=wtjIEIXRmtA6tHUW5zkhYwc1cCYhlFhsW8z2Cf3TUpKacrrnYWBaLUUrQDacfH3kQ3XhftEhVt3f2ONZQhCMog==";


            var eventIds = new[] { "1aa5e37f-652f-4be2-be17-9dd805d90152",
"7b103c54-07d1-4f99-b803-95711f9bcfb1",
"ae7f3e28-75f3-44e6-89c9-c4aedc783869", 
"ae7f3e28-75f3-44e6-89c9-c4aedc783869",  
"c71ecd03-c2e0-4f8e-accf-282e500ddef8",  
"47dddb99-adf2-4a16-a2b2-4cfee3d11aea",  
"9ce78d54-e0fa-4e91-8870-72f3d3745fe4",  
"ad003c6d-807d-44b1-b5f0-26a15d622835",  
"0f6b9881-3a96-4ac6-99d1-576f3267421b",  
"4b2b105b-caf5-44f1-b622-582d6ab6bc4c",  
"63119fda-e335-41fd-acba-8820b5b75d74",  
"39798e0d-f23d-47c1-8090-dad0b03a1a28" };

            var fileName = @"C:\Temp\Logs" + Guid.NewGuid().ToString() + ".txt";

            foreach (var eventid in eventIds)
            {
                var _containerName = "indexer-transaction-logs";
                var remoteSourceFilePath = eventid + "\\txnLog.txt";
                CloudBlobClient blobClient = BlobHelper.GetBlobClient(CloudStorageAccount.Parse(StorageConnectionString), true);
                CloudBlobContainer container = BlobHelper.GetBlobContainer(blobClient, _containerName);
                CloudBlockBlob blob = container.GetBlockBlobReference(remoteSourceFilePath);

                var ss = blob.OpenRead();

                using (StreamReader sr = new StreamReader(ss))
                {
                    var log = sr.ReadToEnd();
                    File.AppendAllText(fileName, "===================================================" + Environment.NewLine + Environment.NewLine+ log);
                    File.AppendAllText( fileName, log);
                }
            }




        }
    }
}
