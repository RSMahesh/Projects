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
    public class LogFileArchiver
    {
        private readonly string _archiveLocation;
        private readonly IFileSystem _fileSystem;
        public LogFileArchiver(IFileSystem fileSystem, string archiveLocation)
        {
            _archiveLocation = archiveLocation;
            _fileSystem = fileSystem;
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

                var targetFile = Path.Combine(_archiveLocation, Path.GetFileName(sourceFile));

                _fileSystem.DeleteFileIfExist(targetFile);

                _fileSystem.CopyFile(sourceFile, targetFile);

                return;
            }
            catch (Exception ex)
            {
                ErrrorLogger.LogError(ex);
            }

            try
            {
                _fileSystem.CopyFile(sourceFile, Path.Combine(_archiveLocation, Guid.NewGuid().ToString() + "_" + Path.GetFileName(sourceFile)));
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
                _fileSystem.CreateDirectoryIfNotExist(_archiveLocation);
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
