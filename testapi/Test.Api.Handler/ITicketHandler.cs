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
    using System;
    using System.Collections.Generic;

    using Test.Api.Business;
    using Test.Api.WebModels;

    /// <summary>
    /// The UserHandler interface.
    /// </summary>
    public interface ITicketHandler
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
        ResponseEnvelope<IEnumerable<TicketWM>> HandleGet(ITestApiContext context);

        ResponseEnvelope<TicketWM> HandlePost(ITestApiContext context, TicketWM ticketWM);

        ResponseEnvelope<TicketWM> HandlePut(Guid id, TicketWM ticketWM, ITestApiContext context);
        ResponseEnvelope<Guid> HandleDelete(Guid id, ITestApiContext context);

        ResponseEnvelope<TicketWM> HandlePatch(Guid ticketId, IEnumerable<PatchOperationBase> changes, ITestApiContext context);
    }
}