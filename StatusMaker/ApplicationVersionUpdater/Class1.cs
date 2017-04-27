using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationVersionUpdater
{
    public class LocationNotReachable : Exception
    {
       public LocationNotReachable(string path, Exception innerException)
            : base(
                string.Format(
                    @"Path '{0}'. ",
                    path), innerException)
        {

        }
    }
}
