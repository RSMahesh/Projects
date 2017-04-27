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

namespace InfoWebTicketSystem.WebUI
{
    public partial class SubmitConfirmation : BasePage
    {
        public string TicketNumber = "";
        public string UserEmail = "";
        public string Subject = "";
        public string Message = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTicket();
        }

        private void LoadTicket()
        {
            //TO DO: Validate User aganist ticket number to disaalow user to view other people ticket 
            // TO DO: SP for insert ticket 

            Guid ticketId = new Guid(Request["ticketId"]);
            Ticket tic = new Ticket();
            var dt = tic.GetUserTicket(ticketId);
            TicketNumber = FormatTicketNumber(dt.Rows[0]["TicketNumber"].ToString());
            Subject = dt.Rows[0]["Subject"].ToString();
            UserEmail = dt.Rows[0]["UserEmail"].ToString();
         
            //ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
            //ddlPriority.SelectedValue = dt.Rows[0]["Priority"].ToString();
            //CreatedOn = ((DateTime)dt.Rows[0]["InsertDate"]).ToString("dd MMMM yyyy HH:mm:ss tt");
            //UpdatedOn = ((DateTime)dt.Rows[0]["LastUpdate"]).ToString("dd MMMM yyyy HH:mm:ss tt");
            //TicketType = GetTicketType(dt.Rows[0]["Type"].ToString());


            TicketConversation ticConv = new TicketConversation();
            var dt1 = ticConv.GetConversion(ticketId);
            Message = dt1.Rows[0]["Message"].ToString();

            SendEmail();
        }

        private void SendEmail()
        {

        }
    }
}
