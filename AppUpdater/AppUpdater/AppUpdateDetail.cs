using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdater
{
    public class AppUpdateDetail
    {
        public AppUpdateDetail(string remoteSource, string localDestination):this(remoteSource,localDestination,Enumerable.Empty<string>())
        {
            //TODO; AssetNotNull
        }

        public AppUpdateDetail(string remoteSource, string localDestination, IEnumerable<string> excludeFiles)
        {
            RemoteSource = remoteSource;
            LocalDestination = localDestination;
            ExcludeFiles = excludeFiles;
        }
        public string RemoteSource { get; private set; }
        public string LocalDestination { get; private set; }
        public IEnumerable<string> ExcludeFiles { get; private set; }
    }
}
