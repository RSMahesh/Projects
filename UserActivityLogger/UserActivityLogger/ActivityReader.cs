using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class ActivityReader :  IActivityReader
    {
        private readonly ActivitesEnumerator _activityEnum;

        public ActivityReader(IEnumerable<string> files, IJarFileFactory jarFileFactory)
        {
            _activityEnum = new ActivitesEnumerator(GetFiles(files), jarFileFactory);
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

        private IEnumerable<FileInfo> GetFiles(string dataFolder)
        {
            return new DirectoryInfo(dataFolder).GetFiles().Where(s => s.FullName.EndsWith(".jar") || s.FullName.EndsWith(".log"))
         .OrderBy(f => f.LastWriteTime)
         .ToList();
        }

        private IEnumerable<FileInfo> GetFiles(IEnumerable<string> files)
        {
            var lst = new List<FileInfo>();

            foreach (var file in files)
            {
                lst.Add(new FileInfo(file));
            }

            return lst.OrderBy(f => f.LastWriteTime).ToList();
        }
    }
}

