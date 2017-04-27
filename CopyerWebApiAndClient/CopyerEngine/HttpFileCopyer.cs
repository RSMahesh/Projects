using MedwriteServiceInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileUploaderClient.UploadMachine
{
    public class HttpFileCopyer : BaseFileCopyer
    {
        FileUploaderManager _fileUploaderManager;
       
        public override void CleanUp()
        {
           
        }
        public override void CopyBytes(byte[] data, int length)
        {
            var base64 = ProcessMessage(data, length);
             _fileUploaderManager.UploadFilePacket(0, _fileInfo.TargetFileName, base64, 0);
           // _fileUploaderManager.UploadFilePacketEncrypt(0, _fileInfo.TargetFileName, base64, 0);

        }
        public override void Init()
        {
            _fileUploaderManager = new FileUploaderManager(new Uri(ConfigurationManager.AppSettings["WebServiceBaseAddress"]));
        }

        string ProcessMessage(byte[] buffer, int validBytesCount)
        {
            string base64 = "";

            if (validBytesCount < buffer.Length)
            {
                byte[] newBuffer = new byte[validBytesCount];
                Buffer.BlockCopy(buffer, 0, newBuffer, 0, validBytesCount);
                base64 = Convert.ToBase64String(newBuffer);
            }
            else
            {
                base64 = Convert.ToBase64String(buffer);
            }

            return base64;
        }
    }
}
