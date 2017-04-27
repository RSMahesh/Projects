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
    public partial class ViewList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Ticket tic = new Ticket();
            DataTable dt = null;

            if (Session["customer"] != null)
            {
                Customer customer = (Customer)Session["customer"];
                try
                {
                    dt = tic.GetUserAllTickets(customer.CusInfo.Email);
                }
                catch (Exception ex)
                {
                    
                    var ff = ex;
                }
               
            }
            else
            {
                dt = tic.GetALLTickets();
            }
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        public string GetLastReplier(object obj)
        {
            return obj.ToString();
        }

       



        public string GetTicketNumber(object num)
        {
            return FormatTicketNumber(num.ToString());
        }

      
    }
}
