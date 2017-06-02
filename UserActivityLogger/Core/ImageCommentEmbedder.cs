using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UserActivityLogger
{
    public class ImageCommentEmbedder : IImageCommentEmbedder
    {
        const string commmentsMetaKey = "/app1/ifd/exif:{uint=40092}";

        public void AddComment(string imageFilePath, string comments)
        {
            string jpegDirectory = Path.GetDirectoryName(imageFilePath);
            string jpegFileName = Path.GetFileNameWithoutExtension(imageFilePath);

            BitmapDecoder decoder = null;
            BitmapFrame bitmapFrame = null;
            BitmapMetadata metadata = null;
            FileInfo originalImage = new FileInfo(imageFilePath);

            if (File.Exists(imageFilePath))
            {
                // load the jpg file with a JpegBitmapDecoder    
                using (Stream jpegStreamIn = File.Open(imageFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {

                    decoder = new JpegBitmapDecoder(jpegStreamIn, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                }

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
                        //string jpegNewFileName = Path.Combine(jpegDirectory, "JpegTemp.jpg");

                        // Delete the original
                        originalImage.Delete();

                        // Save the new image 
                        using (Stream jpegStreamOut = File.Open(imageFilePath, FileMode.CreateNew, FileAccess.ReadWrite))
                        {
                            encoder.Save(jpegStreamOut);
                        }
                    }
                }
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
