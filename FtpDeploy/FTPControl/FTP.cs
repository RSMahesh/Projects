using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPControl
{
    public partial class FTP : UserControl
    {
        private string _ftpServer;

        private string _userName;

        private string _password;

        private string _remoteTargetDirectory;

        private string _localSourceDirectory;

        private List<string> _lstSourceFiles;

        private StringBuilder logs = new StringBuilder();

        public FTP(string ftpServer, string userName, string password, string remoteDirectory, string localSourceDirectory)
        {

            InitializeComponent();

            _ftpServer = ftpServer;
            _userName = userName;
            _password = password;
            _remoteTargetDirectory = remoteDirectory;
            _localSourceDirectory = localSourceDirectory;
            _lstSourceFiles = new List<string>();
            Init();
        }

        private void Init()
        {
            var files = Directory.GetFiles(_localSourceDirectory);
            foreach (var file in files)
            {
                _lstSourceFiles.Add(Path.GetFileName(file));
            }

            //   lstSourceFile.DataSource = Directory.GetFiles(_localSourceDirectory);
            gbSource.Text = _localSourceDirectory;
            gbRemote.Text = _remoteTargetDirectory;
            gbProgess.Visible = false;
            PopulatetFilesListWithFilter();
        }



        private void PopulatetFilesListWithFilter()
        {
            lstSourceFile.Items.Clear();

            foreach (var file in _lstSourceFiles)
            {
                if (string.IsNullOrEmpty(txtFilter.Text.Trim()) || file.ToLowerInvariant().Contains(txtFilter.Text.ToLowerInvariant()))
                {
                    lstSourceFile.Items.Add(Path.GetFileName(file));
                }
            }
            CheckAll();
            UpdateFileCount(0);
        }

        private void lblFTPDrive_Click(object sender, EventArgs e)
        {

        }

        public void StartTransfer()
        {
            gbProgess.Visible = true;

            List<string> files = new List<string>();

            foreach (var item in lstSourceFile.CheckedItems)
            {
                files.Add(item.ToString());
            }

            progressBar1.Maximum = files.Count + 1;
            progressBar1.Step = 1;
            progressBar1.Value = 1;

            Freeze(false);

            var worker = new BackgroundWorker();
            worker.DoWork += (s, args) =>
            {
                Parallel.ForEach(
                         files,
                         file =>
                         {
                             if (Upload(Path.Combine(_localSourceDirectory, file)))
                             {
                                 lock (lstSourceFile)
                                 {

                                     this.Invoke(new MethodInvoker(delegate ()
                                     {
                                         progressBar1.Value += 1;
                                         lstSourceFile.Items.Remove((Path.Combine(_localSourceDirectory, file)));
                                         lstCopyedFiles.Items.Add(file);
                                         txtOutPut.Text += file + " Transfered Successfully" + Environment.NewLine;
                                         UpdateFileCountTarget();
                                     }));

                                 }
                             }
                         });

                MessageBox.Show("Done");
                Freeze(true);
            };



            worker.RunWorkerAsync();

        }

        private void Freeze(bool freeze)
        {

            this.Invoke(new MethodInvoker(delegate ()
            {
                gbRemote.Enabled = gbSource.Enabled = btnStartFtp.Enabled = freeze;
            }));

        }



        private void btnStartFtp_Click(object sender, EventArgs e)
        {
            StartTransfer();
        }

        private bool Upload(string filename)
        {
            var tryFor = 0;
            Exception exception = null;
            while (tryFor++ < 5)
            {
                try
                {
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        client.Credentials = new System.Net.NetworkCredential(_userName, _password);
                        client.UploadFile(
                            _ftpServer + "/" + _remoteTargetDirectory + new FileInfo(filename).Name,
                            "STOR",
                            filename);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }

            if (exception != null)
            {
                txtOutPut.Text += Environment.NewLine + exception.ToString();
            }

            return false;
        }


        private void txtFilter_TextChanged_1(object sender, EventArgs e)
        {
            PopulatetFilesListWithFilter();
        }

        private void chkAll_CheckedChanged_1(object sender, EventArgs e)
        {
            CheckAll();
        }

        private void CheckAll()
        {
            for (var i = 0; i < lstSourceFile.Items.Count; i++)
            {
                lstSourceFile.SetItemChecked(i, chkAll.Checked);
            }

        }


        private void UpdateFileCount(int increase)
        {
            int count = 0;
            foreach (var item in lstSourceFile.CheckedItems)
            {
                count++;
            }


            count += increase;

            lblTotalSiurceFile.Text = "File Count:" + count.ToString();

        }

        private void UpdateFileCountTarget()
        {
            int count = 0;
            foreach (var item in lstCopyedFiles.Items)
            {
                count++;
            }
      
            lblTotalTargetFile.Text = "File Count:" + count.ToString();
        }

        private void lstSourceFile_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            UpdateFileCount(e.NewValue == CheckState.Checked?1:-1);
        }

       
    }
}
