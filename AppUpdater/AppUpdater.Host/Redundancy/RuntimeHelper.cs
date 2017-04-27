using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdater.Host
{
    public class RuntimeHelper
    {
        public static string ExecutionLocation
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }
        }

        public static string ExecutionAssemblyName
        {
            get
            {
                // return Assembly.GetExecutingAssembly().GetName().Name;
                return  Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);


            }
        }

        public static string MapToCurrentLocation(string filePath)
        {
            return Path.Combine(ExecutionLocation, filePath);
        }

        public static void KillAllProcess(string processName)
        {
            foreach (var process in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(processName)))
            {
                process.Kill();
            }
        }
    }
}
