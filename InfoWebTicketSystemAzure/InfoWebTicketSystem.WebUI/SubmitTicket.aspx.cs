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

using System.Text.RegularExpressions;


namespace InfoWebTicketSystem.WebUI
{
    public partial class SubmitTicket : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Request["button"] != null && Request["button"] == "Submit")
                {
                    SubmitTicket123();
                 
                }
            }
        }
        
        private void SubmitTicket123()
        {
            if (!ValidateRequest())
            {
                return;
            }
            Ticket ticket = new Ticket();
            Guid userId = Guid.NewGuid();
            string lastReplier = "";
            userId = new Guid("53D3A680-BDB1-4011-A6A2-C5A9DB7A3207");

            if( Session["customer"] != null)
            {
                Customer cust = (Customer) Session["customer"];
                lastReplier = cust.CusInfo.Name;
            }
            else
            {
              lastReplier =  GetUserEmailId();
            }
            
            Guid ticketId = ticket.InsertTicket("NR", txtSubject.Text, txtMessage.Text, GetAttachmentPath(FileUpload1), ddlType.SelectedValue, GetUserEmailId(), Request["domain"], "", Session["departmentid"].ToString(), lastReplier,"");
            Response.Redirect("SubmitConfirmation.aspx?ticketId=" + ticketId.ToString());
        }

        private bool ValidateRequest(){

            if (Request["email"] != null)
            {
                if (!ValidateEmailId(Request["email"]))
                {
                    DisplayError("Invalid Email Address");
                    return false;
                }
            }
            return true;
        }

        private string GetUserEmailId()
        {
            string userEmail = "";
            if (Request["email"] != null)
            {
                userEmail = Request["email"];
            }
            else
            {
                Customer customer = (Customer)Session["customer"];
                userEmail = customer.CusInfo.Email;
            }
            return userEmail;
        }

    }
}
