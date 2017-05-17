using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class ProcessExtensions
    {
        public static Process GetParentProcess(this Process process)
        {
            var myId = process.Id;
            var query = string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", myId);
            var search = new ManagementObjectSearcher("root\\CIMV2", query);
            var results = search.Get().GetEnumerator();
            results.MoveNext();
            var queryObj = results.Current;
            var parentId = (uint)queryObj["ParentProcessId"];
            return Process.GetProcessById((int)parentId);
        }

        public static Dictionary<string,string> GetCommandLine(this Process process)
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

           var args = commandLine.ToString().Split(' ');
           var dic = new Dictionary<string, string>();

            foreach(var arg in args )
            {
                var arg1 = arg.Split('=');
                if(arg1.Length > 1)
                {
                    dic[arg1[0]] = arg1[1];
                }
            }

            return dic;
        }

        internal static string GetExePath(this Process process)
        {
            return process.MainModule.FileName;
        }
    }
}
