using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using StatusMaker.Business.Columns;

namespace StatusMaker.Business.Validators
{
    public class ValidateComments : IValidateData
    {
        private readonly IOutPutGenerator _outPutGenerator;

        public ValidateComments(IOutPutGenerator outPutGenerator)
        {
            _outPutGenerator = outPutGenerator;
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> ValidateDataAsync(System.Data.DataRow dr, string cellData)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            if (string.IsNullOrEmpty(dr[ColumnTypes.Comments].ToString()))
            {
                return _outPutGenerator.HighlightedTextWithCrazyImage("No Comments");
            }

            return cellData;
        }

        public string ColumnType
        {
            get
            {
                return ColumnTypes.Comments;
            }
        }
    }
}
