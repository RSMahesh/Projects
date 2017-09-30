using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOIW = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EventPublisher;

namespace Utility
{
    internal class MSWordDocHandler : IDisposable, Utility.IOutProcessDoc 
    {


        public static MOIW.Application WordApp;
        MOIW.Document aDoc = null;
        List<string> commandBars = new List<string>();
        private static MSWordDocHandler instance;
        IntPtr wordWinHandle;
        static int processId;

        private MSWordDocHandler()
        {
            List<int> processIds = ProcessHelper.GetRunningProcessIds("WINWORD");
            WordApp = new MOIW.Application();
            WordApp.Visible = true;
            processId = ProcessHelper.GetNewProcessStarted(processIds, "WINWORD");
            wordWinHandle = Window.Instance.FindWindow(processId);
            Init();
        }
        static MSWordDocHandler()
        {
            Instance = new MSWordDocHandler();
        }

        public void Init()
        {
            WordApp.WindowState = MOIW.WdWindowState.wdWindowStateNormal;
            CloseAllCommandBars();
            SetFocus();
            Window.Instance.SetWindowOnTop(wordWinHandle);
            System.Windows.Forms.SendKeys.SendWait("^{F1}");
            Window.Instance.WindowsReStyle(wordWinHandle);
        }

        public void SetFocus()
        {
            Window.Instance.SetWinFocus(wordWinHandle);
           
        }
      
        public bool  Visible
        {
            get
            {
              return  WordApp.Visible;
            }
            set
            {
                WordApp.Visible = value;
            }
        }

        public string Text
        {
            get
            {
                return aDoc.Content.Text;
            }

            set
            {
                 aDoc.Content.Text = value;
            }
        }

        internal static MSWordDocHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new MSWordDocHandler();
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public void Resize(int left, int top, int width, int height)
        {
            Window.Instance.MoveProcessWindow(wordWinHandle, left, top, width, height);
            Window.Instance.SetWinFocus(wordWinHandle);
        }

        public void OpenDoc(string _fileName)
        {
            CloseDoc();
            WordApp.Visible = true;
            // set the file name from the open file dialog
            object fileName = _fileName;
            object readOnly = false;
            object isVisible = true;
            // Here is the way to handle parameters you don't care about in .NET
            object missing = System.Reflection.Missing.Value;
            // Make word visible, so you can see what's happening
            WordApp.Visible = true;

           // aDoc = WordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            aDoc = WordApp.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible);
            // Activate the document so it shows up in front
            aDoc.Activate();

            WordApp.DocumentBeforeClose += WordApp_DocumentBeforeClose;
            

        

            // var text =   aDoc.Range(aDoc.Content.Start, aDoc.Content.End);

            // var tt = aDoc.Content.Text;

            //  aDoc.Content.Text = "New text";

            Window.Instance.SetWinFocus(wordWinHandle);
        }


        private void WordApp_DocumentBeforeClose(MOIW.Document Doc, ref bool Cancel)
        {
          
            EventContainer.PublishEvent(EventPublisher.Events.WordDocClosed.ToString(), new EventArg(Guid.NewGuid(), aDoc.Content.Text));
            Cancel = true;
            WordApp.Visible = false;
        }

  
        ~MSWordDocHandler()
        {
            Clean();
        }

        private void CloseAllCommandBars()
        {
            for (int i = 1; i < WordApp.CommandBars.Count; i++)
            {
                if (WordApp.CommandBars[i].Visible)
                {
                    try
                    {
                        WordApp.CommandBars[i].Visible = false;
                        commandBars.Add(WordApp.CommandBars[i].Name);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

           
        }

        private void Clean()
        {
            try
            {
                int test = WordApp.CommandBars.Count;

                for (int i = 1; i < WordApp.CommandBars.Count; i++)
                {
                    string result = commandBars.Find(
                    delegate(string cname)
                    {
                        return cname == WordApp.CommandBars[i].Name;
                    }
                    );

                    if (!String.IsNullOrEmpty(result))
                    {
                        WordApp.CommandBars[i].Visible = true;
                    }
                }

                CloseDoc();
                WordApp.Quit();

            }
            catch (Exception ex)
            {

            }
            ProcessHelper.CloseProcess(processId);
            instance = null;
           
        }
        private void CloseDoc()
        {
            // Here is the way to handle parameters you don't care about in .NET
            object missing = System.Reflection.Missing.Value;
            object saveChange = true;

            if (aDoc != null)
            {
                aDoc.Close(ref saveChange, ref missing, ref missing);
            }
        }

        public void Dispose()
        {
            Clean();
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            Dispose();
        }
    }
}
