using StatusMaker.Business;
using StatusMaker.UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace StatusMaker.UI
{
    public class MainFormViewModel : IObservable<MainFormViewModel>, IMainFormViewModel
    {

        private List<IObserver<MainFormViewModel>> observers;

        private string _emailTORecipients = "";
        private string _validateJiraStatus = "";
        private string _emailCCRecipients = "";
        private string _statusDateString = "";
        private string _memberName = "";
        private string _status = "";
        private string _excelLocked = "";

        //  private static readonly string excelFilePath = Settings.Default.ExcelFilePath;
        private readonly IEmail _email;
        private readonly ISVN _svn;
        private readonly IStatusGenerator _dailyStatus;

        private readonly string _excelDownloadFilePath;
        private string _userName;
        private string _password;

        private DateTime _statusDateTime;
        public MainFormViewModel()
        {

        }

        public MainFormViewModel(string userName, string password, string excelDownloadFilePath, IEmail email, ISVN svn, IStatusGenerator dailyStatus)
        {

            _userName = userName;
            _password = password;
            _email = email;
            _svn = svn;
            _dailyStatus = dailyStatus;
            _excelDownloadFilePath = excelDownloadFilePath;
            observers = new List<IObserver<MainFormViewModel>>();
        }

        public string EmailTORecipients
        {
            get { return _emailTORecipients; }
            set { OnValueChanged(ref _emailTORecipients, value); }
        }

        public string ExcelLocked
        {
            get { return _excelLocked; }
            set { OnValueChanged(ref _excelLocked, value); }
        }

        public string Status
        {
            get { return _status; }
            set { OnValueChanged(ref _status, value); }
        }

        public string EmailCCRecipients
        {
            get { return _emailCCRecipients; }
            set { OnValueChanged(ref _emailCCRecipients, value); }
        }

        public string MemberName
        {
            get { return _memberName; }
            set
            {
                OnValueChanged(ref _memberName, value);
            }
        }

        public string ValidateJiraStatus
        {
            get { return _validateJiraStatus; }
            set { OnValueChanged(ref _validateJiraStatus, value); }
        }

        public string StatusDate
        {
            get { return _statusDateString; }
            set
            {
                _statusDateTime = DateTime.Parse(value);
                OnValueChanged(ref _statusDateString, value);
            }
        }
        private void OnValueChanged(ref string oldValue, string newValue)
        {
            if (!string.Equals(oldValue, newValue, StringComparison.Ordinal))
            {
                oldValue = newValue;
                foreach (var observer in observers)
                    observer.OnNext(this);
            }
        }

        public IDisposable Subscribe(IObserver<MainFormViewModel> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                observer.OnNext(this);
            }
            return null;
        }

        public void ShowNextDayStatus()
        {

            if (_statusDateTime >= DateTime.Now.Date)
            {
                MessageBox.Show("Slow down man. Check what you do");
                return;
            }

            StatusDate = _statusDateTime.AddDays(1).ToString();
            this.GetSelectedDateVersion();
        }

        public void ShowStatus()
        {
            Status = _dailyStatus.GenerateStatusForSingleDay(
    _statusDateTime,
    MemberName.Trim(),
   bool.Parse(ValidateJiraStatus));
            Cursor.Current = Cursors.Default;

            //m_oWorker = new BackgroundWorker();
            //m_oWorker.DoWork += new DoWorkEventHandler(m_oWorker_DoWork);
            //m_oWorker.RunWorkerAsync();

        }

        private void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Status = _dailyStatus.GenerateStatusForSingleDay(
     _statusDateTime,
     MemberName.Trim(),
    bool.Parse(ValidateJiraStatus));
           Cursor.Current = Cursors.Default;
        }
        public void GetSelectedDateVersion()
        {
            Cursor.Current = Cursors.WaitCursor;
            this._svn.GetLatestOfDay(_statusDateTime, Path.GetDirectoryName(this._excelDownloadFilePath));
            this.ShowStatus();
          
        }

        private static void DeleteLogs()
        {
            var files = Directory.GetFiles(".", "*.mb");

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        BackgroundWorker m_oWorker;
        public void ShowTodaysStatus()
        {
            DeleteLogs();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!this._svn.DownLoadLatestProgressTrackingSheet(_userName, _password))
                {
                    this.GetUserNamePassword();
                    return;
                }

                //_statusDateString = DateTime.Now.ToString();
                _statusDateString = DateTime.Now.AddDays(-3).ToString();

                this.ShowStatus();
            }
            finally
            {
              //  Cursor.Current = Cursors.Default;
            }
        }

        public void GetUserNamePassword()
        {


            using (var dialog = new UserCredentialsDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    if (!this._svn.DownLoadLatestProgressTrackingSheet(dialog.User, dialog.PasswordToString()))
                    {
                        MessageBox.Show("Invalid username or password. Try gain");
                        return;
                    }


                    Settings.Default.UserName = _userName = dialog.User;
                    Settings.Default.Password = _password = dialog.PasswordToString();
                    Settings.Default.Save();

                    this._svn.IsValidUser(Settings.Default.UserName, Settings.Default.Password);

                    if (dialog.SaveChecked)
                    {
                        dialog.ConfirmCredentials(true);
                    }
                }
            }
        }

        public void OpenEmailInoutlook()
        {
            ClipboardHelper.CopyToClipboard(Status, string.Empty);
            this._email.SendMail(
                EmailTORecipients.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries),
                 this.EmailCCRecipients.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries),
                Status);
        }

        public void OpenStatusExcel()
        {
            if (!this.isValidExcelPath())
            {
                return;
            }

            if (this._svn.GetLatestAndLock())
            {
                Process.Start(Settings.Default.ExcelFilePath);
                //  this.btnCommitExcel.Enabled = true;
            }
        }

        public void CommitChangesToSvn()
        {
            this._svn.CheckIn();
            MessageBox.Show("CheckIn Done");
        }

        public bool isValidExcelPath()
        {
            if (string.IsNullOrEmpty(Settings.Default.ExcelFilePath))
            {
                MessageBox.Show("Please Set Status File Path");
                return false;
            }

            return true;
        }

        public void ShowPriviousDayStatus()
        {
            StatusDate = _statusDateTime.AddDays(-1).ToString();
            this.GetSelectedDateVersion();
        }

        public void GetLockForceFully()
        {
            _svn.GetLockForceFully();
        }


        public string ExcelFilePath
        {
            set
            {
                _svn.ExcelFilePath = value;
            }

        }

    }
}
