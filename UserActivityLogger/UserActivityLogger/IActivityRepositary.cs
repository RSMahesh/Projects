using System.Collections.Generic;

namespace UserActivityLogger
{
    public interface IActivityRepositary
    {
        void Add(Activity activity);
        ActivityReader GetReader(IEnumerable<string> files);
        string DataFolder { get;}

    }
}