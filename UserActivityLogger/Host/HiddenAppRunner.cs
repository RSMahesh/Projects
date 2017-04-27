using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class HiddenAppRunner
    {
        public static void Run(string exePath)
        {
            var processInfo = new ProcessStartInfo();

            processInfo.FileName = exePath;

            processInfo.Arguments = "hidden";

            processInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process.Start(processInfo);
        }
    }
}
