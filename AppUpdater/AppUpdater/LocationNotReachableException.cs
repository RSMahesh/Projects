using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdater
{
    public class LocationNotReachableException : Exception
    {
       public LocationNotReachableException(string path, Exception innerException)
            : base(
                string.Format(
                    @"Path '{0}'. ",
                    path), innerException)
        {

        }
    }
}
