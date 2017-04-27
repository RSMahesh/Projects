using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.IO;

namespace UploadFolder
{
    public partial class Form1 : Form
    {
        List<string> files = new List<string>();
        int totalFiles;
        int doneSoFar;

        public Form1()
        {
            InitializeComponent();
            listBox2.HorizontalScrollbar = checkedListBox1.HorizontalScrollbar = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // NameValueCollection nvc = new NameValueCollection();
            //nvc.Add("Button1", "Button");

            // ImportTool.ImportUtilities.HttpUploadFile("http://localhost:3928/WebForm2.aspx", @"C:\CDISDetail.xml", "FileUpload1", "application/xml", nvc);
            string msg = "Client Path:" + txtClientRootFolder.Text + Environment.NewLine + Environment.NewLine +
                "Server Path:" + txtServerRootPath.Text + Environment.NewLine + Environment.NewLine;


            if (MessageBox.Show(msg, "Confirm Upload", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                try
                {

                    StartUpload();

                    MessageBox.Show("Upload Finish");
                }
                catch (Exception ex)
                {
                    Clipboard.SetText(ex.Message);
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("Error Copyed To ClipBoard");
                }

            }

        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != "")
            {

                txtClientRootFolder.Text = folderBrowserDialog1.SelectedPath;
                GetFiles();

            }
        }

        private void GetFiles()
        {
            files = new List<string>();
            DirSearch(txtClientRootFolder.Text);
            char[] chrSp = { '\n' };
            var exludeExtension = txtExlcudeExt.Text.Trim().Split(chrSp);
            checkedListBox1.Items.Clear();


            foreach (var ext in exludeExtension)
            {

                files.RemoveAll(x => Path.GetExtension((x)) == ext.Replace('\r', ' ').Trim());
            }

            foreach (var file in files)
            {
                checkedListBox1.Items.Add(file);

            }
            lblTotal.Text = "Total Files:" + checkedListBox1.Items.Count.ToString();
        }

        private void StartUpload()
        {
            if (txtServerRootPath.Text.Trim() == "" || txtClientRootFolder.Text.Trim() == ""
                 || txtUrl.Text.Trim() == "")
            {
                MessageBox.Show("Input Missing");
                return;
            }


            doneSoFar = 0;

            foreach (var item in checkedListBox1.CheckedItems)
            {

                doneSoFar++;
                var file = item.ToString();
                var serverFile = file.Replace(txtClientRootFolder.Text, txtServerRootPath.Text);
                UploadFile(file, serverFile);
                //   checkedListBox1.Items.Remove( checkedListBox1.Items[0]);
                listBox2.Items.Add(serverFile);
                lblDoneSoFar.Text = "Processed:" + doneSoFar.ToString();

                Application.DoEvents();
            }

        }

        private void UploadFile(string filePath, string serverFile)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("Button1", "Button");
            ImportTool.ImportUtilities.HttpUploadFile(txtUrl.Text, filePath, serverFile, "FileUpload1", "application/xml", nvc);
        }

        private void DirSearch(string sDir)
        {

            foreach (string f in Directory.GetFiles(sDir))
            {
                files.Add(f);
            }

            foreach (string d in Directory.GetDirectories(sDir))
            {
                DirSearch(d);
            }
        }

        private void txtClientRootFolder_Leave(object sender, EventArgs e)
        {
            try
            {
                GetFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, checkBox1.Checked);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtEndsWith.Text == "File Ends With")
                return;

            for (var i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (!checkedListBox1.Items[i].ToString().EndsWith(txtEndsWith.Text, StringComparison.InvariantCultureIgnoreCase))
                {
                    checkedListBox1.Items.RemoveAt(i);
                    i--;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            FTP ftp = new FTP();
            ftp.Show();
        }
    }
}

