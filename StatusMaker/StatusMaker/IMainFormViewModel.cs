using System;

namespace StatusMaker.UI
{
    public interface IMainFormViewModel
    {
        string EmailCCRecipients { get; set; }
        string EmailTORecipients { get; set; }
        string MemberName { get; set; }
        string Status { get; set; }
        string StatusDate { get; set; }
        string ValidateJiraStatus { get; set; }
        string ExcelLocked { get; set; }
        void CommitChangesToSvn();
        void GetSelectedDateVersion();
        void GetUserNamePassword();
        bool isValidExcelPath();
        void OpenEmailInoutlook();
        void OpenStatusExcel();
        void ShowNextDayStatus();
        void ShowPriviousDayStatus();
        void ShowStatus();
        void ShowTodaysStatus();
        void GetLockForceFully();
        IDisposable Subscribe(IObserver<MainFormViewModel> observer);

        string ExcelFilePath { set; }
    }
}