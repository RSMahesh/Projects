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
        public static Process Run(string exePath)
        {
            var processInfo = new ProcessStartInfo();

            processInfo.FileName = exePath;

            return Process.Start(processInfo);
        }

        public static Process GetParentProcess()
        {
            var myId = Process.GetCurrentProcess().Id;
            var query = string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", myId);
            var search = new ManagementObjectSearcher("root\\CIMV2", query);
            var results = search.Get().GetEnumerator();
            results.MoveNext();
            var queryObj = results.Current;
            var parentId = (uint)queryObj["ParentProcessId"];
            return Process.GetProcessById((int)parentId);
        }

        public static string GetCommandLine(Process process)
        {
            var commandLine = new StringBuilder(process.MainModule.FileName);

            commandLine.Append(" ");

            using (var searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            {
                foreach (var @object in searcher.Get())
                {
                    commandLine.Append(@object["CommandLine"]);
                    commandLine.Append(" ");
                }
            }

            return commandLine.ToString();
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
            var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var process = Run(exePath);

            Task.Run(() => WatchProcesExit(process, exePath));
        }

        internal static void WatchProcesExit(Process processToWatch, string exePath)
        {

            try
            {
                string fullPath = GetExePath(processToWatch);

                processToWatch.WaitForExit();

                if (AbortWatch())
                {
                    return;
                }

                Thread.Sleep(500);

                var process = ProcessHelper.Run(exePath);

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

        internal static string GetExePath(Process process)
        {
            return process.MainModule.FileName;
        }
    }
}
