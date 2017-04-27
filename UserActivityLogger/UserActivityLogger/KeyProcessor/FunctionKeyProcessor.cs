using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserActivityLogger
{
    public class FunctionKeyProcessor : SpecificKeysProcessor
    {
        public override bool CanProcess(string loggedKey)
        {
            loggedKey = RemoveShiftKeyIfExist(loggedKey);

            return IsFunctionKey(loggedKey);
        }

        public override string ProcessKey(string loggedKey)
        {
            return NoValue;
        }

        private bool IsFunctionKey(string loggedKey)
        {
            try
            {
                var key = (int)(Keys)Enum.Parse(typeof(Keys), loggedKey);

                return key >= 112 && key <= 135;
            }
            catch
            {

            }

            return false;
        }
    }
}
