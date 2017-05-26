using System;
using System.Drawing;

namespace RecordSession
{
    public interface ILogFolderReader: IDisposable
    {
        void ChangeNextImagePostion(int positionNumber);
        void Dispose();
        int GetFileCountForReading();
        byte[] GetNextImage();
        void SetLogFolderPath(string logFolderPath);
    }
}