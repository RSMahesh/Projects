// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserHandler.cs" company="">
//   
// </copyright>
// <summary>
//   Handles the logic for the User resource end-point.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IdentityModel;

using Test.Api.Business;

using Test.Api.Services;

namespace Test.Api.Handler
{
    using Entities;
    using System.Collections.Generic;
    using System.Linq;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.Data;
    using Test.Api.Data.Entities;
    using Test.Api.Producers;
    using Test.Api.WebModels;


    /// <summary>
    /// Handles the logic for the User resource end-point.
    /// </summary>
    public class TicketHandler : ITicketHandler
    {
        private const ApiResourceEndPoint ApiResourceEndPointForThisHandler = ApiResourceEndPoint.Tickets;
        /// <summary>
        /// The _response builder.
        /// </summary>
        private readonly IResponseBuilder _responseBuilder;

        /// <summary>
        /// The _object factory.
        /// </summary>
        private readonly IObjectFactory _objectFactory;

        /// <summary>
        /// The unit of work.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEntityPatcher _entityPatcher;

        private ITicketService _ticketService;

        public TicketHandler(IResponseBuilder responseBuilder, ITicketService ticketService, IObjectFactory objectFactory, IEntityPatcher entityPatcher)
        {
            Ensure.Argument.IsNotNull(responseBuilder, "responseBuilder");
            Ensure.Argument.IsNotNull(objectFactory, "objectFactory");
            Ensure.Argument.IsNotNull(ticketService, "ticketService");

            _responseBuilder = responseBuilder;
            _objectFactory = objectFactory;
            _ticketService = ticketService;
            _entityPatcher = entityPatcher;
        }

        /// <summary>
        /// The handle get.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="ResponseEnvelope"/>.
        /// </returns>
        public ResponseEnvelope<IEnumerable<TicketWM>> HandleGet(ITestApiContext context)
        {
            var results = _ticketService.GetAllTickets(context.AuthenticatedUser.Id.ToGuid()).ToList();

            var apiSearchResult = new ApiSearchResult<IEnumerable<Ticket>>(CreateRecordCounts(results.Count()), results);

            var ticketWMList = _responseBuilder.CreateDetails<Ticket, TicketWM>(context, ApiResourceEndPointForThisHandler, apiSearchResult);


            //   var ticketWMList = results.Select(ticket => _objectFactory.Create<TicketWM>(ticket));


            var recordCounts = CreateRecordCounts(ticketWMList.Count());

            var searchResults = new ApiSearchResult<IEnumerable<TicketWM>>(recordCounts, ticketWMList);

            return _responseBuilder.CreateResponse(context, searchResults);




        }

        public ResponseEnvelope<TicketWM> HandlePost(ITestApiContext context, TicketWM ticketWM)
        {
            var ticket = _objectFactory.Create<Ticket>(ticketWM);
            ticket.ID = Guid.NewGuid();
            ticket.UserID = context.AuthenticatedUser.Id;
            _ticketService.AddTicket(ticket);

            var addedTicket = _ticketService.GetTicket(ticket.ID);


            var ticketWMAdded = _objectFactory.Create<TicketWM>(ticket);

            var recordCounts = CreateRecordCounts(1);

            var searchResults = new ApiSearchResult<TicketWM>(recordCounts, ticketWMAdded);

            return _responseBuilder.CreateResponse(context, searchResults);
        }

        public ResponseEnvelope<TicketWM> HandlePut(Guid id, TicketWM ticketWM, ITestApiContext context)
        {
            var ticket = _objectFactory.Create<Ticket>(ticketWM);

            _ticketService.UpdateTicket(ticket);

            var updatedTicket = _ticketService.GetTicket(id);


            var ticketWMUpadted = _objectFactory.Create<TicketWM>(updatedTicket);

            var recordCounts = CreateRecordCounts(1);

            var searchResults = new ApiSearchResult<TicketWM>(recordCounts, ticketWMUpadted);

            return _responseBuilder.CreateResponse(context, searchResults);
        }

        public ResponseEnvelope<TicketWM> HandlePatch(Guid entityId, IEnumerable<PatchOperationBase> changes, ITestApiContext context)
        {

            var ticketEntity = _ticketService.GetTicket(entityId);

            var ticket = _objectFactory.Create<Ticket>(ticketEntity);

            var ticketWM = _objectFactory.Create<TicketWM>(ticket);


            var operationBases = changes as IList<PatchOperationBase> ?? changes.ToList();

            //Todo This logic will be changed when rest of PatchOperation will be decided.
            var replaceOperatonList =
                operationBases.Where(operation => operation.GetType() == typeof(ReplacePatchOperation)).ToList();

            var invalidOperations = replaceOperatonList.Where(x => x.Path.TrimStart('/').Equals("ticketNumber", StringComparison.OrdinalIgnoreCase));

            if (invalidOperations.Any())
            {
                context.AddError("Invalid patch request.");
                throw new BadRequestException();
            }

            _entityPatcher.Patch(ticketWM, replaceOperatonList, context);


            _ticketService.UpdateTicket(_objectFactory.Create<Ticket>(ticketWM));


            var updatedTicket = _ticketService.GetTicket(entityId);


            var ticketWMUpadted = _objectFactory.Create<TicketWM>(updatedTicket);

            var recordCounts = CreateRecordCounts(1);

            var searchResults = new ApiSearchResult<TicketWM>(recordCounts, ticketWMUpadted);

            return _responseBuilder.CreateResponse(context, searchResults);
        }

        public ResponseEnvelope<Guid> HandleDelete(Guid id, ITestApiContext context)
        {

            _ticketService.DeleteTicket(id);

            var recordCounts = CreateRecordCounts(1);

            var searchResults = new ApiSearchResult<Guid>(recordCounts, id);

            return _responseBuilder.CreateResponse(context, searchResults);
        }
        private void Add(ITestApiContext context)
        {
            Ticket ticket = new Ticket();
            ticket.ID = Guid.NewGuid();
            ticket.Subject = "my";
            ticket.UserID = context.AuthenticatedUser.Id.ToGuid();
            _ticketService.AddTicket(ticket);
        }

        private static RecordCounts CreateRecordCounts(int count)
        {
            return new RecordCounts
            {
                StartingRecord = 0,
                RecordsReturned = count,
                TotalRecordsFound = count
            };
        }

        /// <summary>
        /// The get user.
        /// </summary>
        private void GetUser()
        {



            // var list = new List<Entities.Ticket>();
            // foreach (var ticket in ticketList)
            // {
            // list.Add(ticket);
            // }

            // return list;
        }
    }
}