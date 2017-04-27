using System;
using System.Collections.Generic;

using StatusMaker.Business.Columns;
using StatusMaker.Data;

namespace StatusMaker.Business.Sections
{
    public class MergedToEpic : ISection
    {
        private ISectionHelper _sectionHelper;
        private readonly IOLDBConnection _oldbConnection;

        public MergedToEpic(ISectionHelper sectionHelper, IOLDBConnection oldbConnection)
        {
            _sectionHelper = sectionHelper;
            _oldbConnection = oldbConnection;
        }


        public string GetItmesAsHtml(DateTime statusDate, string memberName, bool validateAganistJira)
        {
            var dataRows = _oldbConnection.GetMergedToEpicRows(statusDate, memberName);

            return _sectionHelper.CreateSection("Merged To Epic", dataRows, validateAganistJira);
        }


        public string TemplatePlaceHolder
        {
            get
            {
                return SectionTypes.MergedToEpic;
            }
        }

    }
}
