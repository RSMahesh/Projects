using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Expires = -1;
            Response.ContentType = "text/comma-separated-values";
            Response.AddHeader("Content-Type", "text/comma-separated-values");
            Response.AddHeader("Content-Disposition", "attachment; filename=EntitlementReport.csv");

            Response.Write("Company ID,Enternal Site ID,User ID, Status, Service , Entitlement" + Environment.NewLine);

            Response.Buffer = false;
            Response.BufferOutput = false;

            DateTime dt = DateTime.Now.AddSeconds(1);

            while (dt > DateTime.Now)
            {

                Response.Write("Company ID,Enternal Site ID,User ID, Status, Service , Entitlement" + Environment.NewLine);
                Response.Flush();

            }

          //  Thread th = new Thread(new ThreadStart(Test));
          //  Response.Redirect("WebForm1.aspx");
           
            //   th.Start();
          //  Response.End();


                            int min = Session.Timeout;
                            TimeSpan ts = (DateTime.Now - (DateTime)Session["ExportCompletedTime"]);

                            min = Session.Timeout - (int) ts.TotalMinutes; 
                            

                            Response.Write(min > 0 ? (min * 1000):1000);
                      




        }

        private void Test()
        {
            Thread.Sleep(2000);
            Response.Clear();
            Response.Redirect("WebForm1.aspx");
            
        }
    }
}
