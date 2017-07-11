using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserActivityLogger;

namespace ActivityLogger.Filters
{
    public class DateTimeFilter
    {

        public DateTimeFilter()
        {

        }

        public IEnumerable<FileInfo> FilterOutFiles(string dataFolder, ActivityQueryFilter filter)
        {
            var _jarFilesInfos = new DirectoryInfo(dataFolder).GetFiles().Where(s => s.FullName.EndsWith(".jar") || s.FullName.EndsWith(".log"))
           .OrderBy(f => f.LastWriteTime)
           .ToList();

            return Filter(filter, _jarFilesInfos);
        }

        private IEnumerable<FileInfo> Filter(ActivityQueryFilter filter, List<FileInfo> _jarFilesInfos)
        {
            var filteredFiles = new List<FileInfo>();

            if (filter != null)
            {
                return _jarFilesInfos;
            }

            for (var i = 0; i < _jarFilesInfos.Count; i++)
            {
                if (_jarFilesInfos[i].LastWriteTime >= filter.StartDateTime && _jarFilesInfos[i].LastWriteTime <= filter.EndDateTime)
                {
                    filteredFiles.Add(_jarFilesInfos[i]);
                }
            }
            return filteredFiles;
        }
    }
}
