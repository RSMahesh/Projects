namespace Core
{
    public interface IFileSystem
    {
        void CreateDirectoryIfNotExist(string directoryPath);
        void CopyFile(string sourceFile, string destinationFile);
        void DeleteFileIfExist(string file);
    }
}