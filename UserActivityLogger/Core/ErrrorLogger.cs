using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core
{
    public class Logger
    {
        private static object objLock = new object();
        private static string _logFilePath;
        static  Logger()
        {
            // May add try catch
            var userFullName = RuntimeHelper.GetCurrentUserName().ReverseMe();

            _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SysHealth", userFullName + "_error.log");
           // _logFilePath = RuntimeHelper.MapToCurrentExecutionLocation(userFullName + "_error.inc");
        }

        public static void LogError(Exception ex)
        {
            Write(ex.ToString());
        }

        public static void LogInforamtion(string information)
        {
            Write(information);
        }

        private static void Write(string text)
        {
            try
            {
                lock (objLock)
                {
                    File.AppendAllText(_logFilePath, DateTime.UtcNow.ToString() + Environment.NewLine + text + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
