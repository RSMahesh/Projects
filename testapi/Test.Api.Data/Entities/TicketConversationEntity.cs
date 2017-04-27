// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TicketConversation.cs" company="">
//   
// </copyright>
// <summary>
//   The ticket conversation.
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
    /// The ticket conversation.
    /// </summary>
    public class TicketConversationEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the ticket id.
        /// </summary>
        public Guid TicketID { get; set; }

        /// <summary>
        /// Gets or sets the ticket.
        /// </summary>
        [ForeignKey("TicketID")]
        public virtual TicketEntity Ticket { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the attachment.
        /// </summary>
        [StringLength(1000)]
        public string Attachment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether staff.
        /// </summary>
        public bool Staff { get; set; }
    }
}