using CopyerEngine;
using FileUploaderClient;
using FileUploaderClient.UploadMachine;
using MedwriteServiceInterface;
using MedwriteServiceInterface.DataModel.FileUpload;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;

using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedwriteDesktopApp.Models.Windows
{
    public class FileUploaderControlModel : IUploadProgressCommunicator
    {
        string _file;
        int _bufferLength = 10 * 1024;
        long _startingIndex;
        int _userId;
        int fileUploadId;
        Guid fileUploadGUID;
        double fileSizeInMB;
        bool resume = false;
        string _uploadMethod = "";
        public bool Running = false;


        public Action OnUploadCompleted;

        private Action ReadFileAndUploadFileAction;

        BaseFileCopyer _fileCopyer;

        Action<int, string> _ProgressNotification;

        BackgroundWorker worker = new BackgroundWorker();

        FileUploaderManager fileUploaderManager;

        public bool Pause { get; set; }

        public void FileUploadSoFar(long bytes)
        {
            var uploadedSoFarMsg = string.Format("{0} MB of {1} MB", ConvertBytesToMegabytes(bytes).ToString(), fileSizeInMB.ToString());
            worker.ReportProgress(1, uploadedSoFarMsg);
        }

        public void UploadCompleted()
        {
            Running = false;
            DBFileUploader.EndUpload(fileUploadGUID);
            OnUploadCompleted();
        }
        public FileUploaderControlModel(int userId, string file, Action<int, string> ProgressNotification, string uploadMethod)
        {
            this._file = file;
            this._bufferLength = GetChunkSize();
            _uploadMethod = uploadMethod;
            _userId = userId;
            _ProgressNotification = ProgressNotification;

            //fileUploaderManager = _container.Resolve<IFileUploaderManager>();
            fileUploaderManager = new FileUploaderManager(new Uri(GetServiceBaseAddress()));
            worker.DoWork += worker_DoWork;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerSupportsCancellation = true;
        }

        double ConvertBytesToMegabytes(long bytes)
        {
            return Math.Round((bytes / 1024f) / 1024f, 2);
        }

        int GetChunkSize()
        {
            int chunkSize = 10 * 1024;
            if (ConfigurationManager.AppSettings["FileUploadChunkSizeInKB"] != null && ConfigurationManager.AppSettings["FileUploadChunkSizeInKB"] != "")
            {
                chunkSize = int.Parse(ConfigurationManager.AppSettings["FileUploadChunkSizeInKB"]) * 1024;
            }
            return chunkSize;
        }

        public void StartUpload()
        {
            worker.RunWorkerAsync();
        }

        string GetServiceBaseAddress()
        {
            return ConfigurationManager.AppSettings["WebServiceBaseAddress"];
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _ProgressNotification(e.ProgressPercentage, (string)e.UserState);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Running = true;
            if (!resume)
                if (!InitFileUploadOnServerExt())
                    return;

            _fileCopyer.UploadFile(GetFileInfo(), this);

        }

        string GetMD5HashFromFile(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        public int GetProgressLength()
        {
            FileInfo f = new FileInfo(_file);
            fileSizeInMB = ConvertBytesToMegabytes(f.Length);
            return (int)f.Length / _bufferLength;
        }

        public void CancelUpload()
        {
            worker.CancelAsync();
            Pause = true;
            Running = false;

        }
        public void ResumeUpload()
        {
            resume = true;
            Pause = false;
            Running = true;
            worker.RunWorkerAsync();
        }
        public bool InitFileUploadOnServerExt()
        {
            var uploadFileName = Path.GetFileName(_file);
            StartFileUploadResponse startFileUploadResponse = (StartFileUploadResponse)DBFileUploader.StartFileUpload(_file, _userId, "", _uploadMethod);
            bool rtn = true;

            if(startFileUploadResponse.UploadedCompleted)
            {
                MessageBox.Show("Already Copyed File: " + Path.GetFileName(_file) + " using " + startFileUploadResponse.UploadMethod.ToString());
                return false;
            }

            if (startFileUploadResponse.AlredayUploded)
            {
                _startingIndex = startFileUploadResponse.StartingIndex;
                var uploadedSoFarMsg = string.Format("{0} MB of {1} MB", ConvertBytesToMegabytes(_startingIndex).ToString(), fileSizeInMB.ToString());
                worker.ReportProgress((int)(_startingIndex / _bufferLength), uploadedSoFarMsg);
            }
            else
            {
                startFileUploadResponse.UploadMethod = _uploadMethod;
            }

            fileUploadId = startFileUploadResponse.FileUploadID;
            fileUploadGUID = startFileUploadResponse.FileUploadGUID;
            var uploadMethod = startFileUploadResponse.UploadMethod;
            _fileCopyer = GetFileCopyer(uploadMethod);

            return rtn;
        }

        private BaseFileCopyer GetFileCopyer(string uploadMethod)
        {
            BaseFileCopyer fileCopyer = null;
            switch (uploadMethod)
            {
                case "HTTP":
                    fileCopyer = new HttpFileCopyer();
                    break;
                case "FTP":
                    fileCopyer = new FTPFileCopyer();
                    break;
                case "VNC":
                    fileCopyer = new VNCFileCopyer();
                    break;
                default:
                    throw new Exception("Invalid Copyer :" + uploadMethod);
            }

            return fileCopyer;
        }
        private UploadFileInformation GetFileInfo()
        {
            UploadFileInformation fileInfo = new UploadFileInformation();
            fileInfo.SourceFilePath = _file;
            fileInfo.TargetFileName = Path.GetFileName(_file);
            fileInfo.ChunkSizeInBytes = _bufferLength;
            fileInfo.StartingIndex = _startingIndex;

            return fileInfo;
        }

    }
  
}
