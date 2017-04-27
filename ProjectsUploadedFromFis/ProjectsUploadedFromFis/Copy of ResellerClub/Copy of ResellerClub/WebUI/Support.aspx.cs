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
using ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class Support : BasePage
    {
        public string message = "";
        public string contact_msg;
        public string contact_email;
        public string contact_name;
        public string contact_number;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                contact_msg = Request["contact_msg"];
                contact_email = Request["contact_email"];
                contact_name = Request["contact_name"];
                contact_number = Request["contact_number"];

                if (Request["button"] == "SendMail")
                {
                    InserTicket();
                }
            }
        }

        private void InserTicket()
        {
            Ticket tick = new Ticket();
            string subject = Request["contact_msg"];
            if (subject.Length > 50)
            {
                subject = subject.Substring(0, 49) + "...";
            }

            if (ValidateRequest())
            {
                tick.InsertTicket("NI", subject, Request["contact_msg"], "", "NI", Request["contact_email"], "",
                Request["contact_number"], Request["contact_dept"], Request["contact_name"], Request["contact_name"]);

                divUserMsg.Attributes["class"] = "dialoginfo";
                divUserMsgContent.Attributes["class"] = "dialoginfocontent";
                message = "Thank you for submitting your query. One of our representatives will get back to you shortly.";


                Email.SendMail(Request["contact_email"], "Thank you for submitting your query",
                "Hi " + Request["contact_name"] + "," + Environment.NewLine + message, "", "jai-hind@infowebservices.in", "Infowebservices Support");

                contact_msg = "";
                contact_email = "";
                contact_name = "";
                contact_number = "";
            }
            else
            {
                divUserMsg.Attributes["class"] = "dialogerror";
                divUserMsgContent.Attributes["class"] = "dialogerrorcontent";
            }

        }

        private bool ValidateRequest()
        {
            if (!Validator.ValidateEmailId(Request["contact_email"]))
            {
                message = "Invalid email address";
                return false;
            }

            if (!Validator.ValidatePhoneNumber(Request["contact_number"]))
            {
                message = "Invalid contact number";
                return false;
            }

            if (Request["contact_name"].Trim() == "")
            {
                message = "Empty contact name";
                return false;
            }

            if (Request["contact_msg"].Trim() == "")
            {
                message = "Empty Message ";
                return false;
            }

            return true;
        }

    }
}
