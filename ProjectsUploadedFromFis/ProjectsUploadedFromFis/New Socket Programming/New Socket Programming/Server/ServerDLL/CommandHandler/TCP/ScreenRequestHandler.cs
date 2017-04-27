using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDLL.CommandHandler.TCP
{
    internal class ScreenRequestHandler : AbstractCommandHandler
    {
        public override void HandleCommand(CommadMessage message)
        {
            string msg = GetRequestType(message.Message);
            if (msg == "img")
            {
                byte[] buffer = Utility.GetScreenImageAsBytes();
                message.StreamWriter.BaseStream.Write(buffer, 0, buffer.Length - 1);
            }
            else
            {
                nextCommandHandler.HandleCommand(message);
                message.StreamWriter.WriteLine("Done");
          
            }
        }
    }
}
