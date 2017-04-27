using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationVersionUpdater
{
    public class VncAppUpdateDetail : AppUpdateDetail
    {
        public VncAppUpdateDetail(string source, string destination, string userName, string password)
            : base(source, destination, userName, password)
        {

        }

        public VncAppUpdateDetail(string source, string destination)
          : base(source, destination, string.Empty, string.Empty)
        {

        }
    }
}
