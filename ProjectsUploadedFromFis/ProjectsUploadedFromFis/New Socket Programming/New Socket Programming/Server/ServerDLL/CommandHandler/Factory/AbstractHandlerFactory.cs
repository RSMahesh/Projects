using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDLL.CommandHandler.Factory
{
    internal abstract class AbstractHandlerFactory
    {
        internal abstract AbstractCommandHandler GetKeyLoggerRequestHandler();
        internal abstract AbstractCommandHandler GetKeyBoardRequestHandler();
        internal abstract AbstractCommandHandler GetMouseRequestHandler();
        internal abstract AbstractCommandHandler GetScreenRequestHandler();
        internal abstract AbstractCommandHandler GetDefaultRequestHandler();
      
    }
}
