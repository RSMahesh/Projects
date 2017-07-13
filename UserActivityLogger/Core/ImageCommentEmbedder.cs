using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Core
{
    public class ImageCommentEmbedder : IImageCommentEmbedder

    {
        public ImageCommentEmbedder()
        {

        }
        const string commmentsMetaKey = "/app1/ifd/exif:{uint=40092}";

        public void AddComment(string imageFilePath, string comments)
        {
            MemoryStream outStream = null;
            var originalImage = new FileInfo(imageFilePath);

            using (Stream jpegStreamIn = File.Open(imageFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                outStream = AddComment(jpegStreamIn, comments);
            }

            // Delete the original
            originalImage.Delete();

            File.WriteAllBytes(imageFilePath, outStream.ToArray());
        }

        public MemoryStream AddComment(Stream jpegStreamIn, string comments)
        {
            BitmapDecoder decoder = null;
            BitmapFrame bitmapFrame = null;
            BitmapMetadata metadata = null;

            decoder = new JpegBitmapDecoder(jpegStreamIn, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);

            bitmapFrame = decoder.Frames[0];
            metadata = (BitmapMetadata)bitmapFrame.Metadata;

            if (bitmapFrame != null)
            {
                BitmapMetadata metaData = (BitmapMetadata)bitmapFrame.Metadata.Clone();

                if (metaData != null)
                {
                    // modify the metadata   
                    metaData.SetQuery(commmentsMetaKey, comments);

                    // get an encoder to create a new jpg file with the new metadata.      
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapFrame, bitmapFrame.Thumbnail, metaData, bitmapFrame.ColorContexts));

                    var jpegStreamOut = new MemoryStream();

                    encoder.Save(jpegStreamOut);

                    return jpegStreamOut;
                }

            }

            throw new Exception("AddComment Failed In Image");
        }

        public string GetComments(string imagePath)
        {
            using (var stream = File.Open(imagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return GetComments(stream);
            }
        }
        public string GetComments(Stream stream)
        {
            stream.Position = 0;
            BitmapSource img = BitmapFrame.Create(stream);
            BitmapMetadata md = (BitmapMetadata)img.Metadata;
            var abc = (string)md.GetQuery(commmentsMetaKey);
            return abc;
        }
    }
}
