using System;
using System.IO;
using System.Text;
using WebServer.Interface;

namespace HttpHandlers
{
    public class OptionsRequestHttpHandler : IHttpHandler
    {
        string _webServerRootDir;
        public OptionsRequestHttpHandler(string webServerRootDir)
        {
            _webServerRootDir = webServerRootDir;
        }

        public byte[] ProcessRequest(System.Net.HttpListenerRequest request)
        {
          
         
            return Encoding.ASCII.GetBytes(" ");
        }

        private string GetBody(System.Net.HttpListenerRequest  Request)
        {
            string documentContents;
            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Request.ContentEncoding))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            return documentContents;
        }
    }
}
