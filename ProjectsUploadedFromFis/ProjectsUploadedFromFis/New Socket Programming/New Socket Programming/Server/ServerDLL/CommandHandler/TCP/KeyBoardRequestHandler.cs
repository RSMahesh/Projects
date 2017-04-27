using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace ServerDLL.CommandHandler.TCP
{
   internal class KeyBoardRequestHandler :ServerDLL.CommandHandler.Base.KeyBoardRequestHandler 
    {
        public new void HandleCommand(CommadMessage message)
        {
            base.HandleCommand(message);    
        }

    }
}
