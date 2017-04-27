using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Account : BaseEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Staff { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int UserId { get; set; }
    }
}
