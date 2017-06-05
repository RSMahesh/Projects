using System.IO;

namespace FileSystem
{
    public class NtfsFileSystem : IFileSystem
    {
        public void CreateDirectoryIfNotExist(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public void CopyFile(string sourceFile, string destinationFile)
        {

            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);
            }

            using (var sourceStream = new FileStream(
                 sourceFile,
                 FileMode.Open,
                 FileAccess.Read,
                 FileShare.ReadWrite))
            {
                using (var outputFile = new FileStream(destinationFile, FileMode.CreateNew))
                {
                    var buffer = new byte[0x10000];
                    int bytes;

                    while ((bytes = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        outputFile.Write(buffer, 0, bytes);
                    }
                }
            }
        }

       public void DeleteFileIfExist(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}
