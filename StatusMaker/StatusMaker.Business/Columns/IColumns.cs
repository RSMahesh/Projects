using System.Data;
using System.Threading.Tasks;

namespace StatusMaker.Business.Columns
{
    public interface IColumns
    {
        string GetData(DataRow statusDataRow);
       // Task<string> GetDataAndValidateAsync(DataRow statusDataRow);
        string TemplatePlaceHolder { get; }
    }
}
