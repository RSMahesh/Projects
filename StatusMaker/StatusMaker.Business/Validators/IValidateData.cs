using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusMaker.Business.Validators
{
    public interface IValidateData
    {
        Task<string> ValidateDataAsync(DataRow dr, string cellData);

        string ColumnType { get; }

    }
}
