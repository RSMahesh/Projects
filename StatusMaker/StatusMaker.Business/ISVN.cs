using System;

namespace StatusMaker.Business
{
    public interface ISVN
    {
        string ExcelFilePath { set; }
        bool GetLatestAndLock();

        bool GetLockForceFully();

        void GetLatestOfDay(DateTime dt, string checkOutDir);

        void CheckIn();

        bool DownLoadLatestProgressTrackingSheet(string userName, string password);

        bool IsValidUser(string userName, string password);
    }
}