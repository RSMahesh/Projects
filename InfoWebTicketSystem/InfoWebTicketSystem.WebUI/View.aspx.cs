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
using ResellerClub.Common;

namespace InfoWebTicketSystem.WebUI
{
    public partial class View : BasePage
    {
        public string TicketNumber = "";
        public string Subject = "";
        public string CreatedOn = "";
        public string UpdatedOn = "";
        public string TicketType = "";
        public string Department = "";
        
        string userName = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            // Customer customer = (Customer)Session["customer"];
            // userName = customer.CusInfo.Name;

            if (!IsPostBack)
            {
                LoadTicketConversation();
            }
            else
            {
                if (Request["button"] != null)
                {
                    if (Request["button"] == "Send")
                        SubmitTicket();
                    if (Request["button"] == "Update")
                        UpdateTicket();
                }
            }

        }

        public string GetUser(object obj)
        {
            bool staff = (bool)obj;
            return staff ? "Support Group" : userName;
        }

        public string GetUserType(object obj)
        {
            bool staff = (bool)obj;
            return staff ? "Staff" : "User";
        }

        public string GetAttachemtFileName(object obj)
        {
            if (obj.ToString().Length < 1)
                return "";
            string[] str = { "______" };

            string file = Path.GetFileName(obj.ToString()).Split(str, StringSplitOptions.None)[1];

            return file;
        }

        public string GetAttachemtFileNameRaw(object obj)
        {
            if (obj.ToString().Length < 1)
                return "";
            string[] str = { "______" };

            string file = Path.GetFileName(obj.ToString());

            return file;
        }



        public string IsAttachment(object obj)
        {
            if (obj.ToString().Length < 1)
                return "none";

            return "";
        }

        protected void btnPostReply_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
        }

     
        private void SubmitTicket()
        {
            TicketConversation tic = new TicketConversation();
            Guid ticketId = new Guid(Request["ticketId"]);
            Guid userId = Helper.GetEmptyGuid();

            tic.InsertConversation(ticketId, txtMessage.Text, GetAttachmentPath(FileUpload1), userId, Session["admin"] == null ? false : true);

            if (Session["admin"] != null)
            {
                ddlStatus.SelectedValue = "OH";
                var dt = GetTicketData(new Guid(Request["ticketId"]));
                Email.SendMail(dt.Rows[0]["UserEmail"].ToString(), dt.Rows[0]["Subject"].ToString(),
txtMessage.Text, "", "jai-hind@infowebservices.in", "Infowebservices Support");
                //TO DO: Send Email

            }


            UpdateTicket();
            LoadTicketConversation();
        }

        private DataTable GetTicketData(Guid ticketId)
        {
            Ticket tic = new Ticket();
            return tic.GetUserTicket(ticketId);
        }
        private void LoadTicketConversation()
        {
            Guid ticketId = new Guid(Request["ticketId"]);
          //  Ticket tic = new Ticket();
            var dt = GetTicketData(ticketId);

            TicketNumber = FormatTicketNumber(dt.Rows[0]["TicketNumber"].ToString());
            Subject = dt.Rows[0]["Subject"].ToString();

            ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
            ddlPriority.SelectedValue = dt.Rows[0]["Priority"].ToString();
            CreatedOn = ((DateTime)dt.Rows[0]["InsertDate"]).ToString("dd MMMM yyyy HH:mm:ss tt");
            UpdatedOn = ((DateTime)dt.Rows[0]["LastUpdate"]).ToString("dd MMMM yyyy HH:mm:ss tt");
            TicketType = GetTicketType(dt.Rows[0]["Type"].ToString());
            Department = GetDepartment(dt.Rows[0]["Department"]);

            Customer cus = new Customer();
            //if(cus.GetCustomerDetailByUserName(dt.Rows[0]["UserEmail"].ToString()))
            //{
            //userName = cus.CusInfo.Name;
            //}
            //else{
            //userName = dt.Rows[0]["UserName"].ToString();
            //if (userName.Trim().Length  < 1)
            //{
            //    userName = dt.Rows[0]["UserEmail"].ToString();
            //}
            //}

            TicketConversation ticConv = new TicketConversation();
            var dt1 = ticConv.GetConversion(ticketId);
            Repeater1.DataSource = dt1;
            Repeater1.DataBind();

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateTicket();
        }

        private void UpdateTicket()
        {
            Guid ticketId = new Guid(Request["ticketId"]);
            Ticket ticket = new Ticket();
            ticket.UpdateTicket(ticketId, ddlStatus.SelectedValue, ddlPriority.SelectedValue);
            LoadTicketConversation();

        }
    }
}
