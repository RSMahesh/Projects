using System;
using System.IO;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FileSystem
{
    //This is sample request for adding file to github from fiddler

    //    PUT https://api.github.com/repos/MaheshBailwal/StatusMakerLastestApp/contents/index3.html HTTP/1.1
    //Host: api.github.com
    //User-Agent: Fiddler
    //Authorization: basic bWFoZXNoYmFpbHdhbDptYkAyNDgwMDE =
    //Content - Length: 85

    //{
    //	"path": "index.html",
    //	"message": "test",
    //	"content": "dGVzdGluZyBodG1sIHYy"
    //}
    public class GitFileSystem : IFileSystem
    {
        public void CopyFile(string sourceFile, string destinationFile)
        {
            throw new NotImplementedException();
        }

        public void CreateDirectoryIfNotExist(string directoryPath)
        {
            throw new NotImplementedException();
        }

        public void DeleteFileIfExist(string file)
        {
            throw new NotImplementedException();
        }
    }
}
