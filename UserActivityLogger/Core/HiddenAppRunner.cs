using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
   public class ProcessHelper
    {
        public static void RunAsBackGround(string exePath)
        {
            var processInfo = new ProcessStartInfo();

            processInfo.FileName = exePath;

            processInfo.Arguments = "hidden";

            processInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process.Start(processInfo);
        }

        public static void Run(string exePath)
        {
            var processInfo = new ProcessStartInfo();

            processInfo.FileName = exePath;

            Process.Start(processInfo);
        }

        public static void KillProcess(string exeName)
        {
            var processes = GetProcess(exeName);

            foreach (var process in processes)
            {
                process.Kill();
            }
        }

        public static Process[] GetProcess(string processName)
        {
            return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(processName));
        }

        public static IEnumerable<string> GetForegroundProcess()
        {
            Process[] processes = Process.GetProcesses();
            List<string> names = new List<string>();

            foreach (Process p in processes)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                {
                    names.Add(p.ProcessName);
                }
            }

            return names;
        }
    }
}
