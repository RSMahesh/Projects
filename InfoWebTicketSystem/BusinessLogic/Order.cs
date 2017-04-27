using System;
using System.Collections.Generic;

using System.Text;
using ResellerClub.Common;
using ResellerClub.Interface;
using ResellerClub.Interface.Messages;
using System.Linq;
using ResellerClub.DataAccess;
using ResellerClub.Common.Logging;
using System.Data;


namespace ResellerClub.BusinessLogic
{
    public class Order : BaseBRL, IOrder
    {
        public Order() { }

        public Order(string authUser, string authPassword)
            : base(authUser, authPassword) { }

        public IList<IInvoiceInfoMessage> Register(Guid orderId, IList<IOrderItemMessage> orderItems, ICustomerInfoMessage cusInfo)
        {
            ResellerClub.Interface.Messages.IInvoiceInfoMessage invi = null;
            List<ResellerClub.Interface.Messages.IInvoiceInfoMessage> invoiceList = new List<ResellerClub.Interface.Messages.IInvoiceInfoMessage>();


            Domain domain = new Domain();
            WebServices webServices = new WebServices();
            SSL ssl = new SSL();
            Hosting hosting = new Hosting();

            using (var connection = ConnectionFactory.GetConnection())
            {
                var order = new ResellerClub.DataAccess.Order(connection);
                foreach (var item in orderItems)
                {
                    try
                    {
                        var product = GetProductName(item.SubPlanID);
                        var plan = GetPlan(item.SubPlanID);
                        switch (product)
                        {
                            case Constant.EComWebSite:
                            case Constant.EmailHosting:
                            case Constant.StandardWebSite:
                            case Constant.WebSiteBuilder:
                                invi = webServices.Register(item.DomainName, cusInfo.CustomerID, (plan.Year * 12).ToString(), plan.ProductName, plan.PlanName, InvoiceOption.PayInvoice);
                                break;
                            case Constant.SingleDomainHostingWindowsUs:
                            case Constant.SingleDomainHostingLinuxUs:
                                invi = hosting.Add(item.DomainName, cusInfo.CustomerID, (plan.Year * 12).ToString(), plan.ProductName, plan.PlanName, InvoiceOption.PayInvoice);
                                break;
                            case Constant.DomainRegistration:
                                invi = domain.Register(item.DomainName, plan.Year, cusInfo, InvoiceOption.PayInvoice);
                                break;
                            case Constant.SSL_SSL123:
                            case Constant.SSL_FSSL:
                            case Constant.SSL_SGC:
                            case Constant.SSL_WILD:
                                invi = ssl.Register(item.DomainName, plan.Year, plan.ProductName, 0, cusInfo, InvoiceOption.PayInvoice);
                                break;
                        }

                        invi.Amount = plan.Price;
                        invoiceList.Add(invi);
                        order.UpdateOrderItem(orderId, item.SubPlanID, item.DomainName, invi.InvioceNumber, invi.Description, invi.Status, invi.Response);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex, "");
                    }
                }
            }

            return invoiceList;
        }

        public Guid SaveOrder(IList<IOrderItemMessage> orderItems, Guid sessionID, decimal orderAmount)
        {
            Guid orderId;
            using (var connection = ConnectionFactory.GetConnection())
            {
                var order = new ResellerClub.DataAccess.Order(connection);
                orderId = order.SaveOrder(orderItems, sessionID, orderAmount, Constant.OrderItemStatusSaved);

            }
            return orderId;
        }

        public void UpdateOrderStatus(Guid orderId, int status)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var order = new ResellerClub.DataAccess.Order(connection);
                order.UpdateOrderStatus(orderId, status);
            }
        }

        private void UpdateOrderStatus(Guid orderId)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var order = new ResellerClub.DataAccess.Order(connection);
                order.UpdateOrderStatus(orderId);
            }
        }

        public void ProcessOrder(Guid orderId)
        {
            List<IOrderItemMessage> orderItem;
            Guid sessionId;
            string userEmail;
            using (var connection = ConnectionFactory.GetConnection())
            {
                var order = new ResellerClub.DataAccess.Order(connection);
                orderItem = order.GetUnProcessedOrderItem(orderId);
                sessionId = order.GetSessionId(orderId);

                var session = new ResellerClub.DataAccess.SessionLogger(connection);
                userEmail = session.GetUserEmail(sessionId);

            }

            Customer customer = new Customer();
            customer.GetCustomerDetailByUserName(userEmail);
            customer.GetCustomerContactInfo(customer.CusInfo.CustomerID);
            Register(orderId, orderItem, customer.CusInfo);
            UpdateOrderStatus(orderId);
        }

        public List<IOrderItemMessage> GetOrderItem(Guid orderId)
        {
            List<IOrderItemMessage> orderItems = null;
            using (var connection = ConnectionFactory.GetConnection())
            {
                var order = new ResellerClub.DataAccess.Order(connection);
                orderItems = order.GetOrderItem(orderId);
            }

            return orderItems;
        }

        public DataTable GetOrder(Guid orderId)
        {
            DataTable dt = null;
            using (var connection = ConnectionFactory.GetConnection())
            {
                var order = new ResellerClub.DataAccess.Order(connection);
                dt = order.GetOrder(orderId);
            }

            return dt;
        }

        public void UpdatePaymentTranNumber(Guid orderId, string tranNumber)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var order = new ResellerClub.DataAccess.Order(connection);
                order.UpdatePaymentTranNumber(orderId,tranNumber);
                order.UpdateOrderStatus(orderId, Constant.OrderStatusUpdatedTranscationNumber); 
            }
        }

        public void UpdatePaymentMode(Guid orderId, string paymentMode)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var order = new ResellerClub.DataAccess.Order(connection);
                order.UpdatePaymentMode(orderId, paymentMode);
                order.UpdateOrderStatus(orderId, Constant.OrderStatusPaymentAwaited); 
            }
           
        }
    }
}
