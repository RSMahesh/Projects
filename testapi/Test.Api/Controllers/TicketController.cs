// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="">
//   
// </copyright>
// <summary>
//   The HomeController controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Test.Api.Business;
using Test.Api.Producers;

namespace Test.Api.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Test.Api.Handler;
    using WebModels;
    /// <summary>
    ///     The HomeController controller.
    /// </summary>
    public class TicketController : ApiController
    {
        /// <summary>
        /// The _html response builder.
        /// </summary>
        private readonly IHtmlResponseBuilder _htmlResponseBuilder;

        /// <summary>
        /// The _user handler.
        /// </summary>
        private readonly ITicketHandler _ticketHandler;


        private readonly IPatchOperationsTranslator _patchOperationsTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="htmlResponseBuilder">
        /// The html response builder.
        /// </param>
        /// <param name="ticketHandler">
        /// The user handler.
        /// </param>
        public TicketController(IHtmlResponseBuilder htmlResponseBuilder, ITicketHandler ticketHandler)
        {
            _htmlResponseBuilder = htmlResponseBuilder;
            _ticketHandler = ticketHandler;
        }

        /// <summary>
        ///     Retrieve a lists of all functions that are available to the authenticated user.
        /// </summary>
        /// <remarks>
        ///     These functions, combined with the _links attribute of models, should be used by clients to determine
        ///     how to call the API.
        /// </remarks>
        /// <returns>
        ///     List of end-points/verbs that are available to the authenticated user.
        /// </returns>
        /// <response code="200">Request was successful.</response>
        /// <response code="401">The current user is not authorized to view this end-point.</response>
        /// <response code="500">The request failed because of a problem on the server.</response>
        [Route("tickets")]
        [HttpGet]
        public Task<HttpResponseMessage> GetTicketsAsync()
        {
            var context = _htmlResponseBuilder.CreateContext(Request);

            var response = _htmlResponseBuilder.Build(Request, () => _ticketHandler.HandleGet(context), context);

            return Task.FromResult(response);
        }


        [Route("tickets")]
        [HttpPost]
        public Task<HttpResponseMessage> CreateTicketAsync([FromBody] TicketWM ticketWM)
        {
            var context = _htmlResponseBuilder.CreateContext(Request);

            var response = _htmlResponseBuilder.Build(Request, () => _ticketHandler.HandlePost(context, ticketWM), context);

            response.StatusCode = response.IsSuccessStatusCode
                 ? HttpStatusCode.Created
                 : response.StatusCode;

            return Task.FromResult(response);
        }
    

        [Route("tickets/{id}")]
        [HttpPut]
        public Task<HttpResponseMessage> UpdateTicketAsync(Guid id, [FromBody] TicketWM ticketWM)
        {

            var context = _htmlResponseBuilder.CreateContext(Request);

            var response = _htmlResponseBuilder.Build(Request, () => _ticketHandler.HandlePut(id, ticketWM, context), context);

            return Task.FromResult(response);
        }

        [Route("tickets/{id}")]
        [HttpPatch]
        public Task<HttpResponseMessage> PatchTicketAsync(Guid id, [FromBody] IEnumerable<PatchOperation> changes)
        {

            var context = _htmlResponseBuilder.CreateContext(Request);

            var response = _htmlResponseBuilder.Build(Request, () => _ticketHandler.HandlePatch(
                 id.ToEntityId(),
                 _patchOperationsTranslator.Translate(changes, context),
                 context), context);

       
            return Task.FromResult(response);
        }

        [Route("tickets/{id}")]
        [HttpDelete]
        public Task<HttpResponseMessage> DeleteTicketAsync(Guid id)
        {

            var context = _htmlResponseBuilder.CreateContext(Request);

            var response = _htmlResponseBuilder.Build(Request, () => _ticketHandler.HandleDelete(id, context), context);

            response.StatusCode = response.IsSuccessStatusCode
                ? HttpStatusCode.Gone
                : response.StatusCode;

            return Task.FromResult(response);
        }
    }
}