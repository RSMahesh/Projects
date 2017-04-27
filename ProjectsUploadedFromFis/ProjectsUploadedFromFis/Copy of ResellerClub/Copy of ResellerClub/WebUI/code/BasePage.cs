using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using APIC = ResellerClub.Common;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ResellerClub.Interface;
using ResellerClub.Interface.Messages;
using System.Linq;
using ResellerClub.WebUI.code;
using System.IO;
using ResellerClub.Common;



namespace ResellerClub.WebUI
{
    public class BasePage : Page
    {
        #region ClassVariables
        protected string authUser = "329832";
        protected string authPassword = "MB248001";
        protected SessionManager SessionM = new SessionManager();
        const int MONTH_IN_YEAR = 12;

        private Cart cart;

        public Cart UserCart
        {
            get { return cart; }
            set { cart = value; }
        }
        #endregion

        protected void HideParentPageHeading()
        {
            //TO DO : Remove this method

            //         this.Form.Parent.FindControl("divPageHeader").Visible = false;
        }
        protected void RegisterStartUpScript(string script)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "startup", "<script>" + script + "</script>");
        }
        protected ModuleNames CurrentModule
        {

            set
            {
                switch (value.ToString())
                {
                    case "Home":
                        RegisterStartUpScript("current_submenu_clicked='menuhome'");
                        break;
                    case "Domain":
                        RegisterStartUpScript("current_submenu_clicked='menudomain'");
                        break;

                }
            }
        }
        protected string ObjectToJason(object obj)
        {
           return  Util.ObjectToJason(obj);
        }
        public void BindPlan(Guid subPlanID, ref DropDownList ddl)
        {
            var plan = Plan.GetPlanBySubPlanId(subPlanID);
            List<IPlanMessage> allPlans = Plan.GetProductPalns(plan.ProductName).FindAll(x => x.PlanName == plan.PlanName);
            BindPlan(allPlans, ref ddl);
        }
        public void BindPlan(List<IPlanMessage> plans, ref DropDownList ddl)
        {
            plans = (List<IPlanMessage>)plans.OrderBy(x => x.Price).ToList();
            ddl.Items.Clear();
            foreach (var p in plans)
            {
                //ddl.Items.Add(new ListItem(p.Year.ToString() + "year at " + p.CurrencySymbol + p.Price.ToString() + " /" + "Years", p.SubPlanID.ToString()));
                var numberOfMonth = p.Year * 12;
                ddl.Items.Add(new ListItem(p.Year.ToString() + "year at " + p.CurrencySymbol + ((int)(p.Price / numberOfMonth)).ToString() + " /" + "month", p.SubPlanID.ToString()));
            }
           // ddl.DataBind();
        }
        protected void AddItemToCart(string domain, string subPlanId, APIC.WebService ws)
        {
            cart.AddItemToCart(domain, new Guid(subPlanId), false, false);
        }
        public void SetSelectedWebservice(string plan)
        {
            cart.SetSelectedWebservice(plan);
        }
        protected void AddDomainToWebService(string domainName)
        {
            cart.AddDomainToSelectedWebService(domainName);
        }

        protected string UserIPAddress()
        {
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            else
            {
                string[] arr = ip.Split(new char[] { ',' });
                return arr[0];
            }

        }

        protected void SendEmail(string htmlFormatFile, string subject, string toEmailAddress, Dictionary<string, string> replaceString)
        {
            var str = Util.GetHTMLFileContent(@"EmailFormat\" + htmlFormatFile);
            foreach (var key in replaceString.Keys)
            {
                str = str.Replace(key, replaceString[key]);
            }

            var strFooter = Util.GetHTMLFileContent(@"EmailFormat\Email_Footer.htm");
            str = str.Replace("<%=Email_Footer%>", strFooter);

            var strStyle = Util.GetHTMLFileContent(@"EmailFormat\Style.htm");
            str = str.Replace("<%=style%>", strStyle);

            Email.SendMail(toEmailAddress, subject, str, "");
        }

        public string CurrencySymbol
        {
            get
            {
                return Plan.GetCurrencySymbol();
            }
        }

        protected void TurnOffCache()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.AddHeader("Pragma", "no-cache");
            Response.CacheControl = "no-cache";
            Response.Expires = 0;
        }
        protected void DisplayError(string message)
        {
            var divUserMsg = (HtmlGenericControl)Master.FindControl("ContentPlaceHolder1").FindControl("divUserMsg");
            divUserMsg.Attributes["class"] = "dialogerror";
            var divUserMsgContent = (HtmlGenericControl)Master.FindControl("ContentPlaceHolder1").FindControl("divUserMsgContent");
            divUserMsgContent.Attributes["class"] = "dialogerrorcontent";
            divUserMsgContent.InnerText = message;
        }
        protected void DisplayInfo(string message)
        {
            var divUserMsg = (HtmlGenericControl)Master.FindControl("ContentPlaceHolder1").FindControl("divUserMsg");
            divUserMsg.Attributes["class"] = "dialoginfo";
            var divUserMsgContent = (HtmlGenericControl)Master.FindControl("ContentPlaceHolder1").FindControl("divUserMsgContent");
            divUserMsgContent.Attributes["class"] = "dialoginfocontent";
            divUserMsgContent.InnerHtml = message;
        }
        protected void PopulatePlanPanel(ResellerClub.WebUI.UserControl.PlanPanel planPanel,string product,
           List<IPlanMessage> plans,
           HtmlGenericControl divFeature1,
           HtmlGenericControl divFeature2,
           HtmlGenericControl divFeature3,
           HtmlGenericControl divFeature4)
        {
            HideParentPageHeading();
            planPanel.Plan0Visible = planPanel.Plan1Visible = planPanel.Plan2Visible = planPanel.Plan3Visible = false;
            planPanel.Product[0] = planPanel.Product[1] = planPanel.Product[2] = planPanel.Product[3] = product;
            // var plans = Plan.LinuxHostingPlan;

            var distinctPlanSequence = plans
                 .GroupBy(c => c.PlanSequence)
                      .Select(g => g.First());

            foreach (var p in distinctPlanSequence)
            {
                if (!p.Selling)
                    continue;

                switch (p.PlanSequence)
                {
                    case 1:
                        planPanel.Plan0Visible = true;
                        planPanel.PlanHeading[0] = p.DisplayName;
                        planPanel.PlanSequence[0] = 1;
                        planPanel.PlanFeature[0] = divFeature1.InnerHtml;
                        break;
                    case 2:
                        planPanel.Plan1Visible = true;
                        planPanel.PlanHeading[1] = p.DisplayName;
                        planPanel.PlanSequence[1] = 2;
                        planPanel.PlanFeature[1] = divFeature2.InnerHtml;
                        break;
                    case 3:
                        planPanel.Plan2Visible = true;
                        planPanel.PlanHeading[2] = p.DisplayName;
                        planPanel.PlanSequence[2] = 3;
                        planPanel.PlanFeature[2] = divFeature3.InnerHtml;
                        break;
                    case 4:
                        planPanel.Plan3Visible = true;
                        planPanel.PlanHeading[3] = p.DisplayName;
                        planPanel.PlanSequence[3] = 4;
                        planPanel.PlanFeature[3] = divFeature4.InnerHtml;
                        break;

                }
            }

        }

        public  void RedirectToRootPage(string pageName)
        {
            Response.Redirect(Application["rootPath"] + @"/" + pageName);
        }

        #region Disable ViewState
        protected override void SavePageStateToPersistenceMedium(object state)
        {
        }
        protected override object LoadPageStateFromPersistenceMedium()
        {
            return null;
        }
        #endregion

        #region Page Events
        protected override void OnLoad(EventArgs e)
        {
            authUser = ConfigurationManager.AppSettings["ResellerUserName"];
            authPassword = ConfigurationManager.AppSettings["ResellerPassowrd"];

            if (this.Master != null)
            {
                if (this.Master is Main)
                {
                    ((Main)this.Master).SessionM = this.SessionM;
                }
            }

            //======Load Cart========== 
            if (SessionM["Cart"] != null)
                cart = (Cart)SessionM["Cart"];
            else
                cart = new Cart();

            //GetPlansJSON();
            base.OnLoad(e);
        }

        public void GetPlansJSON()
        {
            string json = ObjectToJason(Plan.GetAllPlan());
            json = "<script>var planJason =jQuery.parseJSON('" + json + "');</script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "", json);
        }

        protected string GetLanguage()
        {
            return LanguageSetting.GetLanguage();
        }

        protected string __(string key)
        {
            return LanguageSetting.GetString(key);
        }

        protected override void OnUnload(EventArgs e)
        {
            if (cart!=null && cart.DirtyFlag)
            {
                SessionM["Cart"] = cart;
            }

            base.OnUnload(e);
        }
        #endregion

    }
}
