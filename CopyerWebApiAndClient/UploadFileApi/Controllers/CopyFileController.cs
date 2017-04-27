

using MedwriteServiceInterface.DataModel.FileUpload;
using MvcApplication1.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
   
    public class FileCopyerController : ApiController
    {
        public FileCopyerController()
        {
          
        }

        [HttpPost]
        [Route("fileCopyer")]
        public bool PostUploadFilePacket(UploadPacketRequest request)
        {
            Coyper.CopyRequestData(request);
            return true;
        }


        [HttpPost]
        [Route("fileCopyer/encrypted")]
        public bool PostUploadFilePacketEncrypted(object obj)
        {

            Coyper.CopyRequestData(obj.ToString());
            return true;
        }

    }
}