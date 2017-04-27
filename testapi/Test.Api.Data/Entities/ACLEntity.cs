using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Test.Api.Data.Entities
{

    public class ACLEntity : BaseEntity
    {
        [Key]
        public Guid ID { get; set; }

        public int SecurableObjectType { get; set; }

        public ulong Permissions { get; set; }

        public bool IsPropagated { get; set; }

        public bool IsUser { get; set; }

        public Guid SourceID { get; set; }
    }
}
