using Core;
using EventPublisher;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class LogFolderPurger
    {
        private readonly string _sharedCopyLocation;

        public LogFolderPurger()
        {
            _sharedCopyLocation = Path.Combine(Constants.SharedFolderPath, Runtime.GetCurrentUserName());
            Init();

            EventContainer.SubscribeEvent(Events.LogFileReachedMaxLimit.ToString(), OnNewLogFileCreated);
        }
        public void StartPurging(string logFolder, TimeSpan pollingTimeInterval)
        {
            new Thread(() =>
             {
                 while (true)
                 {
                     try
                     {
                         PurgeFiles(logFolder);
                     }
                     catch (Exception ex)
                     {
                         ErrrorLogger.LogError(ex);
                     }

                     Thread.Sleep(pollingTimeInterval);
                 }
             }).Start();
        }

        private void PurgeFiles(string logFolder)
        {
            var fileInfos = new DirectoryInfo(logFolder).GetFiles("*.log")
                                                                  .OrderBy(f => f.LastWriteTime)
                                                                  .ToList();

            CopyCurrentFileIfUpdated(fileInfos.LastOrDefault());

            var deleteLogsBeforeInDays = ConfigurationManager.AppSettings["DeleteLogsBeforeInDays"] ?? "3";

            var deleteBeforeDate = DateTime.UtcNow.AddDays(-int.Parse(deleteLogsBeforeInDays));

            for (var i = 0; i < fileInfos.Count - 30; i++)
            {
                if (fileInfos[i].LastWriteTime < deleteBeforeDate)
                {
                    //CopyFile(fileInfos[i].FullName);
                    File.Delete(fileInfos[i].FullName);
                }
            }
        }
        private void CopyCurrentFileIfUpdated(FileInfo currentFile)
        {
            if (currentFile.LastWriteTime > DateTime.Now.AddMinutes(-6))
            {
                CopyFile(currentFile.FullName);
            }
        }
        private void CopyFile(string sourceFile)
        {
            try
            {
                var targetFile = Path.Combine(_sharedCopyLocation, Path.GetFileName(sourceFile));

                if (File.Exists(targetFile))
                {
                    File.Delete(targetFile);
                }

                File.Copy(sourceFile, Path.Combine(_sharedCopyLocation, targetFile));
                return;
            }
            catch (Exception ex)
            {
                ErrrorLogger.LogError(ex);
            }

            try
            {
                File.Copy(sourceFile, Path.Combine(_sharedCopyLocation, Guid.NewGuid().ToString() + "_" + Path.GetFileName(sourceFile)));
            }
            catch (Exception ex)
            {
                ErrrorLogger.LogError(ex);
            }

        }

        private void Init()
        {
            try
            {
                FileSystem.CreateDirectoryIfNotExist(_sharedCopyLocation);
            }
            catch (Exception ex)
            {
                ErrrorLogger.LogError(ex);
            }
        }

        private void OnNewLogFileCreated(EventArg eventArg)
        {
            var logFile = eventArg.Arg.ToString();
            CopyFile(logFile);
        }
    }
}
