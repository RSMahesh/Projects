using AppUpdater.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                if(args[0].Equals( "RunWithoutIsUpdateRequiredCheck", StringComparison.OrdinalIgnoreCase))
                {
                    RunWithoutCheck();
                    return;
                }
            }

            CheckIsUpdateRequired();
        }

        private static void CheckIsUpdateRequired()
        {
            File.AppendAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "output.txt"),
            "Local" + Environment.NewLine);

            var startup = GetStartUpInstance();

            if (startup.IsUpdateRequired())
            {
                startup.LaunchProcess();
                Thread.Sleep(TimeSpan.FromSeconds(3));
                return;
            }

           
            EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset, "FunctionalTestAppUpdater");
            handle.Set();
        }

        private static void RunWithoutCheck()
        {

            File.AppendAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "output.txt"),
                "Local" + Environment.NewLine);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = RuntimeHelper.MapToCurrentLocation("Updater.exe");
            Process.Start(startInfo);
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }

        private static dynamic GetStartUpInstance()
        {
            //Assembly assembly = Assembly.LoadFrom("Updater.exe");
            Assembly assembly = Assembly.LoadFrom(@"D:\Projects\AppUpdater\TestApp\bin\Debug\Updater.exe");
            Type type = assembly.GetType("AppUpdater.Host.StartUp");

            dynamic startUp = Activator.CreateInstance(type);

            return startUp;
        }
    }
}
