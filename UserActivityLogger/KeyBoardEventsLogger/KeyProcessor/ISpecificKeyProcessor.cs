using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserActivityLogger
{
    public abstract class SpecificKeysProcessor
    {
        const string SHIFT = "SHF";

        protected static bool CapsLock;
        public abstract bool CanProcess(string loggedKey);
        public abstract string ProcessKey(string loggedKey);




        protected string RemoveShiftKeyIfExist(string loggedKey)
        {
            return loggedKey.Replace(SHIFT, "");
        }

        protected bool ShiftKeyPressed(string loggedKey)
        {
            return loggedKey.StartsWith(SHIFT);
        }

        protected string NoValue
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
