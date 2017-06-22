using Core;
using EventPublisher;
using FileSystem;
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
        private IJarFileWriter _jarFile;
        private IImageCommentEmbedder _imageCommentEmbedder;
        private readonly IJarFileFactory _jarFileFactory;

        private string _dataFolder;

        public ActivityRepositary(IJarFileFactory jarFileFactory, IImageCommentEmbedder imageCommentEmbedder, string dataFolder)
        {
            _jarFileFactory = jarFileFactory;
            _imageCommentEmbedder = imageCommentEmbedder;
            _dataFolder = dataFolder;

            HasWriteAccessToFolder();

            CreateNewJarFile();
        }

        public string DataFolder
        {
            get
            {
                return _dataFolder;
            }
        }

        public void Add(Activity activity)
        {
            var tempFile = RuntimeHelper.MapToTempFolder(Guid.NewGuid().ToString() + ".jpg");

            //TODO: In memory instead saving file
            activity.ScreenShot.Save(tempFile, ImageFormat.Jpeg);

            var headers = new Dictionary<string, string>();
            headers["FileName"] = tempFile;


            JarFileItem item = new JarFileItem(headers, tempFile);

            _imageCommentEmbedder.AddComment(tempFile, activity.KeyPressedData);

            try
            {
                _jarFile.AddFile(item);
            }
            catch (JarFileReachedMaxLimitException)
            {
                CreateNewJarFile();
                _jarFile.AddFile(item);
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
            var logFilePath = Path.Combine(_dataFolder, userFullName) + "_" + Guid.NewGuid().ToString() + "." + Constants.JarFileExtension;
            DisposeCurrentJarFile();
            _jarFile = _jarFileFactory.GetJarFileWriter(logFilePath);
        }

        private void DisposeCurrentJarFile()
        {
            if (_jarFile != null)
            {
                _jarFile.Dispose();
            }
        }

        private void HasWriteAccessToFolder()
        {
            try
            {
                var ds = Directory.GetAccessControl(_dataFolder);

                var testFile = Path.Combine(_dataFolder, "test.temp");

                File.WriteAllText(Path.Combine(_dataFolder, "test.temp"), "test");

                File.Delete(testFile);
            }
            catch (Exception ex)
            {
                _dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                _dataFolder = Path.Combine(_dataFolder, "SysLogs");
            }
        }
    }
}
