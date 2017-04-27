using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface;
using ResellerClub.Interface.Messages;
using ResellerClub.DataAccess;
using System.Net;
using System.IO;
using ResellerClub.Common;
using ResellerClub.Common.Logging;

namespace ResellerClub.BusinessLogic
{
    public class PayPalTranscationLogger : BaseBRL, IPaymentProcessor
    {

        public string CreateRequest(List<IOrderItemMessage> items, Guid orderId, string processorUrl, Dictionary<string, string> settings)
        {
            //PayPal cart version
            string PostUrl = processorUrl;
            string Cmd = "_cart";
            //string Cmd = "_xclick";
            string Upload = "1";
            string BusinessEmail = settings["BusinessEmail"];
            string Currency = "USD";
            string ShipAmount = "1";
            StringBuilder ppForm = new StringBuilder();
            var convRate = new ConversionRate();
            ppForm.Append("<HTML><BODY><form name='frmPP' id='frmPP' action='" + PostUrl + "' method='post'>");
            ppForm.Append("<input type='hidden' name='shipping' value='" + ShipAmount + "'>");
            ppForm.Append("<input type='hidden' name='cmd' value='" + Cmd + "'>");
            ppForm.Append("<input type='hidden' name='upload' value='" + Upload + "}'>");
            ppForm.Append("<input type='hidden' name='business' value='" + BusinessEmail + "'>");
            ppForm.Append("<input type='hidden' name='currency_code' value='" + Currency + "'>");
            ppForm.Append("<input type='hidden' name='rm' value='1'>");
            ppForm.Append("<input type='hidden' name='return' value='" + settings["ReturnUrl"] + "'>");
            ppForm.Append("<input type='hidden' name='notify_url' value='" + settings["NotifyUrl"] + "'>");
            ppForm.Append("<input type='hidden' name='cancel_return' value='" + settings["CancelPurchaseUrl"] + "'>");
            ppForm.Append("<input type='hidden' name='custom' value='" + orderId.ToString() + "'>");

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                ppForm.Append("<input type='hidden' name='item_number_" + (i + 1).ToString() + "' value='" + (i + 1).ToString() + "'>");
                ppForm.Append("<input type='hidden' name='item_name_" + (i + 1).ToString() + "' value='" + Constant.GetItemDescription(item.DomainName, GetPlan(item.SubPlanID).ProductName, GetPlan(item.SubPlanID).DisplayName) + "'>");
                ppForm.Append("<input type='hidden' name='amount_" + (i + 1).ToString() + "' value='" + convRate.ConvertRupeeToDollar(GetPlan(item.SubPlanID)).ToString() + "'>");
                ppForm.Append("<input type='hidden' name='quantity_" + (i + 1).ToString() + "' value='1'>");
            }

            ppForm.Append("</form>" + PayPalPostScript() + "</BODY></HTML>");

            return ppForm.ToString();

        }

        private static decimal ConvertRupeeToDollar(decimal rupeAmount, decimal conversionRate)
        {
            decimal dollar = rupeAmount / conversionRate;
            return decimal.Round(dollar, 2, MidpointRounding.AwayFromZero);
        }

