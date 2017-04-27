using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ResellerClub.WebUI
{
    public partial class DomainSearchControl : BaseUserControl
    {
        public Action ProcessDomainSerachResult;

        protected void Page_Load(object sender, EventArgs e)
        {
            ddlTld.DataSource = Plan.TopLevelDomainList;
            ddlTld.DataTextField = "Name";
            ddlTld.DataValueField = "Name";

                ddlTld.DataBind();
                ddlTld.SelectedValue = ".com";
                btnGo.Text = __("HOMEPAGE_GO");
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {

            SessionM["UserSelectedTDLs"] = Request[ddlTld.UniqueID].ToLowerInvariant();
            SessionM["UserEnteredDomain"] = Request[txtDomin.UniqueID].ToLowerInvariant();
            if (ProcessDomainSerachResult != null)
            {
                ProcessDomainSerachResult();
            }
            //if (domainLst.ContainsKey(txtDomin.Text + "." + ddlTld.SelectedValue))
            //{
            //    lblSearchHeader.Text = "Your Domain <span>" + txtDomin.Text + "." + ddlTld.SelectedValue + "</span> is Available";
            //    pnlSerachControl.Visible = false;
            //    tdDomainName.InnerText = txtDomin.Text + "." + ddlTld.SelectedValue;
            //    domainCart.Add(tdDomainName.InnerText, "");

            //    SessionM["DomainCart"] = domainCart;


            //    Util.BindRateList(ddlPrimeDominPlan, domainLst[tdDomainName.InnerText] as Dictionary<int, int>);
            //    domainLst.Remove(tdDomainName.InnerText);
            //    SessionM["domainLst"] = domainLst;
            //    Repeater1.DataSource = domainLst;
            //    Repeater1.DataBind();
            //}

        }


        public string UserEnteredDomain
        {
            get
            {
                return txtDomin.Text;

            }
        }

        public string UserSelectedTDLs
        {
            get
            {
                return ddlTld.SelectedValue;

            }
        }
    }
}