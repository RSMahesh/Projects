using System;
using FileSystem;

public class JarFileFactory : IJarFileFactory
{
    public IJarFileReader GetJarFileReader(string logFilePath)
    {
        return new JarFile(FileAccessMode.Read, logFilePath);
    }
    public IJarFileWriter GetJarFileWriter(string logFilePath)
    {
        return new JarFile(FileAccessMode.Write, logFilePath);
    }
}