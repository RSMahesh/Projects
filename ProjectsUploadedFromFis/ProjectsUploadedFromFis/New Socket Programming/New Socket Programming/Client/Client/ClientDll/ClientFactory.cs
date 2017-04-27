using System;
using System.Collections.Generic;
using System.Text;


namespace ClientDll
{
   public  class ClientFactory
    {
   
       public static AbstractClient GetClient(ClientType clientType,string port)
       {
           AbstractClient client = null;
    
           switch (clientType)
           {
               case ClientType.TCPClient:
                   client = new TCPClient(port);
                   break;
               case ClientType.FileClient:
                   client = new FileClient(port);
                   break;
           }
           return client;
       }
    }

    public enum ClientType
    {
        TCPClient,
        FileClient,
        HttpClient
    }
}
