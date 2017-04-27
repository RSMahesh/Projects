using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core
{
    public class ErrrorLogger
    {
        private static string LogFilePath = Path.Combine(Constants.SharedFolderPath, "ErrorLogs.txt");
        private static object objLock = new object();
        public static void LogError(Exception ex)
        {
            try
            {
                var userFullName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').LastOrDefault();

                LogFilePath = Path.Combine(Constants.SharedFolderPath, userFullName +"_ErrorLogs.txt");

                lock (objLock)
                {
                    File.AppendAllText(LogFilePath, DateTime.UtcNow.ToString() + Environment.NewLine + ex.ToString() + Environment.NewLine);
                }
            }
            catch
            {

            }
        }
    }
}
