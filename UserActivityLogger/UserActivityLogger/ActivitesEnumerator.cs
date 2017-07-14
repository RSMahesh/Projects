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
        private readonly IEnumerable<FileInfo> _jarFilesInfos;
        private int _nextJarfileIndex;
        IJarFileReader _jarFileReader = null;
        private ConcurrentDictionary<int, JarItemMetaData> _fileOffsetInfoMap = new ConcurrentDictionary<int, JarItemMetaData>();

        public int FileCount { get; private set; }
        public ActivitesEnumerator(IEnumerable<FileInfo> fileInfos, IJarFileFactory jarFileFactory, ActivityQueryFilter filter)
        {
            _jarFileFactory = jarFileFactory;
            _nextJarfileIndex = 0;

            _jarFilesInfos = fileInfos;

            PopulateFileOffsetInfoMap();

            SetReaderToNextFile();
        }

        public ActivityQueryFilter Filter { private get; set; }
        public Activity Current { get; private set; }
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
        public bool MoveNext()
        {
            return GetNextFile();
        }
        public void Reset()
        {
            ChangePostion(0);
        }
        public void ChangePostion(int positionNumber)
        {
            var fileItemInfo = _fileOffsetInfoMap[positionNumber];
            _nextJarfileIndex = fileItemInfo.JarFileIndex + 1;

            if (_jarFileReader != null && _jarFileReader.JarFilePath != fileItemInfo.JarFilePath)
            {
                _jarFileReader.Dispose();
                _jarFileReader = _jarFileFactory.GetJarFileReader(fileItemInfo.JarFilePath);
            }

            _jarFileReader.MoveFileHeader(fileItemInfo.OffSetInJarFile);
        }
        public void Dispose()
        {
            if (_jarFileReader != null)
            {
                _jarFileReader.Dispose();
            }
        }

        private void PopulateFileOffsetInfoMap()
        {
            if (!_jarFilesInfos.Any())
            {
                return;
            }

            var jarFileOffSetMapping = GetOffSetOfFilesinAllJar();
            var itemfileCount = 0;
            var jarfileCount = 0;

            foreach (var file in _jarFilesInfos)
            {
                var list = jarFileOffSetMapping[file.FullName];

                for (var i = 0; i < list.Count; i++)
                {
                    _fileOffsetInfoMap[itemfileCount++] = new JarItemMetaData(file.FullName, list[i], jarfileCount);
                }

                jarfileCount++;
            }

            FileCount = itemfileCount;
        }

        private ConcurrentDictionary<string, List<long>> GetOffSetOfFilesinAllJar()
        {
            var jarFileOffSetMapping = new ConcurrentDictionary<string, List<long>>();

            Parallel.ForEach(_jarFilesInfos, (file) => {
                jarFileOffSetMapping[file.FullName] = GetOffSetOfFilesinJar(file.FullName);
            });

            return jarFileOffSetMapping;
        }
        private List<long> GetOffSetOfFilesinJar(string jarFilePath)
        {
            var offSetList = new List<long>();

            using (var logFileReader = _jarFileFactory.GetJarFileReader(jarFilePath))
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
            var file = _jarFileReader.GetNextFile();

            if (file == JarFileItem.Empty)
            {
                if (IsEndReached())
                {
                    return false;
                }

                _jarFileReader.Dispose();

                SetReaderToNextFile();

                GetNextFile();
                return true;
            }

            Current = BytesToActivity(file.Containt);
            return true;
        }

        private bool IsEndReached()
        {
            if (_nextJarfileIndex >= _jarFilesInfos.Count())
            {
                Current = Activity.Empty;
                return true;
            }

            return false;
        }

        private void SetReaderToNextFile()
        {
            _jarFileReader = _jarFileFactory.GetJarFileReader(_jarFilesInfos.ElementAt(_nextJarfileIndex).FullName);
            _nextJarfileIndex++;
        }

        private Activity BytesToActivity(byte[] imageBytes)
        {
            if (imageBytes == null)
                return null;

            using (var fs = new MemoryStream(imageBytes, false))
            {
                return new Activity(Image.FromStream(fs), GetComments(fs));
            }
        }

        private string GetComments(Stream stream)
        {
            if (stream == null)
                return string.Empty;

            return new ImageCommentEmbedder().GetComments(stream);
        }

        private class JarItemMetaData
        {
            public JarItemMetaData(string jarFilePath, long offSetInJarFile, int index)
            {
                JarFilePath = jarFilePath;
                OffSetInJarFile = offSetInJarFile;
                JarFileIndex = index;
            }
            public string JarFilePath { get; private set; }
            public int JarFileIndex { get; private set; }
            public long OffSetInJarFile { get; private set; }
        }
    }
}