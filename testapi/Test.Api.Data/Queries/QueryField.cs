// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITestApiQueryFilter.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The TestApiQueryFilter interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Test.Api.Core;

namespace Test.Api.Data.Queries
{
    /// <summary>
    /// Definition of a clause in a search.
    /// </summary>
    public class QueryField : IQueryField
    {

        public QueryField(string searchField, EqualityOperator equalityOperator, object searchValue)
        {
            SearchField = searchField;
            EqualityOperator = equalityOperator;
            SearchValue = searchValue;
        }

        /// <summary>
        /// Gets the name of the field to be searched.
        /// </summary>
        public string SearchField { get; private set; }

        /// <summary>
        /// Gets the operator to apply to this field name / value pair.
        /// </summary>
        public EqualityOperator EqualityOperator { get; private set; }

        /// <summary>
        /// Gets the value to be searched for.
        /// </summary>
        /// <summary>
        /// Gets the search value.
        /// </summary>
        public object SearchValue { get; private set; }
    }
}