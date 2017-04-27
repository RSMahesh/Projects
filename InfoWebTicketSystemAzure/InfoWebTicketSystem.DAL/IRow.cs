//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================

using System;

namespace InfoWebTicketSystem.DAL
{
    public interface IRow : IEntity
    {
        string Kind { get; set; }
        DateTime InsertDate { get; set; }
        DateTime LastUpdate { get; set; }
    }
}