using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Configuration;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using ServerDLL;
using System.Threading;


namespace Recorder
{
    public partial class Recorder : Form
    {
        string folder = "";
        ImageCodecInfo jpegCodec = null;
        int imageQuality =int.Parse( System.Configuration.ConfigurationManager.AppSettings["imageQuality"]);

        public Recorder()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Start()
        {
            folder = Path.GetDirectoryName( Application.StartupPath) + "\\" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();
            Directory.CreateDirectory(folder);
            timer1.Enabled = true;
        }

        private void SaveScreen()
        {
            SaveJpeg(folder + @"\" + Guid.NewGuid().ToString() + ".jpeg", ScreenCapture.CaptureScreen(), imageQuality);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        
        public void SaveJpeg(string path, Image img, int quality)
        {
            //      if (quality < 0 || quality > 100)
            //        throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        public static void HideWindow(object  show)
        {
            int hWnd;
            Thread.Sleep(100);
            Process[] processRunning = Process.GetProcesses();
            string exeName = System.Windows.Forms.Application.ProductName;
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == exeName)
                {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd,(int) show);
                }
            }
        }

        public static void KillProcess()
        {
        
            Process[] processRunning = Process.GetProcesses();
            string exeName = System.Windows.Forms.Application.ProductName;
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == exeName)
                {
                    pr.Kill();
                }
            }
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            SaveScreen();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            jpegCodec = GetEncoderInfo("image/jpeg");
            Start();
            button1.Enabled = false;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
          //  ThreadStart st = new ThreadStart(HideWindow);
            ParameterizedThreadStart thread = new ParameterizedThreadStart(HideWindow);
            Thread th = new Thread(thread);
            th.Start(0);
        }

        private void btnPlayer_Click(object sender, EventArgs e)
        {
            RecordSession.frmControlBox frm = new RecordSession.frmControlBox();
            frm.Show();
        }

    }
}
