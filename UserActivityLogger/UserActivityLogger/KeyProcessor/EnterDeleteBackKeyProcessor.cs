using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class EnterDeleteBackKeyProcessor : SpecificKeysProcessor
    {
        public override bool CanProcess(string loggedKey)
        {
            return "Enter" == loggedKey;
        }

        public override string ProcessKey(string loggedKey)
        {
            return Environment.NewLine;
        }
    }
}
