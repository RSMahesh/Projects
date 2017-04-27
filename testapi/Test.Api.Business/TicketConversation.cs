// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TicketConversation.cs" company="">
//   
// </copyright>
// <summary>
//   The ticket conversation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Test.Api.Business
{
    /// <summary>
    /// The ticket conversation.
    /// </summary>
    public class TicketConversation
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
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the attachment.
        /// </summary>
        public string Attachment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether staff.
        /// </summary>
        public bool Staff { get; set; }
    }
}