using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPControl
{
    public partial class MainForm : Form
    {
        private string _ftpServer;

        private string _userName;

        private string _password;

        private string _remoteTargetDirectory;

        private string _localSourceDirectory;

        FTPControl.FTP _ApiTransferControl;

        FTPControl.FTP _WebJobTransferControl;

        public MainForm()
        {
            InitializeComponent();
        }

   
        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void Alert(string msg)
        {
            MessageBox.Show(msg);
        }
    
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadFtpControl();

        }

        private void LoadFtpControl()
        {
            _ftpServer = "ftp://waws-prod-sg1-001.ftp.azurewebsites.windows.net";
            _userName = @"MBDevAsiaAPI2\RSMahesh";
            _password = "1234test!";
            _localSourceDirectory = @"D:\GitRepo\MediaValetAPI\MediaValet.Api.IISHost\bin";
            _remoteTargetDirectory = "site/wwwroot/Test/";

            _ApiTransferControl = new FTPControl.FTP(
                _ftpServer,
                _userName,
                _password,
                _remoteTargetDirectory,
                _localSourceDirectory);

            _ApiTransferControl.Dock = DockStyle.Fill;
            tabControl1.TabPages[0].Controls.Add(_ApiTransferControl);


            _ftpServer = "ftp://waws-prod-sg1-001.ftp.azurewebsites.windows.net";
            _userName = @"MBDevAsiaAPI2\RSMahesh";
            _password = "1234test!";
            _localSourceDirectory = @"D:\GitRepo\MediaValetAPI\MediaValet.WebJobs.EntityChangeListener\bin\Debug";
            _remoteTargetDirectory = "site/wwwroot/Test1/";

            _WebJobTransferControl = new FTPControl.FTP(
                _ftpServer,
                _userName,
                _password,
                _remoteTargetDirectory,
                _localSourceDirectory);

            _WebJobTransferControl.Dock = DockStyle.Fill;
            tabControl1.TabPages[1].Controls.Add(_WebJobTransferControl);

        }


        private void btnStartTransfer_Click(object sender, EventArgs e)
        {
            _ApiTransferControl.StartTransfer();
        }
    }
}
