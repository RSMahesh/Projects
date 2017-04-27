using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationVersionUpdater
{
    public class AppUpdateDetail
    {
        public AppUpdateDetail(string source, string destination)
        {
            Source = source;
            Destination = destination;
        }
        public string Source { get; private set; }
        public string Destination { get; private set; }
    }
}
