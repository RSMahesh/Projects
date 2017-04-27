// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResponseBuilder.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The response builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers
{
    using System.Collections.Generic;
    using System.Linq;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.Data;
    using Test.Api.HyperMedia;
    using Test.Api.WebModels;

    /// <summary>
    /// The response builder.
    /// </summary>
    public sealed class ResponseBuilder : IResponseBuilder
    {
        private readonly IObjectFactory _objectFactory;
        private readonly IWebFunctionAppender _functionAppender;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBuilder"/> class.
        /// </summary>
        /// <param name="objectFactory">
        /// The object factory.
        /// </param>
        /// <param name="functionAppender">
        /// Appends functions to Entities.
        /// </param>
        public ResponseBuilder(IObjectFactory objectFactory, IWebFunctionAppender functionAppender)
        {
            Ensure.Argument.IsNotNull(objectFactory, "objectFactory");
            Ensure.Argument.IsNotNull(functionAppender, "functionAppender");

            this._objectFactory = objectFactory;
            this._functionAppender = functionAppender;
        }

        /// <summary>
        /// Create the response from the request context and the record.
        /// </summary>
        /// <param name="context">
        /// Context containing information about the request.
        /// </param>
        /// <param name="results">
        /// The data record for the request.
        /// </param>
        /// <typeparam name="T">
        /// The type of object that is being returned from the request.
        /// </typeparam>
        /// <returns>
        /// The <see cref="ResponseEnvelope{T}"/> describing the data for the request.
        /// </returns>
        public ResponseEnvelope<T> CreateResponse<T>(ITestApiContext context, IApiSearchResult<T> results)
        {
            var response = new ResponseEnvelope<T>
            {
                Meta = context.MetaData, 
                Payload = results.Results,
                RecordCount = CreateRecordCounts(results.RecordCounts), 
                Debug = ExcludeDebugInformation(context), 
                Warnings = context.Warnings.IsNullOrEmpty() ? null : context.Warnings, 
                Errors = context.Errors.IsNullOrEmpty() ? null : context.Errors
            };

            return response;
        }

        /// <summary>
        /// The create details.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="results">
        /// The results.
        /// </param>
        /// <typeparam name="TIn">
        /// </typeparam>
        /// <typeparam name="TOut">
        /// </typeparam>
        /// <returns>
        /// A IEnumerable containing all the results translated.
        /// </returns>
        public IEnumerable<TOut> CreateDetails<TIn, TOut>(
            ITestApiContext context,
            ApiResourceEndPoint endPoint, 
            IApiSearchResult<IEnumerable<TIn>> results)
            //// This constraint is to ensure the object being created is an IWebLinkable.  The reason for the
            //// generic is to use in _objectFactory.Create
            where TOut : IWebLinkable
        {  
            var recordDetails = context.TimeIt("Transform To Response Type", () => results
                                    .Results
                                    .Select(record => this._objectFactory.Create<TOut>(record))).ToList();

            context.TimeIt("Append Functions", () => AppendFunctionsTo(recordDetails, endPoint, context));

            return recordDetails;
        }

        /// <summary>
        /// The create details.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="record">
        /// The record.
        /// </param>
        /// <typeparam name="TIn">
        /// </typeparam>
        /// <typeparam name="TOut">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TOut"/>.
        /// </returns>
        public TOut CreateDetails<TIn, TOut>(ITestApiContext context, ApiResourceEndPoint endPoint, IApiSearchResult<TIn> record)
            //// This constraint is to ensure the object being created is an IWebLinkable.  The reason for the
            //// generic is to use in _objectFactory.Create
            where TOut : IWebLinkable
        {
            var recordDetail = this._objectFactory.Create<TOut>(record.Results);
            AppendFunctionsTo(new[] { recordDetail }, endPoint, context);

            return recordDetail;
        }

        private static RecordCounts CreateRecordCounts(IRecordCounts recordCounts)
        {
            return new RecordCounts
            {
                StartingRecord = recordCounts.StartingRecord,
                RecordsReturned = recordCounts.RecordsReturned,
                TotalRecordsFound = recordCounts.TotalRecordsFound
            };
        }

        private static IDebugInformation ExcludeDebugInformation(ITestApiContext testApiContext)
        {
            if (!testApiContext.NeedsDebugInformation())
            {
                return null;
            }

            if (testApiContext.DebugLevel == DebugLevel.InternalQueryOnly)
            {
                return new DebugInformation { Filters = testApiContext.Debug.Filters };
            }

            if (testApiContext.DebugLevel == DebugLevel.TimingsOnly)
            {
                var debug = new DebugInformation();

                testApiContext.Debug.Timings.ForEach(debug.AddTimingEvent);

                return debug;
            }

            return testApiContext.Debug;
        }

        private void AppendFunctionsTo<T>(IEnumerable<T> webLinkable, ApiResourceEndPoint endPoint, ITestApiContext context)
            where T : IWebLinkable
        {
            webLinkable.ForEach(linkable => this._functionAppender.AppendLinksTo(linkable, endPoint, context));
        }
    }
}