        public string CreateRequest12(List<IOrderItemMessage> items, Guid orderId, string processorUrl, Dictionary<string, string> settings)
        {
            //PayPal cart version
            string PostUrl = processorUrl;
            //string Cmd = "_cart";xclick
            string Cmd = "_xclick";
            string Upload = "1";
            string BusinessEmail = settings["BusinessEmail"];
            string Currency = "USD";
            string ShipAmount = "4.95";

            //Create the Form to write to the page with PayPal parameters
            StringBuilder ppForm = new StringBuilder();

            //     <form id="payForm" method="post" action="<%Response.Write (URL);%>">
            //<input type="hidden" name="cmd" value="<%Response.Write (cmd);%>">
            //<input type="hidden" name="business" value="<%Response.Write (business);%>">
            //<input type="hidden" name="item_name" value="<%Response.Write (item_name);%>">
            //<input type="hidden" name="amount" value="<%Response.Write (amount);%>">
            //<input type="hidden" name="no_shipping" value="<%Response.Write (no_shipping);%>">
            //<input type="hidden" name="return" value="<%Response.Write (return_url);%>">
            //<input type="hidden" name="rm" value="<%Response.Write (rm);%>">
            //<input type="hidden" name="notify_url" value="<%Response.Write (notify_url);%>">
            //<input type="hidden" name="cancel_return" value="<%Response.Write (cancel_url);%>">
            //<input type="hidden" name="currency_code" value="<%Response.Write (currency_code);%>">
            //<input type="hidden" name="custom" value="<%Response.Write (request_id);%>">

            ppForm.Append("<HTML><BODY><form name='frmPP' id='frmPP' action='" + PostUrl + "' method='post'>");
            ppForm.Append("<input type='hidden' name='shipping' value='" + ShipAmount + "'>");
            ppForm.Append("<input type='hidden' name='cmd' value='" + Cmd + "'>");
            ppForm.Append("<input type='hidden' name='upload' value='" + Upload + "}'>");
            ppForm.Append("<input type='hidden' name='business' value='" + BusinessEmail + "'>");
            ppForm.Append("<input type='hidden' name='currency_code' value='" + Currency + "'>");
            ppForm.Append("<input type='hidden' name='rm' value='1'>");
            ppForm.Append("<input type='hidden' name='return' value='" + settings["ReturnUrl"] + "'>");
            ppForm.Append("<input type='hidden' name='notify_url' value='" + settings["NotifyUrl"] + "'>");
            ppForm.Append("<input type='hidden' name='cancel_return' value='" + settings["CancelPurchaseUrl"] + "'>");
            ppForm.Append("<input type='hidden' name='custom' value='" + orderId.ToString() + "'>");

            ppForm.Append("<input type='hidden' name='item_name' value='Purchase of Web service'>");
            decimal amount = items.Sum(x => GetPlan(x.SubPlanID).Price);
            ppForm.Append("<input type='hidden' name='amount' value='" + amount .ToString()+ "'>");
         
            //for (int i = 0; i < items.Count; i++)
            //{
            //    var item = items[i];
            //    ppForm.Append("<input type='hidden' name='item_number_" + (i + 1).ToString() + "' value='" + (i + 1).ToString() + "'>");
            //    ppForm.Append("<input type='hidden' name='item_name_" + (i + 1).ToString() + "' value='" + item.DomainName + "'>");
            //    ppForm.Append("<input type='hidden' name='amount_" + (i + 1).ToString() + "' value='" + GetPlan(item.SubPlanID).Price.ToString() + "'>");
            //    ppForm.Append("<input type='hidden' name='quantity_" + (i + 1).ToString() + "' value='1'>");
            //}

            ppForm.Append("</form>" + PayPalPostScript() + "</BODY></HTML>");

            return ppForm.ToString();

        }
        
        private string PayPalPostScript()
        {
            //This registers Javascript to the page which is used to post the PayPal Form details
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var ctlForm = document.getElementById('frmPP');");
            strScript.Append("ctlForm.submit();");
            strScript.Append("</script>");
            return strScript.ToString();
            // ClientScript.RegisterClientScriptBlock(this.GetType(), "PPSubmit", strScript.ToString());
        }

        public void InsertTransactionLog(string request, Guid orderId)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var processor = new ResellerClub.DataAccess.PaymentProcessor(connection);
                processor.InsertTransactionLog(request, orderId);
            }
        }

        public void UpdateTransactionLog(string response, Guid orderId)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var processor = new ResellerClub.DataAccess.PaymentProcessor(connection);
                processor.UpdateTransactionLog(response, orderId);
            }
        }

        public bool ProcessResponse(System.Web.HttpRequest request, string payPalUrl)
        {
            bool success = false;
            try
            {
                // payPalUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr";
                //string strLive = "https://www.paypal.com/cgi-bin/webscr";
                var order = new Order();
                var orderID = new Guid(request["custom"]);
                var requestInputStream = new StreamReader(request.InputStream);
                var requestContent = requestInputStream.ReadToEnd();
                requestInputStream.Close();
          
                UpdateTransactionLog(requestContent, orderID);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(payPalUrl);

                //Set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                string strRequest = requestContent;
                strRequest += "&cmd=_notify-validate";
                req.ContentLength = strRequest.Length;

                //Send the request to PayPal and get the response
                StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                streamOut.Write(strRequest);
                streamOut.Close();
                StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
                string strResponse = streamIn.ReadToEnd();
                streamIn.Close();

                if (strResponse == "VERIFIED")
                {
                    success = true;
                    order.UpdateOrderStatus(orderID, Constant.OrderStatusPaymentVerified);
                    order.ProcessOrder(orderID);
                    //check the payment_status is Completed
                    //check that txn_id has not been previously processed
                    //check that receiver_email is your Primary PayPal email
                    //check that payment_amount/payment_currency are correct
                    //process payment
                }
                else if (strResponse == "INVALID")
                {
                    order.UpdateOrderStatus(orderID, Constant.OrderStatusPaymentInvalid);
                    //log for manual investigation
                }
                else
                {
                    //log response/ipn data for manual investigation
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "");
            }

            return success;
        }

        private void Test()
        {

            using (var connection = ConnectionFactory.GetConnection())
            {
                var DALP = new ResellerClub.DataAccess.PaymentProcessor(connection);
                DALP.UpdateTable_1();
            }   //=========

        }
    }
}
