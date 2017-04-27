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
    public class ValidateEpic : IValidateData
    {
       private IJira _jira;
        private readonly IOutPutGenerator _outPutGenerator;

        public ValidateEpic(IJira jira, IOutPutGenerator outPutGenerator)
        {
            _jira = jira;
            _outPutGenerator = outPutGenerator;
        }

        public async Task<string> ValidateDataAsync(DataRow statusDataRow, string cellData)
        {
            if (string.IsNullOrEmpty(cellData))
            {
                return "NO EPIC LINKED";
            }

            var epicInJira =
                await _jira.IsValidEpicNumberAsync(statusDataRow[ColumnTypes.JiraNumber].ToString(), cellData);

            return epicInJira != string.Empty ? _outPutGenerator.IncorrectAndCorrect(cellData, epicInJira) : cellData;
        }

        public string ColumnType
        {
            get
            {
                return ColumnTypes.EPIC;
            }
        }
    }
}
