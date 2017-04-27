using System;
using ResellerClub.Interface.Messages; 
namespace ResellerClub.Interface
{
    public interface ICustomer : IBaseInterface 
    {
        ICustomerInfoMessage CusInfo
        {
            get;
            set;
        }
        string GetAdminUrlWithToken();
        object GetAllOrders();
        void GetCustomerContactInfo(string customerID);
        bool GetCustomerDetailByUserName(string userEmail);
        void Register();
        bool ValidateCustomer(string userEmail, string password);
        bool GenerateTempPasswordSendEmail(string userEmail, out string message);
        bool ChangePassword(string userEmail, string oldPassword, string newPassword, out string message);
        bool GenerateTempPassword(string userEmail, out string message, out string tempPassword);
    }
}
