using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoWebTicketSystem.BRL.Interface;
using Repository.Interface;
using System.IO;

namespace InfoWebTicketSystem.BRL
{
    public class TicketService : ITicketService
    {
        public TicketService()
        {

        }
        IUnitOfWork unitOfWork = UnitWorkFactory.GetUnitOfWork();

        public Guid AddTicket(Entities.Ticket ticket)
        {
            // var unitOfWork = UnitWorkFactory.GetUnitOfWork();
            var ticketRepositary = unitOfWork.GetRepository<Entities.Ticket>();
            ticket.ID = Guid.NewGuid();
            ticket.TicketConversations.ElementAt(0).ID = Guid.NewGuid();
            ticket.TicketConversations.ElementAt(0).LastUpdatedUserID = ticket.LastUpdatedUserID;
            ticket.Priority = "NORMAL";
            ticket.Status = "OPEN";
            ticketRepositary.Add(ticket);
            unitOfWork.SaveChanges();
            return ticket.ID;
        }

        public void UpdateTicket(Entities.Ticket ticket)
        {

            var ticketRepositary = unitOfWork.GetRepository<Entities.Ticket>();
            ticketRepositary.Update(ticket);
            unitOfWork.SaveChanges();
        }

        public List<Entities.Ticket> GetUserAllTickets(string userEmailId)
        {
            throw new NotImplementedException();
        }

        public List<Entities.Ticket> GetALLTickets()
        {
            ///var unitOfWork = UnitWorkFactory.GetUnitOfWork();
            var ticketRepositary = unitOfWork.GetRepository<Entities.Ticket>();
            var ticketList = ticketRepositary.All();
            var list = new List<Entities.Ticket>();
            foreach (var ticket in ticketList)
            {
                list.Add(ticket);
            }

            return list;
        }

        public Entities.Ticket GetTicket(Guid ticketId)
        {
            // var unitOfWork = UnitWorkFactory.GetUnitOfWork();
            var ticketRepositary = unitOfWork.GetRepository<Entities.Ticket>();
            return ticketRepositary.GetById(ticketId);
        }

        string uploadedFileNameSeperator = "______";

        public string SaveUploadedFile(string rootPath, System.Web.HttpPostedFileBase uploadedFile)
        {
            if(!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            string file = rootPath + "\\" + Guid.NewGuid().ToString() + uploadedFileNameSeperator + uploadedFile.FileName;
            uploadedFile.SaveAs(file);
            return file;
        }

        public string GetUploadedFileName(string file)
        {
            if(string.IsNullOrEmpty(file))
                return "";

           var arr = file.Split(new string[]{ uploadedFileNameSeperator},StringSplitOptions.None);
           return arr[1];
        }

    }
}
