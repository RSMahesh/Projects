using Core;
using EventPublisher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserActivityLogger;

namespace UserActivityLogger
{
    public class FileAppender : IFileAppender
    {
       private int fileCount;

        public void AppendFile(string fileToAppend, string dataFile)
        {
           var  fileCount = GetFileCount(dataFile);

            using (BinaryWriter writer = new BinaryWriter(File.Open(dataFile, FileMode.OpenOrCreate)))
            {
                fileCount++;
                writer.Seek(0, SeekOrigin.Begin);
                writer.Write(Encoding.ASCII.GetBytes(fileCount.ToString().PadLeft(10)));
                writer.Seek(0, SeekOrigin.End);
                var fileBytes = File.ReadAllBytes(fileToAppend);
                writer.Write(Encoding.ASCII.GetBytes(fileBytes.Length.ToString().PadLeft(10)));
                writer.Write(fileBytes);
            }
        }

        public int GetFileCount(string dataFile)
        {
            if (File.Exists(dataFile))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(dataFile, FileMode.Open, System.IO.FileAccess.Read)))
                {
                    var bytes = reader.ReadBytes(10);
                    var result = System.Text.Encoding.UTF8.GetString(bytes);
                    return int.Parse(result.Trim());
                }
            }

            return 0;
        }
    }
}
