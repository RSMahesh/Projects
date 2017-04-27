using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileUploaderClient.UploadMachine
{
    public class FTPFileCopyer : BaseFileCopyer
    {
        Stream _ftpStream = null;
        public override void CleanUp()
        {
            _ftpStream.Dispose();
        }
        public override void CopyBytes(byte[] data, int length)
        {
            _ftpStream.Write(data, 0, length);
        }
        public override void Init()
        {
            string ftpRootUri = ConfigurationManager.AppSettings["FtpRootUrl"];

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpRootUri + "//" + _fileInfo.TargetFileName);
            request.Method = WebRequestMethods.Ftp.AppendFile;
            request.KeepAlive = true;
            request.UseBinary = true;
            request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FtpUserName"], ConfigurationManager.AppSettings["FtpUserPassword"]);
           
            _ftpStream = request.GetRequestStream();
        }


        void CreateDirectoryFtp(string directoryUri)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(directoryUri);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                //  request.KeepAlive = true;
                // request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                // This example assumes the FTP site uses anonymous logon.
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        // Directory not found.  
                    }
                }
            }


        }
    }
}
