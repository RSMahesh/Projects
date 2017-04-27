// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   Class implmentation for IEntity.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace InfoWebTicketSystem.DAL
{
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the timestamp for the entity.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The entity's timestamp.
        /// </value>
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the partition key of a table entity.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The partition key.
        /// </value>
        string PartitionKey { get; set; }

        /// <summary>
        /// Gets or sets the row key of a table entity.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The row key.
        /// </value>
        string RowKey { get; set; }
    }
}