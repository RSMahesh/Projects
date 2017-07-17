using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    public partial class JarFile : IJarFileReader
    {
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
                var headers = ReadHeaders();

                bytes = _reader.ReadBytes(FileLengthFieldSize);

                if (bytes.Count() == 0)
                {
                    throw new EndOfJarFileException();
                }

                var imageBytes = _reader.ReadBytes(int.Parse(Encoding.UTF8.GetString(bytes).Trim()));

                return new JarFileItem(headers, imageBytes, offset);
            }

            private Dictionary<string, string> ReadHeaders()
            {
                var headers = new Dictionary<string, string>();

                if (!_oldFormat)
                {
                    var bytes = _reader.ReadBytes(HeaderFieldSize);

                    if (bytes.Count() == 0)
                    {
                        throw new EndOfJarFileException();
                    }

                    headers = stringToDic(Encoding.UTF8.GetString(bytes).Trim());
                }

                return headers;
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

                if (arr.Length > 1)
                {

                    foreach (var ar in arr)
                    {
                        var KeyValue = ar.Split('=');

                        dic[KeyValue[0]] = KeyValue[1];
                    }
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
