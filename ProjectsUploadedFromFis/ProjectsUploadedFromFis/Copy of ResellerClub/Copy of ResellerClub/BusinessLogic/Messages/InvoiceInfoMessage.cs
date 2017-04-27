using System;
using System.Collections.Generic;

using System.Text;
using ResellerClub.Interface.Messages; 

namespace ResellerClub.BusinessLogic.Messages
{
    public class InvoiceInfoMessage : IInvoiceInfoMessage
    {
        string domain;
        string description;
        string invioceNumber;
        int amount;

        public string Domain
        {
            get { return domain; }
            set { domain = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string InvioceNumber
        {
            get { return invioceNumber; }
            set { invioceNumber = value; }
        }
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

    }
}
