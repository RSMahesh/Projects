using System;

namespace FileSystem
{
    public interface IJarFileWriter : IDisposable
    {
        void AddFile(JarFileItem jarFileItem);
    }

    public interface IJarFileReader : IDisposable
    {
        int FilesCount { get; }
        JarFileItem GetNextFile();
    }
}