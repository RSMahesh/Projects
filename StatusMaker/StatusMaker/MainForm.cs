using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using StatusMaker.Business;
using StatusMaker.UI.Properties;

namespace StatusMaker.UI
{
    public partial class MainForm : Form, IMainForm, IObserver<IMainFormViewModel>
    {

        private IMainFormViewModel _subject;

        public string VersionNumber { get; set; }
        public string EmailTORecipients
        {
            get { return _subject.EmailTORecipients; }
            set { _subject.EmailTORecipients = value; }
        }
        public string ExcelLocked
        {
            get { return _subject.ExcelLocked; }
            set { _subject.ExcelLocked = value; }
        }
        public string Status
        {
            get { return _subject.Status; }
            set { _subject.Status = value; }
        }
        public string EmailCCRecipients
        {
            get { return _subject.EmailCCRecipients; }
            set { _subject.EmailCCRecipients = value; }
        }
        public string StatusDate
        {
            get { return _subject.StatusDate; }
            set { _subject.StatusDate = value; }
        }
        public string ValidateJiraStatus
        {
            get { return _subject.ValidateJiraStatus; }
            set { _subject.ValidateJiraStatus = value; }
        }
        public string MemberName
        {
            get { return _subject.MemberName; }
            set { _subject.MemberName = value; }
        }
        public void OnNext(IMainFormViewModel uiValues)
        {
            txtTORecipients.Text = uiValues.EmailTORecipients;
            txtCCRecipients.Text = uiValues.EmailCCRecipients;
            chkValidateJiraStatus.Checked = bool.Parse(uiValues.ValidateJiraStatus);
            //  dateTimePicker1.Value = DateTime.Parse(uiValues.StatusDate);
            txtMemberName.Text = uiValues.MemberName;
            webBrowser1.DocumentText = uiValues.Status;
        }
        public void OnError(Exception e)
        {

        }
        public void OnCompleted()
        {
        }
        public MainForm(IMainFormViewModel subject)
        {
            _subject = subject;
            InitializeComponent();
            _subject.EmailTORecipients = txtTORecipients.Text;
            _subject.EmailCCRecipients = txtCCRecipients.Text;
            _subject.ValidateJiraStatus = chkValidateJiraStatus.Checked.ToString();
            _subject.StatusDate = dateTimePicker1.Value.ToString();
            _subject.MemberName = MemberName;
            _subject.Subscribe(this);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.webBrowser1.Left = 5;
            this.webBrowser1.Width = this.Width - 20;

            this.groupBox1.Top = this.Height - 250;
            this.groupBox1.Left = (this.Width / 2) - (this.groupBox1.Width / 2);

            this.webBrowser1.Height = this.groupBox1.Top - 20;
        }

        private void btnBackWard_Click(object sender, EventArgs e)
        {
            _subject.ShowPriviousDayStatus();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            _subject.ShowNextDayStatus();
        }
        private void btnUnlockForeFully_Click(object sender, EventArgs e)
        {
            _subject.GetLockForceFully();
        }
        private void btnShowTodaysStatus_Click(object sender, EventArgs e)
        {
            _subject.ShowTodaysStatus();
        }
        private void btnOpenInOutlook_Click(object sender, EventArgs e)
        {
            _subject.OpenEmailInoutlook();
        }
        private void btnShowExcel_Click(object sender, EventArgs e)
        {
            _subject.OpenStatusExcel();
            this.btnCommitExcel.Enabled = true;
        }
        private void btnCommitExcel_Click(object sender, EventArgs e)
        {
            _subject.CommitChangesToSvn();
        }
        private void btnSetStatusFilePath_Click(object sender, EventArgs e)
        {
            SetStatusFilePath();
        }

        public void SetStatusFilePath()
        {
            this.openFileDialog1.FileName = "Progress Tracking.xlsx";

            this.openFileDialog1.Filter = "Files|Progress Tracking.xlsx";
            DialogResult result = this.openFileDialog1.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                Settings.Default.ExcelFilePath = this.openFileDialog1.FileName;
                _subject.ExcelFilePath = Settings.Default.ExcelFilePath;
                Settings.Default.Save();
            }
        }
        private void txtTORecipients_TextChanged(object sender, EventArgs e)
        {
            EmailTORecipients = ((TextBox)sender).Text;
        }
        private void chkValidateJiraStatus_CheckedChanged(object sender, EventArgs e)
        {
            ValidateJiraStatus = ((CheckBox)sender).Checked.ToString();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            StatusDate = ((DateTimePicker)sender).Value.ToString();
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Status = this.webBrowser1.DocumentText;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text += " " + VersionNumber;
        }
        private void btnCleanSetting_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
        }
    }
}