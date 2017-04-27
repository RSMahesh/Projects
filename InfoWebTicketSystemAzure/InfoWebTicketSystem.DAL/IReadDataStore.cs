// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReadDataStore.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   Class implmentation for IReadDataStore.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace InfoWebTicketSystem.DAL
{
    public interface IReadDataStore
    {

        /// <summary>
        /// Method to display the list of item.
        /// </summary>
        /// <param name="query">
        /// Internal representation of a query to select the items.
        /// </param>
        /// <returns>The an <see cref="IEnumerable{T}"/> of items that satisfy the search criteria.</returns>
        IEnumerable<T> Read<T>(Expression<Func<T, bool>> query) where T : IEntity;
    }
}