// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserHandler.cs" company="">
//   
// </copyright>
// <summary>
//   The UserHandler interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Handler
{
    using System.Collections.Generic;

    using Test.Api.Business;
    using Test.Api.WebModels;

    /// <summary>
    /// The UserHandler interface.
    /// </summary>
    public interface IUserHandler
    {
        /// <summary>
        /// The handle get.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="ResponseEnvelope"/>.
        /// </returns>
        ResponseEnvelope<IEnumerable<UserWM>> HandleGet(ITestApiContext context);

        /// <summary>
        /// Handles a get request for a current user Id.
        /// </summary>
        /// <param name="context">
        /// Information about the request.
        /// </param>
        /// <returns>
        /// The response to return to the client.
        /// </returns>
        ResponseEnvelope<UserWM> HandleCurrentUserGet(ITestApiContext context);
    }
}