using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusMaker.Business
{
    public class OutPutGenerator
    {
        protected static string _tempalteRootPath = @"D:\Projects\StatusMaker\StatusMaker\bin\Debug";

        public static string GenerateSection(string heading, string content, string templateName = "TableTemplate.html")
        {
            var tableTempalte = File.ReadAllText(Path.Combine(_tempalteRootPath, templateName));

            tableTempalte = tableTempalte.Replace("<%Rows%>", content);

            var heading1 =
                " <p class=MsoNormal><span lang=EN-US style='mso-ansi-language:EN-US'><o:p>&nbsp;</o:p></span></p> <p class=MsoNormal><span style='color:#1F497D'>"
                + heading + ":</span></p>";


            return heading1 + tableTempalte;
        }

        public static string GenerateRow(Dictionary<string, string> tokens, string templateName = "TableRowTemplate.html")
        {
            var itemTempalte = File.ReadAllText(Path.Combine(_tempalteRootPath, templateName));

            return tokens.Keys.Aggregate(itemTempalte, (current, key) => current.Replace("<%" + key + "%>", tokens[key]));
        }

        public static string GenerateMail(ConcurrentDictionary<string, string> tokens)
        {
            var itemTempalte = File.ReadAllText(Path.Combine(_tempalteRootPath, "MailTemplate.html"));

            return tokens.Keys.Aggregate(itemTempalte, (current, key) => current.Replace("<%" + key + "%>", tokens[key]));
        }

        public static string HighlightedText(string text)
        {
            return "<font color='red'>" + text + "</font>";
        }

        public static string HighlightedTextWithToolTip(string text, string toolTip)
        {
            return "<div style='display: inline;' title='" + toolTip + "'> <font color='red'>" + text + "</font></div>";
        }

        public static string HighlightedTextWithCrazyImage(string text)
        {
            return HighlightedText(text)
                   + "<image src='http://10.131.60.128/yellow_guy_crazy_hg_wht.gif' alt='NO COMMENT' style='width:40px;height:40px' />";
        }

        public static string IncorrectAndCorrect(string incorrect, string correct)
        {
            return "<font color='red'> <span style='text-decoration: line-through;'>" + incorrect
                       + "</span><br/>" + "Correct :" + correct + "</font>";
        }

        public static string TextWithJiraLink(string jiraNumber)
        {
            return "<a style='text-decoration:none' target='_blank' href=https://mediavalet.atlassian.net/browse/"
                 + jiraNumber + ">"
                 + jiraNumber + "</a>";
        }
    }
}
