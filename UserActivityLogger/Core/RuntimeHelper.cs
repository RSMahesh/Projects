using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RuntimeHelper
    {
        public static string GetCurrentUserName()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').LastOrDefault();
        }

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
                return Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            }
        }

        public static string MapToCurrentExecutionLocation(string filePath)
        {
            return Path.Combine(ExecutionLocation, filePath);
        }

        public static string MapToTempFolder(string filePath)
        {
            return Path.Combine(Path.GetTempPath(), filePath);
        }

        private void HasWriteAccessToFolder(string _dataFolder)
        {
            try
            {
                var ds = Directory.GetAccessControl(_dataFolder);

                var testFile = Path.Combine(_dataFolder, "test.temp");

                File.WriteAllText(Path.Combine(_dataFolder, "test.temp"), "test");

                File.Delete(testFile);
            }
            catch (Exception ex)
            {
                _dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                _dataFolder = Path.Combine(_dataFolder, "SysLogs");
            }
        }
    }
}
