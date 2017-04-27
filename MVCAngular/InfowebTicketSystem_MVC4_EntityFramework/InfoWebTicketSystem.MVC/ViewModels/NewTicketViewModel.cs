
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InfoWebTicketSystem.MVC.ViewModels
{
    public class SelectDepartmentViewModel
    {
        [Display(Name = "Select Department")]
        public string Department { get; set; }
    }

    public class CollectTicketDetailViewModal
    {
        public string TicketType { get; set; }
        [StringLength(100)]
        public string UserEmail { get; set; }
        [StringLength(500)]
        public string Domain { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Attachment { get; set; }
        public HttpPostedFileBase AttachedFile { get; set; }
    }

    public class TicketList
    {
        public List<Ticket> Tickets = new List<Ticket>();
    }

    public class Ticket
    {
        public string ID { get; set; }
        public string TicketNumber { get; set; }
        public string LastReplie { get; set; }
        public string Department { get; set; }
        public string Type { get; set; }
        public string UserEmail { get; set; }
        public string Domain { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Subject { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }


    public class SubmitConfirmationViewModal
    {
        public string TicketNumber { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}