// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlResponseBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   Class implmentation for HtmlResponseBuilder.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Web;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.Producers;
    using Test.Api.WebModels;

    /// <summary>
    /// The html response builder.
    /// </summary>
    public sealed class HtmlResponseBuilder : IHtmlResponseBuilder
    {
        /// <summary>
        /// The _parser.
        /// </summary>
        private readonly IRequestParser _parser;

        /// <summary>
        /// The _ie tag generator locater.
        /// </summary>
        private readonly IETagGeneratorLocater _ieTagGeneratorLocater;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlResponseBuilder"/> class.
        /// </summary>
        /// <param name="parser">
        /// The parser.
        /// </param>
        /// <param name="ieTagGeneratorLocater">
        /// The ie tag generator locater.
        /// </param>
        public HtmlResponseBuilder(IRequestParser parser, IETagGeneratorLocater ieTagGeneratorLocater)
        {
            this._parser = parser;
            this._ieTagGeneratorLocater = ieTagGeneratorLocater;
        }

        /// <summary>
        /// The create context.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ITestApiContext"/>.
        /// </returns>
        public ITestApiContext CreateContext(HttpRequestMessage request)
        {
            var authenticatedUser = (AuthenticatedUser)HttpContext.Current.Items["AuthenticatedUser"];
            return this._parser.ParseQueryStringToContext(
                request.GetQueryNameValuePairs(), 
                authenticatedUser, 
                EntityId.Empty, 
                EntityId.Empty);
        }

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
        public HttpResponseMessage Build(HttpRequestMessage request, Action action)
        {
            try
            {
                action.Invoke();

                return request.CreateResponse(HttpStatusCode.OK);
            }
            catch (PostRequestEntityDetailsNullException ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            catch (ResourceNotFoundException ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

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
        public HttpResponseMessage Build<T>(
            HttpRequestMessage request, 
            Func<ResponseEnvelope<T>> responseBuilder, 
            ITestApiContext context)
        {
            try
            {
                var response = responseBuilder.Invoke();

                var httpResponse = request.CreateResponse(HttpStatusCode.OK, response);

                response.Meta.AddMetaDataInformation("elapsedTimeInMS", context.ElapsedTimeInMS);

                AddETag(httpResponse, (dynamic)response.Payload, response.ApiVersion);

                return httpResponse;
            }
            catch (ArgumentNullException ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        // private void AddETag(HttpResponseMessage response, AssetRecord assetRecord, string apiVersion)
        // {
        // if (assetRecord == null)
        // {
        // return;
        // }

        // this.AddETag(response, assetRecord.Assets, apiVersion);
        // }

        /// <summary>
        /// The add e tag.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="apiVersion">
        /// The api version.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        private void AddETag<T>(HttpResponseMessage response, T value, string apiVersion)
        {
            if (value == null)
            {
                return;
            }

            var typeofT = typeof(T);

            if (typeofT == typeof(string) || typeofT.GetInterface("IEnumerable") == null)
            {
                this.AddETagForSingleRecord(response, value, apiVersion);
                return;
            }

            var enumerableGenericType = typeofT.IsArray ? typeofT.GetElementType() : typeofT.GetGenericArguments()[0];

            var typeOfThis = this.GetType();
            var method = typeOfThis.GetMethod("AddETagForList", BindingFlags.NonPublic | BindingFlags.Instance);
            var genericMethod = method.MakeGenericMethod(enumerableGenericType);

            genericMethod.Invoke(this, new object[] { response, value, apiVersion });
        }

        /// <summary>
        /// The add e tag for single record.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="apiVersion">
        /// The api version.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        private void AddETagForSingleRecord<T>(HttpResponseMessage response, T value, string apiVersion)
        {
            var generator = this._ieTagGeneratorLocater.Find<T>();

            if (generator == null)
            {
                return;
            }

            var eTag = generator.GenerateETag(value, apiVersion);

            if (eTag == string.Empty)
            {
                return;
            }

            response.Headers.ETag = new EntityTagHeaderValue(eTag, true);
        }

        /// <summary>
        /// The add e tag for list.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="apiVersion">
        /// The api version.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        private void AddETagForList<T>(HttpResponseMessage response, IEnumerable<T> value, string apiVersion)
        {
            var generator = this._ieTagGeneratorLocater.Find<T>();

            if (generator == null)
            {
                return;
            }

            var eTag = generator.GenerateETag(value, apiVersion);

            if (eTag == string.Empty)
            {
                return;
            }

            response.Headers.ETag = new EntityTagHeaderValue(eTag, true);
        }
    }
}