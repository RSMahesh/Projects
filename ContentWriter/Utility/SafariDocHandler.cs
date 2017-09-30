using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Utility
{
    class SafariDocHandler:IDisposable,IOutProcessDoc 
    {
        IntPtr handle;

        bool visible = true;
        public static SafariDocHandler Instance;
        static int processId;
        private SafariDocHandler()
        {
        }
        static SafariDocHandler()
        {
            Instance = new SafariDocHandler();
        }

        public void Init()
        {
            List<int> processIds = ProcessHelper.GetRunningProcessIds("Safari");

            Process notePad = new Process();
            notePad.StartInfo.FileName = "Safari.exe";
            notePad.StartInfo.Arguments  = @"C:\hhjj.dfg";
            notePad.Start();
            processId = ProcessHelper.GetNewProcessStarted(processIds, "Safari");

            System.Threading.Thread.Sleep(4000);

            handle = Window.Instance.FindWindow(processId);
        }

        public void OpenDoc(string _fileName)
        {
            if (handle ==(IntPtr) 0)
            {
                Init();
            }
                Window.Instance.SetWinFocus(handle);
                Window.Instance.SetWindowOnTop(handle);
               
                Clipboard.Clear();
                Clipboard.SetText(_fileName);
                Window.SetFocus(handle);
                System.Windows.Forms.SendKeys.SendWait("^l");
                System.Windows.Forms.SendKeys.SendWait("^v");
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                Window.Instance.WindowsReStyle(handle);
            }
          
       
        public void Resize(int left, int top, int width, int height)
        {
            Window.Instance.MoveProcessWindow(handle, left, top, width, height);
            Window.Instance.SetWinFocus(handle);
        }

        public void SetFocus()
        {
            throw new NotImplementedException();
        }

        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                Window.Instance.ShowWindow(handle, value);
                visible = value;
            }
        }

        public string Text
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            ProcessHelper.CloseProcess(processId);
        }


        public void Close()
        {
            Dispose();
        }
    }
}
