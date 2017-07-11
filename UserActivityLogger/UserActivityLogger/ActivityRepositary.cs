using Core;
using EventPublisher;
using FileSystem;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;

namespace UserActivityLogger
{
    public class ActivityRepositary : IActivityRepositary
    {
        private IJarFileWriter _jarFileWriter;
        private IImageCommentEmbedder _imageCommentEmbedder;
        private readonly IJarFileFactory _jarFileFactory;

        private string _dataFolder;

        //TODO: one consturctor only inject only dependencies  
        public ActivityRepositary(IJarFileFactory jarFileFactory, IImageCommentEmbedder imageCommentEmbedder, string dataFolder)
        {
            _jarFileFactory = jarFileFactory;
            _imageCommentEmbedder = imageCommentEmbedder;
            _dataFolder = dataFolder;

            HasWriteAccessToFolder();

            CreateNewJarFile();
        }

        //This constructor for reading (waty too bad)
        public ActivityRepositary(IJarFileFactory jarFileFactory, IImageCommentEmbedder imageCommentEmbedder)
        {
            _jarFileFactory = jarFileFactory;
            _imageCommentEmbedder = imageCommentEmbedder;
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
                _jarFileWriter.AddFile(item);
            }
            catch (JarFileReachedMaxLimitException)
            {
                //TO DO: move this event to appropiate class or remove
                EventContainer.PublishEvent(Events.LogFileReachedMaxLimit.ToString(), new EventArg(Guid.NewGuid(), _jarFileWriter.JarFilePath));

                CreateNewJarFile();
                _jarFileWriter.AddFile(item);
            }

            File.Delete(tempFile);
        }

        //public ActivityReader GetReader()
        //{
        //    return new ActivityReader(_dataFolder, _jarFileFactory, null);
        //}

        public ActivityReader GetReader(IEnumerable<string> files)
        {
            return new ActivityReader(files, _jarFileFactory, null);
        }

  
        private void CreateNewJarFile()
        {
            var ipUser = IPAddress.GetCurrentMachineIp() + RuntimeHelper.GetCurrentUserName();
            var logFilePath = Path.Combine(_dataFolder, ipUser.ReverseMe()) + "_" + Guid.NewGuid().ToString() + "." + Constants.JarFileExtension;
            DisposeCurrentJarFile();
            _jarFileWriter = _jarFileFactory.GetJarFileWriter(logFilePath);
        }

        private void DisposeCurrentJarFile()
        {
            if (_jarFileWriter != null)
            {
                _jarFileWriter.Dispose();
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
