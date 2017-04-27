
namespace Test.Api.Data
{
    using Test.Api.Core;

    /// <summary>
    /// Definition of a clause in a search.
    /// </summary>
    public interface ITestApiQueryFilter : IQuery
    {
        /// <summary>
        /// Gets the name of the field to be searched.
        /// </summary>
        string SearchField { get; }

        /// <summary>
        /// Gets the operator to apply to this field name / value pair.
        /// </summary>
        EqualityOperator EqualityOperator { get; }

        /// <summary>
        /// Gets the value to be searched for.
        /// </summary>
        object SearchValue { get; }
    }
}