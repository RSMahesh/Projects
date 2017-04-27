using Core;
using EventPublisher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserActivityLogger;

namespace UserActivityLogger
{
    public class LogFileAppender
    {
        private readonly string _logFolder;
        private int _fileCount;
        private string _logFilePath;
        private const int MaxFileCount = 100;

        public LogFileAppender(string logFolder)
        {
            _logFolder = logFolder;

            if (!Directory.Exists(_logFolder))
            {
                Directory.CreateDirectory(_logFolder);
            }

            InitNewLogFile();
        }
        public void AppendFile(string file)
        {
            var fileName = GetFileName();

            _fileCount = GetFileCount(fileName);

            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            {
                _fileCount++;

                writer.Seek(0, SeekOrigin.Begin);
                writer.Write(Encoding.ASCII.GetBytes(_fileCount.ToString().PadLeft(10)));
                writer.Seek(0, SeekOrigin.End);
                var fileBytes = File.ReadAllBytes(file);
                writer.Write(Encoding.ASCII.GetBytes(fileBytes.Length.ToString().PadLeft(10)));
                writer.Write(fileBytes);
            }
        }

        private string GetFileName()
        {
            if (_fileCount >= MaxFileCount)
            {
                EventContainer.PublishEvent(Events.LogFileReachedMaxLimit.ToString(), new EventArg(Guid.Empty, _logFilePath));
                InitNewLogFile();
            }

            return _logFilePath;
        }

        private void InitNewLogFile()
        {
            var userFullName = Runtime.GetCurrentUserName();
            _logFilePath = Path.Combine(_logFolder, userFullName) + "_" + Guid.NewGuid().ToString() + ".log";
            _fileCount = 0;
        }

        private int GetFileCount(string fileName)
        {
            if (_fileCount > 0)
            {
                return _fileCount;
            }

            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open, FileAccess.Read)))
                {
                    var bytes = reader.ReadBytes(10);
                    var result = System.Text.Encoding.UTF8.GetString(bytes);
                    return int.Parse(result.Trim());

                }
            }

            return 0;
        }

    }
}
