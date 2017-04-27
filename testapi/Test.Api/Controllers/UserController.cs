// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="">
//   
// </copyright>
// <summary>
//   The HomeController controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Test.Api.Business;

namespace Test.Api.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Test.Api.Handler;

    /// <summary>
    ///     The HomeController controller.
    /// </summary>
    public class UserController : ApiController
    {
        /// <summary>
        /// The _html response builder.
        /// </summary>
        private readonly IHtmlResponseBuilder _htmlResponseBuilder;

        /// <summary>
        /// The _user handler.
        /// </summary>
        private readonly IUserHandler _userHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="htmlResponseBuilder">
        /// The html response builder.
        /// </param>
        /// <param name="userHandler">
        /// The user handler.
        /// </param>
        public UserController(IHtmlResponseBuilder htmlResponseBuilder, IUserHandler userHandler)
        {
            _htmlResponseBuilder = htmlResponseBuilder;
            _userHandler = userHandler;
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
        [Route("users")]
        [HttpGet]
        public Task<HttpResponseMessage> ListUsersAsync()
        {
            var context = _htmlResponseBuilder.CreateContext(Request);

            var response = _htmlResponseBuilder.Build(Request, () => _userHandler.HandleGet(context), context);

            return Task.FromResult(response);
        }

        /// <summary>
        ///     Retrieve detailed information for the current user.
        /// </summary>
        /// <returns>
        ///     A model representing the current user.
        /// </returns>
        /// <response code="200">Request was successful.</response>
        /// <response code="401">The current user is not authorized to view this end-point.</response>
        /// <response code="500">The request failed because of a problem on the server.</response>
        [Route("users/current")]
        [HttpGet]
      //  [a(SecurableObjectType.Organization, ulong.MaxValue)]
        public Task<HttpResponseMessage> GetCurrentUserAsync()
        {
            var context = _htmlResponseBuilder.CreateContext(Request);

            var response = _htmlResponseBuilder.Build(Request, () => _userHandler.HandleCurrentUserGet(context), context);

            return Task.FromResult(response);
        }
    }
}