using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
  public partial class  JarFile : IJarFileWriter
    {
        private class Writer
        {
            private readonly string _logFile;
            public Writer(string logFile)
            {
                _logFile = logFile;

                var rootDir = Path.GetDirectoryName(logFile);

                if (!Directory.Exists(rootDir))
                {
                    Directory.CreateDirectory(rootDir);
                }

            }
            public void AddFile(string fileToAppend)
            {
                var fileCount = GetFileCount();

                using (BinaryWriter writer = new BinaryWriter(File.Open(_logFile, FileMode.OpenOrCreate)))
                {
                    fileCount++;
                    writer.Seek(0, SeekOrigin.Begin);
                    writer.Write(Encoding.ASCII.GetBytes(fileCount.ToString().PadLeft(FileCountFieldSize)));
                    writer.Seek(0, SeekOrigin.End);

                    var fileBytes = File.ReadAllBytes(fileToAppend);
                    writer.Write(Encoding.ASCII.GetBytes(fileBytes.Length.ToString().PadLeft(FileLengthFieldSize)));
                    writer.Write(fileBytes);
                }
            }

            public void AddFile(JarFileItem jarFileItem)
            {
                var fileCount = GetFileCount();

                using (BinaryWriter writer = new BinaryWriter(File.Open(_logFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)))
                {
                    fileCount++;
                    writer.Seek(0, SeekOrigin.Begin);
                    writer.Write(Encoding.ASCII.GetBytes(fileCount.ToString().PadLeft(FileCountFieldSize)));
                    writer.Seek(0, SeekOrigin.End);

                    var headerString = DicToString(jarFileItem.Headers);

                    var headerBytes = Encoding.ASCII.GetBytes(headerString.PadLeft(HeaderFieldSize));
                    if (headerBytes.Count() > HeaderFieldSize)
                    {
                        throw new Exception("Header out of limit");
                    }

                    writer.Write(headerBytes);
                    writer.Seek(0, SeekOrigin.End);

                    //var fileBytes = File.ReadAllBytes(jarFileItem.FilePath);
                    var fileBytes = jarFileItem.Containt;
                    writer.Write(Encoding.ASCII.GetBytes(fileBytes.Length.ToString().PadLeft(FileLengthFieldSize)));
                    writer.Write(fileBytes);
                }
            }


            private string DicToString(Dictionary<string, string> dic)
            {
                return string.Join(";", dic.Select(x => x.Key + "=" + x.Value).ToArray());
            }
            public int GetFileCount()
            {
                if (File.Exists(_logFile))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(_logFile, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite)))
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
}
