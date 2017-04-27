using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using APIC = ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class SelectHostingDomain : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideParentPageHeading();
            if (SessionM["AddWebService"] == null)
            {
                //       Response.Redirect("Home.aspx");
            }
            if (IsPostBack)
            {
                if (Request["btnContinue"] != null)
                {
                    // ContinueClicked();
                }

                if (Request["btnSearch"] != null)
                {
                    // SearchClicked();
                }
            }


            if (UserCart != null && UserCart.Items != null)
            {

                foreach (var ws in UserCart.Items)
                {
                    if (Plan.GetProductName(ws.SubPlanID) == APIC.Constant.DomainRegistration)
                    {
                            ddlExistingDomain.Items.Add(ws.DomainName);
                    }
                }

                if (ddlExistingDomain.Items.Count > 0)
                {
                    ddlExistingDomain.Items.Add("Other");
                    txtExistingDomain.Style["display"] = "none";
                    ddlExistingDomain.Visible = true;
                }
            }
        }

        protected void ContinueClicked(object sender, EventArgs e)
        {
            string domain = "";
            if (Request[ddlExistingDomain.UniqueID] == null || Request[ddlExistingDomain.UniqueID] == "Other")
            {
                domain = Request[txtExistingDomain.UniqueID];
            }
            else
            {
                domain = Request[ddlExistingDomain.UniqueID];
            }

            AddDomainToWebService(domain);
            UserCart.SetSelectedWebServiceNULL();
            Response.Redirect("CheckOut.aspx");

        }
        protected void SearchClicked(object sender, EventArgs e)
        {
            SessionM["UserEnteredDomain"] = txtDomainName.Text;
            SessionM["UserSelectedTDLs"] = ddlTld.SelectedValue;
            Response.Redirect("Domain.aspx?DoSearch=1");
        }
    }
}
