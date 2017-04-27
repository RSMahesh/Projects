using System;
using System.IO;
using System.Linq;

namespace UserActivityLogger
{
    public class LogFileReader : IDisposable, ILogFileReader
    {
        BinaryReader _reader = null;
        private bool disposed = false;
        private readonly string _logFile;
        public LogFileReader(string logFile)
        {
            _logFile = logFile;
            FileCount = GetFileCountForReading();
        }

        public int FileCount { get; private set; }

        private int GetFileCountForReading()
        {
            _reader = new BinaryReader(File.Open(_logFile, FileMode.Open, FileAccess.Read));

            var bytes = _reader.ReadBytes(10);
            var fileCount = int.Parse(System.Text.Encoding.UTF8.GetString(bytes));

            return fileCount;
        }

        public string GetNextImagePath()
        {
            var bytes = _reader.ReadBytes(10);
            if (bytes.Count() == 0)
            {
                return string.Empty;
            }

            var result = System.Text.Encoding.UTF8.GetString(bytes);
            var bb = _reader.ReadBytes(int.Parse(result.Trim()));
            var filePath = "C:\\Temp\\" + Guid.NewGuid().ToString() + ".jpg";
            File.WriteAllBytes(filePath, bb);
            return filePath;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }

                if (_reader != null)
                {
                    _reader.Dispose();
                }

                disposed = true;
            }
        }

        ~LogFileReader()
        {
            Dispose(false);
        }

    }
}
