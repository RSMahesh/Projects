using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.Business
{
    public class ACL
    {
        public Guid ID { get; set; }

        public int SecurableObjectType { get; set; }

        public ulong Permissions { get; set; }

        public bool IsPropagated { get; set; }

        public bool IsUser { get; set; }

        public Guid SourceID { get; set; }

    }
}
