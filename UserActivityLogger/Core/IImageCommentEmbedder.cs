using System.IO;

namespace Core
{
    public interface IImageCommentEmbedder
    {
        void AddComment(string imageFilePath, string comments);
        string GetComments(Stream stream);
    }
}