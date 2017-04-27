using System.Data;
using System.Threading.Tasks;

namespace StatusMaker.Business.Columns
{
    public class Epic : IColumns
    {
        public string TemplatePlaceHolder
        {
            get
            {
                return "EPIC";
            }
        }

        public string GetData(DataRow row)
        {
            return row[TemplatePlaceHolder].ToString();
        }
    }
}
