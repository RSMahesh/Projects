using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class FormulaRunner
    {
        DataGridView dataGridView;
        string currentFormula;
        public FormulaRunner(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
        }
        public string ExecuteFromula(DataGridViewCell cell, string currentFormula)
        {
            this.currentFormula = currentFormula;
            var replaceOutPut = ReplacePlaceHolders(cell);
            replaceOutPut = Trim(replaceOutPut);
            replaceOutPut = RemoveIfRequired(replaceOutPut);
            replaceOutPut = MakeFirstCharacterCaptial(replaceOutPut);
            replaceOutPut = MakePragraphCase(replaceOutPut);
            return replaceOutPut;
        }
        private string MakeFirstCharacterCaptial(string replaceOutPut)
        {
            //TODO:This formula will fail if text got mutiple %p%. Need to get fixed.
            const string remove = "%tc%";
            var indx = replaceOutPut.IndexOf(remove, StringComparison.OrdinalIgnoreCase);

            var arr = replaceOutPut.Split(new string[] { remove }, StringSplitOptions.None);
            if (arr.Length > 2)
            {
                var one = FirstCharToUpper(arr[1].ToLowerInvariant());

                replaceOutPut = one + arr[2];
            }

            return replaceOutPut;
        }

        private string MakePragraphCase(string replaceOutPut)
        {
            //TODO:This formula will fail if text got mutiple %pc%. Need to get fixed.
            const string remove = "%sc%";
            var indx = replaceOutPut.IndexOf(remove, StringComparison.OrdinalIgnoreCase);

            var arr = replaceOutPut.Split(new string[] { remove }, StringSplitOptions.None);
            if (arr.Length > 2)
            {
                var one = MakeAllSetenceCase(arr[1].ToLowerInvariant());

                replaceOutPut = one + arr[2];
            }

            return replaceOutPut;
        }

        private string Trim(string replaceOutPut)
        {
            const string remove = "%t%";
            var indx = replaceOutPut.IndexOf(remove, StringComparison.OrdinalIgnoreCase);

            var arr = replaceOutPut.Split(new string[] { remove }, StringSplitOptions.None);
            if (arr.Length > 2)
            {
                var one = arr[1].Trim();

                replaceOutPut = one + arr[2];
            }

            return replaceOutPut.Trim();
        }
        string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default:
                    var arr = input.Split(' ');

                    for (var i = 0; i < arr.Length; i++)
                    {
                        if (arr[i].Length > 1)
                        {
                            arr[i] = arr[i].First().ToString().ToUpper() + arr[i].Substring(1);
                        }
                    }


                    return string.Join(' '.ToString(), arr);
            }
        }


        string MakeAllSetenceCase(string input)
        {
            var arr = input.Split('.');

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)

                    if (arr[i][j] != ' ')
                    {
                        arr[i] = ' ' + arr[i][j].ToString().ToUpper() + arr[i].Substring(j + 1);
                        break;
                    }
            }

            return string.Join(".", arr).Trim();
        }

        private string RemoveIfRequired(string replaceOutPut)
        {
            const string remove = "%-%";
            var indx = replaceOutPut.IndexOf(remove, StringComparison.OrdinalIgnoreCase);

            if (indx > 0)
            {
                var arr = replaceOutPut.Split(new string[] { remove }, StringSplitOptions.None);
                if (arr.Length > 1)
                {
                    replaceOutPut = Regex.Replace(arr[0], arr[1], string.Empty, RegexOptions.IgnoreCase);
                }
            }

            return replaceOutPut;
        }
        private string ReplacePlaceHolders(DataGridViewCell cell)
        {
            var regex = new Regex("{.*?}");
            var matches = regex.Matches(currentFormula);
            //  var formula = FormulaWindow.Formula;
            string formulaOutPut = currentFormula;

            foreach (Match match in matches)
            {
                var replaceValue = string.Empty;
                var columnName = match.Value.Replace("{", "").Replace("}", "");
                if (columnName.Equals(Constants.CurrentCell, StringComparison.OrdinalIgnoreCase))
                {
                    replaceValue = cell.Value.ToString();
                }
                else
                {
                    replaceValue = dataGridView.Rows[cell.RowIndex].Cells[columnName].Value.ToString();
                }
                formulaOutPut = formulaOutPut.Replace("{" + columnName + "}", replaceValue);
            }

            return formulaOutPut;
        }
    }
}
