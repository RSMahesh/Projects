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

    public interface ITicketConversationRow : IRow
    {

        Guid FID { get; set; }

         Guid TicketId { get; set; }

         string Attachment { get; set; }

         string Message { get; set; }

         Guid UserId { get; set; }

         string Staff { get; set; }

    }
}