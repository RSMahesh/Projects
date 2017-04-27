using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ResellerClub.Common;

namespace ResellerClub.WebUI.UserControl
{
    public partial class PlanPanel : BaseUserControl
    {
        #region PublicVariable
        public string Product1, Product2, Product3, Product4;

        public string[] Product = new string[4];
        public string[] PlanHeading = new string[4];
        public string[] PlanFeature = new string[4];
        public string[] PlanStartingPrice = new string[4];
        public int[] PlanSequence = new int[4];
        #endregion

        #region Properties 
        public bool Plan0Visible
        {
            get
            {
                return th1.Visible;
            }

            set
            {
                th1.Visible = value;
                td1.Visible = value;
            }
        }
        public bool Plan1Visible
        {
            get
            {
                return th2.Visible;
            }

            set
            {
                th2.Visible = value;
                td2.Visible = value;
            }
        }
        public bool Plan2Visible
        {
            get
            {
                return th3.Visible;
            }

            set
            {
                th3.Visible = value;
                td3.Visible = value;
            }
        }
        public bool Plan3Visible
        {
            get
            {
                return th4.Visible;
            }

            set
            {
                th4.Visible = value;
                td4.Visible = value;
            }
        }
        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
            if (IsPostBack)
            {
                if (Request["btnPlan1"] != null)
                {
                    AddPlan(Request[ddlPlan1.UniqueID]);
                }
                else if (Request["btnPlan2"] != null)
                {
                    AddPlan(Request[ddlPlan2.UniqueID]);
                }
                else if (Request["btnPlan3"] != null)
                {
                    AddPlan(Request[ddlPlan3.UniqueID]);
                }
                else if (Request["btnPlan4"] != null)
                {
                    AddPlan(Request[ddlPlan4.UniqueID]);
                }
            }

        }

        private void BindData()
        {
            if (Plan0Visible)
            {
                ParentBasePage.BindPlan(Plan.GetPlansBySequence(Product[0], PlanSequence[0]), ref ddlPlan1);
                PlanStartingPrice[0] = Plan.GetPlanStartingPrice(Product[0], PlanSequence[0]).ToString();
            }
            if (Plan1Visible)
            {
                ParentBasePage.BindPlan(Plan.GetPlansBySequence(Product[1], PlanSequence[1]), ref ddlPlan2);
                PlanStartingPrice[1] = Plan.GetPlanStartingPrice(Product[1], PlanSequence[1]).ToString();
            }
            if (Plan2Visible)
            {
                ParentBasePage.BindPlan(Plan.GetPlansBySequence(Product[2], PlanSequence[2]), ref ddlPlan3);
                PlanStartingPrice[2] = Plan.GetPlanStartingPrice(Product[2], PlanSequence[2]).ToString();
            }
            if (Plan3Visible)
            {
                ParentBasePage.BindPlan(Plan.GetPlansBySequence(Product[3], PlanSequence[3]), ref ddlPlan4);
                PlanStartingPrice[3] = Plan.GetPlanStartingPrice(Product[3], PlanSequence[3]).ToString();
            }

        }

        private void AddPlan(string plan)
        {
            ParentBasePage.SetSelectedWebservice(plan);
            RedirectToRootPage("SelectHostingDomain.aspx");
        }

        public string CurrencySymbol
        {
            get
            {
                return Plan.GetCurrencySymbol();
            }
        }

        protected string GetRightBorder(int planNumber)
        {
            string rightBorder = "style='border-right:solid 0px'";
            switch (planNumber)
            {
                case 0:
                    if ( Plan1Visible || Plan2Visible || Plan3Visible)
                        rightBorder = "";
                    break;
                case 1:
                    if (Plan2Visible || Plan3Visible)
                        rightBorder = "";
                    break;

                case 2:
                    if (Plan3Visible)
                        rightBorder = "";
                    break;
        
            }

            return rightBorder;

        }
    }
}