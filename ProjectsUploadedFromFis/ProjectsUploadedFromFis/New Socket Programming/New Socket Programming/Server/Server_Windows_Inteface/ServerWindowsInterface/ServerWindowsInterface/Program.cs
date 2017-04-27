using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace ServerWindowsInterface
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            bool createdNew = true;
            string applicationId = "04BBF451-0359-4cfa-AC67-E5D409E470FB";
            using (Mutex mutex = new Mutex(true, applicationId, out createdNew))
            {
                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
            }

          
        }


    }
}