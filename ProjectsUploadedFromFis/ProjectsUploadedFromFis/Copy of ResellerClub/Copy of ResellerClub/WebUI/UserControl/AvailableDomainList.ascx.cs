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
using APIC = ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class AvailableDomainList : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetPlanDdl(ResellerClub.Interface.Messages.IDomainInfoMessage _dataItem)
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = _dataItem.DomainName  + "_plan";
            
            ParentBasePage.BindPlan(Plan.GetTopLevelDomainPlan(
                _dataItem.DomainName.Substring(_dataItem.DomainName.IndexOf('.'))), ref ddl);

            return Util.RenderControl(ddl);
        }

        public string GetCheckBox(ResellerClub.Interface.Messages.IDomainInfoMessage _dataItem)
        {
            return "<input type=checkbox id='" + _dataItem.DomainName  + "_input' value='" + _dataItem.DomainName  + "' name='domainnamearr[]' onclick='GetTotalAmount()' />";
        }

        public string GetDominName(ResellerClub.Interface.Messages.IDomainInfoMessage _dataItem)
        {
            return _dataItem.DomainName;
        }

        public void BindData(List<ResellerClub.Interface.Messages.IDomainInfoMessage> domainList)
        {
            Repeater1.DataSource = domainList;
            Repeater1.DataBind();
        }

        public Dictionary<string, string> GetSelectedValues()
        {
            Dictionary<string, string> domainCart = new Dictionary<string, string>();
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (Request[item.Controls[1].UniqueID] != null)
                {
                    string domainName = ((Label)item.Controls[3]).Text;
                    string plan = Request[item.Controls[5].UniqueID];
                    domainCart.Add(domainName, plan);
                }
            }

            return domainCart;


        }
    }
}