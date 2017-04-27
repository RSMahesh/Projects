using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserActivityLogger
{
    public class NumberKeyProcessor : SpecificKeysProcessor
    {
       static Dictionary<string, char> ShiftCharacterMapping = new Dictionary<string, char> {
            { "D1", '!' },
            { "D2", '@' },
            { "D3", '#' },
            { "D4", '$' },
            { "D5", '%' },
            { "D6", '^' },
            { "D7", '&' },
            { "D8", '*' },
            { "D9", '(' },
            { "D0", ')' }
        };
        public override bool CanProcess(string loggedKey)
        {
            loggedKey = RemoveShiftKeyIfExist(loggedKey);

            return ShiftCharacterMapping.ContainsKey(loggedKey);
        }

        public override string ProcessKey(string loggedKey)
        {
            if (ShiftKeyPressed(loggedKey))
            {
                loggedKey = RemoveShiftKeyIfExist(loggedKey);

                return ShiftCharacterMapping[loggedKey].ToString();
            }

            return   loggedKey.LastOrDefault().ToString();
        }

    }
}
