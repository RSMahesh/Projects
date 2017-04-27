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
using ResellerClub.Interface.Messages;
using ResellerClub.Common;
using ResellerClub.Interface;

namespace ResellerClub.WebUI
{
    public partial class UpdatePaymentReference : BasePage
    {
        public string OrderNumber;
        public string UserEmail;
        private Guid orderId;
        protected string message="";
        protected void Page_Load(object sender, EventArgs e)
        {
            TurnOffCache();
            orderId = new Guid(Request.QueryString["rid"]);
            SessionM["OrderId"] = orderId;
            UserEmail = Util.GetUserEmailByOrderId(Request.QueryString["rid"], out OrderNumber);

            if (IsPostBack)
            {
                if (Request["button"] != null && Request["button"] == "submit")
                {
                   
                    UpdatePayementTranscationNumber();
                }

            }
        }

        private void UpdatePayementTranscationNumber()
        {

            if (txtTranscationNumber.Text == "")
            {
                DisplayError("Transcation Number Required");
                return;
            }
                
            IOrder order = ApiObjectFactory.GetObject<IOrder>();
            order.UpdatePaymentTranNumber(orderId, txtTranscationNumber.Text);
            RegisterStartUpScript("$('#divContainer').hide();");

            DisplayInfo("Your payment confirmation number is updated. Your order will get executed as soon as we will verify your payment confirmation number");
        }

      
    }
}