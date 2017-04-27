using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using ResellerClub.BusinessLogic;
using System.Text;
using System.IO;
using APIC = ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class Domain : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentModule = ModuleNames.Domain;
            HideParentPageHeading();
            LoadSearchControl();
            GetPlansJSON();
            if (IsPostBack)
            {
                if (Request["btnContinue"] != null)
                {
                    btnContinue_Click();
                }
            }
            else
            {
                if (Request["DoSearch"] != null)
                {
                    ProcessDomainSerachResult();
                }
            }
        }

        private void btnContinue_Click()
        {
            LoadAvailableDomainControl();
            AddDomainToCart();
        }

        private void AddDomainToCart()
        {
            string yy = SessionM["UserEnteredDomain"].ToString();
            string yy1 = SessionM["UserSelectedTDLs"].ToString();
            string yy2 = Request[ddlPrimeDominPlan.UniqueID];

            AddItemToCart(SessionM["UserEnteredDomain"].ToString() + SessionM["UserSelectedTDLs"].ToString(), Request[ddlPrimeDominPlan.UniqueID], APIC.WebService.Domain);
            if (UserCart.IsWebServiceSelected)
            {
                AddDomainToWebService(SessionM["UserEnteredDomain"].ToString() + SessionM["UserSelectedTDLs"].ToString());
            }
            if (Request["domainnamearr[]"] != null)
            {
                string[] selectDomains = Request["domainnamearr[]"].Split(new char[] { ',' });
                foreach (string domain in selectDomains)
                {
                    AddItemToCart(domain, Request[domain + "_plan"], APIC.WebService.Domain);

                    if (UserCart.IsWebServiceSelected)
                    {
                        AddDomainToWebService(domain);
                    }
                }
            }
            if (UserCart.IsWebServiceSelected)
            {
                UserCart.SetSelectedWebServiceNULL();
                Response.Redirect("CheckOut.aspx");
            }
            else
            {
                Response.Redirect("SelectWebService.aspx");
            }
        }

        private void LoadSearchControl()
        {
            DomainSearchControl myUserControl = (DomainSearchControl)Page.LoadControl("UserControl/DomainSearchControl.ascx");
            myUserControl.ID = "domainSearchControl";
            plhSearchControl.Controls.Add(myUserControl);
            myUserControl.ProcessDomainSerachResult += new Action(ProcessDomainSerachResult);
        }

        private void LoadAvailableDomainControl()
        {
            AvailableDomainList myUserControl = (AvailableDomainList)Page.LoadControl("UserControl/AvailableDomainList.ascx");
            myUserControl.ID = "availableDomainList";
            plhDomaninValibaleList.Controls.Add(myUserControl);
        }

        private void ProcessDomainSerachResult()
        {
            try
            {
               if( !ValidateDomainName(SessionM["UserEnteredDomain"].ToString()))
               {
                   DisplayError("Domain name entered by you contains some invalid character.");
                   return;
               }

                DomainSearchControl domainSearchControl = (DomainSearchControl)this.plhSearchControl.FindControl("domainSearchControl");
                var domain = ApiObjectFactory.GetObject<ResellerClub.Interface.IDomain>();
                var list = Plan.TopLevelDomainList;
                var domainLst = domain.SearchDomain(SessionM["UserEnteredDomain"].ToString(), list);
                var pDomainInfo = domainLst.Find(x => x.DomainName == SessionM["UserEnteredDomain"].ToString() + SessionM["UserSelectedTDLs"].ToString());
                if (pDomainInfo != null)
                {
                    trDomainAvailable.Visible = true;
                    tdDomainName.InnerText = SessionM["UserEnteredDomain"].ToString() + SessionM["UserSelectedTDLs"].ToString();
                    BindPlan(Plan.GetTopLevelDomainPlan(SessionM["UserSelectedTDLs"].ToString()), ref ddlPrimeDominPlan);

                    domainLst.Remove(pDomainInfo);
                    SessionM["domainLst"] = domainLst;
                    LoadAvailableDomainControl();
                    AvailableDomainList availableDomainList = (AvailableDomainList)this.plhSearchControl.FindControl("availableDomainList");
                    availableDomainList.BindData(domainLst);
                    plhSearchControl.Visible = false;
                    trDisplaySearchResult.Visible = true;
                    divAmountStrip.Visible = true;
                }
                else
                {
                    trDomainNotAvailable.Visible = true;
                }
            }
            catch (ServerException ex)
            {
                DisplayError("Server Error");
            }
        }

        public string GetAlsoAvailableDomainName(object _dataItem)
        {
            KeyValuePair<string, object> dataItem = (KeyValuePair<string, object>)_dataItem;
            return "<label  for='" + dataItem.Key + "_input' >" + dataItem.Key + "</label>";
            return dataItem.Key;
        }

        private bool ValidateDomainName(string domainName)
        {
            if (domainName.IndexOf('.') > -1 || 
                domainName.IndexOf(',') > -1 ||
                domainName.IndexOf('$') > -1 ||
                domainName.IndexOf('=') > -1
                )
            {
                return false;
            }
            return true;
        }

    }
}
