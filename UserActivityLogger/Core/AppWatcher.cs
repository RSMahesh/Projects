using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

            var parentProcess = Process.GetCurrentProcess().GetParentProcess();

            ProcessHelper.WatchProcesExit(parentProcess, parentProcess.GetExePath());
        }
    
    }
}