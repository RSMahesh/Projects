//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================

using System;

using AExpense.DataAccessLayer;

using Microsoft.WindowsAzure.StorageClient;

namespace InfoWebTicketSystem.DAL
{
    [CLSCompliant(false)]
    public abstract class Row : TableServiceEntity, IRow
    {
       public string Kind { get; set; }
       public DateTime InsertDate { get; set; }
       public DateTime LastUpdate { get; set; }
  
    }
}