using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using APIC = ResellerClub.Common;
using ResellerClub.Interface.Messages;
using ResellerClub.Messages;

namespace ResellerClub.WebUI
{
    public sealed class Cart
    {
        List<IOrderItemMessage> items = new List<IOrderItemMessage>();

        public List<IOrderItemMessage> Items
        {
            get { return items; }
            set { items = value; }
        }

        IOrderItemMessage selectedWebservice;
        bool dirtyFlag = false;

        public void AddItemToCart(string domainName, Guid subPlanID, bool enableSsl, bool enableMaintenance)
        {
            if(!ItemExist(domainName,subPlanID))
            {
            IOrderItemMessage item = new OrderItemMessage(subPlanID, domainName, enableSsl, enableMaintenance);
            items.Add(item);
            dirtyFlag = true;
            }
        }

        private bool ItemExist(string domainName, Guid subPlanID)
        {
           return items.Find(x => x.DomainName == domainName && x.SubPlanID == subPlanID) != null;
        }

        public void SetSelectedWebservice(string subPlanId)
        {
            SetSelectedWebservice(new Guid( subPlanId), false, false);
        }

        public void SetSelectedWebservice(Guid  subPlanId, bool enableSsl, bool enableMaintenance)
        {
            selectedWebservice = new OrderItemMessage(subPlanId, "", enableSsl, enableMaintenance);
            dirtyFlag = true;
        }

        public void AddDomainToSelectedWebService(string domainName)
        {
            selectedWebservice.DomainName = domainName;
            items.Add(selectedWebservice);
        }

        public bool DirtyFlag
        {
            get { return dirtyFlag; }
        }

        public bool IsWebServiceSelected
        {
            get { return selectedWebservice != null; }
        }

        public void SetSelectedWebServiceNULL()
        {
            selectedWebservice = null;
        }

        public void RemoveAllItem()
        {
            items.Clear(); ;
       }
    }
}
