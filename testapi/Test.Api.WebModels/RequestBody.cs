// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestBody.cs" company="">
//   
// </copyright>
// <summary>
//   The layout of the body submitted for all requests to the API.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.WebModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The layout of the body submitted for all requests to the API.
    /// </summary>
    public sealed class RequestBody
    {
        /// <summary>
        /// Gets or sets the pagination information for the request.
        /// </summary>
        public Pagination Pagination { get; set; }

        /// <summary>
        /// Gets or sets a string representation of the filters to be applied to the request.
        /// </summary>
        /// <remarks>
        /// This search recognizes operators: AND, OR, NOT, GT, LT, GE, and LE, and will follow order of operations.
        ///     Brackets ( and ) can be used within the query.
        /// </remarks>
        public string Filters { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of fields that have been requested for the record.
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Gets or sets a the fields to sort the results by.
        /// </summary>
        /// <remarks>
        /// Fields are to be in the format: fieldName [ASC|DESC] where the direction is optional.  If omitted, ASC is
        ///     assumed.
        /// </remarks>
        public IEnumerable<string> SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the append.
        /// </summary>
        /// <value>
        /// The append.
        /// </value>
        public IDictionary<string, RequestBody> Append { get; set; }
    }
}