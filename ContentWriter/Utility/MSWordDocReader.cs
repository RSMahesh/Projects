using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOIW = Microsoft.Office.Interop.Word;

namespace Utility
{
    public static class MSWordDocReader
    {
        static MOIW.Application application;
        static int processId;

        static MSWordDocReader()
        {
            List<int> processIds = ProcessHelper.GetRunningProcessIds("WINWORD");
            application = new MOIW.Application();
            application.Visible = false;
            processId = ProcessHelper.GetNewProcessStarted(processIds, "WINWORD");
        }

        public static void Close()
        {
            ProcessHelper.CloseProcess(processId);
            // Utility.Window.Instance.CloseProcess("WINWORD");
        }

        public static bool Find(string path, object findText)
        {
            MOIW.Document document = null;
            object missing = Type.Missing;
            object openreadonly = true;
            bool found = false;
            try
            {
                //application.Documents.OpenNoRepairDialog 
                document = application.Documents.OpenNoRepairDialog(path, ref missing,ref missing);
                found = document.Range(document.Content.Start, document.Content.End).
                        Find.Execute(ref findText, ref missing, ref missing, ref missing,
                                 ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                                 ref missing, ref missing, ref missing, ref missing, ref missing);
            }
            finally
            {
                document.Close(ref missing, ref missing, ref missing);
            }

            return found;

        }
    }
}
