using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using FileSystem;
using Core;

namespace UserActivityLogger
{
    public class ActivitesEnumerator : IEnumerator<Activity>, IDisposable
    {
        private readonly string _logFolder;
        private readonly IJarFileFactory _jarFileFactory = null;
        private List<FileInfo> _fileInfos;
        private int _fileIndex;
        IJarFileReader _jarFileReader = null;
        List<int> imagesInLogFiles = new List<int>();

        private Activity _currentActivity;
        private Dictionary<int, FileItemInfo> _fileOffsetInfoMap = new Dictionary<int, FileItemInfo>();

        public int FileCount { get; private set; }

        public ActivitesEnumerator(string logFolderPath, IJarFileFactory jarFileFactory, ActivityQueryFilter filter)
        {
            this._jarFileFactory = jarFileFactory;
            this._logFolder = logFolderPath;
            this._fileIndex = 0;

          
            this._fileInfos = new DirectoryInfo(this._logFolder).GetFiles().Where(s => s.FullName.EndsWith(".jar") || s.FullName.EndsWith(".log"))
                .OrderBy(f => f.LastWriteTime)
                .ToList();

            this.FileCount = this.GetFileCount();

        }


        public ActivityQueryFilter Filter { private get; set; }

        public Activity Current
        {
            get
            {
                return this._currentActivity;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public bool MoveNext()
        {
            return this.GetNextFile();
        }

        public void Reset()
        {
            this.MoveIndexToPostion(0);
        }

        public void ChangePostionOld(int positionNumber)
        {
            int imagePositionInFile = this.GetImagePositionInLogFileAndSetFileIndex(positionNumber);

            if (this._jarFileReader != null)
            {
                this._jarFileReader.Dispose();
            }

            this._jarFileReader = this._jarFileFactory.GetJarFileReader(this._fileInfos[this._fileIndex].FullName);

            this.MoveIndexToPostion(imagePositionInFile);
        }

        public void ChangePostion(int positionNumber)
        {
            int imagePositionInFile = this.GetImagePositionInLogFileAndSetFileIndex(positionNumber);

            var fileItemInfo = _fileOffsetInfoMap[positionNumber];
            
            if (this._jarFileReader != null && this._jarFileReader.JarFilePath != fileItemInfo.JarFilePath)
            {
                this._jarFileReader.Dispose();
                this._jarFileReader = this._jarFileFactory.GetJarFileReader(fileItemInfo.JarFilePath);
            }

            _jarFileReader.MoveFileHeader(fileItemInfo.OffSetInJarFile);
        }

        public void Dispose()
        {
            if (this._jarFileReader != null)
            {
                this._jarFileReader.Dispose();
            }
        }

        private int GetFileCount()
        {
            var fileCount = 0;

            this.imagesInLogFiles = new List<int>();


            foreach (var file in this._fileInfos)
            {
                using (var logFileReader = this._jarFileFactory.GetJarFileReader(file.FullName))
                {
                    //fileCount += logFileReader.FilesCount;

                    var offset = logFileReader.GetNextFileOffset();

                    while (offset != -1)
                    {
                        fileCount++;
                        _fileOffsetInfoMap[fileCount] = new FileItemInfo(file.FullName, offset, fileCount);
                        offset = logFileReader.GetNextFileOffset();
                    }

                    this.imagesInLogFiles.Add(logFileReader.FilesCount);
                }
            }

            return fileCount;
        }

        private bool GetNextFile()
        {
            if (this._fileIndex >= this._fileInfos.Count)
            {
                this._currentActivity = null;
            }

            if (this._jarFileReader == null)
            {
                this._jarFileReader = this._jarFileFactory.GetJarFileReader(this._fileInfos[this._fileIndex].FullName);
            }

            var file = this._jarFileReader.GetNextFile();

            if (file == null)
            {
                if (this._fileIndex + 1 >= this.imagesInLogFiles.Count)
                {
                    return false;
                }

                this._fileIndex++;
                this._jarFileReader.Dispose();
                this._jarFileReader = null;
                this.GetNextFile();
                return true;
            }

            this._currentActivity = this.BytesToActivity(file.Containt);
            return true;
        }

        private Activity BytesToActivity(byte[] imageBytes)
        {
            Activity activity = null;

            using (var fs = new MemoryStream(imageBytes, false))
            {
                return activity = new Activity(Image.FromStream(fs), this.GetComments(fs));
            }
        }

        private string GetComments(Stream stream)
        {
            if (stream == null)
                return string.Empty;

            return new ImageCommentEmbedder().GetComments(stream);
        }

        private void MoveIndexToPostion(int imagePositionInFile)
        {
            var ind = 0;

            while (ind < imagePositionInFile)
            {
                this._jarFileReader.GetNextFile();
                ind++;
            }
        }

        private int GetImagePositionInLogFileAndSetFileIndex(int positionNumber)
        {
            int count = 0;
            var imagePositionInFile = -1;

            for (var i = 0; i < this.imagesInLogFiles.Count; i++)
            {
                if (count + this.imagesInLogFiles[i] >= positionNumber)
                {
                    this._fileIndex = i;
                    imagePositionInFile = positionNumber - count;
                    break;
                }
                count += this.imagesInLogFiles[i];
            }
            return imagePositionInFile;
        }

        private int GetImagePositionInLogFileAndSetFileIndexExt(int positionNumber)
        {
            int count = 0;
            var imagePositionInFile = -1;

            for (var i = 0; i < this.imagesInLogFiles.Count; i++)
            {
                if (count + this.imagesInLogFiles[i] >= positionNumber)
                {
                    this._fileIndex = i;
                    imagePositionInFile = positionNumber - count;
                    break;
                }
                count += this.imagesInLogFiles[i];
            }
            return imagePositionInFile;
        }

        //Future used methods
        private void FilterOutFiles(ActivityQueryFilter filter)
        {
            const int FiltersCount = 2;
            if (filter != null)
            {
                var filterPassed = 0;
                for (var i = 0; i < this._fileInfos.Count; i++)
                {
                    if (filter.StartDateTime.HasValue &&
                        this._fileInfos[i].LastWriteTime >= filter.StartDateTime.Value)
                    {
                        filterPassed++;
                    }

                    if (filter.EndDateTime.HasValue &&
                        this._fileInfos[i].LastWriteTime <= filter.EndDateTime.Value)
                    {
                        filterPassed++;
                    }

                    if (filterPassed == FiltersCount)
                    {

                    }
                }
            }
        }


        private class FileItemInfo
        {
            public FileItemInfo(string jarFilePath, long offSetInJarFile, int index)
            {
                JarFilePath = jarFilePath;
                OffSetInJarFile = offSetInJarFile;
                Index = index;
            }
            public string JarFilePath { get; private set; }
            public long OffSetInJarFile { get; private set; }
            public int Index { get; private set; }
        }

    }








}