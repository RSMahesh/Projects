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
    public class VNCFileCopyer : BaseFileCopyer
    {
        FileStream _vncStream = null;
        public override void CleanUp()
        {
            _vncStream.Dispose();
        }
        public override void CopyBytes(byte[] data, int length)
        {
            _vncStream.Write(data, 0, length);
        }
        public override void Init()
        {
            var targetFilePath = ConfigurationManager.AppSettings["VncLocation"] + _fileInfo.TargetFileName;

            _vncStream = new FileStream(targetFilePath, FileMode.Append);
        }
    }
}
