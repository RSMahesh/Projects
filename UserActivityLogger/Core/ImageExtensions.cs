using System.Drawing;
using System.IO;


namespace Core
{
   public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}
