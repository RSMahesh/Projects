// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ticket.cs" company="">
//   
// </copyright>
// <summary>
//   The ticket.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Test.Api.Data.Entities;

    /// <summary>
    /// The ticket.
    /// </summary>
    public class TicketEntity : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ticket"/> class.
        /// </summary>
        public TicketEntity()
        {
            this.TicketConversations = new HashSet<TicketConversationEntity>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public Guid? UserID { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [ForeignKey("UserID")]
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        public int TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [StringLength(10)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        [StringLength(20)]
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [StringLength(1000)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the ticket type.
        /// </summary>
        [StringLength(10)]
        public string TicketType { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        [StringLength(500)]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        [StringLength(20)]
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the user communication email.
        /// </summary>
        [StringLength(100)]
        public string UserCommunicationEmail { get; set; }

        /// <summary>
        /// Gets or sets the ticket conversations.
        /// </summary>
        public virtual ICollection<TicketConversationEntity> TicketConversations { get; set; }
    }
}