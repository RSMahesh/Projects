using Core;
using EventPublisher;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;

namespace UserActivityLogger
{
    public class LogFileArchiver
    {
        private readonly string _archiveLocation;
        private readonly IFileSystem _fileSystem;
        private string _lastCopyedFileName;
        private DateTime _lastModifiedTime;
        public LogFileArchiver(IFileSystem fileSystem, string archiveLocation)
        {
            _archiveLocation = archiveLocation;
            _fileSystem = fileSystem;
            _lastCopyedFileName = string.Empty;

            Init();

            EventContainer.SubscribeEvent(Events.LogFileReachedMaxLimit.ToString(), OnNewLogFileCreated);
        }
        public void Start(string logFolder, TimeSpan pollingTimeInterval)
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
                         Logger.LogError(ex);
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

            const int KeepLatestFileCount = 30;

            for (var i = 0; i < fileInfos.Count - KeepLatestFileCount; i++)
            {
                if (fileInfos[i].LastWriteTime < deleteBeforeDate)
                {
                    File.Delete(fileInfos[i].FullName);
                }
            }
        }
        private void CopyCurrentFileIfUpdated(FileInfo currentFile)
        {
            if (!_lastCopyedFileName.Equals(currentFile.Name, StringComparison.OrdinalIgnoreCase) || currentFile.LastWriteTime > _lastModifiedTime)
            {
                CopyFile(currentFile.FullName);
                _lastCopyedFileName = currentFile.Name;
                _lastModifiedTime = currentFile.LastWriteTime;
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
                Logger.LogError(ex);
                TryCopyFileWithNewUniueName(sourceFile);
            }
        }
        private void TryCopyFileWithNewUniueName(string sourceFile)
        {
            try
            {
                _fileSystem.CopyFile(sourceFile, Path.Combine(_archiveLocation, Guid.NewGuid().ToString() + "_" + Path.GetFileName(sourceFile)));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
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
                Logger.LogError(ex);
            }
        }
        private void OnNewLogFileCreated(EventArg eventArg)
        {
            var logFile = eventArg.Arg.ToString();
            CopyFile(logFile);
        }
    }
}
