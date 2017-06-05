using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class CurrentActivityProvider : ICurrentActivityProvider
    {
        private readonly IKeyLogger _keyLogger;
        private readonly IScreenCapturer _screenCapture;

        public CurrentActivityProvider(IKeyLogger keyLogger, IScreenCapturer screenCapture)
        {
            _keyLogger = keyLogger;
            _screenCapture = screenCapture;
        }

        public Activity GetActivity()
        {
            var keysLogged = _keyLogger.GetKeys();

            if (string.IsNullOrEmpty(keysLogged))
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
