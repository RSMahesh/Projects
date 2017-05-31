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
    public class JarFile : IJarFile
    {
        readonly FileAccessMode _fileAccessMode;
        readonly Writer _writer;
        readonly Reader _reader;
        const int FieldSize = 10;
        readonly int _maxFileCount;

        public JarFile(FileAccessMode fileAccessMode, string jarFilePath) : this(fileAccessMode, jarFilePath, 100)
        { }

        public JarFile(FileAccessMode fileAccessMode, string jarFilePath, int maxfileCount)
        {
            _fileAccessMode = fileAccessMode;
            JarFilePath = jarFilePath;
            _maxFileCount = maxfileCount;

            if (_fileAccessMode == FileAccessMode.Read)
            {
                _reader = new Reader(jarFilePath);
            }
            else
            {
                _writer = new Writer(jarFilePath);
            }
        }

        public string JarFilePath { get; private set; }

        public void AddFile(string fileToAppend)
        {
            if (_fileAccessMode == FileAccessMode.Read)
            {
                throw new Exception("Append File can not be peromed on read mode");
            }

            if (_writer.GetFileCount() >= _maxFileCount)
            {
                throw new JarFileReachedMaxLimitException();
            }

            _writer.AddFile(fileToAppend);
        }

        public int FilesCount
        {
            get
            {
                if (!File.Exists(JarFilePath))
                {
                    return 0;
                }

                if (_reader != null)
                {
                    return _reader.FileCount;
                }
                else
                {
                    using (var tempReader = new Reader(JarFilePath))
                    {
                        return tempReader.FileCount;
                    }
                }
            }
        }

        public byte[] GetNextFile()
        {
            if (_fileAccessMode == FileAccessMode.Write)
            {
                throw new Exception("GetNextImage can not be peromed on write mode");
            }

            return _reader.GetNextImageBytes();
        }

        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }
        }

        private class Writer
        {
            private readonly string _logFile;
            public Writer(string logFile)
            {
                _logFile = logFile;
            }
            public void AddFile(string fileToAppend)
            {
                var fileCount = GetFileCount();

                using (BinaryWriter writer = new BinaryWriter(File.Open(_logFile, FileMode.OpenOrCreate)))
                {
                    fileCount++;
                    writer.Seek(0, SeekOrigin.Begin);
                    writer.Write(Encoding.ASCII.GetBytes(fileCount.ToString().PadLeft(FieldSize)));
                    writer.Seek(0, SeekOrigin.End);

                    var fileBytes = File.ReadAllBytes(fileToAppend);
                    writer.Write(Encoding.ASCII.GetBytes(fileBytes.Length.ToString().PadLeft(FieldSize)));
                    writer.Write(fileBytes);
                }
            }

            public int GetFileCount()
            {
                if (File.Exists(_logFile))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(_logFile, FileMode.Open, System.IO.FileAccess.Read)))
                    {
                        var bytes = reader.ReadBytes(10);
                        var result = System.Text.Encoding.UTF8.GetString(bytes);
                        return int.Parse(result.Trim());
                    }
                }

                return 0;
            }
        }
        private class Reader : IDisposable
        {
            BinaryReader _reader = null;
            private bool disposed = false;
            private readonly string _logFile;

            public Reader(string logFile)
            {
                _logFile = logFile;
                FileCount = GetFileCountForReading();
            }

            public int FileCount { get; private set; }
            private int GetFileCountForReading()
            {
                _reader = new BinaryReader(File.Open(_logFile, FileMode.Open, System.IO.FileAccess.Read));

                var bytes = _reader.ReadBytes(FieldSize);
                var fileCount = int.Parse(System.Text.Encoding.UTF8.GetString(bytes));

                return fileCount;
            }
            public byte[] GetNextImageBytes()
            {
                var bytes = _reader.ReadBytes(FieldSize);
                if (bytes.Count() == 0)
                {
                    return null;
                }

                var result = Encoding.UTF8.GetString(bytes);
                var imageBytes = _reader.ReadBytes(int.Parse(result.Trim()));
                return imageBytes;
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
            ~Reader()
            {
                Dispose(false);
            }
        }
    }

    public interface IJarFileFactory
    {
        IJarFile GetJarFile(FileAccessMode fileAccess, string logFilePath);
    }

    public class JarFileFactory : IJarFileFactory
    {
        public IJarFile GetJarFile(FileAccessMode fileAccess, string logFilePath)
        {
            return new JarFile(fileAccess, logFilePath);
        }
    }
}
