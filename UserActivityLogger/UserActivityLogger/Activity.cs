using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger
{
    public class Activity
    {
        public Activity(Image screenShot, string keyPressedData)
        {
            ScreenShot = screenShot;
            keyPressedData = keyPressedData;
        }
        public Image ScreenShot { get; private set; }
        public string KeyPressedData { get; private set; }

    }
}
