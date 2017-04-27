
namespace Test.Api.Data
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Test.Api.Core;

    /// <summary>
    /// Generic query to be used within the API and data layer.
    /// </summary>
    public sealed class TestApiQuery : ITestApiQuery
    {
        /// <summary>
        /// The _elements.
        /// </summary>
        private readonly IList<QueryElement> _elements;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestApiQuery"/> class.
        /// </summary>
        public TestApiQuery()
        {
            this._elements = new Collection<QueryElement>();
        }

        /// <summary>
        /// Gets the query elements.
        /// </summary>
        public IReadOnlyCollection<QueryElement> QueryElements
        {
            get
            {
                return new ReadOnlyCollection<QueryElement>(this._elements);
            }
        }

        /// <summary>
        /// Add a clause to start the query.
        /// </summary>
        /// <param name="query">
        /// Class definition of the clause to add.
        /// </param>
        /// <returns>
        /// Instance of the <see cref="TestApiQuery"/> to allow method chaining.
        /// </returns>
        public ITestApiQuery Where(IQuery query)
        {
            this._elements.Add(new QueryElement(query, QueryJoinVerb.None));

            return this;
        }

        /// <summary>
        /// Add an and clause to the query.
        /// </summary>
        /// <param name="query">
        /// Class definition of the clause to add.
        /// </param>
        /// <returns>
        /// Instance of the <see cref="TestApiQuery"/> to allow method chaining.
        /// </returns>
        /// <exception cref="InvalidQueryOperationOrderException">
        /// Indicates the clause is trying to be added to an empty query.
        /// </exception>
        public ITestApiQuery And(IQuery query)
        {
            if (!this._elements.Any())
            {
                throw new InvalidQueryOperationOrderException("And");
            }

            this._elements.Add(new QueryElement(query, QueryJoinVerb.And));

            return this;
        }

        /// <summary>
        /// Add an or clause to the query.
        /// </summary>
        /// <param name="query">
        /// CLass definition of the clause to add.
        /// </param>
        /// <returns>
        /// Instance of the <see cref="TestApiQuery"/> to allow method chaining.
        /// </returns>
        /// <exception cref="InvalidQueryOperationOrderException">
        /// Indicates the clause is trying to be added to an empty query.
        /// </exception>
        public ITestApiQuery Or(IQuery query)
        {
            if (!this._elements.Any())
            {
                throw new InvalidQueryOperationOrderException("Or");
            }

            this._elements.Add(new QueryElement(query, QueryJoinVerb.Or));

            return this;
        }
    }
}