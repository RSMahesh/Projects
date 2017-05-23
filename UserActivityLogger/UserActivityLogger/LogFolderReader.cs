using Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class LogFolderReader : ILogFolderReader
    {
        private string _logFolder;
        private List<FileInfo> _fileInfos;
        private int _fileIndex;
        LogFileReader _logFileReader = null;
        List<int> imagesInLogFiles = new List<int>();
       
        public LogFolderReader()
        {
           
        }
        public void SetLogFolderPath(string logFolderPath)
        {
            _fileIndex = 0;
            _logFolder = logFolderPath;
            _fileInfos = new DirectoryInfo(_logFolder).GetFiles("*.log")
                                                            .OrderBy(f => f.LastWriteTime)
                                                            .ToList();
        }
        public int GetFileCountForReading()
        {
            int fileCount = 0;

            imagesInLogFiles = new List<int>();

            foreach (var file in _fileInfos)
            {
                using (var logFileReader = new LogFileReader(file.FullName))
                {
                    fileCount += logFileReader.FileCount;
                    imagesInLogFiles.Add(logFileReader.FileCount);
                }
            }

            return fileCount;
        }

        public byte[] GetNextImageBytes()
        {
            if (_fileIndex >= _fileInfos.Count)
            {
                return null;
            }

            if (_logFileReader == null)
            {
                _logFileReader = new LogFileReader(_fileInfos[_fileIndex].FullName);
            }

            var image = _logFileReader.GetNextImageBytes();

            if (image == null)
            {
                _fileIndex++;
                _logFileReader.Dispose();
                _logFileReader = null;
                return GetNextImageBytes();
            }

            return image;
        }

        public void Dispose()
        {
            if (_logFileReader != null)
            {
                _logFileReader.Dispose();
            }
        }

        public void ChangeNextImagePostion(int positionNumber)
        {
            int imagePositionInFile = GetImagePositionInLogFileAndSetFileIndex(positionNumber);

            if (_logFileReader != null)
            {
                _logFileReader.Dispose();
            }

            _logFileReader = new LogFileReader(_fileInfos[_fileIndex].FullName);

            MoveIndexToPostion(imagePositionInFile);
        }

        private void MoveIndexToPostion(int imagePositionInFile)
        {
            var ind = 0;

            while (ind < imagePositionInFile)
            {
                _logFileReader.GetNextImageBytes();
                ind++;
            }

        }

        private int GetImagePositionInLogFileAndSetFileIndex(int positionNumber)
        {
            int count = 0;
            var imagePositionInFile = -1;

            for (var i = 0; i < imagesInLogFiles.Count; i++)
            {

                if (count + imagesInLogFiles[i] >= positionNumber)
                {
                    _fileIndex = i;
                    imagePositionInFile = positionNumber - count;
                    break;
                }

                count += imagesInLogFiles[i];
            }

            return imagePositionInFile;
        }

        private class LogFileReader : IDisposable
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
     
            public byte[] GetNextImageBytes()
            {
                var bytes = _reader.ReadBytes(10);
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
            ~LogFileReader()
            {
                Dispose(false);
            }
        }
    }
}
