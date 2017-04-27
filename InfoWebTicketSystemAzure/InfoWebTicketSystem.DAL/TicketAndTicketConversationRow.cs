// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ticket.cs" company="">
//   
// </copyright>
// <summary>
//   The ticket.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using AExpense.DataAccessLayer;

using InfoWebTicketSystem.DAL;

namespace Entities
{
    using System;
    /// <summary>
    /// The ticket.
    /// </summary>
    public class TicketAndTicketConversationRow : Row, ITicketRow, ITicketConversationRow, IRow
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid FID
        {
            get
            {
                return string.IsNullOrEmpty(RowKey) ? Guid.Empty : Guid.Parse(RowKey);
            }
            set
            {
                RowKey = value.ToString();
            }
        }

        public int TicketNumber { get; set; }
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the ticket type.
        /// </summary>
        public string ContactNumber { get; set; }

        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string LastReplier { get; set; }

        public string UserName { get; set; }


        //======================================Convrstaion Property 
        //FID,ticketId,attachment,message,userId,staff,InsertDate,UpdateDate

        public Guid TicketId { get; set; }

        public string Attachment { get; set; }

        public string Message { get; set; }

        public Guid UserId { get; set; }

        public string Staff { get; set; }


    }
}