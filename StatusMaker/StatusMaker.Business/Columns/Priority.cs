using System.Data;
using System.Threading.Tasks;

namespace StatusMaker.Business.Columns
{
    public class Priority : IColumns
    {

        public string TemplatePlaceHolder
        {
            get
            {
                return "Priority";
            }
        }

        public string GetData(DataRow row)
        {
            return row[TemplatePlaceHolder].ToString();
        }

    }
}
