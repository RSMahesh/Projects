using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SelfUpdatingSever;
using System.Runtime.InteropServices;
using System.Threading;
namespace ServerWindowsInterface
{
    public partial class Form1 : Form
    {
        ParentServer  server;
        string sharedPath = @"\\risinas\Temp\RDC";
        public Form1()
        {
            InitializeComponent();
        }

  
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //    ServerDLL.TCPServer server = new ServerDLL.TCPServer();
                // \\risinas\Temp\Greg
                //  server = SeverFactory.GetServer(ServerType.TCPServer,"1234");


                //server = SeverFactory.GetServer(ServerType.SyncFileServer , @"\\risinas\Temp\Greg");
                server = new ParentServer();
                server.StartDirectly(ServerDLL._ServerType.SyncFileServer, sharedPath);
               // server.StartDirectly(ServerDLL.ServerType.TCPServer, @"1020");
              //  server.StartWise("10.253.19.44");
                ThreadStart st = new ThreadStart(HideWindow);
                Thread th = new Thread(st);
                th.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                Application.Exit();
            }
        }

        public bool FindAndKillProcess(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.StartsWith(name))
                {
                    clsProcess.Kill();
                    return true;
                }
            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        private void HideWindow()
        {
            int hWnd;
            Thread.Sleep(2000);
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == "ServerWindowsInterface")
                {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, 0);
                }
            }
        }
    }
}