using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdater
{
    public class AppUpdatersFailedException : Exception
    {
        public AppUpdatersFailedException(IEnumerable<LocationNotReachableException> LocationNotReachableExceptions)
             : base("All App Updaters failed. See inner exceptions")
        {
        }

        public IEnumerable<LocationNotReachableException> LocationNotReachableExceptions { get; private set; }
    }
}
