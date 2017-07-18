using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserActivityLogger;

namespace ActivityLogger
{
    public class ActivityReaderFactory: IActivityReaderFactory
    {
        IJarFileFactory _jarFileFactory;
        public ActivityReaderFactory(IJarFileFactory jarFileFactory)
        {
            _jarFileFactory = jarFileFactory;
        }
        public IActivityReader GetReader(IEnumerable<string> files)
        {
            return new ActivityReader(files, _jarFileFactory);
        }

    }
}
