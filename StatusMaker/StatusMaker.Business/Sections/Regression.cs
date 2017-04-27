using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StatusMaker.Business.Columns;
using StatusMaker.Data;

namespace StatusMaker.Business.Sections
{
    public class Regression : ISection
    {
        private ISectionHelper _sectionHelper;
        private readonly IOLDBConnection _oldbConnection;

        public Regression(ISectionHelper sectionHelper, IOLDBConnection oldbConnection)
        {
            _sectionHelper = sectionHelper;
            _oldbConnection = oldbConnection;
        }

        public string GetItmesAsHtml(DateTime statusDate, string memberName, bool validateAganistJira)
        {
            var dataRows = _oldbConnection.GetInProgessRegressionRows(statusDate, memberName);

            return _sectionHelper.CreateSection(
                "Regression",
                dataRows,
                validateAganistJira);
        }


        public string TemplatePlaceHolder
        {
            get
            {
                return SectionTypes.Regression;
            }
        }

    }
}
