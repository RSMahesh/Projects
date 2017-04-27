
namespace Test.Api.Business
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Test.Api.Core;

    /// <summary>
    /// Class for passing around arguments from the QueryString.
    /// </summary>
    public class TestApiContext : ITestApiContext
    {
        /// <summary>
        /// The _stop watch.
        /// </summary>
        private readonly Stopwatch _stopWatch;

        /// <summary>
        /// The _query parameters.
        /// </summary>
        private readonly IDictionary<string, string> _queryParameters;

        /// <summary>
        /// The _warnings.
        /// </summary>
        private readonly IList<string> _warnings;

        /// <summary>
        /// The _errors.
        /// </summary>
        private readonly IList<string> _errors;

    
        public TestApiContext(
            IDictionary<string, string> queryParameters, 
            int count, 
            int offset, 
            IQuery filters, 
            AuthenticatedUser user, 
            EntityId organizationalUnitId, 
            EntityId repositoryId, 
            IMetaDataEnvelope metaData, 
            IEnumerable<string> warnings, 
            IEnumerable<string> errors)
        {
            Ensure.Argument.IsNotNull(queryParameters, "queryParameters");

            this._queryParameters = queryParameters;

            this._stopWatch = Stopwatch.StartNew();
            this._warnings = warnings.ToList();
            this._errors = errors.ToList();

            this.Filters = filters;
            this.Count = count;
            this.Offset = offset;

            this.AuthenticatedUser = user;
            this.OrganizationalUnitId = organizationalUnitId;
            this.RepositoryId = repositoryId;

            this.MetaData = metaData;

            this.ParseQueryParameters(this._queryParameters);

            this.Include = this.ExtractFieldsAndRemoveFromQueryParametersFor("include", "*");
            this.Exclude = this.ExtractFieldsAndRemoveFromQueryParametersFor("exclude");

            this.Debug = new DebugInformation();
        }

        /// <summary>
        /// The extract fields and remove from query parameters for.
        /// </summary>
        /// <param name="fieldListName">
        /// The field list name.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <returns>
        /// The <see cref="IReadOnlyCollection"/>.
        /// </returns>
        private IReadOnlyCollection<string> ExtractFieldsAndRemoveFromQueryParametersFor(
            string fieldListName, 
            string defaultValue = "")
        {
            if (!this._queryParameters.ContainsKey(fieldListName))
            {
                return
                    (defaultValue == string.Empty ? Enumerable.Empty<string>() : new[] { defaultValue })
                        .ToReadOnlyCollection();
            }

            var list =
                this._queryParameters[fieldListName].Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToReadOnlyCollection();

            this._queryParameters.Remove(fieldListName);

            return list;
        }

        /// <summary>
        /// Gets the <see cref="Core.DebugLevel"/> requested in the web request.
        /// </summary>
        public DebugLevel DebugLevel { get; private set; }

        /// <summary>
        /// Gets the <see cref="DebugInformation"/> used to help diagnose problems in the API.
        /// </summary>
        public IDebugInformation Debug { get; private set; }

        /// <summary>
        /// Gets the meta data, useful information for clients, for the request.
        /// </summary>
        public IMetaDataEnvelope MetaData { get; private set; }

        /// <summary>
        /// Add a custom parameter to the query parameters.
        /// </summary>
        /// <remarks>
        /// Use this method to add flags to be used in further processing.  An example would be
        /// to exclude the attributes from an Asset.  This method will not over-write an existing value.
        /// </remarks>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        public void AddQueryParameter(string key, string value)
        {
            if (this._queryParameters.ContainsKey(key))
            {
                return;
            }

            this._queryParameters[key] = value;
        }

        /// <summary>
        /// Gets the Filters query passed in with the request.
        /// </summary>
        public IQuery Filters { get; private set; }

        /// <summary>
        /// Gets the include.
        /// </summary>
        public IReadOnlyCollection<string> Include { get; private set; }

        /// <summary>
        /// Gets the exclude.
        /// </summary>
        public IReadOnlyCollection<string> Exclude { get; private set; }

        /// <summary>
        /// Gets the number of records to return.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the starting record.
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// Gets the authenticated user.
        /// </summary>
        /// <value>
        /// The authenticated user.
        /// </value>
        public AuthenticatedUser AuthenticatedUser { get; private set; }

        /// <summary>
        /// Gets the organizational unit identifier.
        /// </summary>
        public EntityId OrganizationalUnitId { get; private set; }

        /// <summary>
        /// Gets the repository identifier.
        /// </summary>
        public EntityId RepositoryId { get; private set; }

        /// <summary>
        /// Gets the elapsed time for the request in Milliseconds.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public long ElapsedTimeInMS
        {
            get
            {
                return this._stopWatch.ElapsedMilliseconds;
            }
        }

        /// <summary>
        /// Gets a list of warnings that occurred during the request.
        /// </summary>
        public IReadOnlyCollection<string> Warnings
        {
            get
            {
                return this._warnings.ToReadOnlyCollection();
            }
        }

        /// <summary>
        /// Gets a list of errors that occurred during the request.
        /// </summary>
        public IReadOnlyCollection<string> Errors
        {
            get
            {
                return this._errors.ToReadOnlyCollection();
            }
        }

        /// <summary>
        /// Gets the query parameters.
        /// </summary>
        public IReadOnlyDictionary<string, string> QueryParameters
        {
            get
            {
                return new ReadOnlyDictionary<string, string>(this._queryParameters);
            }
        }

        /// <summary>
        /// Add a <see cref="EventTime"/> to the event collection.
        /// </summary>
        /// <param name="description">
        /// The description of the even being added.
        /// </param>
        public void AddTimingEvent(string description)
        {
            this.Debug.AddTimingEvent(new EventTime(description, this._stopWatch.ElapsedMilliseconds));
        }

        public bool NeedsDebugInformation()
        {
            return this.DebugLevel != DebugLevel.NoDebug;
        }

        /// <summary>
        /// Add a timing event for the specified <see cref="Action"/>.
        /// </summary>
        /// <param name="description">
        /// Description of the timing event.
        /// </param>
        /// <param name="action">
        /// Action to time.
        /// </param>
        public void TimeIt(string description, Action action)
        {
            action.Invoke();

            this.AddTimingEvent(string.Format(@"{0}", description));
        }

        /// <summary>
        /// Add a timing event for the specified <see cref="Action"/>.
        /// </summary>
        /// <param name="description">
        /// Description of the timing event.
        /// </param>
        /// <param name="func">
        /// Function to time.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T TimeIt<T>(string description, Func<T> func)
        {
            var retValue = func.Invoke();

            this.AddTimingEvent(string.Format(@"{0}", description));

            return retValue;
        }

        /// <summary>
        /// Add a warning to the response.
        /// </summary>
        /// <param name="warning">
        /// The warning message.
        /// </param>
        public void AddWarning(string warning)
        {
            this._warnings.Add(warning);
        }

        /// <summary>
        /// Add an error message to the response.
        /// </summary>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        public void AddError(string errorMessage)
        {
            this._errors.Add(errorMessage);
        }

        /// <summary>
        /// The parse query parameters.
        /// </summary>
        /// <param name="queryParameters">
        /// The query parameters.
        /// </param>
        private void ParseQueryParameters(IDictionary<string, string> queryParameters)
        {
            this.DebugLevel = queryParameters.ContainsKey("debug")
                                  ? this.ParseDebug(queryParameters)
                                  : DebugLevel.NoDebug;
        }

        /// <summary>
        /// The parse debug.
        /// </summary>
        /// <param name="queryParameters">
        /// The query parameters.
        /// </param>
        /// <returns>
        /// The <see cref="DebugLevel"/>.
        /// </returns>
        private DebugLevel ParseDebug(IDictionary<string, string> queryParameters)
        {
            var debug = queryParameters["debug"];
            DebugLevel debugLevel;

            if (Enum.TryParse(debug, true, out debugLevel))
            {
                queryParameters.Remove("debug");
                return debugLevel;
            }

            this._warnings.Add(string.Format(@"Invalid debug value '{0}'.  Defaulting to NoDebug.", debug));

            return DebugLevel.NoDebug;
        }
    }
}