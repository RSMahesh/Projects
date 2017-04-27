using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using StatusMaker.Business.Columns;

namespace StatusMaker.Business.Validators
{
    public class ValidateDescription : IValidateData
    {
       private IJira _jira;
        private readonly IOutPutGenerator _outPutGenerator;

        public ValidateDescription(IJira jira, IOutPutGenerator outPutGenerator)
        {
            _jira = jira;
            _outPutGenerator = outPutGenerator;
        }

        public async Task<string> ValidateDataAsync(DataRow statusDataRow, string cellData)
        {
            var description =
                await _jira.GetDescriptionAsync(statusDataRow[ColumnTypes.JiraNumber].ToString()).ConfigureAwait(false);

            return cellData.Equals(description, StringComparison.OrdinalIgnoreCase)? cellData : _outPutGenerator.IncorrectAndCorrect(cellData, description);
        }

        public string ColumnType
        {
            get
            {
                return ColumnTypes.Description;
            }
        }
    }
}
