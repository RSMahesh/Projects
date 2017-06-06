using System;

namespace FileSystem
{
    public interface IJarFile: IDisposable
    {
        void AddFile(JarFileItem jarFileItem);
        int FilesCount { get;}
        JarFileItem GetNextFile();
        string JarFilePath { get;}
    }

    public interface IJarFileWriter
    {
        void AddFile(JarFileItem jarFileItem);
        int FilesCount { get; }
        JarFileItem GetNextFile();
        string JarFilePath { get; }
    }
}