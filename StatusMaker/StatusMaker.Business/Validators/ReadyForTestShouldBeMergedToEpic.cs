using System;
using System.Data;
using System.Threading.Tasks;

using StatusMaker.Business.Columns;

namespace StatusMaker.Business.Validators
{
    public class ReadyForTestShouldBeMergedToEpic : IValidateData
    {
        private IJira _jira;
        private readonly IOutPutGenerator _outPutGenerator;

        public ReadyForTestShouldBeMergedToEpic(IJira jira, IOutPutGenerator outPutGenerator)
        {
            _jira = jira;
            _outPutGenerator = outPutGenerator;
        }

        public async Task<string> ValidateDataAsync(DataRow statusDataRow, string cellData)
        {
            var statusInJira = await _jira.IsValidJiraStatusAsync(
                  statusDataRow[ColumnTypes.JiraNumber].ToString(),
                  statusDataRow[ColumnTypes.Status].ToString());

            if (cellData == "Ready For Test")
            {
                if (statusDataRow["Category"].ToString() != "Merged to Epic")
                {
                    return _outPutGenerator.HighlightedText(cellData + Environment.NewLine + "Categoty Should be ->Merged to Epic");
                }
            }

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
