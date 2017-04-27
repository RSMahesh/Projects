// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiSearchResult.cs" company="">
//   
// </copyright>
// <summary>
//   The api search result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Data
{
    using System.Diagnostics.CodeAnalysis;

    using Test.Api.Core;

    /// <summary>
    /// The results returned from a DataStore.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object that is expected for the results.
    /// </typeparam>
    [ExcludeFromCodeCoverage]
    public class ApiSearchResult<T> : IApiSearchResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSearchResult{T}"/> class.
        /// </summary>
        /// <param name="recordCounts">
        /// The record counts.
        /// </param>
        /// <param name="results">
        /// The result expected from the data store.
        /// </param>
        public ApiSearchResult(IRecordCounts recordCounts, T results)
        {
            this.RecordCounts = recordCounts;
            this.Results = results;
        }

        /// <summary>
        /// Gets the record counts.
        /// </summary>
        public IRecordCounts RecordCounts { get; private set; }

        /// <summary>
        /// Gets the results.
        /// </summary>
        public T Results { get; private set; }
    }
}