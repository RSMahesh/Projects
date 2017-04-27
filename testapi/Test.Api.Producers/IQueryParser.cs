// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQueryParser.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the behavior of the engine for parsing QueryStrings into a  structure.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers
{
    using Test.Api.Data;

    /// <summary>
    /// Defines the behavior of the engine for parsing QueryStrings into a <see cref="ITestApiQuery"/> structure.
    /// </summary>
    public interface IQueryParser
    {
        /// <summary>
        /// Parses the query from the QueryString into an internal generic representation of the query.
        /// </summary>
        /// <param name="query">
        /// The search element as it is passed in through the QueryString.
        /// </param>
        /// <returns>
        /// <see cref="ITestApiQuery"/> tree representing the query as passed in.
        /// </returns>
        ITestApiQuery Parse(string query);
    }
}