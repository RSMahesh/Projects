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

namespace FtpDeploy
{
    public partial class Form1 : Form
    {
        private string _ftpServer;

        private string _userName;

        private string _password;

        private string _remoteTargetDirectory;

        private string _localSourceDirectory;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ftp ftpClient = new ftp(@"ftp://waws-prod-sg1-001.ftp.azurewebsites.windows.net", @"MBDevAsiaAPI2\RSMahesh", "1234test!");

            /* Upload a File */
            ftpClient.upload("", @"C:\test.txt");

        }

        private void button2_Click(object sender, EventArgs e)
        {

            var files = Directory.GetFiles(@"D:\GitRepo\MediaValetAPI\MediaValet.Api.IISHost\bin");

            //foreach (var file in files)
            //{
            //    Upload("ftp://waws-prod-sg1-001.ftp.azurewebsites.windows.net", @"MBDevAsiaAPI2\RSMahesh", "1234test!", file);
            //}

            //return;

            Parallel.ForEach(
                files,
                file =>
                {
                    Upload("ftp://waws-prod-sg1-001.ftp.azurewebsites.windows.net", @"MBDevAsiaAPI2\RSMahesh", "1234test!", file);

                });
        }

        private static void Upload(string ftpServer, string userName, string password, string filename)
        {
            var tryFor = 0;
            while (tryFor++ < 5)
            {
                try
                {
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        client.Credentials = new System.Net.NetworkCredential(userName, password);
                        client.UploadFile(
                            ftpServer + "/site/wwwroot/Test/" + new FileInfo(filename).Name,
                            "STOR",
                            filename);
                    }
                    return;
                }
                catch (Exception ex)
                {


                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _ftpServer = "ftp://waws-prod-sg1-001.ftp.azurewebsites.windows.net";
            _userName = @"MBDevAsiaAPI2\RSMahesh";
            _password = "1234test!";
            _localSourceDirectory = @"D:\GitRepo\MediaValetAPI\MediaValet.Api.IISHost\bin";
            _remoteTargetDirectory = "site/wwwroot/Test/";

            var ed = new FTPControl.FTP(
                _ftpServer,
                _userName,
                _password,
                _remoteTargetDirectory,
                _localSourceDirectory);

            ed.Dock = DockStyle.Fill;
            panelFTP.Controls.Add(ed);

        }
    }
}
