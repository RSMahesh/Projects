using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedwriteServiceInterface.DataModel.FileUpload
{
    [Serializable]
    public class UploadPacketRequest : BaseMessageRequest
    {
        public long PacketNumber { get; set; }
        public string Base64Content { get; set; }
     }
}
