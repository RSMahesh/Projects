using System.Collections.Generic;

namespace UserActivityLogger
{
    public interface IActivityRepositary
    {
        void Add(Activity activity);
        IActivityReader GetReader(IEnumerable<string> files);
        string DataFolder { get;}

    }
}