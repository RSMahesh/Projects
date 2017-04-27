using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedwriteServiceInterface.DataModel.FileUpload
{
 [Serializable]
    public abstract class BaseMessageRequest
    {
        public int FileUploadID { get; set; }
        public Guid  FileUploadGUID { get; set; }
        public string ClientFilePath { get; set; }
        public string FileName { get; set; }
        public string FileHash { get; set; }
        public int UserId { get; set; }
    }
}
