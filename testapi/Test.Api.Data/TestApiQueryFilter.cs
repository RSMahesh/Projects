
namespace Test.Api.Data
{
    /// <summary>
    /// Definition of a class able to store a query in a generic form.
    /// </summary>
    public class TestApiQueryFilter : ITestApiQueryFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestApiQueryFilter"/> class.
        /// </summary>
        /// <param name="searchField">
        /// Name of the field being searched.
        /// </param>
        /// <param name="equalityOperator">
        /// The equality Operator.
        /// </param>
        /// <param name="searchValue">
        /// Value that is being searched.
        /// </param>
        public TestApiQueryFilter(string searchField, EqualityOperator equalityOperator, object searchValue)
        {
            this.SearchField = searchField;
            this.SearchValue = searchValue;
            this.EqualityOperator = equalityOperator;
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
        public object SearchValue { get; private set; }
    }
}