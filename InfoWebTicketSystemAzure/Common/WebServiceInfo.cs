using System;
using System.Data;
using System.Configuration;

using System.Web;

namespace ResellerClub.Common
{
    public class WebServiceInfo : ICloneable
    {
        public string DomainName { get; set; }
        public string PlanId { get; set; }
        public bool EnableSsl { get; set; }
        public bool EnableMaintenance { get; set; }
        public string ItemId { get; set; }

        public WebServiceInfo(string domainName, string planId, bool enableSsl, bool enableMaintenance)
        {
            this.DomainName = domainName;
            this.PlanId = planId;
            this.EnableSsl = enableSsl;
            this.EnableMaintenance = enableMaintenance;
            this.ItemId = Guid.NewGuid().ToString();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}