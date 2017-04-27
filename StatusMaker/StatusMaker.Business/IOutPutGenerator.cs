using System.Collections.Concurrent;
using System.Collections.Generic;

namespace StatusMaker.Business
{
    public interface IOutPutGenerator
    {
        string GenerateSection(string heading, string content);

        string GenerateAhockSection(string heading, string content);

        string GenerateRow(Dictionary<string, string> tokens);

        string GeneratAdhockeRow(Dictionary<string, string> tokens);

        string GenerateMail(ConcurrentDictionary<string, string> tokens);

        string HighlightedText(string text);

        string HighlightedTextWithToolTip(string text, string toolTip);

        string HighlightedTextWithCrazyImage(string text);

        string IncorrectAndCorrect(string incorrect, string correct);

        string TextWithJiraLink(string jiraNumber);
    }
}