using System.Drawing;

namespace UserActivityLogger
{
    public class Activity
    {
        public Activity(Image screenShot, string keyPressedData)
        {
            ScreenShot = screenShot;
            KeyPressedData = keyPressedData;
        }
        public Image ScreenShot { get; private set; }
        public string KeyPressedData { get; private set; }

    }
}
