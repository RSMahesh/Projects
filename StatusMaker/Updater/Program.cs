using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            Update();
        }

        private static void Update()
        {
            string rootPath = @"\\10.131.40.102\StatusMaker\ExecutionFolder";

            var files = Directory.GetFiles(rootPath);

            string executionFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            KillProcess();

            foreach (var file in files)
            {

                if (!Path.GetFileName(file).Equals("Updater.exe", StringComparison.OrdinalIgnoreCase))
                {
                    var destenatiuonFile = Path.Combine(executionFolder, Path.GetFileName(file));
                    File.Copy(file, destenatiuonFile, true);
                }
            }

            Process.Start(Path.Combine(executionFolder, "StatusMaker.exe"));
        }

        private static void KillProcess()
        {
            foreach (var process in Process.GetProcessesByName("StatusMaker"))
            {
                process.Kill();
            }
        }
    }
}
