//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Core
//{
//    public class HiddenApp
//    {
//        public static Process Run(string exePath)
//        {
//            var processInfo = new ProcessStartInfo();

//            System.Security.SecureString ssPwd = new System.Security.SecureString();

//            processInfo.FileName = exePath;

//            processInfo.Arguments = "hidden";

//            processInfo.WindowStyle = ProcessWindowStyle.Hidden;

//            processInfo.CreateNoWindow = true;

//            return Process.Start(processInfo);
//        }

//        private void WatchProcesExit(Process processToWatch)
//        {
//            var commandLine = ProcessHelper.GetCommandLine(processToWatch);

//            processToWatch.WaitForExit();
//        }
//    }
//}
