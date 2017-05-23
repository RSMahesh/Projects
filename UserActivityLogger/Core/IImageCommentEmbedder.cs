using System.IO;

namespace UserActivityLogger
{
    public interface IImageCommentEmbedder
    {
        void AddComment(string imageFilePath, string comments);
        string GetComments(Stream stream);
    }
}