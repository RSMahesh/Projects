using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class ActivityReader : IEnumerable<Activity>, IDisposable
    {
        private readonly ActivitesEnum _activityEnum;
        public ActivityReader(string dataFolder, IJarFileFactory jarFileFactory)
        {
            _activityEnum = new ActivitesEnum(dataFolder, jarFileFactory);
        }
        public IEnumerator<Activity> GetEnumerator()
        {
            return _activityEnum;
        }

        public int FileCount()
        {
            return _activityEnum.FileCount;
        }

        public void ChangePostion(int positionNumber)
        {
            _activityEnum.ChangePostion(positionNumber);
        }
        public void Dispose()
        {
            _activityEnum.Dispose();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public DateTime? StartDateTime  { private get; set; }

        public DateTime? EndDateTime { private get; set; }
    }

    public class ActivitesEnum : IEnumerator<Activity>, IDisposable
    {
        private readonly string _logFolder;
        private readonly IJarFileFactory _jarFileFactory = null;

        private List<FileInfo> _fileInfos;
        private int _fileIndex;
        IJarFile _jarFile = null;
        List<int> imagesInLogFiles = new List<int>();
        private Activity _currentActivity;

        public int FileCount { get; private set; }

        public ActivitesEnum(string logFolderPath, IJarFileFactory jarFileFactory)
        {
            _jarFileFactory = jarFileFactory;
            _logFolder = logFolderPath;
            _fileIndex = 0;
            _fileInfos = new DirectoryInfo(_logFolder).GetFiles("*.log")
                                                            .OrderBy(f => f.LastWriteTime)
                                                            .ToList();

            FileCount = SetFileCount();
        }

        public Activity Current
        {
            get
            {
                return _currentActivity;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            GetNextImage();
            return true;
        }

        public void Reset()
        {
            MoveIndexToPostion(0);
        }

        public void ChangePostion(int positionNumber)
        {
            int imagePositionInFile = GetImagePositionInLogFileAndSetFileIndex(positionNumber);

            if (_jarFile != null)
            {
                _jarFile.Dispose();
            }

            _jarFile = _jarFileFactory.GetJarFile(FileAccessMode.Read, _fileInfos[_fileIndex].FullName);

            MoveIndexToPostion(imagePositionInFile);
        }

        public void Dispose()
        {
            if (_jarFile != null)
            {
                _jarFile.Dispose();
            }
        }

        private int SetFileCount()
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

        private bool GetNextImage()
        {
            if (_fileIndex >= _fileInfos.Count)
            {
                _currentActivity = null;
            }

            if (_jarFile == null)
            {
                _jarFile = _jarFileFactory.GetJarFile(FileAccessMode.Read, _fileInfos[_fileIndex].FullName);
            }

            var image = _jarFile.GetNextFile();

            if (image == null)
            {
                if(_fileIndex + 1 >= imagesInLogFiles.Count)
                {
                    return false;
                }

                _fileIndex++;
                _jarFile.Dispose();
                _jarFile = null;
                GetNextImage();
                return true;
            }

            _currentActivity = BytesToActivity(image);
            return true;
        }

        private Activity BytesToActivity(byte[] imageBytes)
        {
            Activity activity = null;

            using (MemoryStream fs = new MemoryStream(imageBytes, false))
            {
                return activity = new Activity(Image.FromStream(fs), GetComments(fs));
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

