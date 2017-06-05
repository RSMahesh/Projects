using FileSystem;

public class JarFileFactory : IJarFileFactory
{
    public IJarFile GetJarFile(FileAccessMode fileAccess, string logFilePath)
    {
        return new JarFile(fileAccess, logFilePath);
    }
}