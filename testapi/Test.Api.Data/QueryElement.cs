// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryElement.cs" company="">
//   
// </copyright>
// <summary>
//   The query element.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Data
{
    using System;

    using Test.Api.Core;

    /// <summary>
    /// Store part of the query, including the clause, and the <see cref="QueryJoinVerb"/> to join it to the rest of the query.
    /// </summary>
    public sealed class QueryElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryElement"/> class.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="verb">
        /// The verb.
        /// </param>
        public QueryElement(IQuery query, QueryJoinVerb verb)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            this.Query = query;
            this.Verb = verb;
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        public IQuery Query { get; private set; }

        /// <summary>
        /// Gets the value to be searched for.
        /// </summary>
        public QueryJoinVerb Verb { get; private set; }
    }
}