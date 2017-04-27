// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestParser.cs" company="">
//   
// </copyright>
// <summary>
//   Translates the request body to objects to process the request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.Data;
    using Test.Api.WebModels;

    /// <summary>
    /// Translates the request body to objects to process the request.
    /// </summary>
    public sealed class RequestParser : IRequestParser
    {
        /// <summary>
        /// The default count.
        /// </summary>
        private const int DefaultCount = 50;

        /// <summary>
        /// The default offset.
        /// </summary>
        private const int DefaultOffset = 0;

        /// <summary>
        /// The _parser.
        /// </summary>
        private readonly IQueryParser _parser;

        /// <summary>
        /// The _object factory.
        /// </summary>
        private readonly IObjectFactory _objectFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestParser"/> class.
        /// </summary>
        /// <param name="objectFactory">
        /// The object factory.
        /// </param>
        /// <param name="parser">
        /// The parser.
        /// </param>
        public RequestParser(IObjectFactory objectFactory, IQueryParser parser)
        {
            this._objectFactory = objectFactory;
            this._parser = parser;
        }

        public ITestApiContext ParseBodyToContext(
            RequestBody body, 
            IEnumerable<KeyValuePair<string, string>> queryKeyValuePairs, 
            AuthenticatedUser authenticatedUser, 
            EntityId organizationUnitId, 
            EntityId repositoryId)
        {
            var queryString = this.QueryStringToDictionary(queryKeyValuePairs);

            if (body.Pagination != null)
            {
                queryString["Count"] = body.Pagination.Count.ToString(CultureInfo.InvariantCulture);
                queryString["Offset"] = body.Pagination.Offset.ToString(CultureInfo.InvariantCulture);
            }

            queryString["Filters"] = body.Filters;

            return this.ParseQueryStringToContext(queryString, authenticatedUser, organizationUnitId, repositoryId);
        }

        public ITestApiContext ParseQueryStringToContext(
            IEnumerable<KeyValuePair<string, string>> queryKeyValuePairs, 
            AuthenticatedUser authenticatedUser, 
            EntityId organizationUnitId, 
            EntityId repositoryId)
        {
            var metaData = new MetaDataEnvelope();
            var warnings = new List<string>();

            var querystringAsDictionary = this.QueryStringToDictionary(queryKeyValuePairs);

            var filters = querystringAsDictionary.ContainsKey("filters")
                              ? querystringAsDictionary["filters"]
                              : string.Empty;

            var context = new TestApiContext(
                querystringAsDictionary, 
                querystringAsDictionary.ContainsKey("count")
                    ? ParseInt("Count", querystringAsDictionary["count"], warnings)
                    : DefaultCount, 
                querystringAsDictionary.ContainsKey("offset")
                    ? ParseInt("Offset", querystringAsDictionary["offset"], warnings)
                    : DefaultOffset, 
                this.ParseFilters(filters, metaData), 
                authenticatedUser, 
                organizationUnitId, 
                repositoryId, 
                metaData, 
                Enumerable.Empty<string>(), 
                Enumerable.Empty<string>());

            PopulateMetaData(metaData, context, filters);

            querystringAsDictionary.Remove("count");
            querystringAsDictionary.Remove("offset");
            querystringAsDictionary.Remove("filters");

            AddFiltersToDebugIfRequired(context);

            warnings.ForEach(context.AddWarning);

            return context;
        }

        /// <summary>
        /// The add filters to debug if required.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private static void AddFiltersToDebugIfRequired(ITestApiContext context)
        {
            if (context.NeedsDebugInformation())
            {
                context.Debug.Filters = context.Filters;
            }
        }

        /// <summary>
        /// The populate meta data.
        /// </summary>
        /// <param name="metaData">
        /// The meta data.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="filters">
        /// The filters.
        /// </param>
        private static void PopulateMetaData(IMetaDataEnvelope metaData, ITestApiContext context, string filters)
        {
            metaData.AddMetaDataInformation("count", context.Count);
            metaData.AddMetaDataInformation("offset", context.Offset);
            metaData.AddMetaDataInformation("filters", filters);
        }

        /// <summary>
        /// The parse int.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="warnings">
        /// The warnings.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int ParseInt(string key, string value, ICollection<string> warnings)
        {
            int result;
            if (!int.TryParse(value, out result))
            {
                warnings.Add(string.Format("Invalid {0} value '{1}'.  Setting {0} to '0'.", key, value));
            }

            return result;
        }

        /// <summary>
        /// The query string to dictionary.
        /// </summary>
        /// <param name="queryKeyValuePairs">
        /// The query key value pairs.
        /// </param>
        /// <returns>
        /// The <see cref="IDictionary"/>.
        /// </returns>
        private IDictionary<string, string> QueryStringToDictionary(
            IEnumerable<KeyValuePair<string, string>> queryKeyValuePairs)
        {
            return queryKeyValuePairs.ToDictionary(q => q.Key, q => q.Value, StringComparer.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// The parse filters.
        /// </summary>
        /// <param name="filterString">
        /// The filter string.
        /// </param>
        /// <param name="meta">
        /// The meta.
        /// </param>
        /// <returns>
        /// The <see cref="ITestApiQuery"/>.
        /// </returns>
        private ITestApiQuery ParseFilters(string filterString, IMetaDataEnvelope meta)
        {
            if (filterString.IsNullOrEmpty())
            {
                return null;
            }

            meta.AddMetaDataInformation("filters", filterString);

            var filter = this._parser.Parse(filterString);

            return filter;
        }
    }
}