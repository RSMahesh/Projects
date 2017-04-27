namespace Test.Api.Services
{
    using System;
    using System.Collections.Generic;
    using Test.Api.Business;

    public interface ITicketService
    {
       IEnumerable<Ticket> GetAllTickets(Guid userId);

        void AddTicket(Ticket ticket);

        void UpdateTicket(Ticket ticket);

         Ticket GetTicket(Guid id);

        void DeleteTicket(Guid id);
    }
}