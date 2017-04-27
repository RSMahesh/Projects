using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
     class CapsLockKeyProcessor : SpecificKeysProcessor
    {
        public override bool CanProcess(string loggedKey)
        {
            return "CapsLock" == loggedKey;
        }
        public override string ProcessKey(string loggedKey)
        {
            CapsLock = !CapsLock;
            return NoValue;
        }
    }
}
