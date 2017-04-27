using System.Data;
using System.Threading.Tasks;

namespace StatusMaker.Business.Columns
{
    public class Comments : IColumns
    {
       
        public string TemplatePlaceHolder
        {
            get
            {
                return "Comments";
            }
        }

        public string GetData(DataRow row)
        {
            return row[TemplatePlaceHolder].ToString();
        }


    }
}
