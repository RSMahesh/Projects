using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using StatusMaker.Business.Columns;

namespace StatusMaker.Business.Sections
{
    public class FromName : ISection
    {
       
        public string GetItmesAsHtml(DateTime statusDate, string memberName, bool validateAganistJira)
        {
            var userFullName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').LastOrDefault();
            var userFirstName = userFullName.Split('.').FirstOrDefault();
            return new CultureInfo("en-US", false).TextInfo.ToTitleCase(userFirstName);
        }

        public string TemplatePlaceHolder
        {
            get
            {
                return SectionTypes.From;
            }
        }
    }
}
