
using MedwriteServiceInterface.DataModel.FileUpload;
using Rijndael;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MedwriteServiceInterface
{

   public class FileUploaderManager 
    {

        #region Private Members

        HttpClient client = new HttpClient();

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="webServerUri"></param>
        public FileUploaderManager(Uri webServerUri)
        {
            client.BaseAddress = webServerUri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileUploadID"></param>
        /// <param name="uploadFileName"></param>
        /// <param name="contentBase64"></param>
        /// <param name="userId"></param>
        public void UploadFilePacket(int fileUploadID, string uploadFileName, string contentBase64, int userId)
        {

            string requestUri = string.Format("fileCopyer");
 
            UploadPacketRequest uploadPacketReq = new UploadPacketRequest();
            uploadPacketReq.FileName = uploadFileName;
            uploadPacketReq.Base64Content = contentBase64;
            uploadPacketReq.FileUploadID = fileUploadID;
            uploadPacketReq.UserId = userId;
            var response = client.PostAsJsonAsync(requestUri, uploadPacketReq).Result;
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var meeting = response.Content.ReadAsAsync<bool>().Result;

        }


        public void UploadFilePacketEncrypt(int fileUploadID, string uploadFileName, string contentBase64, int userId)
        {

            string requestUri = string.Format("fileCopyer/encrypted");
            UploadPacketRequest uploadPacketReq = new UploadPacketRequest();
            uploadPacketReq.FileName = uploadFileName;
            uploadPacketReq.Base64Content = contentBase64;
            uploadPacketReq.FileUploadID = fileUploadID;
            uploadPacketReq.UserId = userId;

            var request = Encrypter.Encrypt<UploadPacketRequest>(uploadPacketReq);

            var response = client.PostAsJsonAsync(requestUri, request).Result;
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var meeting = response.Content.ReadAsAsync<bool>().Result;

        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="userId"></param>
        /// <param name="fileHash"></param>
        /// <returns></returns>
        public object  StartFileUpload(string file, int userId, string fileHash)
        {
            string requestUri = string.Format("api/FileUpload/PostStartFileUpload");
         //   TokenValidation.AssignToken(requestUri, ref client);

            StartFileUploadRequest startFileUploadRequest = new StartFileUploadRequest();
            startFileUploadRequest.FileName = Path.GetFileName(file);
            startFileUploadRequest.ClientFilePath = file;
            startFileUploadRequest.UserId = userId;
            startFileUploadRequest.FileHash = fileHash;
            var response = client.PostAsJsonAsync(requestUri, startFileUploadRequest).Result;
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var startFileUploadResponse = response.Content.ReadAsAsync<StartFileUploadResponse>().Result;
            return startFileUploadResponse;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileUploadID"></param>
        /// <param name="fileName"></param>
        /// <param name="userId"></param>
        /// <param name="fileHash"></param>
        /// <returns></returns>
        public bool EndFileUpload(int fileUploadID, string fileName, int userId, string fileHash)
        {
            string requestUri = string.Format("api/FileUpload/PostEndFileUpload");
           // TokenValidation.AssignToken(requestUri, ref client);

            EndFileUploadRequest endFileUploadRequest = new EndFileUploadRequest();
            endFileUploadRequest.FileUploadID = fileUploadID;
            endFileUploadRequest.FileName = fileName;
            endFileUploadRequest.UserId = userId;
            endFileUploadRequest.FileHash = fileHash;
            var response = client.PostAsJsonAsync(requestUri, endFileUploadRequest).Result;
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var result = response.Content.ReadAsAsync<bool>().Result;
            return result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileUploadID"></param>
        /// <param name="fileName"></param>
        /// <param name="userId"></param>
        /// <param name="fileHash"></param>
        /// <returns></returns>
        public bool RenameUploadedFile(int fileUploadID, string fileName, int userId, string fileHash)
        {
            string requestUri = string.Format("api/FileUpload/PostRenameUploadedFile");
           // TokenValidation.AssignToken(requestUri, ref client);

            StartFileUploadRequest startFileUploadRequest = new StartFileUploadRequest();
            startFileUploadRequest.FileName = fileName;
            startFileUploadRequest.FileUploadID = fileUploadID;
            startFileUploadRequest.UserId = userId;
            startFileUploadRequest.FileHash = fileHash;
            var response = client.PostAsJsonAsync(requestUri, startFileUploadRequest).Result;
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var result = response.Content.ReadAsAsync<bool>().Result;
            return result;

        }

        #endregion



        public object GetPendingUploads(int userId)
        {
            string requestUri = string.Format("api/FileUpload/PostGetPendingUploads");
           // TokenValidation.AssignToken(requestUri, ref client);

            PendingUploadsRequest requset = new PendingUploadsRequest();
            requset.UserId = userId;
            var response = client.PostAsJsonAsync(requestUri, requset).Result;
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var pendingResponse = response.Content.ReadAsAsync<PendingUploadsResponse>().Result;
            return pendingResponse;
        }
    }
}
