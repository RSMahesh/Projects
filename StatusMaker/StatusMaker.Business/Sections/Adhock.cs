using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

using StatusMaker.Data;

namespace StatusMaker.Business.Sections
{
    public class Adhock : ISection
    {
        private readonly IOLDBConnection _oldbConnection;
        private readonly IOutPutGenerator _outPutGenerator;

        public Adhock(IOLDBConnection oldbConnection,IOutPutGenerator outPutGenerator)
        {
               _oldbConnection = oldbConnection;
            _outPutGenerator = outPutGenerator;
        }

        public string GetItmesAsHtml(DateTime statusDate, string memberName, bool validateAganistJira)
        {
            var excelDt = _oldbConnection.GetAhocDataTable(statusDate, memberName);
      
            if (excelDt.DefaultView.Count < 1)
            {
                return string.Empty;
            }

            return _outPutGenerator.GenerateAhockSection("AD-Hoc", this.CreateAdhocRows(excelDt.DefaultView));
        }

        public string TemplatePlaceHolder
        {
            get
            {
                return SectionTypes.ADHOCK;
            }
        }

        private string CreateAdhocRows(DataView dv)
        {
            var sb = new StringBuilder(500);

            foreach (DataRowView rowView in dv)
            {
                var token = new Dictionary<string, string>();
                var row = rowView.Row;

                token.Add("Work", row["Work"].ToString());
                token.Add("Status", row["Status"].ToString());
                token.Add("Author", row["Author"].ToString());
                sb.AppendLine(_outPutGenerator.GeneratAdhockeRow(token));
            }

            return sb.ToString();
        }
    }
}
