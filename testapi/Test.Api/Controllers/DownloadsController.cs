// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DownloadsController.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   Class implmentation for DownloadsController.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MediaValet.Api.Handler;
using MediaValet.Api.WebModels;
using MediaValet.Core;

namespace MediaValet.Api.Controllers
{
    /// <summary>
    ///     Class implementation for DownloadsController.cs.
    /// </summary>
    public class DownloadsController : ApiController
    {
        private readonly IDownloadsHandler _handler;
        private readonly IHtmlResponseBuilder _htmlResponseBuilder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DownloadsController" /> class.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="htmlResponseBuilder"></param>
        public DownloadsController(
            IDownloadsHandler handler,
            IHtmlResponseBuilder htmlResponseBuilder)
        {
            Ensure.Argument.IsNotNull(handler, "handler");
            Ensure.Argument.IsNotNull(htmlResponseBuilder, "htmlResponseBuilder");

            _handler = handler;
            _htmlResponseBuilder = htmlResponseBuilder;
        }

        /// <summary>
        ///     List all downloads for the authenticated user.
        /// </summary>
        /// <returns>
        ///     List of downloads that have not been hidden.
        /// </returns>
        /// <response code="200">Request was successful.</response>
        /// <response code="401">The current user is not authorized to view this end-point.</response>
        /// <response code="500">The request failed because of a problem on the server.</response>
        [Route("downloads")]
        [HttpGet]
        public Task<HttpResponseMessage> ListAllDownloadsAsync()
        {
            var context = _htmlResponseBuilder.CreateContext(Request);

            var getResponse = _handler.HandleGetRequest(context);

            var statusCode = getResponse.Payload.Hash == string.Empty
                ? HttpStatusCode.RequestTimeout
                : HttpStatusCode.OK;

            var response = _htmlResponseBuilder.Build(Request, () => getResponse, context);

            response.StatusCode = statusCode;

            return Task.FromResult(response);
        }

        /// <summary>
        ///     Creates a new download.
        /// </summary>
        /// <param name="downloadSettingsPostDetail">
        ///     Download settings object.
        /// </param>
        /// <returns>
        ///     The id of the download record.
        /// </returns>
        /// <response code="201">Request was successful.</response>
        /// <response code="401">The current user is not authorized to view this end-point.</response>
        /// <response code="500">The request failed because of a problem on the server.</response>
        [Route("downloads")]
        [HttpPost]
        public Task<HttpResponseMessage> CreateDownloadAsync(
            [FromBody] DownloadSettingsPostDetail downloadSettingsPostDetail)
        {
            var context = _htmlResponseBuilder.CreateContext(Request);

            var response = _htmlResponseBuilder.Build(Request, () => _handler.HandleDownloadRequest(downloadSettingsPostDetail, context), context);

            response.StatusCode = response.IsSuccessStatusCode
                ? HttpStatusCode.Created
                : response.StatusCode;

            return Task.FromResult(response);
        }
        
        /// <summary>
        ///     Retrieve detailed information for a specific download.
        /// </summary>
        /// <param name="id">
        ///     The id of the download.
        /// </param>
        /// <returns>
        ///     Detailed information about the specified download.
        /// </returns>
        /// <response code="200">Request was successful.</response>
        /// <response code="401">The current user is not authorized to view this end-point.</response>
        /// <response code="404">The download was not found.</response>
        /// <response code="500">The request failed because of a problem on the server.</response>
        [Route("downloads/{id}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetBatchDetailsByIdAsync(Guid id)
        {
            var context = _htmlResponseBuilder.CreateContext(Request);
            
            var response = _htmlResponseBuilder.Build(Request, () => _handler.HandleGetRequestById(id, context), context);

            return Task.FromResult(response);
        }
    }
}