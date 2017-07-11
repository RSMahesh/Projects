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
        private IEnumerable<FileInfo> _jarFilesInfos;
        private int _currentJarfileIndex;
        IJarFileReader _jarFileReader = null;
        private ConcurrentDictionary<int, FileItemInfo> _fileOffsetInfoMap = new ConcurrentDictionary<int, FileItemInfo>();

        public int FileCount { get; private set; }
        public ActivitesEnumerator(IEnumerable<FileInfo> fileInfos, IJarFileFactory jarFileFactory, ActivityQueryFilter filter)
        {
            this._jarFileFactory = jarFileFactory;
            this._currentJarfileIndex = 0;

            this._jarFilesInfos = fileInfos;

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
            this.ChangePostion(0);
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

            if (!_jarFilesInfos.Any())
            {
                return 0;
            }

            Parallel.ForEach(this._jarFilesInfos, (file) =>
                {
                    tempDic[file.FullName] = GetOffSetOfFilesinJar(file.FullName);

                });


            foreach (var file in this._jarFilesInfos)
            {
                var list = tempDic[file.FullName];
                

                for (var i = 0; i < list.Count; i++)
                {
                   
                    _fileOffsetInfoMap[ItemfileCount++] = new FileItemInfo(file.FullName, list[i], jarfileCount);
                }

                jarfileCount++;

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
          
            if(! ValidateBeforeGettingNextFile())
            {
                return false;
            }

            var file = this._jarFileReader.GetNextFile();

            if (file == null)
            {
                if (this._currentJarfileIndex + 1 >= _jarFilesInfos.Count())
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



        private bool ValidateBeforeGettingNextFile()
        {
           
            if (this._currentJarfileIndex >= _jarFilesInfos.Count())
            {
                Current = null;
                return false;
            }

            if (this._jarFileReader == null)
            {
                this._jarFileReader = this._jarFileFactory.GetJarFileReader(_jarFilesInfos.ElementAt(this._currentJarfileIndex).FullName);
            }

            return true;
        }

        //private void ReadNewJarFileIfRequired()
        //{
        //        this._jarFileReader = this._jarFileFactory.GetJarFileReader(_jarFilesInfos.ElementAt(this._currentJarfileIndex).FullName);
        //}

        private Activity BytesToActivity(byte[] imageBytes)
        {
            if (imageBytes == null)
                return null;

            using (var fs = new MemoryStream(imageBytes, false))
            {
                return new Activity(Image.FromStream(fs), this.GetComments(fs));
            }
        }

        private string GetComments(Stream stream)
        {
            if (stream == null)
                return string.Empty;

            return new ImageCommentEmbedder().GetComments(stream);
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