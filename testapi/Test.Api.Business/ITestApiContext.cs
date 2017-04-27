// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITestApiContext.cs" company="">
//   
// </copyright>
// <summary>
//   The Context interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System;
    using System.Collections.Generic;

    using Test.Api.Core;

    /// <summary>
    /// The time it delegate.
    /// </summary>
    /// <param name="args">
    /// The args.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    public delegate T TimeItDelegate<out T>(params object[] args);

    /// <summary>
    /// Defines behavior for classes passing around context information.
    /// </summary>
    public interface ITestApiContext
    {
        /// <summary>
        /// Gets the <see cref="Core.DebugLevel"/> requested in the web request.
        /// </summary>
        DebugLevel DebugLevel { get; }

        /// <summary>
        /// Gets a list of warnings that occurred during the request.
        /// </summary>
        IReadOnlyCollection<string> Warnings { get; }

        /// <summary>
        /// Gets a list of errors that occurred during the request.
        /// </summary>
        IReadOnlyCollection<string> Errors { get; }

        /// <summary>
        /// Gets the <see cref="IDebugInformation"/> used to help diagnose problems in the API.
        /// </summary>
        IDebugInformation Debug { get; }

        /// <summary>
        /// Gets the meta data, useful information for clients, for the request.
        /// </summary>
        IMetaDataEnvelope MetaData { get; }

        /// <summary>
        /// Gets the elapsed time for the request in Milliseconds.
        /// </summary>
        long ElapsedTimeInMS { get; }

        /// <summary>
        /// Gets the query parameters.
        /// </summary>
        IReadOnlyDictionary<string, string> QueryParameters { get; }

        /// <summary>
        /// Gets the number of records to return.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the starting record.
        /// </summary>
        int Offset { get; }

        /// <summary>
        /// Gets the authenticated user.
        /// </summary>
        AuthenticatedUser AuthenticatedUser { get; }

        /// <summary>
        /// Gets the organizational unit identifier.
        /// </summary>
        EntityId OrganizationalUnitId { get; }

        /// <summary>
        /// Gets the repository identifier.
        /// </summary>
        EntityId RepositoryId { get; }

        /// <summary>
        /// Add a <see cref="EventTime"/> to the event collection.
        /// </summary>
        /// <param name="description">
        /// The description of the even being added.
        /// </param>
        void AddTimingEvent(string description);

        /// <summary>
        /// Determines if the Debug information should be included in the response.
        /// </summary>
        /// <returns><c>true</c> if <see cref="ITestContext.DebugLevel"/> != <c>DebugLevel.NoDebug</c>: <c>false</c> otherwise <c>false</c>.</returns>
        bool NeedsDebugInformation();

        /// <summary>
        /// Add a timing event for the specified <see cref="Action"/>.
        /// </summary>
        /// <param name="description">
        /// Description of the timing event.
        /// </param>
        /// <param name="action">
        /// Action to time.
        /// </param>
        void TimeIt(string description, Action action);

        /// <summary>
        /// Add a timing event for the specified <see cref="Func{T}"/>.
        /// </summary>
        /// <param name="description">
        /// Description of the timing event.
        /// </param>
        /// <param name="func">
        /// Function to time.
        /// </param>
        /// <typeparam name="T">
        /// Return type from the function.
        /// </typeparam>
        /// <returns>
        /// Return value from <paramref name="func"/>
        /// </returns>
        T TimeIt<T>(string description, Func<T> func);

        /// <summary>
        /// Add a warning to the response.
        /// </summary>
        /// <param name="warning">
        /// The warning message.
        /// </param>
        void AddWarning(string warning);

        /// <summary>
        /// Add an error message to the response.
        /// </summary>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        void AddError(string errorMessage);

        /// <summary>
        /// Add a custom parameter to the query parameters.
        /// </summary>
        /// <remarks>
        /// Use this method to add flags to be used in further processing.  An example would be
        /// to exclude the attributes from an Asset.
        /// </remarks>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        void AddQueryParameter(string key, string value);

        /// <summary>
        /// Gets the Filters query passed in with the request.
        /// </summary>
        IQuery Filters { get; }

        /// <summary>
        /// Gets the include.
        /// </summary>
        IReadOnlyCollection<string> Include { get; }

        /// <summary>
        /// Gets the exclude.
        /// </summary>
        IReadOnlyCollection<string> Exclude { get; }
    }
}