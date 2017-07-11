using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JarFileGeneratorTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(ProcessJarFileRequest()));
        }

        private string ProcessJarFileRequest()
        {
            List<string> files = new List<string>();

            foreach (var item in ListBoxFiles.Items)
            {
                if (ListBoxFiles.GetItemCheckState(ListBoxFiles.Items.IndexOf(item)) == CheckState.Checked)
                {
                    files.Add(Path.Combine(txtSourceFolder.Text, item.ToString()));
                }
            }

            var jarFilePath = RuntimeHelper.MapToTempFolder(Path.Combine(Guid.NewGuid().ToString(), "include.jar"));

            GenerateJarFile(jarFilePath, files);

            return jarFilePath;
        }

        private void ProcessPackageFileRequest()
        {
            var JarFilePath = ProcessJarFileRequest();
            var dirPath = Path.GetDirectoryName(JarFilePath);

            foreach (var item in ListBoxFiles.Items)
            {
                if (ListBoxFiles.GetItemCheckState(ListBoxFiles.Items.IndexOf(item)) == CheckState.Unchecked)
                {
                    File.Copy(Path.Combine(txtSourceFolder.Text, item.ToString()), Path.Combine(dirPath, Path.GetFileName(item.ToString())));
                }
            }
            Process.Start(dirPath);
        }

        void GenerateJarFile(string jarFilePath, IEnumerable<string> files)
        {
            if (File.Exists(jarFilePath))
            {
                File.Delete(jarFilePath);
            }

            if (!Directory.Exists(Path.GetDirectoryName(jarFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(jarFilePath));
            }


            JarFileFactory jarFactory = new JarFileFactory();

            Dictionary<string, string> header = new Dictionary<string, string>();

            using (var jarFileWriter = jarFactory.GetJarFileWriter(jarFilePath))
            {
                foreach (var file in files)
                {
                    header["FileName"] = Path.GetFileName(file);
                    var fvi = FileVersionInfo.GetVersionInfo(file);
                    header["version"] = fvi.FileVersion;

                    jarFileWriter.AddFile(new FileSystem.JarFileItem(header, file));
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtSourceFolder.Text;

            var rseult = folderBrowserDialog1.ShowDialog();

            if (rseult == DialogResult.OK)
            {
                txtSourceFolder.Text = folderBrowserDialog1.SelectedPath;
                PopulateFiles();
            }
        }

        private void PopulateFiles()
        {
            //  var files = Directory.GetFiles(txtSourceFolder.Text);

            var files = Directory
    .GetFiles(txtSourceFolder.Text)
    .Where(file => HugFilter(file))
    .ToList();

            ListBoxFiles.Items.Clear();

            foreach (var file in files)
            {
                ListBoxFiles.Items.Add(Path.GetFileName(file));
            }
        }

        private bool HugFilter(string file)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
                return true;

            var extensions = txtFilter.Text.Split(',');

            return extensions.Contains(Path.GetExtension(file));

        }

        private void txtSourceFolder_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PopulateFiles();
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < ListBoxFiles.Items.Count; i++)
            {
                ListBoxFiles.SetItemChecked(i, chkSelectAll.Checked);
            }
        }

        private void btnGeneratePackage_Click(object sender, EventArgs e)
        {
            ProcessPackageFileRequest();
        }

    }
}
