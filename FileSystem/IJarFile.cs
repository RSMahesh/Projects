using System;

namespace FileSystem
{
    public interface IJarFile: IDisposable
    {
        void AddFile(string filePath);
        int FilesCount { get;}
        byte[] GetNextFile();
        string JarFilePath { get;}
    }
}