using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Security.Authentication.ExtendedProtection;
using System.Threading;
using System.Linq;
using System.Text;

using HttpHandlers;

using WebServer;
using WebServer.Interface;
namespace SimpleWebServer
{
    public class WebServer : IWebServer
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly IHttpHandler _httpHandler;
        Encoding enc = new UTF8Encoding(true, true);
        private readonly string _webSitePhysicalPath;
        public WebServer(string[] prefixes, string webSitePhysicalPath)
        {
            _webSitePhysicalPath = webSitePhysicalPath;
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Needs Windows XP SP2, Server 2003 or later.");

            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

     
            foreach (string s in prefixes)
                _listener.Prefixes.Add(s);

            _listener.Start();
        }


        public void StartServer()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Console.WriteLine("Webserver running...");
                try
                {
                    StartListening();
                }
                catch { }
            });

        }


        private void StartListening()
        {
            while (_listener.IsListening)
            {
                ThreadPool.QueueUserWorkItem((c) =>
                {
                    var ctx = c as HttpListenerContext;
                    try
                    {

                        byte[] buf = GetHttpHandler(ctx.Request).ProcessRequest(ctx.Request);
                        ctx.Response.ContentType = MimeType.GetMimeType(Path.GetExtension(ctx.Request.Url.AbsolutePath));
                        ctx.Response.ContentLength64 = buf.Length;
                        ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                    }
                    catch (Exception ex)
                    {
                        ctx.Response.StatusCode = 500;
                        byte[] buf = enc.GetBytes("<HTML><BODY>" + ex.ToString() + "</BODY></HTML>");
                        ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                        Console.Write(ex.ToString());
                    }
                    finally
                    {

                        ctx.Response.OutputStream.Close();
                    }
                }, _listener.GetContext());
            }
        }

         IHttpHandler GetHttpHandler(HttpListenerRequest request)
        {
            if (request.QueryString["exe"] != null)
            {
                return new ExecuteExeHttpHandler();
            }

            return new HttpHandlers.StaticFileHttpHandler(_webSitePhysicalPath);
        }

        public void StopServer()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}