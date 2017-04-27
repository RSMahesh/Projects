using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

using MediaValet.Exercise.Common;

using System.IO;

namespace TransparentWindow
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (!SingleInstance.IsApplicationAlreadyRunning("TransparentWindow"))
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Init());
        }
    }


 
    public class ListItem
    {
   
        public string label { get; set; }

      
        public string value { get; set; }
    }

}



