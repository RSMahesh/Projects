using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ServerDLL.CommandHandler
{
    public class CommadMessage
    {
        private string message;
        private StreamWriter streamWriter;
        private string port;
        private string messageID;

        public CommadMessage(string message, StreamWriter streamWriter)
        {
            this.message = message;
            this.streamWriter = streamWriter;
        }

        public CommadMessage(string message, StreamWriter streamWriter,string port,string messageID)
        {
            this.message = message;
            this.streamWriter = streamWriter;
            this.messageID = messageID;
            this.port = port;
        }

        public CommadMessage(string message, StreamWriter streamWriter,string port)
        {
            this.message = message;
            this.streamWriter = streamWriter;
            this.port = port;
        }


        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public StreamWriter StreamWriter
        {
            get { return streamWriter; }
            set { streamWriter = value; }
        }

        public string Port
        {
            get { return port; }
            set { port = value; }
        }

        public string MessageID
        {
            get { return messageID; }
            set { messageID = value; }
        }

    }
}
