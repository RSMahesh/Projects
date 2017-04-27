using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using InfoWebTicketSystem.BRL;
using System.IO;


namespace InfoWebTicketSystem.WebUI
{
    public partial class SubmitTicket : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Ticket ticket = new Ticket();
            Guid userId = Guid.NewGuid();
            userId = new Guid("53D3A680-BDB1-4011-A6A2-C5A9DB7A3207");

            Guid ticketId = ticket.InsertTicket("OP", "NR", txtSubject.Text, txtMessage.Text, GetAttachmentPath(FileUpload1), ddlType.SelectedValue, userId);
        }
      
      
        

    }
}
