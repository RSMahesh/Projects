using System;
using System.Drawing;

namespace UserActivityLogger
{
    public interface ILogFolderReader: IDisposable
    {
        void ChangeNextImagePostion(int positionNumber);
        void Dispose();
        int GetFileCountForReading();
        byte[] GetNextImageBytes();
        void SetLogFolderPath(string logFolderPath);
    }
}