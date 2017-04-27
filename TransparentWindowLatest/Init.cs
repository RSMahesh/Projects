using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TransparentWindow
{

    public partial class Init : Form
    {
        string filename = "Destop.jpg";

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public Init()
        {
            InitializeComponent();
        }


        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void checkychecky()
        {
            var pps = Process.GetProcesses();
            List<ComboboxItem> chromeProcess = new List<ComboboxItem>();
            foreach (var pp in pps)
            {
                if (!string.IsNullOrEmpty(pp.MainWindowTitle))
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = pp.MainWindowTitle;
                    item.Value = pp.Id;

                    if (pp.MainWindowTitle.Contains("Chrome"))
                    {
                        chromeProcess.Add(item);
                    }
                    else
                    {
                        cmbProcess.Items.Add(item);
                    }

                }
            }

            foreach (var item in chromeProcess)
            {
                cmbProcess.Items.Insert(0, item);
            }
        }



        [DllImport("user32.dll")]
        public static extern long GetWindowRect(int hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }



        private void Init_Load(object sender, EventArgs e)
        {
            checkychecky();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {

            this.Visible = false;

            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(textBox1.Text)));

            if (!checkBox1.Checked || !File.Exists(filename))
            {

                ServerDLL.ScreenCapture.CaptureScreenToFile(filename, ImageFormat.Jpeg);
            }
            var attachedWindow = new AttachedWindow((cmbProcess.SelectedItem as ComboboxItem).Value.ToString());

            attachedWindow.ShowAttachWindow();
            TransparentFrm frm = new TransparentFrm(attachedWindow);
            frm.Show();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            checkychecky();
        }

        private void Init_Activated(object sender, EventArgs e)
        {
            if (groupBox1.Visible == false)
            {

            }
        }
    }
}
