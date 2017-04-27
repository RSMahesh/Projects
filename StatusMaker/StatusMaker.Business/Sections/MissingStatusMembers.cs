using System;
using System.Collections.Generic;
using System.Data;

using StatusMaker.Business.Columns;
using StatusMaker.Data;

namespace StatusMaker.Business.Sections
{
    public class MissingStatusMembers : ISection
    {
        private string Authors = string.Empty;
        private readonly IOutPutGenerator _outPutGenerator;

    
        private readonly IOLDBConnection _oldbConnection;

        public MissingStatusMembers(IOLDBConnection oldbConnection, IOutPutGenerator outPutGenerator)
        {
            _oldbConnection = oldbConnection;
            _outPutGenerator = outPutGenerator;
        }

        public string GetItmesAsHtml(DateTime statusDate, string memberName, bool validateAganistJira)
        {
            var excelDt = _oldbConnection.GetInProgessDataTable(statusDate, memberName);

            foreach (DataRowView rowView in excelDt.DefaultView)
            {
                var row = rowView.Row;
                this.Authors += row["Author"] + row["Review"].ToString() + row["Tester"].ToString();
            }

            excelDt = _oldbConnection.GetAhocDataTable(statusDate, memberName);

            foreach (DataRowView rowView in excelDt.DefaultView)
            {
                this.Authors += rowView.Row["Author"];
            }

            return this.GetMissingStatusForMembers();
        }


        public string TemplatePlaceHolder
        {
            get
            {
                return SectionTypes.StatusMissing;
            }
        }

        private string GetMissingStatusForMembers()
        {
            string missing = string.Empty;

            this.Authors = this.Authors.ToLowerInvariant();

            foreach (var member in Enum.GetValues(typeof(TeamMember)))
            {
                if (!this.Authors.Contains(member.ToString().ToLowerInvariant()))
                {
                    missing += member + Environment.NewLine + ",";
                }
            }
            if (string.IsNullOrEmpty(missing))
            {
                return string.Empty;
            }

         return   _outPutGenerator.HighlightedText(
                "Status Missing for following members:" + Environment.NewLine + missing.Trim(','));
        }
    }
}
