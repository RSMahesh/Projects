using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UploadFolder
{
    public class Info
    {
        public Info()
        {
        }

        public Info(string fileName, bool isFolder)
        {
            this.fileName = fileName;
            this.isFolder = isFolder;
        }
        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private bool isFolder;

        public bool IsFolder
        {
            get { return isFolder; }
            set { isFolder = value; }
        }
    }
}
