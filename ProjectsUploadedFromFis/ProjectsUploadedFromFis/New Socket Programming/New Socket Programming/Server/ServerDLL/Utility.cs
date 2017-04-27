using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ServerDLL
{
    internal static class Utility
    {

       public static byte[] GetScreenImageAsBytes()
        {
            MemoryStream stream = new MemoryStream();
            Image img = ScreenCapture.CaptureScreen();
            img.Save(stream, ImageFormat.Jpeg);
            //img.Save(@"D:\Server\Temp.jpeg", ImageFormat.Jpeg);
            byte[] buffer = stream.ToArray();
            img.Dispose();
            return buffer;
        }

       public static Image GetScreenImageAsImage()
       {
           return ScreenCapture.CaptureScreen();
       }


    }
            

}
