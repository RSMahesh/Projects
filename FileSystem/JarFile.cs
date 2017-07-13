using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileSystem
{
    public partial class JarFile : IJarFileWriter, IJarFileReader
    {
        readonly FileAccessMode _fileAccessMode;
        readonly Writer _writer;
        readonly Reader _reader;
        const int FileCountFieldSize = 10;
        const int FileLengthFieldSize = 10;
        const int HeaderFieldSize = 500;
        readonly int _maxFileCount;
        bool _oldFormat;

        public JarFile(FileAccessMode fileAccessMode, string jarFilePath) : this(fileAccessMode, jarFilePath, 50)
        { }

        public JarFile(FileAccessMode fileAccessMode, string jarFilePath, int maxfileCount)
        {
            _fileAccessMode = fileAccessMode;
            JarFilePath = jarFilePath;
            _maxFileCount = maxfileCount;

            if (Path.GetExtension(jarFilePath) == ".log")
            {
                _oldFormat = true;
            }

            if (_fileAccessMode == FileAccessMode.Read)
            {
                _reader = new Reader(jarFilePath, _oldFormat);
            }
            else
            {
                _writer = new Writer(jarFilePath);
            }

        }

        public string JarFilePath { get; private set; }

        public void AddFile(JarFileItem jarFileItem)
        {
            if (_fileAccessMode == FileAccessMode.Read)
            {
                throw new InvalidOperationException("Append File can not be peromed on read mode");
            }

            if (_writer.GetFileCount() >= _maxFileCount)
            {
                throw new JarFileReachedMaxLimitException();
            }

            _writer.AddFile(jarFileItem);
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
                    using (var tempReader = new Reader(JarFilePath, _oldFormat))
                    {
                        return tempReader.FileCount;
                    }
                }
            }
        }

        public JarFileItem GetNextFile()
        {
            if (_fileAccessMode == FileAccessMode.Write)
            {
                throw new InvalidOperationException("GetNextImage can not be peromed on write mode");
            }

            return _reader.GetNextFile();
        }

        public long GetNextFileOffset()
        {
            return _reader.GetNextFileOffset();
        }
        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Dispose();
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
                _logFile = logFile;

                var rootDir = Path.GetDirectoryName(logFile);

                if (!Directory.Exists(rootDir))
                {
                    Directory.CreateDirectory(rootDir);
                }

            }
            public void AddFile(string fileToAppend)
            {
                var fileCount = GetFileCount();

                using (BinaryWriter writer = new BinaryWriter(File.Open(_logFile, FileMode.OpenOrCreate)))
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
                var fileCount = GetFileCount();

                using (BinaryWriter writer = new BinaryWriter(File.Open(_logFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)))
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

                    //var fileBytes = File.ReadAllBytes(jarFileItem.FilePath);
                    var fileBytes = jarFileItem.Containt;
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
                if (File.Exists(_logFile))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(_logFile, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite)))
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
                _logFile = logFile;
                _oldFormat = oldFormat;
                FileCount = GetFileCountForReading();
            }
            public int FileCount { get; private set; }
            private int GetFileCountForReading()
            {
                _reader = new BinaryReader(File.Open(_logFile, FileMode.Open, System.IO.FileAccess.Read));

                var bytes = _reader.ReadBytes(FileCountFieldSize);
                var fileCount = int.Parse(System.Text.Encoding.UTF8.GetString(bytes));

                return fileCount;
            }

            public JarFileItem GetNextFile()
            {
                var offset = _reader.BaseStream.Position;
                byte[] bytes;
                var headers = new Dictionary<string, string>();

                if (!_oldFormat)
                {
                    bytes = _reader.ReadBytes(HeaderFieldSize);
                    if (bytes.Count() == 0)
                    {
                        return JarFileItem.Empty;
                    }

                    headers = stringToDic(Encoding.UTF8.GetString(bytes).Trim());
                }

                bytes = _reader.ReadBytes(FileLengthFieldSize);

                if (bytes.Count() == 0)
                {
                    return JarFileItem.Empty;
                }

                var imageBytes = _reader.ReadBytes(int.Parse(Encoding.UTF8.GetString(bytes).Trim()));

                return new JarFileItem(headers, imageBytes, offset);

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
                    _reader.BaseStream.Position = _reader.BaseStream.Position + HeaderFieldSize;
                }

                var bytes = _reader.ReadBytes(FileLengthFieldSize);

                if (bytes.Count() == 0)
                {
                    return -1;
                }

                var fileLength = int.Parse(Encoding.UTF8.GetString(bytes).Trim());

                _reader.BaseStream.Position = _reader.BaseStream.Position + fileLength;

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
}
