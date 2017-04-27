using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StatusMaker.Business
{
    public class HtmlOutPutGenerator : IOutPutGenerator
    {
        protected string _tempalteRootPath = AppDomain.CurrentDomain.BaseDirectory;

        public string GenerateSection(string heading, string content)
        {
            return GetSection(heading, content, "TableTemplate.html");
        }

        public string GenerateAhockSection(string heading, string content)
        {
            return GetSection(heading, content, "AdHocTable.html");
        }

        private string GetSection(string heading, string content, string templateName)
        {
            var tableTempalte = File.ReadAllText(Path.Combine(_tempalteRootPath, templateName));

            tableTempalte = tableTempalte.Replace("<%Rows%>", content);

            var heading1 =
                " <p class=MsoNormal><span lang=EN-US style='mso-ansi-language:EN-US'><o:p>&nbsp;</o:p></span></p> <p class=MsoNormal><span style='color:#1F497D'>"
                + heading + ":</span></p>";


            return heading1 + tableTempalte;
        }

        public string GeneratAdhockeRow(Dictionary<string, string> tokens)
        {
            return GetRow(tokens, "AdHockTableRow.html");
        }

        public string GenerateRow(Dictionary<string, string> tokens)
        {
            return GetRow(tokens, "TableRowTemplate.html");
        }

        private string GetRow(Dictionary<string, string> tokens, string templateName)
        {
            var itemTempalte = File.ReadAllText(Path.Combine(_tempalteRootPath, templateName));

            return tokens.Keys.Aggregate(itemTempalte, (current, key) => current.Replace("<%" + key + "%>", tokens[key]));
        }


        public string GenerateMail(ConcurrentDictionary<string, string> tokens)
        {
            var itemTempalte = File.ReadAllText(Path.Combine(_tempalteRootPath, "MailTemplate.html"));

            return tokens.Keys.Aggregate(itemTempalte, (current, key) => current.Replace("<%" + key + "%>", tokens[key]));
        }

        public string HighlightedText(string text)
        {
            return "<font color='red'>" + text + "</font>";
        }

        public string HighlightedTextWithToolTip(string text, string toolTip)
        {
            return "<div style='display: inline;' title='" + toolTip + "'> <font color='red'>" + text + "</font></div>";
        }

        public string HighlightedTextWithCrazyImage(string text)
        {
            return HighlightedText(text)
                   + "<image src='http://10.131.40.102/yellow_guy_crazy_hg_wht.gif' alt='NO COMMENT' style='width:40px;height:40px' />";
        }

        public string IncorrectAndCorrect(string incorrect, string correct)
        {
            return "<font color='red'> <span style='text-decoration: line-through;'>" + incorrect
                       + "</span><br/>" + "Correct :" + correct + "</font>";
        }

        public string TextWithJiraLink(string jiraNumber)
        {
            return "<a style='text-decoration:none' target='_blank' href=https://mediavalet.atlassian.net/browse/"
                 + jiraNumber + ">"
                 + jiraNumber + "</a>";
        }
    }
}
