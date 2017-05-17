using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdater.Core
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

        public static string GetParentProcessName()
        {
            try
            {
                var myId = Process.GetCurrentProcess().Id;
                var query = string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", myId);
                var search = new ManagementObjectSearcher("root\\CIMV2", query);
                var results = search.Get().GetEnumerator();
                results.MoveNext();
                var queryObj = results.Current;
                var parentId = (uint)queryObj["ParentProcessId"];
                var parent = Process.GetProcessById((int)parentId);
                return parent.ProcessName;
            }
            catch(Exception ex)
            {

            }

            //Bull Shit need to be done for now
            return "StatusMaker";
        }
    }
}
