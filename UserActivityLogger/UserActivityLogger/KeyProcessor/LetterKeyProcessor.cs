using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class LetterKeyProcessor : SpecificKeysProcessor
    {
        public override bool CanProcess(string loggedKey)
        {
            loggedKey = RemoveShiftKeyIfExist(loggedKey);

            return loggedKey.Length ==1 && Char.IsLetter(loggedKey.FirstOrDefault());
        }

        public override string ProcessKey(string loggedKey)
        {
            bool shiftKeyPressed = ShiftKeyPressed(loggedKey);

            loggedKey = RemoveShiftKeyIfExist(loggedKey);

            if (CapsLock)
            {
                if (shiftKeyPressed)
                {
                    return Char.ToLowerInvariant(loggedKey[0]).ToString(); 
                }
                else
                {
                    return loggedKey[0].ToString();
                }
            }

            if(shiftKeyPressed)
            {
                return loggedKey[0].ToString();
            }

            return Char.ToLowerInvariant(loggedKey[0]).ToString();
        }
    }
}
