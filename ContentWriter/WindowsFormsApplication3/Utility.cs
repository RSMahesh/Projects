using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    public class Utility
    {
        public static string  CountWordsInString(string content, IEnumerable<string> excludeWords)
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

            foreach(var key in words.Keys)
            {
                if( !excludeWords.Contains(key) &&   words[key] > 1)
                   
                wordsCountInformation += key + " " + words[key] + "" + Environment.NewLine;
            }


            return wordsCountInformation;
        }

        private static string GetCountString(int count)
        {
            var output = "";
            for(var i=1; i <= count; i++)
            {
                output += i.ToString() + " ";
            }

            return output;
        }
    }
}
