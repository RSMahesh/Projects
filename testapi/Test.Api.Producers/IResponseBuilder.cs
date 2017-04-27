// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IResponseBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   Contract for objects that will create ResponseEnvelopes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers
{
    using Core;
    using System.Collections.Generic;

    using Test.Api.Business;
    using Test.Api.Data;
    using Test.Api.HyperMedia;
    using Test.Api.WebModels;

    /// <summary>
    ///     Contract for objects that will create ResponseEnvelopes.
    /// </summary>
    public interface IResponseBuilder
    {
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
        ResponseEnvelope<T> CreateResponse<T>(ITestApiContext context, IApiSearchResult<T> results);

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// </param>
        /// <param name="endPoint">
        /// The end Point.
        /// </param>
        /// <param name="results">
        /// </param>
        /// <typeparam name="TIn">
        /// </typeparam>
        /// <typeparam name="TOut">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<TOut> CreateDetails<TIn, TOut>(
            ITestApiContext context, 
            ApiResourceEndPoint endPoint, 
            IApiSearchResult<IEnumerable<TIn>> results) where TOut : IWebLinkable;

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// </param>
        /// <param name="endPoint">
        /// The end Point.
        /// </param>
        /// <param name="record">
        /// </param>
        /// <typeparam name="TIn">
        /// </typeparam>
        /// <typeparam name="TOut">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TOut"/>.
        /// </returns>
        TOut CreateDetails<TIn, TOut>(
            ITestApiContext context, 
            ApiResourceEndPoint endPoint, 
            IApiSearchResult<TIn> record) where TOut : IWebLinkable;
    }
}