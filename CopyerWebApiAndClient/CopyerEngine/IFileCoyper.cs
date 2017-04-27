using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploaderClient
{
    public interface IFileCoyper
    {
        void UploadFile(UploadFileInformation fileInfo, IUploadProgressCommunicator progessCommunicator);
    }
  
    public class UploadFileInformation
    {
        public string SourceFilePath { get; set; }
        public string TargetFileName { get; set; }
        public long StartingIndex { get; set; }
        public int ChunkSizeInBytes { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public interface IUploadProgressCommunicator
    {
        void FileUploadSoFar(long bytes);
        void UploadCompleted();
        bool Pause { get; set; }
    }
}
