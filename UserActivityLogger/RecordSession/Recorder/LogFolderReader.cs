using Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserActivityLogger;

namespace RecordSession
{
    public class LogFolderReader : ILogFolderReader
    {
        private string _logFolder;
        private List<FileInfo> _fileInfos;
        private int _fileIndex;
        IJarFile _jarFile = null;
        IJarFileFactory _jarFileFactory = null;
        List<int> imagesInLogFiles = new List<int>();
       
        public LogFolderReader()
        {
            _jarFileFactory = new JarFileFactory();
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
                using (var logFileReader = _jarFileFactory.GetJarFile(FileAccessMode.Read, file.FullName))
                {
                    fileCount += logFileReader.FilesCount;
                    imagesInLogFiles.Add(logFileReader.FilesCount);
                }
            }

            return fileCount;
        }

        public byte[] GetNextImage()
        {
            if (_fileIndex >= _fileInfos.Count)
            {
                return null;
            }

            if (_jarFile == null)
            {
                _jarFile = _jarFileFactory.GetJarFile(FileAccessMode.Read,_fileInfos[_fileIndex].FullName);
            }

            var image = _jarFile.GetNextFile();

            if (image == null)
            {
                _fileIndex++;
                _jarFile.Dispose();
                _jarFile = null;
                return GetNextImage();
            }

            return image;
        }

        public void Dispose()
        {
            if (_jarFile != null)
            {
                _jarFile.Dispose();
            }
        }

        public void ChangeNextImagePostion(int positionNumber)
        {
            int imagePositionInFile = GetImagePositionInLogFileAndSetFileIndex(positionNumber);

            if (_jarFile != null)
            {
                _jarFile.Dispose();
            }

            _jarFile =  _jarFileFactory.GetJarFile(FileAccessMode.Read, _fileInfos[_fileIndex].FullName);

            MoveIndexToPostion(imagePositionInFile);
        }

        private void MoveIndexToPostion(int imagePositionInFile)
        {
            var ind = 0;

            while (ind < imagePositionInFile)
            {
                _jarFile.GetNextFile();
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
    }
}
