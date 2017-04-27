using System.Data;
using System.Threading.Tasks;

namespace StatusMaker.Business.Columns
{
    public class Description : IColumns
    {
        public string TemplatePlaceHolder
        {
            get
            {
                return "Description";
            }
        }

        public string GetData(DataRow row)
        {
            return row[TemplatePlaceHolder].ToString();
        }
    }
}
