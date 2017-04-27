using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoWebTicketSystem.MVC.ViewModels
{
    public class ViewTicketViewModel
    {
        public ViewTicketViewModel()
        {
            Conversation = new List<TicketConversation>();
        }
        public System.Guid ID { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Department { get; set; }
        public string TicketType { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Message { get; set; }
        public string Attachment { get; set; }
        public HttpPostedFileBase AttachedFile { get; set; }
        public List<TicketConversation> Conversation { get; set; }
    }

    public class TicketConversation
    {
        public string Message { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachmentFileRaw { get; set; }
        public string User { get; set; }
        public string Staff { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string GotAttachment
        {
            get
            {
                if (!string.IsNullOrEmpty(AttachmentFileName) &&  AttachmentFileName.Trim().Length > 0)
                    return "";
                else
                    return "none";
            }
        }
    }
}