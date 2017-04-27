using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Interface;

namespace HttpHandlers
{
    public class ExecuteExeHttpHandler : IHttpHandler
    {
        public ExecuteExeHttpHandler()
        {
         
        }

        public byte[] ProcessRequest(System.Net.HttpListenerRequest request)
        {
            if (request.QueryString["exe"] == null) return null;
            var qq = request.QueryString["exe"];
            return Encoding.ASCII.GetBytes(ExeRunner.Execute(qq, ""));
        }
    }
}
