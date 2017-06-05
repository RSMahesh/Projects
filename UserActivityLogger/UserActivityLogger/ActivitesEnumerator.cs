using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using FileSystem;

namespace UserActivityLogger
{
    public class ActivitesEnumerator : IEnumerator<Activity>, IDisposable
    {
        private readonly string _logFolder;
        private readonly IJarFileFactory _jarFileFactory = null;
        private List<FileInfo> _fileInfos;
        private int _fileIndex;
        IJarFile _jarFile = null;
        List<int> imagesInLogFiles = new List<int>();
        private Activity _currentActivity;

        public int FileCount { get; private set; }

        public ActivitesEnumerator(string logFolderPath, IJarFileFactory jarFileFactory, ActivityQueryFilter filter)
        {
            this._jarFileFactory = jarFileFactory;
            this._logFolder = logFolderPath;
            this._fileIndex = 0;
            this._fileInfos = new DirectoryInfo(this._logFolder).GetFiles("*.log")
                .OrderBy(f => f.LastWriteTime)
                .ToList();

            this.FileCount = this.SetFileCount();

        }

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

                    if(filterPassed == FiltersCount)
                    {

                    }
                }
            }
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
            return this.GetNextImage();
        }

        public void Reset()
        {
            this.MoveIndexToPostion(0);
        }

        public void ChangePostion(int positionNumber)
        {
            int imagePositionInFile = this.GetImagePositionInLogFileAndSetFileIndex(positionNumber);

            if (this._jarFile != null)
            {
                this._jarFile.Dispose();
            }

            this._jarFile = this._jarFileFactory.GetJarFile(FileAccessMode.Read, this._fileInfos[this._fileIndex].FullName);

            this.MoveIndexToPostion(imagePositionInFile);
        }

        public void Dispose()
        {
            if (this._jarFile != null)
            {
                this._jarFile.Dispose();
            }
        }

        private int SetFileCount()
        {
            var fileCount = 0;

            this.imagesInLogFiles = new List<int>();

            foreach (var file in this._fileInfos)
            {
                using (var logFileReader = this._jarFileFactory.GetJarFile(FileAccessMode.Read, file.FullName))
                {
                    fileCount += logFileReader.FilesCount;
                    this.imagesInLogFiles.Add(logFileReader.FilesCount);
                }
            }

            return fileCount;
        }

        private bool GetNextImage()
        {
            if (this._fileIndex >= this._fileInfos.Count)
            {
                this._currentActivity = null;
            }

            if (this._jarFile == null)
            {
                this._jarFile = this._jarFileFactory.GetJarFile(FileAccessMode.Read, this._fileInfos[this._fileIndex].FullName);
            }

            var image = this._jarFile.GetNextFile();

            if (image == null)
            {
                if (this._fileIndex + 1 >= this.imagesInLogFiles.Count)
                {
                    return false;
                }

                this._fileIndex++;
                this._jarFile.Dispose();
                this._jarFile = null;
                this.GetNextImage();
                return true;
            }

            this._currentActivity = this.BytesToActivity(image);
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
                this._jarFile.GetNextFile();
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
    }
}