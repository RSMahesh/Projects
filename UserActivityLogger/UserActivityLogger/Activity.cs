using System.Drawing;

namespace UserActivityLogger
{
    public class Activity : ValueObject<Activity>
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

        protected override int GetHashCodeCore()
        {
            int hash = 13;
            hash = (hash * 7) + ScreenShot.GetHashCode();
            hash = (hash * 7) + KeyPressedData.GetHashCode();

            return hash;
        }

    }
}
