using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileCopyer
{
    class Copyer
    {
        private readonly string _sourceFolderPath = @"D:\ActivityLogs";
        private readonly string _destinationFolderPath = @"D:\ActivityLogsBackup";
        private bool _running;
        private const int ThreadSleepIntervalInMilliseconds = 1000 * 60 * 2;

        public void WatchFtpFolder(string searchPattern)
        {
            while (true)
            {
                var files = Directory.GetFiles(_sourceFolderPath, searchPattern, SearchOption.AllDirectories);

                ProcessFiles(files);

                Thread.Sleep(ThreadSleepIntervalInMilliseconds);
            }
        }

        private void ProcessFiles(IEnumerable<string> files)
        {
            var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = 32 };
            Parallel.ForEach(files, parallelOptions, file =>
            {
                try
                {
                    ProcessFile(file);
                }
                catch (Exception ex)
                {
                    // _logger.Write(LogEventKind.Error, string.Format("File {0} Unhandled Error {1}", file, ex));
                }
            });
        }

        private void ProcessFile(string file)
        {
            if (IsFileLocked(file))
            {
                return;
            }

            MoveFileToAnotherFolderAndReturnFilePath(file, _destinationFolderPath);
        }

        private void MoveFileToAnotherFolderAndReturnFilePath(string sourceFilePath, string destinationFolder)
        {
            var destinationFilePath = sourceFilePath.Replace(_sourceFolderPath, _destinationFolderPath);
            
            if(!Directory.Exists(Path.GetDirectoryName( destinationFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destinationFilePath));
            }

                if (File.Exists(destinationFilePath))
                {
                    File.Delete(destinationFilePath);
                }

            File.Move(sourceFilePath, destinationFilePath);
        }

        public bool IsFileLocked(string file)
        {
            try
            {
                using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    fs.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }

    }
}
