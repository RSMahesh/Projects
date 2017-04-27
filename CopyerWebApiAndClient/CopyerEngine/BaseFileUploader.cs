using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploaderClient.UploadMachine
{
    public abstract class BaseFileCopyer
    {
        protected UploadFileInformation _fileInfo;
        public void UploadFile(UploadFileInformation fileInfo, IUploadProgressCommunicator progessCommunicator)
        {
            _fileInfo = fileInfo;

            Init();

            var uploadFileName = Path.GetFileName(_fileInfo.SourceFilePath);

            try
            {
                using (FileStream stream = new FileStream(_fileInfo.SourceFilePath, FileMode.Open))
                {
                    byte[] buffer = new byte[_fileInfo.ChunkSizeInBytes];

                    int count;

                    stream.Seek(fileInfo.StartingIndex, SeekOrigin.Begin);

                    while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        CopyBytes(buffer, count);

                        progessCommunicator.FileUploadSoFar(stream.Position);

                        if (progessCommunicator.Pause)
                        {
                            return;
                        }
                    }
                }

                progessCommunicator.UploadCompleted();
            }
            finally
            {
                CleanUp();
            }
        }

        public abstract void Init();
        public abstract void CopyBytes(byte[] data, int length);
        public abstract void CleanUp();
    }
}
