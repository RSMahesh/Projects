using MedwriteServiceInterface.DataModel.FileUpload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyerEngine
{
    public class DBFileUploader
    {
        static List<UploadedFileInfo> uploadFiles = new List<UploadedFileInfo>();
        static string dbFile;
        static object objlock;

        static DBFileUploader()
        {
            dbFile = "C:\\UploadFileInfoNew.txt";
            objlock = new object();

            if (File.Exists(dbFile))
            {
                var con = File.ReadAllText(dbFile);
                uploadFiles = (List<UploadedFileInfo>)new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(con, typeof(List<UploadedFileInfo>));
            }

        }

        public static UploadedFileInfo GetUploadedFileInfo(string fileName, int userId, string fileHash)
        {
            return uploadFiles.Find(f => f.ClientFilePath == fileName && f.UserId == userId);
        }

        public static List<UploadedFileInfo> GetPendingUploadsold(int userId)
        {
            return uploadFiles.FindAll(f => f.UserId == userId && f.UploadedCompleted == false);
        }

        public static PendingUploadsResponse GetPendingUploads(int userId)
        {

            PendingUploadsResponse reponse = new PendingUploadsResponse();
            List<PendingUpload> listPendingUpload = new List<PendingUpload>();
            var listUserUploadedFile = uploadFiles.FindAll(f => f.UserId == userId && f.UploadedCompleted == false);

            if (listUserUploadedFile != null && listUserUploadedFile.Count > 0)
            {
                foreach (var userUploadedFile in listUserUploadedFile)
                {
                    PendingUpload pending = new PendingUpload()
                    {
                        ClientFilePath = userUploadedFile.ClientFilePath,
                        FileHash = userUploadedFile.FileHash,
                        FileName = userUploadedFile.FileName,
                        FileUploadGUID = userUploadedFile.ID,
                        UserId = userUploadedFile.UserId,
                        UploadMethod = userUploadedFile.UploadMethod
                    };

                    listPendingUpload.Add(pending);
                }
            }

            reponse.PendingUploads = listPendingUpload;
            return reponse;
        }
        public static void SaveFileInfo(string fileName, int userId, string fileHash, long savedBytes, bool uploadCompleted)
        {
            var result = GetUploadedFileInfo(fileName, userId, fileHash);
            if (result == null)
            {
                result = new UploadedFileInfo();
                lock (objlock)
                {
                    uploadFiles.Add(result);
                }
            }
            result.UserId = userId;
            result.FileName = fileName;
            result.LastUpdate = DateTime.Now;
            result.SavedBytes = savedBytes;
            result.UploadedCompleted = uploadCompleted;
            Save();

        }
        public static void Save()
        {
            lock (objlock)
            {
                string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(uploadFiles);
                File.WriteAllText(dbFile, json);
            }
        }
        public static StartFileUploadResponse StartFileUpload(string file, int userId, string fileHash, string uploadMethod)
        {

            var result = GetUploadedFileInfo(file, userId, fileHash);
            var alreadyUploaded = true;

            if (result == null)
            {
                alreadyUploaded = false;
                result = new UploadedFileInfo();
                result.ID = Guid.NewGuid();
                result.UserId = userId;
                result.FileName = Path.GetFileName(file);
                result.ClientFilePath = file;
                result.LastUpdate = DateTime.Now;
                result.SavedBytes = 0;
                result.UploadedCompleted = false;
                result.UploadMethod = uploadMethod;
                lock (objlock)
                {
                    uploadFiles.Add(result);
                }
                Save();
            }


            StartFileUploadResponse response = new StartFileUploadResponse()
            {
                ClientFilePath = result.ClientFilePath,
                FileHash = result.FileHash,
                FileName = result.FileName,
                FileUploadGUID = result.ID,
                UserId = result.UserId,
                StartingIndex = result.SavedBytes,
                AlredayUploded = alreadyUploaded,
                UploadMethod = result.UploadMethod,
                UploadedCompleted = result.UploadedCompleted
            };

            return response;
        }
        public static void UpdateUploadInfo(Guid uploaGUID, long savedBytes)
        {
            var uploadInfo = uploadFiles.FirstOrDefault(f => f.ID == uploaGUID);
            uploadInfo.SavedBytes = savedBytes;
            Save();
        }
        public static void EndUpload(Guid uploaGUID)
        {
            var uploadInfo = uploadFiles.FirstOrDefault(f => f.ID == uploaGUID);
            uploadInfo.UploadedCompleted = true;
            Save();
        }
    }

    public class UploadedFileInfo
    {
        public Guid ID { get; set; }
        public string ClientFilePath { get; set; }
        public string FileName { get; set; }
        public string FileHash { get; set; }
        public int UserId { get; set; }
        public DateTime LastUpdate { get; set; }
        public long SavedBytes { get; set; }
        public bool UploadedCompleted { get; set; }
        public string UploadMethod { get; set; }
    }
}
