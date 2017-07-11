using System;

namespace FileSystem
{
    public interface IJarFileWriter : IDisposable
    {
        void AddFile(JarFileItem jarFileItem);
        string JarFilePath { get; }
    }

    public interface IJarFileReader : IDisposable
    {
        int FilesCount { get; }
        JarFileItem GetNextFile();
        long GetNextFileOffset();
        void MoveFileHeader(long position);
        string JarFilePath { get; }
    }
}