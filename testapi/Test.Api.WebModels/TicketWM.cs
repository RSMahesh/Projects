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
using System.Runtime.Serialization;
using Test.Api.Business;
using Test.Api.HyperMedia;

namespace Test.Api.WebModels
{
    /// <summary>
    /// The ticket.
    /// </summary>
    [DataContract]
    public class TicketWM : IWebLinkable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ticket"/> class.
        /// </summary>
        public TicketWM()
        {

            Links = new WebLinks();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "id")]
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [DataMember(Name = "userId")]
        public Guid? UserID { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        [DataMember(Name = "ticketNumber")]
        public int TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        [DataMember(Name = "priority")]
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the ticket type.
        /// </summary>
        [DataMember(Name = "ticketType")]
        public string TicketType { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        [DataMember(Name = "domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        [DataMember(Name = "department")]
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the user communication email.
        /// </summary>
        [DataMember(Name = "userCommunicationEmail")]
        public string UserCommunicationEmail { get; set; }

        [DataMember(Name = "ticketConversations")]
        public virtual IEnumerable<TicketConversation> TicketConversations { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "_links")]
        public IWebLinks Links { get; private set; }
    }
}