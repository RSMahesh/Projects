// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWriteDataStore.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   Class implmentation for IWriteDataStore.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace InfoWebTicketSystem.DAL
{
    public interface IWriteDataStore
    {
        /// <summary>
        /// Method for inserting an item.
        /// </summary>
        /// <param name="item">representing a T type item.</param>
        /// <param name="context">
        ///     Information about the request.
        /// </param>
        T Insert<T>(T item) where T : IEntity;

        /// <summary>
        /// Method for updating an item.
        /// </summary>
        /// <param name="item">representing a T type item.</param>
        /// <param name="context">
        /// Information about the request.
        /// </param>
        T Update<T>(T item) where T : IEntity;
    }
}