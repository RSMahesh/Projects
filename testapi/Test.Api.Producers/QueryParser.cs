// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryParser.cs" company="">
//   
// </copyright>
// <summary>
//   The query parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Test.Api.Core;
    using Test.Api.Data;

    /// <summary>
    ///     Parse queries passed through the QueryString into an internal object representation.
    /// </summary>
    public sealed class QueryParser : IQueryParser
    {
        /// <summary>
        /// The equality operators.
        /// </summary>
        private const string EqualityOperators = "(eq|ne|gt|lt|ge|le|sw|ew|ct)";

        /// <summary>
        /// The join operators.
        /// </summary>
        private static readonly string[] JoinOperators = { " or ", " and " };

        /// <summary>
        /// The starts with operator.
        /// </summary>
        private static readonly Regex StartsWithOperator = new Regex(
            @"^(\s*)?(and|or)", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// The first operator index.
        /// </summary>
        private static readonly Regex FirstOperatorIndex = new Regex(
            @"(and|or)", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// The clause splitter.
        /// </summary>
        private static readonly Regex ClauseSplitter = new Regex(
            string.Format(@"\s{0}\s", EqualityOperators), 
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// The _object factory.
        /// </summary>
        private readonly IObjectFactory _objectFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParser"/> class.
        /// </summary>
        /// <param name="objectFactory">
        /// Factory for creating objects at runtime.
        /// </param>
        public QueryParser(IObjectFactory objectFactory)
        {
            this._objectFactory = objectFactory;
        }

        /// <summary>
        /// The join operator.
        /// </summary>
        private enum JoinOperator
        {
            /// <summary>
            ///     JoinOperator is Where.
            /// </summary>
            Where, 

            /// <summary>
            ///     JoinOperator is And.
            /// </summary>
            And, 

            /// <summary>
            ///     JoinOperator is Or.
            /// </summary>
            Or
        }

        /// <summary>
        /// Parses the query from the QueryString into an internal generic representation of the query.
        /// </summary>
        /// <param name="query">
        /// The search element as it is passed in through the QueryString.
        /// </param>
        /// <returns>
        /// <see cref="ITestApiQuery"/> tree representing the query as passed in.
        /// </returns>
        public ITestApiQuery Parse(string query)
        {
            return (ITestApiQuery)this.ProcessGrouping(query);
        }

        /// <summary>
        /// The add next operator.
        /// </summary>
        /// <param name="nextOperator">
        /// The next operator.
        /// </param>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="clause">
        /// The clause.
        /// </param>
        private static void AddNextOperator(JoinOperator nextOperator, ITestApiQuery query, IQuery clause)
        {
            switch (nextOperator)
            {
                case JoinOperator.And:
                    query.And(clause);
                    break;
                case JoinOperator.Or:
                    query.Or(clause);
                    break;
                default:
                    query.Where(clause);
                    break;
            }
        }

        /// <summary>
        /// The next operator.
        /// </summary>
        /// <param name="segment">
        /// The segment.
        /// </param>
        /// <returns>
        /// The <see cref="JoinOperator"/>.
        /// </returns>
        private static JoinOperator NextOperator(string segment)
        {
            if (!StartsWithOperator.IsMatch(segment))
            {
                return JoinOperator.Where;
            }

            JoinOperator nextOperator;
            return Enum.TryParse(StartsWithOperator.Match(segment).Value.Trim(), true, out nextOperator)
                       ? nextOperator
                       : JoinOperator.Where;
        }

        /// <summary>
        /// The is grouped by brackets.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsGroupedByBrackets(string query)
        {
            return query.StartsWith("(") && query.EndsWith(")");
        }

        /// <summary>
        /// The check for and remove brackets.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string CheckForAndRemoveBrackets(string query)
        {
            return IsGroupedByBrackets(query) ? query.Substring(1, query.Length - 2) : query;
        }

        /// <summary>
        /// The extract grouping.
        /// </summary>
        /// <param name="clause">
        /// The clause.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string ExtractGrouping(string clause)
        {
            var openParenthesisIndex = 0;

            var firstOperatorIndex = FirstOperatorIndex.Match(clause).Index;

            var counter = 0;
            for (var i = 0; i < clause.Length; i++)
            {
                var character = clause[i];

                switch (character)
                {
                    case '(':

                        if (FirstOperatorIsBeforeAGrouping(i, firstOperatorIndex))
                        {
                            return string.Empty;
                        }

                        firstOperatorIndex = clause.Length;
                        if (counter++ == 0)
                        {
                            openParenthesisIndex = i;
                        }

                        break;
                    case ')':

                        if (--counter == 0)
                        {
                            return clause.Substring(openParenthesisIndex, i + 1 - openParenthesisIndex);
                        }

                        break;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The first operator is before a grouping.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <param name="firstOperatorIndex">
        /// The first operator index.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool FirstOperatorIsBeforeAGrouping(int i, int firstOperatorIndex)
        {
            return i >= firstOperatorIndex;
        }

        /// <summary>
        /// The create clause.
        /// </summary>
        /// <param name="clause">
        /// The clause.
        /// </param>
        /// <returns>
        /// The <see cref="ITestApiQueryFilter"/>.
        /// </returns>
        private ITestApiQueryFilter CreateClause(string clause)
        {
            var stringSplit = ClauseSplitter.Split(clause);

            EqualityOperator op;
            Enum.TryParse(stringSplit[1].Trim(), true, out op);

            return this._objectFactory.Create<TestApiQueryFilter>(stringSplit[0], op, stringSplit[2].Trim());
        }

        /// <summary>
        /// The process grouping.
        /// </summary>
        /// <param name="grouping">
        /// The grouping.
        /// </param>
        /// <returns>
        /// The <see cref="IQuery"/>.
        /// </returns>
        private IQuery ProcessGrouping(string grouping)
        {
            var query = this._objectFactory.Create<TestApiQuery>();

            var outerGrouping = grouping;
            while (!string.IsNullOrWhiteSpace(outerGrouping))
            {
                var innerOperator = NextOperator(outerGrouping);
                outerGrouping = StartsWithOperator.Replace(outerGrouping, string.Empty);

                var innerGrouping = ExtractGrouping(outerGrouping);
                if (innerGrouping != string.Empty)
                {
                    AddNextOperator(
                        innerOperator, 
                        query, 
                        this.ProcessGrouping(CheckForAndRemoveBrackets(innerGrouping)));
                    outerGrouping = outerGrouping.Replace(innerGrouping, string.Empty);
                }
                else
                {
                    var segments = outerGrouping.Split(JoinOperators, StringSplitOptions.RemoveEmptyEntries);
                    if (!segments.Any())
                    {
                        return query;
                    }

                    var segment = segments[0].Trim();
                    outerGrouping = outerGrouping.Replace(segment, string.Empty).Trim();

                    AddNextOperator(innerOperator, query, this.CreateClause(CheckForAndRemoveBrackets(segment)));
                }
            }

            return query;
        }
    }
}