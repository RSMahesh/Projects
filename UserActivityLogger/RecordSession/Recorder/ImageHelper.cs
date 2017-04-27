﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordSession
{
    public class ImageHelper
    {
        static ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
        public static string  ChangeJPGQuality( string imagePath, int quality)
        {
            var img = Image.FromFile(imagePath);
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            var newPath = imagePath.Replace(Path.GetFileNameWithoutExtension(imagePath), Guid.NewGuid().ToString());

            img.Save(newPath, jpegCodec, encoderParams);

            return newPath;
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
    }
}
