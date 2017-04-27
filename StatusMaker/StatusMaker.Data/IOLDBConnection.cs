using System;
using System.Collections.Generic;
using System.Data;

namespace StatusMaker.Data
{
    public interface IOLDBConnection
    {
        DataTable GetInProgessDataTable(DateTime statusDate, string memberName);

        DataTable GetAhocDataTable(DateTime statusDate, string memberName);

        IEnumerable<DataRow> GetInProgessRowsExcludingRegression(DateTime statusDate, string memberName);

        IEnumerable<DataRow> GetMergedToEpicRows(DateTime statusDate, string memberName);

        IEnumerable<DataRow> GetInProgessRegressionRows(DateTime statusDate, string memberName);
    }
}