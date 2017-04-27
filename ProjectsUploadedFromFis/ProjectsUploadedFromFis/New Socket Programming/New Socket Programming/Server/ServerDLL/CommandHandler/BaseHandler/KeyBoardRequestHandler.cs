using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ServerDLL.CommandHandler.Base
{
   internal class KeyBoardRequestHandler : AbstractCommandHandler
    {
        public override void HandleCommand(CommadMessage message)
        {
            string msg = GetRequestType(message.Message);
            switch (msg)
            {
                case "keyup":
                    SendKeys.SendWait(argumentList[0]);
                    AcknowledgeClient(message);
                    break;
                default:
                    nextCommandHandler.HandleCommand(message);
                    break;
            }
            
        }

    }
}
