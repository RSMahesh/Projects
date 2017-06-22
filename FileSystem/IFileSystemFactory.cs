namespace FileSystem
{
    public interface IFileSystemFactory
    {
        IFileSystem GetFileSystem(string fileSystemType);
    }
}