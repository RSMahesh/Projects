using System;
using System.IO;
using System.Text;
using WebServer.Interface;

namespace HttpHandlers
{
    public class PostRequestHttpHandler : IHttpHandler
    {
        string _webServerRootDir;
        public PostRequestHttpHandler(string webServerRootDir)
        {
            _webServerRootDir = webServerRootDir;
        }

        public byte[] ProcessRequest(System.Net.HttpListenerRequest request)
        {
            var body = GetBody(request);
            var fileName = request.Headers["filename"];
            var filePath = Path.Combine(_webServerRootDir, fileName);

            File.WriteAllText(filePath, body);
         
            return Encoding.ASCII.GetBytes("Saved");
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
