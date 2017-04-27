using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using ServerDLL.CommandHandler.Factory;
using ServerDLL.CommandHandler;
using System.IO;

namespace ServerDLL
{
    public static class SeverFactory
    {

        public static string GetMachineIP()
        {
            IPAddress[] a = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            return a[0].ToString();
        }


        public static AbstractServer GetServer(ServerType serverType, string port)
        {
            AbstractServer server = null;
            AbstractHandlerFactory handlerFactory = null;
            AbstractCommandHandler handler = null;

            switch (serverType)
            {
                case ServerType.TCPServer:
                    server = new TCPServer(port);
                    handlerFactory = new TCPHandlerFactory();
                    break;
                case ServerType.FileServer:
                    port = GetFilserverPort(port);
                    server = new FileServer(300, port);
                    handlerFactory = new FileHandlerFactory();
                    break;
                case ServerType.SyncFileServer:
                    port = GetFilserverPort(port);
                    server = new SyncFileServer(300, port);
                    handlerFactory = new FileHandlerFactory();
                    break;
            }

            server.commandHandler = handlerFactory.GetKeyBoardRequestHandler();
            handler = server.commandHandler.SetNextHandler(handlerFactory.GetMouseRequestHandler());
            handler = handler.SetNextHandler(handlerFactory.GetKeyLoggerRequestHandler());
            handler = handler.SetNextHandler(handlerFactory.GetScreenRequestHandler());
            handler.SetNextHandler(handlerFactory.GetDefaultRequestHandler());
            return server;
        }

        private static string GetFilserverPort(string port)
        {
            port += @"\" + GetMachineIP();
            if (!Directory.Exists(port))
                Directory.CreateDirectory(port);
            return port;
        }
    }

    public enum ServerType
    {
        TCPServer,
        FileServer,
        SyncFileServer,
        HttpServer
    }
}
