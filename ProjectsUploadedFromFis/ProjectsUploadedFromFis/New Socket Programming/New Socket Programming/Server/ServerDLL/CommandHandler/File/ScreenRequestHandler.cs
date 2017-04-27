using System;
using System.Collections.Generic;
using System.Text;
using SI = System.IO;
using System.Drawing.Imaging;


namespace ServerDLL.CommandHandler.File
{
    internal class ScreenRequestHandler : AbstractCommandHandler
    {

        //  SI.StreamWriter trace;
        public ScreenRequestHandler()
        {
            //trace = new System.IO.StreamWriter(@"C:\ServerTrace.txt");
        }

        public override void HandleCommand(CommadMessage message)
        {
            string msg = GetRequestType(message.Message);
            if (msg == "img")
            {
                try
                {
                    if (SI.File.Exists(message.Port + @"\Screen.jpeg"))
                    {
                        SI.File.Delete(message.Port + @"\Screen" + Guid.NewGuid().ToString() + ".jpeg");
                    }

                    Utility.GetScreenImageAsImage().Save(message.Port + @"\Screen.jpeg", ImageFormat.Jpeg);
                }
                catch{ }
                finally {AcknowledgeClient(message);}
            }
            else
            {
                nextCommandHandler.HandleCommand(message);
            }

        }
    }
}
