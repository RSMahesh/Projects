using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDLL.CommandHandler.TCP
{
    internal class MouseRequestHandler : AbstractCommandHandler
    {
        public override void HandleCommand(CommadMessage message)
        {
            string msg = GetRequestType(message.Message);
            switch (msg)
            {
                case "mouseclickleft":
                    MouseSimulater.MoveTo(new System.Drawing.Point(int.Parse(argumentList[0]), int.Parse(argumentList[1])));
                    MouseSimulater.Click_Left(int.Parse(argumentList[0]), int.Parse(argumentList[1]));
                    break;

                case "mouseclickright":
                    MouseSimulater.MoveTo(new System.Drawing.Point(int.Parse(argumentList[0]), int.Parse(argumentList[1])));
                    MouseSimulater.Click_Right(int.Parse(argumentList[0]), int.Parse(argumentList[1]));
                    break;
                default:
                    nextCommandHandler.HandleCommand(message);
                    break;
            }
            
        }

    }
}
