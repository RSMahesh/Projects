using System.Threading.Tasks;

using StatusMaker.Business.Columns;

namespace StatusMaker.Business.Validators
{
    public class ValidateReviewer : IValidateData
    {
        
        private readonly IOutPutGenerator _outPutGenerator;

        public ValidateReviewer(IOutPutGenerator outPutGenerator)
        {
            _outPutGenerator = outPutGenerator;
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> ValidateDataAsync(System.Data.DataRow dr, string cellData)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {

            if (dr["Category"].ToString() == "Merged to Epic")
            {
                if (string.IsNullOrEmpty(dr["Review"].ToString()))
                {
                    return cellData + _outPutGenerator.HighlightedTextWithCrazyImage(" Reviewer Missing");
                }
            }

            return cellData;
        }

        public string ColumnType
        {
            get
            {
                return ColumnTypes.Author;
            }
        }
    }
}
