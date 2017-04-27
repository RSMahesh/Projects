using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages;
using System.Web;

namespace ResellerClub.Interface
{
    public interface IPaymentProcessor : IBaseInterface 
    {
       string CreateRequest(List<IOrderItemMessage> items,Guid orderId,string processorUrl, Dictionary<string, string> settings);
        void InsertTransactionLog(string request, Guid orderId);
        void UpdateTransactionLog(string response, Guid orderId);
        bool ProcessResponse(HttpRequest request, string payPalUrl);
    }
}
