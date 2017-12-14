using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Interface;

namespace HttpHandlers
{
    /// <summary>
    /// Executes exe defenied in parmaters
    /// </summary>
    public class ExecuteExeHttpHandler : IHttpHandler
    {
        public byte[] ProcessRequest(System.Net.HttpListenerRequest request)
        {
            if (request.QueryString["runexe"] == null) return null;
            return Encoding.ASCII.GetBytes(ExeRunner.Execute(request.QueryString["runexe"], ""));
        }
    }
}
