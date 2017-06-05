using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class ActivityReader : IEnumerable<Activity>, IDisposable
    {
        private readonly ActivitesEnumerator _activityEnum;

        public ActivityReader(string dataFolder, IJarFileFactory jarFileFactory, ActivityQueryFilter filter)
        {
            _activityEnum = new ActivitesEnumerator(dataFolder, jarFileFactory, filter);
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

        public ActivityQueryFilter Filter
        {
            set
            {
                _activityEnum.Filter = value;
            }
        }

    }
}

