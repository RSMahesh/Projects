using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileSystem
{
    public class JarFile : IJarFileWriter, IJarFileReader
    {
        readonly FileAccessMode _fileAccessMode;
        readonly Writer _writer;
        readonly Reader _reader;
        const int FileCountFieldSize = 10;
        const int FileLengthFieldSize = 10;
        const int HeaderFieldSize = 500;
        readonly int _maxFileCount;
        bool _oldFormat;

        public JarFile(FileAccessMode fileAccessMode, string jarFilePath) : this(fileAccessMode, jarFilePath, 100)
        { }

        public JarFile(FileAccessMode fileAccessMode, string jarFilePath, int maxfileCount)
        {
            this._fileAccessMode = fileAccessMode;
            this.JarFilePath = jarFilePath;
            this._maxFileCount = maxfileCount;

            if (Path.GetExtension(jarFilePath) == ".log")
            {
                _oldFormat = true;
            }

            if (this._fileAccessMode == FileAccessMode.Read)
            {
                this._reader = new Reader(jarFilePath, _oldFormat);
            }
            else
            {
                this._writer = new Writer(jarFilePath);
            }


        }

        public string JarFilePath { get; private set; }

        public void AddFileObselete(string fileToAppend)
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

        public void AddFile(JarFileItem jarFileItem)
        {
            if (this._fileAccessMode == FileAccessMode.Read)
            {
                throw new InvalidOperationException("Append File can not be peromed on read mode");
            }

            if (this._writer.GetFileCount() >= this._maxFileCount)
            {
                throw new JarFileReachedMaxLimitException();
            }

            this._writer.AddFile(jarFileItem);
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
                    using (var tempReader = new Reader(this.JarFilePath, _oldFormat))
                    {
                        return tempReader.FileCount;
                    }
                }
            }
        }

        public JarFileItem GetNextFile()
        {
            if (this._fileAccessMode == FileAccessMode.Write)
            {
                throw new InvalidOperationException("GetNextImage can not be peromed on write mode");
            }

            return this._reader.GetNextFile();
        }

        public long GetNextFileOffset()
        {
            return _reader.GetNextFileOffset();
        }
        public void Dispose()
        {
            if (this._reader != null)
            {
                this._reader.Dispose();
            }
        }

        public void MoveFileHeader(long position)
        {
            _reader.MoveFileHeader(position);
        }

        private class Writer
        {
            private readonly string _logFile;
            public Writer(string logFile)
            {
                this._logFile = logFile;

                var rootDir = Path.GetDirectoryName(logFile);

                if (!Directory.Exists(rootDir))
                {
                    Directory.CreateDirectory(rootDir);
                }

            }
            public void AddFile(string fileToAppend)
            {
                var fileCount = this.GetFileCount();

                using (BinaryWriter writer = new BinaryWriter(File.Open(this._logFile, FileMode.OpenOrCreate)))
                {
                    fileCount++;
                    writer.Seek(0, SeekOrigin.Begin);
                    writer.Write(Encoding.ASCII.GetBytes(fileCount.ToString().PadLeft(FileCountFieldSize)));
                    writer.Seek(0, SeekOrigin.End);

                    var fileBytes = File.ReadAllBytes(fileToAppend);
                    writer.Write(Encoding.ASCII.GetBytes(fileBytes.Length.ToString().PadLeft(FileLengthFieldSize)));
                    writer.Write(fileBytes);
                }
            }

            public void AddFile(JarFileItem jarFileItem)
            {
                var fileCount = this.GetFileCount();

                using (BinaryWriter writer = new BinaryWriter(File.Open(this._logFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)))
                {
                    fileCount++;
                    writer.Seek(0, SeekOrigin.Begin);
                    writer.Write(Encoding.ASCII.GetBytes(fileCount.ToString().PadLeft(FileCountFieldSize)));
                    writer.Seek(0, SeekOrigin.End);

                    var headerString = DicToString(jarFileItem.Headers);

                    var headerBytes = Encoding.ASCII.GetBytes(headerString.PadLeft(HeaderFieldSize));
                    if (headerBytes.Count() > HeaderFieldSize)
                    {
                        throw new Exception("Header out of limit");
                    }

                    writer.Write(headerBytes);
                    writer.Seek(0, SeekOrigin.End);

                    var fileBytes = File.ReadAllBytes(jarFileItem.FilePath);
                    writer.Write(Encoding.ASCII.GetBytes(fileBytes.Length.ToString().PadLeft(FileLengthFieldSize)));
                    writer.Write(fileBytes);
                }
            }


            private string DicToString(Dictionary<string, string> dic)
            {
                return string.Join(";", dic.Select(x => x.Key + "=" + x.Value).ToArray());
            }
            public int GetFileCount()
            {
                if (File.Exists(this._logFile))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(this._logFile, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite)))
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
            private bool _oldFormat;
            private readonly string _logFile;

            public Reader(string logFile, bool oldFormat)
            {
                this._logFile = logFile;
                _oldFormat = oldFormat;
                this.FileCount = this.GetFileCountForReading();
            }

            public int FileCount { get; private set; }
            private int GetFileCountForReading()
            {
                this._reader = new BinaryReader(File.Open(this._logFile, FileMode.Open, System.IO.FileAccess.Read));

                var bytes = this._reader.ReadBytes(FileCountFieldSize);
                var fileCount = int.Parse(System.Text.Encoding.UTF8.GetString(bytes));

                return fileCount;
            }


            public JarFileItem GetNextFile()
            {
                var offset = this._reader.BaseStream.Position;
                byte[] bytes;
                Dictionary<string, string> headers = new Dictionary<string, string>();

                if (!_oldFormat)
                {
                    bytes = this._reader.ReadBytes(HeaderFieldSize);
                    if (bytes.Count() == 0)
                    {
                        return null;
                    }

                    headers = stringToDic(Encoding.UTF8.GetString(bytes).Trim());
                }

                bytes = _reader.ReadBytes(FileLengthFieldSize);

                if (bytes.Count() == 0)
                {
                    return null;
                }

                var imageBytes = _reader.ReadBytes(int.Parse(Encoding.UTF8.GetString(bytes).Trim()));

                return new JarFileItem(headers, string.Empty, imageBytes, offset);

            }

            public long GetNextFileOffset()
            {
                var currentFileOffset = _reader.BaseStream.Position;

                if (_reader.BaseStream.Position >= _reader.BaseStream.Length - 2)
                {
                    return -1;
                }

                if (!_oldFormat)
                {
                    this._reader.BaseStream.Position = this._reader.BaseStream.Position + HeaderFieldSize;
                }

                var bytes = _reader.ReadBytes(FileLengthFieldSize);

                if (bytes.Count() == 0)
                {
                    return -1;
                }

                var fileLength = int.Parse(Encoding.UTF8.GetString(bytes).Trim());

                this._reader.BaseStream.Position = this._reader.BaseStream.Position + fileLength;

                return currentFileOffset;
            }

            public void MoveFileHeader(long position)
            {
                _reader.BaseStream.Position = position;
            }

            private Dictionary<string, string> stringToDic(string text)
            {
                var arr = text.Split(';');
                var dic = new Dictionary<string, string>();
                foreach (var ar in arr)
                {
                    var KeyValue = ar.Split('=');

                    dic[KeyValue[0]] = KeyValue[1];
                }


                return dic;

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
