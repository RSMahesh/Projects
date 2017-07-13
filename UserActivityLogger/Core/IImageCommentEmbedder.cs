using System.IO;

namespace Core
{
    public interface IImageCommentEmbedder
    {
        void AddComment(string imageFilePath, string comments);
        MemoryStream AddComment(Stream jpegStreamIn, string comments);
        string GetComments(Stream stream);
    }
}