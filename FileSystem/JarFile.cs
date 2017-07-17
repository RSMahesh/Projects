using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileSystem
{
    public partial class JarFile 
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
    }
}
