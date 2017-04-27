using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace InfoWebTicketSystem.WebUI
{
    public class BasePage : Page
    {
        public  string GetAttachmentPath(FileUpload FileUpload1)
        {
            if (FileUpload1.FileName == null || FileUpload1.FileName.Trim() == "")
                return "";

            string file = HttpContext.Current.Server.MapPath(@"UploadedFiles\" + Guid.NewGuid().ToString() + "______" + FileUpload1.FileName);
            if (!Directory.Exists(Path.GetDirectoryName(file)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(file));
            }

            FileUpload1.SaveAs(file);
            return file;
        }

        public string FormatTicketNumber(string ticketNumber)
        {
          return  "#NMU-"+  ticketNumber.ToString().PadLeft(5, '0') + "-XOJ";
        }

        public bool UserValidated()
        {
            bool rtn = false;
            if (Session["userValidated"] != null && (bool)Session["userValidated"])
            {
                rtn = true;
            }
            return rtn;
        }


        protected void RegisterCript(string script)
        {
            this.Page.RegisterStartupScript(Guid.NewGuid().ToString(), script);
        }

        protected void DisplayError(string message)
        {
         RegisterCript("<script>$('.dialogerrorcontent').html('"+message+" ');$('.dialogerror').show();</script>");
        }

        public string GetTicketType(object _type)
        {
            string rtn = "";
            switch (_type.ToString())
            {
                case "NI":
                    rtn = "Need Information";
                    break;
                case "ER":
                    rtn = "Error on web site";
                    break;

            }
            return rtn;
        }

        public string GetStatus(object _type)
        {
            string rtn = "";
            switch (_type.ToString())
            {
                case "OP":
                    rtn = "OPEN";
                    break;
                case "CS":
                    rtn = "Closed";
                    break;
                case "OH":
                    rtn = "On Hold";
                    break;
                case "AW":
                    rtn = "Awaiting Client Update";
                    break;

            }
            return rtn;
        }

        public string GetPriority(object _type)
        {

            string rtn = "";
            switch (_type.ToString())
            {
                case "NR":
                    rtn = "Normal";
                    break;
                case "EM":
                    rtn = "Emergency";
                    break;
                case "CR":
                    rtn = "Critical";
                    break;

            }
            return rtn;
        }

        public string GetDepartment(object obj)
        {
            string department = "";
            switch (obj.ToString())
            {
                case "SALE":
                    department = "Sales";
                    break;

                case "BILL":
                    department = "Billing";
                    break;

                case "TECH":
                    department = "Technical";
                    break;

            }

            return department;
        }

        protected  bool ValidateEmailId(string inputEmail)
        {

            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
    }
}
