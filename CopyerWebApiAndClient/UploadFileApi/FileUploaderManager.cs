using MedwriteServiceInterface.DataModel.FileUpload;
using Rijndael;
using System;
using System.IO;

namespace MvcApplication1.BLL
{
    public static class Coyper
    {
        static string rootPath = string.Empty;
        static Coyper()
        {
            rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~/CopyedFile");
        }

        public static void CopyRequestData(UploadPacketRequest request)
        {
            CopyToFile(request);
        }

        public static void CopyRequestData(string text)
        {
            var request = Encrypter.Decrypt<UploadPacketRequest>(text);
            CopyToFile(request);
        }

        private static void CopyToFile(UploadPacketRequest request)
        {
            string userDirectory = rootPath + "\\";

            if (!Directory.Exists(userDirectory))
            {
                Directory.CreateDirectory(userDirectory);
            }

            var filePath = userDirectory + request.FileName;
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            {
                byte[] buffer = Convert.FromBase64String(request.Base64Content);
                stream.Write(buffer, 0, buffer.Length);
            }
        }

    }
}
