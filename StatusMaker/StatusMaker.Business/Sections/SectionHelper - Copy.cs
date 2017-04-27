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

        private string CreateRows(IEnumerable<DataRow> rows, bool validateAganistJira)
        {
            var sb = new StringBuilder(1000);
            var parentfile = "parent" + Guid.NewGuid().ToString() + ".mb";

            File.AppendAllText(parentfile, "Got In" + Environment.NewLine);

            var tasks = rows.Select(async row =>
            {
                var tokens = new Dictionary<string, string>();

                var file = Thread.CurrentThread.ManagedThreadId.ToString() + ".mb";
                File.AppendAllText(file, "1" + Environment.NewLine);

                foreach (var column in _columns)
                {
                    var columnData = column.GetData(row);

                    tokens[column.TemplatePlaceHolder] = columnData;

                    IEnumerable<IValidateData> valis = _validators.FindAll(x => x.ColumnType == column.TemplatePlaceHolder);

                    if (validateAganistJira)
                    {
                        foreach (var vali in valis)
                        {
                            File.AppendAllText(file, "2 " + vali.ToString()  + Environment.NewLine);
                            tokens[column.TemplatePlaceHolder] = await vali.ValidateDataAsync(row, columnData);
                            File.AppendAllText(file, "3" + Environment.NewLine);
                        }
                    }
                }

                File.AppendAllText(file, "4" + Environment.NewLine);
                var htmlRow = _outPutGenerator.GenerateRow(tokens);
                File.AppendAllText(file, "5" + Environment.NewLine);

                File.AppendAllText(file, "Getting lock");

                lock (sb)
                {
                    File.AppendAllText(file, "Got lock");
                    sb.AppendLine(htmlRow);
                }
                File.AppendAllText(file, "OutOf lock");
            });

            File.AppendAllText(parentfile, "About start threads In" + Environment.NewLine);
            Task.WaitAll(tasks.ToArray());

            File.AppendAllText(parentfile, "Completed start threads" + Environment.NewLine);

            return sb.ToString();
        }
    }
}
