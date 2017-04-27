// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="">
//   
// </copyright>
// <summary>
//   The HomeController controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.Handler;
    using Test.Api.Producers;

    /// <summary>
    ///     The HomeController controller.
    /// </summary>
    public class HomeController : ApiController
    {
        private readonly IHomeHandler _homeHandler;
        private readonly IRequestParser _parser;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="homeHandler">The home handler.</param>
        /// <param name="parser">The context builder.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="homeHandler" /> is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="parser" /> is null.</exception>
        public HomeController(IHomeHandler homeHandler, IRequestParser parser)
        {
            Ensure.Argument.IsNotNull(homeHandler, "homeHandler");
            Ensure.Argument.IsNotNull(parser, "parser");

            _homeHandler = homeHandler;
            _parser = parser;
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
        [HttpGet]
        [Route("")]
        public Task<HttpResponseMessage> ListAllFunctionsForCurrentUserAsync()
        {
            try
            {
                var authenticatedUser = (AuthenticatedUser)HttpContext.Current.Items["AuthenticatedUser"];
                var context = _parser.ParseQueryStringToContext(
                    Request.GetQueryNameValuePairs(),
                    authenticatedUser,
                    authenticatedUser.OrgUnitId.ToEntityId(),
                    EntityId.Empty);

                //implict operator testing
                Guid dd = EntityId.Empty;
                EntityId id = Guid.NewGuid();

                context.AddTimingEvent("Request Start");

                var response = _homeHandler.HandleGet(context);

                context.AddTimingEvent("Requst End");

                return Task.FromResult(Request.CreateResponse(response));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }
    }
}