using System.Data;
using System.Threading.Tasks;

namespace StatusMaker.Business.Columns
{
    public class Status : IColumns
    {
  
        public string TemplatePlaceHolder
        {
            get
            {
                return "Status";
            }
        }

        public string GetData(DataRow row)
        {
            return row[TemplatePlaceHolder].ToString();
        }
    }
}
