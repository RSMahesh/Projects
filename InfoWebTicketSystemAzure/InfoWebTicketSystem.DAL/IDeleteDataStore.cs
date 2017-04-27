// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeleteDataStore.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   Class implmentation for IDeleteDataStore.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using MediaValet.Data.Core;

namespace InfoWebTicketSystem.DAL
{
    public interface IDeleteDataStore
    {
        /// <summary>
        /// Method for deleting an item.
        /// </summary>
        /// <param name="item">representing a T type item.</param>
        /// /// <param name="context">
        /// Information about the request.
        /// </param>
        void Delete<T>(T item) where T : IEntity;
    }
}