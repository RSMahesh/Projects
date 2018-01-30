using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;
using System.Data;
using System.IO;
using StatusMaker.Data;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public class Utility
    {
        public static MColor ToMediaColor(DColor color)
        {
            return MColor.FromArgb(color.A, color.R, color.G, color.B);
        }
        public static string CountWordsInString(string content, IEnumerable<string> excludeWords)
        {
            Dictionary<string, int> words = new Dictionary<string, int>();

            var wordPattern = new Regex(@"\w+", RegexOptions.IgnoreCase);

            foreach (Match match in wordPattern.Matches(content))
            {
                int currentCount = 0;
                words.TryGetValue(match.Value.ToLowerInvariant(), out currentCount);

                currentCount++;
                words[match.Value.ToLowerInvariant()] = currentCount;
            }

            string wordsCountInformation = string.Empty;

            foreach (var key in words.Keys)
            {
                if (!excludeWords.Contains(key) && words[key] > 1)

                    wordsCountInformation += key + " " + words[key] + "" + Environment.NewLine;
            }


            return wordsCountInformation;
        }

        private static string GetCountString(int count)
        {
            var output = "";
            for (var i = 1; i <= count; i++)
            {
                output += i.ToString() + " ";
            }

            return output;
        }


        public static void CheckDataSavedProperly(string excelPath)
        {
            var backUpFolder = Path.GetDirectoryName(excelPath) + "\\" +
                     Path.GetFileNameWithoutExtension(excelPath)
                     + "_dataBackup\\Full";


            if (!Directory.Exists(backUpFolder))
            {
                return;
            }

            var files = Directory.GetFiles(backUpFolder, "*.xml").OrderByDescending(x => x.ToString()).ToList();
            if (!files.Any())
            {
                return;
            }

            var xmlDataTable = OLDBConnection12.ReadFromXml(files.FirstOrDefault());
            OLDBConnection12 connection = new OLDBConnection12(excelPath);
            var excelDataTable = connection.ExecuteDatatable("Select * from [Sheet1$]", false);

            DataDiff diff = new DataDiff();
            diff.AreTablesTheSame(xmlDataTable, excelDataTable);
        }

        public static IEnumerable<string> AreTablesTheSame(DataTable xmlDataTable, DataTable excelDataTable)
        {
            var unique_items = new HashSet<string>();
            if (xmlDataTable.Columns.Contains("ColorCode"))
            {
                xmlDataTable.Columns.Remove("ColorCode");
            }

            if (xmlDataTable.Rows.Count != excelDataTable.Rows.Count)
            {
                unique_items.Add("Rows count mismatch");
            }


            if (xmlDataTable.Columns.Count != excelDataTable.Columns.Count)
            {
                var tb1 = excelDataTable;
                var tb2 = xmlDataTable;

                List<string> unMatchedColumns = new List<string>();
                //if there is no value in any row for a column the column won't
                // be available in xml data file. So we need to remove that column 
                // for compariosion
                foreach (DataColumn col in tb1.Columns)
                {
                    if (!tb2.Columns.Contains(col.ColumnName))
                    {
                        unMatchedColumns.Add(col.ColumnName);
                    }
                }

                foreach (var col in unMatchedColumns)
                {
                    tb1.Columns.Remove(col);
                }
            }

            for (int i = 0; i < xmlDataTable.Rows.Count; i++)
            {
                for (int c = 0; c < xmlDataTable.Columns.Count; c++)
                {
                    var xmlValue = RemoveMetaCharacter(xmlDataTable.Rows[i][c].ToString());
                    var excelValue = RemoveMetaCharacter(excelDataTable.Rows[i][c].ToString());


                    if (!EqualString(xmlValue, excelValue))

                        unique_items.Add((i + 1).ToString() + ":" + xmlDataTable.Columns[c].ColumnName +
                            Environment.NewLine + "backup:" + xmlDataTable.Rows[i][c].ToString() +
                              Environment.NewLine + "excel:" + excelDataTable.Rows[i][c].ToString() +

                               Environment.NewLine + "Diff:" + GetDiff(xmlValue, excelValue)



                            );
                }
            }
            return unique_items.ToList();
        }

        private static bool EqualString(string one, string two)
        {

            if (!one.Equals(two))
            {
                if (one.Length != two.Length)
                {
                    var tt = "dsad";
                }
            }
            return one.Equals(two);
        }

        private static string RemoveMetaCharacter(string text)
        {
            return text.Trim(Environment.NewLine.ToCharArray()).Trim();
        }

        static string GetDiff(string text1, string text2)
        {


            List<string> diff;
            IEnumerable<string> set1 = text1.Split(' ').Distinct();
            IEnumerable<string> set2 = text2.Split(' ').Distinct();

            if (set2.Count() > set1.Count())
            {
                diff = set2.Except(set1).ToList();
            }
            else
            {
                diff = set1.Except(set2).ToList();
            }

            return string.Join(";", diff);
        }
    }
}
