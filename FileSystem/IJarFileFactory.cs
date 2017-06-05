using FileSystem;

public interface IJarFileFactory
{
    IJarFile GetJarFile(FileAccessMode fileAccess, string logFilePath);
}
