using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace StatusMaker.Business
{
    public class XMLOutPutGenerator : IOutPutGenerator
    {
      
        public string GenerateSection(string heading, string content)
        {
            return GetSection(heading, content);
        }

        public string GenerateAhockSection(string heading, string content)
        {
            return GetSection(heading, content);
        }

        private string GetSection(string heading, string content)
        {
            return "<SECTION> <HEADING>" + heading + " </HEADING>" + content + "</SECTION>" + Environment.NewLine;
        }

        public string GeneratAdhockeRow(Dictionary<string, string> tokens)
        {
            return GetRow(tokens);
        }

        public string GenerateRow(Dictionary<string, string> tokens)
        {
            return GetRow(tokens);
        }

        private string GetRow(Dictionary<string, string> tokens)
        {
            var sb = new StringBuilder();

            foreach (var key in tokens.Keys)
            {
                sb.Append("<" + key.Replace("#", "") + ">" + tokens[key] + "</" + key.Replace("#", "") + ">" + Environment.NewLine);
            }

            return sb.ToString();
        }

        public string GenerateMail(ConcurrentDictionary<string, string> tokens)
        {
            var itemTempalte = File.ReadAllText(Path.Combine("", "MailTemplate.html"));

            return tokens.Keys.Aggregate(itemTempalte, (current, key) => current.Replace("<%" + key + "%>", tokens[key]));
        }

        public string HighlightedText(string text)
        {
            return "<HIGHLIGHTEDTEXT>" + text + "</HIGHLIGHTEDTEXT>";
        }

        public string HighlightedTextWithToolTip(string text, string toolTip)
        {
            return "<HIGHLIGHTEDTEXTWITHTOOLTIP>" + text + "</HIGHLIGHTEDTEXTWITHTOOLTIP>";
        }

        public string HighlightedTextWithCrazyImage(string text)
        {
            return "<HIGHLIGHTEDTEXTWITHCRAZYIMAGE>" + text + "</HIGHLIGHTEDTEXTWITHCRAZYIMAGE>";
        }

        public string IncorrectAndCorrect(string incorrect, string correct)
        {
            return "<CORRECT>" + incorrect + "</CORRECT>"
                       + "<INCORRECT>" + correct + "</INCORRECT>";
        }

        public string TextWithJiraLink(string jiraNumber)
        {
            return "<TEXTWITHJIRALINK>" + jiraNumber + "</TEXTWITHJIRALINK>";
        }
    }
}
