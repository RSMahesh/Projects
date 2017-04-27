
namespace Test.Api.Data
{
    using System.Collections.Generic;

    using Test.Api.Core;

    /// <summary>
    /// Definition of a class able to store a query in a generic form.
    /// </summary>
    public interface ITestApiQuery : IQuery
    {
        /// <summary>
        /// Gets the individual elements of the query.
        /// </summary>
        IReadOnlyCollection<QueryElement> QueryElements { get; }

        /// <summary>
        /// Add a where clause to the query.
        /// </summary>
        /// <param name="query">
        /// <see cref="IQuery"/> representing a clause in the search.
        /// </param>
        /// <returns>
        /// <c>this</c> instance to allow fluent building of a query.
        /// </returns>
        ITestApiQuery Where(IQuery query);

        /// <summary>
        /// Add an <c>And</c> clause to the query.
        /// </summary>
        /// <param name="query">
        /// <see cref="IQuery"/> representing a clause in the search.
        /// </param>
        /// <returns>
        /// <c>this</c> instance to allow fluent building of a query.
        /// </returns>
        ITestApiQuery And(IQuery query);

        /// <summary>
        /// Add an <c>Or</c> clause to the query.
        /// </summary>
        /// <param name="query">
        /// <see cref="IQuery"/> representing a clause in the search.
        /// </param>
        /// <returns>
        /// <c>this</c> instance to allow fluent building of a query.
        /// </returns>
        ITestApiQuery Or(IQuery query);
    }
}