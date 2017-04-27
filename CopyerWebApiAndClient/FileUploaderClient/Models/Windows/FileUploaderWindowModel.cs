
using CopyerEngine;
using MedwriteServiceInterface;
using MedwriteServiceInterface.DataModel.FileUpload;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedwriteDesktopApp.Models.Windows
{
    public class FileUploaderWindowModel
    {
        int _userId;
        FileUploaderManager fileUploaderManager;

        public FileUploaderWindowModel(int userId)
        {
            //fileUploaderManager = _container.Resolve<IFileUploaderManager>();
            fileUploaderManager = new FileUploaderManager(new Uri(GetServiceBaseAddress()));
            _userId = userId;

        }
        string GetServiceBaseAddress()
        {
            return "http://localhost:52743/";
        }


        public PendingUploadsResponse GetPendingUploads(int userId)
        {
            return DBFileUploader.GetPendingUploads(userId);
 
        }

    }
}
