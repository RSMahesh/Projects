//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================

using InfoWebTicketSystem.DAL;

namespace AExpense.DataAccessLayer
{
    using System;

    public interface ITicketRow : IRow
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        Guid FID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        string Priority { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        string Subject { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        string Domain { get; set; }

        /// <summary>
        /// Gets or sets the ticket type.
        /// </summary>
        string ContactNumber { get; set; }

         string Department { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        string LastReplier { get; set; }

        string UserName { get; set; }

    }
}