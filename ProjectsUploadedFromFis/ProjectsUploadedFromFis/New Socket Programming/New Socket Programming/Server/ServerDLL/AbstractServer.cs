using System;
using System.Collections.Generic;
using System.Text;
using ServerDLL.CommandHandler;

namespace ServerDLL
{
    public abstract class AbstractServer : IDisposable 
    {
        private string port;
        internal AbstractCommandHandler commandHandler;
       
        protected AbstractServer(string port)
        {
            this.port = port;
        }

        protected string Port
        {
            get { return port; }
        }

        public abstract void Start();


        public virtual void Dispose()
        {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
