// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ticket.cs" company="">
//   
// </copyright>
// <summary>
//   The ticket.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Test.Api.Business
{
    /// <summary>
    /// The ticket.
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ticket"/> class.
        /// </summary>
        public Ticket()
        {
           
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public Guid? UserID { get; set; }

      //  public virtual UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        public int TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the ticket type.
        /// </summary>
        public string TicketType { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the user communication email.
        /// </summary>
        public string UserCommunicationEmail { get; set; }

        public virtual IEnumerable<TicketConversation> TicketConversations { get; set; }
    }
}