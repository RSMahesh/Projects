using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class ActivityProvider : IActivityProvider
    {
        private readonly IKeyLogger _keyLogger;
        private readonly IScreenCapturer _screenCapture;

        public ActivityProvider(IKeyLogger keyLogger, IScreenCapturer screenCapture)
        {

        }

        public Activity GetActivity()
        {
            var keysLogged = _keyLogger.GetKeys();
            if (!string.IsNullOrEmpty(keysLogged))
            {
                return null;
            }

            var img = _screenCapture.CaptureScreen();
            var keyPressedData = _keyLogger.GetKeys();
            _keyLogger.CleanBuffer();
            return new Activity(img, keyPressedData);
        }

        public Activity GetActivity(string keyPressedData)
        {
            var img = _screenCapture.CaptureScreen();
            return new Activity(img, keyPressedData);
        }
    }
}
