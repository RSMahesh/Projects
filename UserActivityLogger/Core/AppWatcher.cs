using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class AppWatcher
    {
        static void Main(string[] args)
        {
            if (SingleInstance.IsApplicationAlreadyRunning("AppWatcherCore"))
            {
                return;
            }

            var process = ProcessHelper.GetParentProcess();

            ProcessHelper.WatchProcesExit(process, ProcessHelper.GetExePath(process));
        }
    }
}