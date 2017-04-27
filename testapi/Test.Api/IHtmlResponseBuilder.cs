// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHtmlResponseBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   Class implmentation for IHtmlResponseBuilder.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api
{
    using System;
    using System.Net.Http;

    using Test.Api.Business;
    using Test.Api.WebModels;

    /// <summary>
    /// The HtmlResponseBuilder interface.
    /// </summary>
    public interface IHtmlResponseBuilder
    {
        /// <summary>
        /// The create context.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ITestApiContext"/>.
        /// </returns>
        ITestApiContext CreateContext(HttpRequestMessage request);

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="responseBuilder">
        /// The response builder.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        HttpResponseMessage Build<T>(
            HttpRequestMessage request, 
            Func<ResponseEnvelope<T>> responseBuilder, 
            ITestApiContext context);

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        HttpResponseMessage Build(HttpRequestMessage request, Action action);
    }
}