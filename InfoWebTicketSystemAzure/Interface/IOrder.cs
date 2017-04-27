using System;
using System.Collections.Generic;

using System.Text;
using ResellerClub.Common;
using ResellerClub.Interface.Messages;
using System.Data; 

namespace ResellerClub.Interface
{
   public interface IOrder :IBaseInterface 
    {
       IList<IInvoiceInfoMessage> Register(Guid orderId, IList<IOrderItemMessage> orderItems, ICustomerInfoMessage cusInfo);
       Guid SaveOrder(IList<IOrderItemMessage> orderItems,Guid sessionID, decimal orderAmount);
       void UpdateOrderStatus(Guid orderId, int status);
       void UpdatePaymentTranNumber(Guid orderId, string tranNumber);
       void UpdatePaymentMode(Guid orderId, string paymentMode);
       void ProcessOrder(Guid orderId);
       List<IOrderItemMessage> GetOrderItem(Guid orderId);
       DataTable GetOrder(Guid orderId);
    }
}
