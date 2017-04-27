using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDLL.CommandHandler
{
    internal  abstract class AbstractCommandHandler
    {
        protected AbstractCommandHandler nextCommandHandler;
        protected  List<string> argumentList;
        public abstract void HandleCommand(CommadMessage message);
        protected const char splitChr = (char)3;
        public AbstractCommandHandler  SetNextHandler(AbstractCommandHandler commandHandler)
        {
            nextCommandHandler = commandHandler;
            return commandHandler;
        }
        protected string GetRequestType(string msg)
        {
            char[] arrCh = { '^' };
            string[] arrcommand = msg.Split(arrCh);
            argumentList = new List<string>();
            for (int indx = 1; indx < arrcommand.Length; indx++)
            {
                argumentList.Add(arrcommand[indx]);
            }
            return arrcommand[0];    

        }
        protected void AcknowledgeClient(CommadMessage message)
        {
            message.StreamWriter.BaseStream.Position = 0;
            string m = "Done" + splitChr + message.MessageID + "                ";
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
             if(m.IndexOf("mouse", StringComparison.InvariantCulture) > -1 ||m.IndexOf("img", StringComparison.InvariantCulture)> -1 ) 
             {
                 m= "error" + m;
             }
            message.StreamWriter.BaseStream.Write(enc.GetBytes(m), 0, m.Length);
            message.StreamWriter.BaseStream.Flush();
         }

    }

    }

