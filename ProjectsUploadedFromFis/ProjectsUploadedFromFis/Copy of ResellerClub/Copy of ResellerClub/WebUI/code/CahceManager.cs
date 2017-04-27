using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Loc = ResellerClub.Localization;


namespace ResellerClub.WebUI
{
    public static class CahceManager
    {
        public static Loc.Setting GetCountrySetting(string country)
        {
            country = country.ToLower();
            string xmlPath = HttpContext.Current.Server.MapPath(".") + "\\XMLs\\" + country + ".xml";
            Cache cache = HttpContext.Current.Cache;
            if (cache[country] == null)
            {
                cache.Insert(country, Loc.LocalizationFactory.GetSettings(xmlPath), new CacheDependency(xmlPath));
            }

           return  cache[country] as Loc.Setting;
        }
    }
}
