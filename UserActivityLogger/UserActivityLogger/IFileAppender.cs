namespace UserActivityLogger
{
    public interface IFileAppender
    {
        void AppendFile(string fileToAppend, string dataFile);
        int GetFileCount(string dataFile);
    }
}