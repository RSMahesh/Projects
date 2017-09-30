using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.ThreadException += Application_ThreadException1;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.Run(new MDIParent1());
        }

        private static void Application_ThreadException1(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.LogError(e.Exception);
            MessageBox.Show(GetExceptionMessage(e.Exception));
        }

        private static string GetExceptionMessage(Exception ex)
        {
            if (ex.InnerException == null)
            {
                return ex.Message;
            }

            return ex.Message + Environment.NewLine + GetExceptionMessage(ex.InnerException);
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Logger.LogError(e);
            MessageBox.Show(e.Message);
        }

    }
}
