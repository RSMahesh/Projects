using System;
using System.IO;
using System.Linq;

using Microsoft.WindowsAzure.Storage;

namespace DeleteQueues
{
    class Program
    {
        static void Main(string[] args)
        {
            var logFile = "Log.txt";
           
            var account =
                CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=mediavaletdevasia;AccountKey=wtjIEIXRmtA6tHUW5zkhYwc1cCYhlFhsW8z2Cf3TUpKacrrnYWBaLUUrQDacfH3kQ3XhftEhVt3f2ONZQhCMog==");
           
            var queueClient = account.CreateCloudQueueClient();
         
            var quesues = queueClient.ListQueues().Where(q => q.Name.EndsWith("mahesh"));
            
            File.AppendAllText(logFile,
                "===================Starting " + DateTime.Now + "==========================================" + Environment.NewLine);
            
            foreach (var queue in quesues)
            {
                queue.Delete();
                File.AppendAllText(logFile, DateTime.Now + " Deleted Queue:" + queue.Name + Environment.NewLine);
            }
        }
    }
}
