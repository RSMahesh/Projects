
using Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class StartUp
    {
        private TimeSpan _screenCaptureTimeInterval;
        private readonly IKeyLogger _keyLogger;
        private readonly ImageCommentEmbedder _imageCommentEmbedder;
        private readonly IActivityProvider _activityProvider;
        private readonly ActivitySaver _activitySaver;
        public StartUp(TimeSpan fulshTimeInterval, string logFolder, IKeyLogger keyLogger)
        {
            _screenCaptureTimeInterval = fulshTimeInterval;
            _keyLogger = keyLogger;
            _imageCommentEmbedder = new ImageCommentEmbedder();
            _activityProvider = new ActivityProvider(keyLogger, new ScreenCapturer());
            _activitySaver = new ActivitySaver(logFolder,new FileAppender(), new ImageCommentEmbedder() );
        }

        public void Start()
        {
            _keyLogger.StartListening();
       
            //Add one log when process started
            _activitySaver.Save(_activityProvider.GetActivity("Process Started"));
          
            while (true)
            {
                Thread.Sleep(_screenCaptureTimeInterval);
                try
                {
                    var activity = _activityProvider.GetActivity();
                    if (activity != null)
                    {
                        _activitySaver.Save(activity);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }

            }
        }

    }
}
