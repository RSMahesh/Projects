using ResellerClub.Interface.Messages;

namespace InfoWebTicketSystem.WebUI
{
    public class Customer 
    {
        CustomerInfoMessage cusInfo;
        internal string __password;

        const string adminAreaUrl = "http://webmasters.infowebservices.in/servlet/AutoLoginServlet?role=customer&langpref=en&newFlow=true&userLoginId=";

        public Customer()
        {
            this.cusInfo = new CustomerInfoMessage();
        }

         public CustomerInfoMessage CusInfo
        {
            get { return this.cusInfo; }
            set { this.cusInfo = value; }
        }

    }
}
