using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using FileSystem;
using Core;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace UserActivityLogger
{
    public class ActivitesEnumerator : IEnumerator<Activity>, IDisposable
    {
        private readonly IJarFileFactory _jarFileFactory;
        private List<FileInfo> _jarFilesInfos;
        private int _currentJarfileIndex;
        IJarFileReader _jarFileReader = null;
        private Activity _currentActivity;
        private ConcurrentDictionary<int, FileItemInfo> _fileOffsetInfoMap = new ConcurrentDictionary<int, FileItemInfo>();

        public int FileCount { get; private set; }

        public ActivitesEnumerator(string logFolderPath, IJarFileFactory jarFileFactory, ActivityQueryFilter filter)
        {
            this._jarFileFactory = jarFileFactory;
            this._currentJarfileIndex = 0;

            this._jarFilesInfos = new DirectoryInfo(logFolderPath).GetFiles().Where(s => s.FullName.EndsWith(".jar") || s.FullName.EndsWith(".log"))
                .OrderBy(f => f.LastWriteTime)
                .ToList();

            this.FileCount = this.GetFileCount();

        }

        public ActivityQueryFilter Filter { private get; set; }
        public Activity Current { get; private set; }

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

        public void ChangePostion(int positionNumber)
        {
            var fileItemInfo = _fileOffsetInfoMap[positionNumber];
            _currentJarfileIndex = fileItemInfo.Index;

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
            var ItemfileCount = 0;
            var jarfileCount = 0;
            var tempDic = new ConcurrentDictionary<string, List<long>>();

            Parallel.ForEach(this._jarFilesInfos, (file) =>
                {
                    tempDic[file.FullName] = GetOffSetOfFilesinJar(file.FullName);
                });


            foreach (var file in this._jarFilesInfos)
            {
                jarfileCount++;

                var list = tempDic[file.FullName];

                for (var i = 0; i < list.Count; i++)
                {
                    ItemfileCount++;
                    _fileOffsetInfoMap[ItemfileCount] = new FileItemInfo(file.FullName, list[i], jarfileCount);
                }
            }

            return ItemfileCount;
        }

        private List<long> GetOffSetOfFilesinJar(string jarFilePath)
        {
            var offSetList = new List<long>();

            using (var logFileReader = this._jarFileFactory.GetJarFileReader(jarFilePath))
            {
                var offset = logFileReader.GetNextFileOffset();

                while (offset != -1)
                {
                    offSetList.Add(offset);
                    offset = logFileReader.GetNextFileOffset();
                }
            }

            return offSetList;
        }

        private bool GetNextFile()
        {
            if (this._currentJarfileIndex >= _jarFilesInfos.Count)
            {
                Current = null;
            }

            if (this._jarFileReader == null)
            {
                this._jarFileReader = this._jarFileFactory.GetJarFileReader(_jarFilesInfos[this._currentJarfileIndex].FullName);
            }

            var file = this._jarFileReader.GetNextFile();

            if (file == null)
            {
                if (this._currentJarfileIndex + 1 >= _jarFilesInfos.Count)
                {
                    return false;
                }

                this._currentJarfileIndex++;
                this._jarFileReader.Dispose();
                this._jarFileReader = null;
                this.GetNextFile();
                return true;
            }

            Current = this.BytesToActivity(file.Containt);
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


        //Future used methods
        private void FilterOutFiles(ActivityQueryFilter filter)
        {
            const int FiltersCount = 2;
            if (filter != null)
            {
                var filterPassed = 0;
                for (var i = 0; i < _jarFilesInfos.Count; i++)
                {
                    if (filter.StartDateTime.HasValue &&
                        this._jarFilesInfos[i].LastWriteTime >= filter.StartDateTime.Value)
                    {
                        filterPassed++;
                    }

                    if (filter.EndDateTime.HasValue &&
                        this._jarFilesInfos[i].LastWriteTime <= filter.EndDateTime.Value)
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