using Core;
using EventPublisher;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class ActivityRepositary : IActivityRepositary
    {
        private IJarFile _jarFile;
        private IImageCommentEmbedder _imageCommentEmbedder;
        private readonly JarFileFactory _jarFileFactory;

        private string _dataFolder;

        public ActivityRepositary(JarFileFactory jarFileFactory, IImageCommentEmbedder imageCommentEmbedder, string dataFolder)
        {
            _jarFileFactory = jarFileFactory;
            _imageCommentEmbedder = imageCommentEmbedder;
            _dataFolder = dataFolder;
            CreateNewJarFile();
        }

        public void Add(Activity activity)
        {
            var tempFile = RuntimeHelper.MapToTempFolder(Guid.NewGuid().ToString() + ".jpg");

            //TODO: In memory instead saving file
            activity.ScreenShot.Save(tempFile, ImageFormat.Jpeg);

            _imageCommentEmbedder.AddComment(tempFile, activity.KeyPressedData);

            try
            {
                _jarFile.AddFile(tempFile);
            }
            catch (JarFileReachedMaxLimitException)
            {
                CreateNewJarFile();
                _jarFile.AddFile(tempFile);
            }

            File.Delete(tempFile);
        }

        public ActivityReader GetReader()
        {
            return new ActivityReader(_dataFolder, _jarFileFactory, null);
        }

        public ActivityReader GetReader(DateTime startDate, DateTime endDate)
        {
            return new ActivityReader(_dataFolder, _jarFileFactory, null);
        }

        private void CreateNewJarFile()
        {
            var userFullName = RuntimeHelper.GetCurrentUserName().ReverseMe();
            var logFilePath = Path.Combine(_dataFolder, userFullName) + "_" + Guid.NewGuid().ToString() + ".log";
            DisposeCurrentJarFile();
            _jarFile = _jarFileFactory.GetJarFile(FileAccessMode.Write, logFilePath);
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
