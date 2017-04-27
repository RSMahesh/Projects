using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using StatusMaker.Business.Validators;

namespace StatusMaker.Business.Columns
{
    public class Author : IColumns
    {
#pragma warning disable CS0169 // The field 'Author._validators' is never used
        private IEnumerable<IValidateData> _validators;
#pragma warning restore CS0169 // The field 'Author._validators' is never used

        public string TemplatePlaceHolder
        {
            get
            {
                return "Author";
            }
        }

        public string GetData(DataRow row)
        {
            return this.GetAuthor(row);
        }

        private string GetAuthor(DataRow dr)
        {
            var author = dr[TemplatePlaceHolder].ToString();
            author += GetMemberNameWithRole(dr, "Review", "R");
            author += GetMemberNameWithRole(dr, "Tester", "T");

            if (dr[TemplatePlaceHolder].ToString() != author)
            {
                author = "A:" + author;
            }

            return author;
        }

        private string GetMemberNameWithRole(DataRow dr, string columName, string role)
        {
            return !string.IsNullOrEmpty(dr[columName].ToString())
                       ? Environment.NewLine + role + ":" + dr[columName]
                       : string.Empty;
        }
    }
}
