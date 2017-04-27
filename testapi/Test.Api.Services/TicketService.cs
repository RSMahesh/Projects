using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace Test.Api.Services
{
    using System.Runtime.InteropServices;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.Data;
    using Test.Api.Data.Entities;
    using Test.Api.Producers.Translators;

    public class TicketService : ITicketService
    {
        /// <summary>
        /// The _object factory.
        /// </summary>
        private readonly IObjectFactory _objectFactory;

        /// <summary>
        /// The unit of work.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        private IRepository<TicketEntity> _ticketRepository;

        public TicketService(IObjectFactory objectFactory, IUnitOfWork unitOfWork)
        {
            Ensure.Argument.IsNotNull(objectFactory, "objectFactory");
            Ensure.Argument.IsNotNull(unitOfWork, "unitOfWork");

            _objectFactory = objectFactory;
            _unitOfWork = unitOfWork;
            _ticketRepository = _unitOfWork.GetRepository<TicketEntity>();
        }

        public IEnumerable<Ticket> GetAllTickets(Guid userId)
        {
            var results = _ticketRepository.Find(x => x.UserID == userId);

            var tickets = results.Select(ticketEntity => _objectFactory.Create<Ticket>(ticketEntity));

            return tickets;
        }

        public void AddTicket(Ticket ticket)
        {
            var ticketEntity = _objectFactory.Create<TicketEntity>(ticket);
            _ticketRepository.Add(ticketEntity);
            _unitOfWork.SaveChanges();
        }

        public void UpdateTicket(Ticket ticket)
        {
            var ticketEntity = _objectFactory.Create<TicketEntity>(ticket);
            _ticketRepository.Update(ticketEntity);
            _unitOfWork.SaveChanges();
        }

        public void DeleteTicket(Guid id)
        {
           var entity =  _ticketRepository.GetById(id);
            _ticketRepository.Delete(entity);
            _unitOfWork.SaveChanges();
        }


        public Ticket GetTicket(Guid id)
        {
            var ticketEntity = _ticketRepository.GetById(id);
            return _objectFactory.Create<Ticket>(ticketEntity);
        }

    }
}
