using System;
namespace ServerDLL
{
    interface IServer
    {
        void Dispose();
        void Start(ServerType serverType, string port);
    }
}
