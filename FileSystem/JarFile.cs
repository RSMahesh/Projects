using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileSystem
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
            this._fileAccessMode = fileAccessMode;
            this.JarFilePath = jarFilePath;
            this._maxFileCount = maxfileCount;

            if (this._fileAccessMode == FileAccessMode.Read)
            {
                this._reader = new Reader(jarFilePath);
            }
            else
            {
                this._writer = new Writer(jarFilePath);
            }
        }

        public string JarFilePath { get; private set; }

        public void AddFile(string fileToAppend)
        {
            if (this._fileAccessMode == FileAccessMode.Read)
            {
                throw new InvalidOperationException("Append File can not be peromed on read mode");
            }

            if (this._writer.GetFileCount() >= this._maxFileCount)
            {
                throw new JarFileReachedMaxLimitException();
            }

            this._writer.AddFile(fileToAppend);
        }

        public int FilesCount
        {
            get
            {
                if (!File.Exists(this.JarFilePath))
                {
                    return 0;
                }

                if (this._reader != null)
                {
                    return this._reader.FileCount;
                }
                else
                {
                    using (var tempReader = new Reader(this.JarFilePath))
                    {
                        return tempReader.FileCount;
                    }
                }
            }
        }

        public byte[] GetNextFile()
        {
            if (this._fileAccessMode == FileAccessMode.Write)
            {
                throw new InvalidOperationException("GetNextImage can not be peromed on write mode");
            }

            return this._reader.GetNextFileBytes();
        }

        public void Dispose()
        {
            if (this._reader != null)
            {
                this._reader.Dispose();
            }
        }

        private class Writer
        {
            private readonly string _logFile;
            public Writer(string logFile)
            {
                this._logFile = logFile;
            }
            public void AddFile(string fileToAppend)
            {
                var fileCount = this.GetFileCount();

                using (BinaryWriter writer = new BinaryWriter(File.Open(this._logFile, FileMode.OpenOrCreate)))
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
                if (File.Exists(this._logFile))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(this._logFile, FileMode.Open, System.IO.FileAccess.Read)))
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
                this._logFile = logFile;
                this.FileCount = this.GetFileCountForReading();
            }

            public int FileCount { get; private set; }
            private int GetFileCountForReading()
            {
                this._reader = new BinaryReader(File.Open(this._logFile, FileMode.Open, System.IO.FileAccess.Read));

                var bytes = this._reader.ReadBytes(FieldSize);
                var fileCount = int.Parse(System.Text.Encoding.UTF8.GetString(bytes));

                return fileCount;
            }
            public byte[] GetNextFileBytes()
            {
                var bytes = this._reader.ReadBytes(FieldSize);
                if (bytes.Count() == 0)
                {
                    return null;
                }

                var result = Encoding.UTF8.GetString(bytes);
                var imageBytes = this._reader.ReadBytes(int.Parse(result.Trim()));
                return imageBytes;
            }
            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        // Free other state (managed objects).
                    }

                    if (this._reader != null)
                    {
                        this._reader.Dispose();
                    }

                    this.disposed = true;
                }
            }
            ~Reader()
            {
                this.Dispose(false);
            }
        }
    }
}
