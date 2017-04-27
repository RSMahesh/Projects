// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApiSearchResult.cs" company="">
//   
// </copyright>
// <summary>
//   The ApiSearchResult interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Data
{
    using Test.Api.Core;

    /// <summary>
    /// Declares the contract for returning search results.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object that is expected for the results.
    /// </typeparam>
    public interface IApiSearchResult<out T>
    {
        /// <summary>
        /// Gets the information on the number of records found, returned, and the start index.
        /// </summary>
        IRecordCounts RecordCounts { get; }

        /// <summary>
        /// Gets the search results.
        /// </summary>
        T Results { get; }
    }
}