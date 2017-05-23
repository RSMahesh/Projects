using Core;
using EventPublisher;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserActivityLogger;

namespace UserActivityLogger
{
    public  class ActivitySaver 
    {
        private const int MaxFileCount = 100;
        private readonly string _logFolder;
        private readonly IFileAppender _fileAppender;
        private readonly IImageCommentEmbedder _imageCommentEmbedder;
        private string _logFilePath;

        public ActivitySaver(string logFolder, IFileAppender fileAppender, IImageCommentEmbedder imageCommentEmbedder)
        {
            _logFolder = logFolder;
            _fileAppender = fileAppender;
            _imageCommentEmbedder =  imageCommentEmbedder;

            if (!Directory.Exists(_logFolder))
            {
                Directory.CreateDirectory(_logFolder);
            }

            CreateNewLogFile();
        }
        public void Save(Activity activity)
        {
            var captureImgpath = Path.Combine(_logFolder, Guid.NewGuid().ToString() + ".jpg");
    
            //TODO: In memory instead saving file
            activity.ScreenShot.Save(captureImgpath, ImageFormat.Jpeg);

            _imageCommentEmbedder.AddComment(captureImgpath, activity.KeyPressedData);

            var dataFile = GetDataFileName();

            _fileAppender.AppendFile(captureImgpath, dataFile);

            File.Delete(captureImgpath);
            
        }

        private string GetDataFileName()
        {
            if (_fileAppender.GetFileCount(_logFilePath) >= MaxFileCount)
            {
                EventContainer.PublishEvent(Events.LogFileReachedMaxLimit.ToString(), new EventArg(Guid.Empty, _logFilePath));
                CreateNewLogFile();
            }

            return _logFilePath;
        }

        private void CreateNewLogFile()
        {
            var userFullName = RuntimeHelper.GetCurrentUserName().ReverseMe();
            _logFilePath = Path.Combine(_logFolder, userFullName) + "_" + Guid.NewGuid().ToString() + ".log";
        }
    }
}
