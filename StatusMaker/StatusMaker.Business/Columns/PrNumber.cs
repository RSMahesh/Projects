using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StatusMaker.Business.Columns
{
    public class PrNumber : IColumns
    {
      
        public string TemplatePlaceHolder
        {
            get
            {
                return "PR #";
            }
        }

        public string GetData(DataRow statusDataRow)
        {
            return statusDataRow[TemplatePlaceHolder].ToString();
        }
    }
}
