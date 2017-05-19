using System;
using System.Data;
using System.Threading.Tasks;

using StatusMaker.Business.Columns;

namespace StatusMaker.Business.Validators
{
    public class StatusShouldMatchJira : IValidateData
    {
        private IJira _jira;
        private readonly IOutPutGenerator _outPutGenerator;

        public StatusShouldMatchJira(IJira jira, IOutPutGenerator outPutGenerator)
        {
            _jira = jira;
            _outPutGenerator = outPutGenerator;
        }

        public async Task<string> ValidateDataAsync(DataRow statusDataRow, string cellData)
        {
            var statusInJira = await _jira.IsValidJiraStatusAsync(
                  statusDataRow[ColumnTypes.JiraNumber].ToString(),
                  statusDataRow[ColumnTypes.Status].ToString());

            if (statusInJira != string.Empty)
            {
                return _outPutGenerator.IncorrectAndCorrect(statusDataRow[ColumnTypes.Status].ToString(), statusInJira);
            }

            return statusDataRow[ColumnTypes.Status].ToString();
        }

        public string ColumnType
        {
            get
            {
                return ColumnTypes.Status;
            }
        }
    }
}
