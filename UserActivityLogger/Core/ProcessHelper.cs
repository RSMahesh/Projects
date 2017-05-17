using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class ProcessHelper
    {
        public static string MainProcessEventHandleName = "MainProcessEventHandleName";

        public static string WatcherProcessEventHandleName = "WatcherProcessEventHandleName";
        public static Process Run(string exePath, Dictionary<string, string> parameters=null)
        {
            var processInfo = new ProcessStartInfo();

            string para = string.Empty;

            if (parameters != null)
            {
                foreach (var key in parameters.Keys)
                {
                    para += key + "=" + parameters[key] + " ";
                }
            }

            processInfo.Arguments = para;

            processInfo.FileName = exePath;

            return Process.Start(processInfo);
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

        public static Process RunHidden(string exePath)
        {
            var processInfo = new ProcessStartInfo();

            processInfo.FileName = exePath;

            processInfo.Arguments = "hidden";

            processInfo.WindowStyle = ProcessWindowStyle.Hidden;

            processInfo.CreateNoWindow = true;

            return Process.Start(processInfo);
        }

        public static void Watch()
        {
            //Path of the core.exe
            var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var coreProcess = RunHidden(exePath);

            Task.Run(() => WatchProcesExit(coreProcess, exePath));
        }

        internal static void WatchProcesExit(Process processToWatch, string exePath)
        {

            try
            {

                processToWatch = ProcessHelper.GetProcess(exePath).FirstOrDefault();

                processToWatch.WaitForExit();

                if (AbortWatch())
                {
                    return;
                }

                Thread.Sleep(500);

                var process = ProcessHelper.RunHidden(exePath);

                Thread.Sleep(500);

                Console.WriteLine(exePath);

                WatchProcesExit(process, exePath);

               
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex);
                Thread.Sleep(500);
               // WatchProcesExit(processToWatch);
            }

        }


        private static bool AbortWatch()
        {
            return File.Exists(RuntimeHelper.MapToCurrentExecutionLocation("abort.txt"));
        }

     
    }
}
