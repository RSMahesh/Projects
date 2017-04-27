using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserActivityLogger
{
    public class PunctuationKeyProcessor : SpecificKeysProcessor
    {

        static Dictionary<string, char> omeCharacterMapping = new Dictionary<string, char> {
                                                                     { "Oem4", '[' },
                                                                     { "Oem6", ']' },
                                                                     { "OemSemicolon", ';' },
                                                                     { "OemQuotes", '\'' },
                                                                     { "Oemcomma", ',' },
                                                                     { "OemPeriod", '.' },
                                                                     { "Oem2", '/' },
                                                                     { "SHFOem4", '{' },
                                                                     { "SHFOem6", '}' },
                                                                     { "SHFOemSemicolon", ':' },
                                                                     { "SHFOemQuotes", '"' },
                                                                     { "SHFOemcomma", '<' },
                                                                     { "SHFOemPeriod", '>' },
                                                                     { "SHFOem2", '?' },
                                                                     { "Oemtilde", '`' },
                                                                     { "SHFOemtilde", '~' },
                                                                     { "Oemplus", '=' },
                                                                     { "OemMinus", '-' },
                                                                     { "SHFOemplus", '+' },
                                                                     { "SHFOemMinus", '_' },
                                                                     { "OemPipe", '\\' },
                                                                     { "SHFOemPipe", '|' } };
        public override bool CanProcess(string loggedKey)
        {
            return omeCharacterMapping.ContainsKey(loggedKey);
        }

        public override string ProcessKey(string loggedKey)
        {
            return omeCharacterMapping[loggedKey].ToString();
        }
    }
}
