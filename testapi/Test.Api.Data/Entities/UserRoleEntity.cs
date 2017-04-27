using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class UserRoleEntity
    {
        [Key]
        public Guid ID { get; set; }

        public string Description { get; set; }
   
        public Guid OrgUnitId { get; set; }
    }
}
