using System.Drawing;

namespace UserActivityLogger
{
    public class Activity
    {
        private static volatile Activity _emptyInstance = new Activity();
        public Activity(Image screenShot, string keyPressedData)
        {
            ScreenShot = screenShot;
            KeyPressedData = keyPressedData;
        }

        private Activity()
        {

        }
        public Image ScreenShot { get; private set; }
        public string KeyPressedData { get; private set; }

        public static Activity Empty
        {
            get
            {
                return _emptyInstance;
            }
        }
    }
}
