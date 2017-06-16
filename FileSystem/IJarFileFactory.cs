using FileSystem;

public interface IJarFileFactory
{
    IJarFileWriter GetJarFileWriter(string logFilePath);
    IJarFileReader GetJarFileReader(string logFilePath);
}
