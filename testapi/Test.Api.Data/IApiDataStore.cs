// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApiDataStore.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The IApiDataStore interface illustrates the common method declaration for CRUD operation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Test.Api.Data;

using Test.Api.Core;

namespace Test.Api.Data
{
    /// <summary>
    /// The IApiDataStore interface illustrates the common method declaration for CRUD operation.
    /// </summary>
    /// <typeparam name="T">
    /// Object type this data store represents.
    /// </typeparam>
    public interface IApiDataStore<T> : IDataStoreBase
    {
        /// <summary>
        /// Method for inserting an item.
        /// </summary>
        /// <param name="item">representing a T type item.</param>
        /// <param name="context">
        ///     Information about the request.
        /// </param>
        IApiSearchResult<T> Insert(T item);

        /// <summary>
        /// Method for deleting an item.
        /// </summary>
        /// <param name="item">representing a T type item.</param>
        /// /// <param name="context">
        /// Information about the request.
        /// </param>
        void Delete(T item);

        /// <summary>
        /// Method for updating an item.
        /// </summary>
        /// <param name="item">representing a T type item.</param>
        /// <param name="context">
        /// Information about the request.
        /// </param>
        IApiSearchResult<T> Update(T item);

        /// <summary>
        /// Method to display the list of item.
        /// </summary>
        /// <param name="query">
        /// Internal representation of a query to select the items.
        /// </param>
        /// <param name="context">
        /// Information about the request.
        /// </param>
        /// <returns>The an <see cref="IEnumerable{T}"/> of items that satisfy the search criteria.</returns>
        IApiSearchResult<IEnumerable<T>> Read(IQuery query);
    }
}
