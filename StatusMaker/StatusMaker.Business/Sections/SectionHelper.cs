using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatusMaker.Business.Columns;
using StatusMaker.Business.Validators;
using StatusMaker.Data;
using System.Threading;

namespace StatusMaker.Business.Sections
{
    public class SectionHelper : ISectionHelper
    {
        private readonly IEnumerable<IColumns> _columns;

        private readonly List<IValidateData> _validators;

        private readonly IOutPutGenerator _outPutGenerator;

        public SectionHelper(IEnumerable<IColumns> columns, IEnumerable<IValidateData> validators, IOutPutGenerator outPutGenerator)
        {
            _columns = columns;
            _validators = validators.ToList();
            _outPutGenerator = outPutGenerator;
        }

        public string CreateSection(string heading, IEnumerable<DataRow> rows, bool validateAganistJira)
        {
            if (!rows.Any())
            {
                return string.Empty;
            }
            return _outPutGenerator.GenerateSection(heading, CreateRows(rows, validateAganistJira));
        }

        private string CreateRows12(IEnumerable<DataRow> rows, bool validateAganistJira)
        {
            var sb = new StringBuilder(1000);

            var tasks = rows.Select(async row =>
            {
                var tokens = new Dictionary<string, string>();

                foreach (var column in _columns)
                {
                    var columnData = column.GetData(row);

                    tokens[column.TemplatePlaceHolder] = columnData;

                    IEnumerable<IValidateData> valis = _validators.FindAll(x => x.ColumnType == column.TemplatePlaceHolder);

                    if (validateAganistJira)
                    {
                        foreach (var vali in valis)
                        {
                            tokens[column.TemplatePlaceHolder] = await vali.ValidateDataAsync(row, columnData);
                        }
                    }
                }

                var htmlRow = _outPutGenerator.GenerateRow(tokens);

                lock (sb)
                {
                    sb.AppendLine(htmlRow);
                }
            });

            Task.WaitAll(tasks.ToArray());

            return sb.ToString();
        }

        private string CreateRows(IEnumerable<DataRow> rows, bool validateAganistJira)
        {
            var sb = new StringBuilder(1000);

            var tasks = rows.Select(async row =>
            {
                var tokens = new Dictionary<string, string>();

                foreach (var column in _columns)
                {
                    var columnData = column.GetData(row);

                    tokens[column.TemplatePlaceHolder] = columnData;

                    IEnumerable<IValidateData> valis = _validators.FindAll(x => x.ColumnType == column.TemplatePlaceHolder);

                    if (validateAganistJira)
                    {
                        foreach (var vali in valis)
                        {
                            //var file = "5__Http" + Guid.NewGuid().ToString() + ".mb";
                            //File.AppendAllText(file, Environment.NewLine + vali.ToString() + " Start " + row[ColumnTypes.JiraNumber].ToString());

                            //NOTE :  ConfigureAwait has another important aspect: It can avoid deadlocks.(https://msdn.microsoft.com/en-us/magazine/jj991977.aspx)

                            tokens[column.TemplatePlaceHolder] = await vali.ValidateDataAsync(row, columnData).ConfigureAwait(false);
                            //File.AppendAllText(file, Environment.NewLine + vali.ToString() + " End " + row[ColumnTypes.JiraNumber].ToString());
                            //File.Move(file, file + "-done.mb");
                        }
                    }
                }

                var htmlRow = _outPutGenerator.GenerateRow(tokens);

                lock (sb)
                {
                    sb.AppendLine(htmlRow);
                }
            });

             Task.WaitAll(tasks.ToArray());

            return sb.ToString();
        }
    }
}
