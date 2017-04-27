using System.Data;
using System.Threading.Tasks;

namespace StatusMaker.Business.Columns
{
    public class JiraNumber : IColumns
    {

         private readonly IOutPutGenerator _outPutGenerator;

         public JiraNumber(IOutPutGenerator outPutGenerator)
        {
            _outPutGenerator = outPutGenerator;
        }

        public string TemplatePlaceHolder
        {
            get
            {
                return "JIRA #";
            }
        }

        public string GetData(DataRow statusDataRow)
        {
            return _outPutGenerator.TextWithJiraLink(statusDataRow[TemplatePlaceHolder].ToString());
        }
    }
}
