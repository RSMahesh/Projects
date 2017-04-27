using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedwriteServiceInterface.DataModel.FileUpload
{
    [Serializable]
   public class StartFileUploadResponse: BaseMessageRequest
    {
        public long StartingIndex { get; set; }
        public bool AlredayUploded { get; set; }
        public bool UploadedCompleted { get; set; }
        public string UploadMethod { get; set; }
    }

   
}
