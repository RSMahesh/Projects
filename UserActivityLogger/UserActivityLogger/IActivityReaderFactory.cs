using System.Collections.Generic;
using UserActivityLogger;

namespace ActivityLogger
{
    public interface IActivityReaderFactory
    {
        IActivityReader GetReader(IEnumerable<string> files);
    }
}