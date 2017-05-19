using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using StatusMaker.Business.Columns;

namespace StatusMaker.Business.Validators
{
    public class PrNumberShouldMatchJira : IValidateData
    {
        private IJira _jira;
        private readonly IOutPutGenerator _outPutGenerator;

        public PrNumberShouldMatchJira(IJira jira, IOutPutGenerator outPutGenerator)
        {
            _jira = jira;
            _outPutGenerator = outPutGenerator;
        }


        public async Task<string> ValidateDataAsync(DataRow statusDataRow, string cellData)
        {
            var pullsInJira = await _jira.GetAllValidPullsAsync(statusDataRow[ColumnTypes.JiraNumber].ToString());
            var pullsInStatus = GetPullsDefeniedInStatus(cellData);

            if (pullsInJira.Any() && !pullsInStatus.Any())
            {
                return _outPutGenerator.HighlightedTextWithToolTip("PR Not Mentioned", string.Join(",", pullsInJira));
            }


            var matchedPulls = pullsInJira.Intersect(pullsInStatus);

            if (pullsInStatus.Except(matchedPulls).Any())
            {
                return _outPutGenerator.HighlightedTextWithToolTip(cellData, string.Join(",", pullsInJira));
            }

            return cellData;
        }

        public string ColumnType
        {
            get
            {
                return ColumnTypes.PRNumber;
            }
        }

        private IEnumerable<string> GetPullsDefeniedInStatus(string cellData)
        {
            if (string.IsNullOrEmpty(cellData.Trim()))
            {
                return Enumerable.Empty<string>();
            }

            return cellData.Split(',');
        }
    }
}
