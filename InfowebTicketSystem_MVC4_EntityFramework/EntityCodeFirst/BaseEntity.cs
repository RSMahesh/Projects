using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
        }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Nullable<System.DateTime> CreateDate { get; set; }
        //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Nullable<System.DateTime> LastUpdateDate { get; set; }

        public Guid? LastUpdatedUserID { get; set; }
        [ForeignKey("LastUpdatedUserID")]
        public virtual Account LastUpdatedUser { get; set; }

    }
}
