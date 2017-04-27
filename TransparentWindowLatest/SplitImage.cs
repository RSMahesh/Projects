using MB.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TransparentWindow
{
    public class SplitImage
    {
        private static int count = 0;
        public static string TransparentWindowImagesDir = "TransparentWindowImages";
        
        public static void SplitImages(Image image, Dictionary<string, Rectangle> splits)
        {
            TransparentWindowImagesDir = RuntimeHelper.MapToCurrentLocation(TransparentWindowImagesDir);

            foreach (var imageName in splits.Keys)
            {
                var dir = "TransparentWindowImages";

                if (!Directory.Exists(TransparentWindowImagesDir))
                {
                    Directory.CreateDirectory(TransparentWindowImagesDir);
                }

                Rectangle targetRect = splits[imageName];

                Image topImage = new Bitmap(targetRect.Width, targetRect.Height);

                var graphics1 = Graphics.FromImage(topImage);
                graphics1.DrawImage(image, new Rectangle(0, 0, targetRect.Width, targetRect.Height), targetRect, GraphicsUnit.Pixel);
                graphics1.Dispose();
                topImage.Save(TransparentWindowImagesDir +"\\"+imageName + ".jpg", ImageFormat.Jpeg);
            }
        }
    }
}
