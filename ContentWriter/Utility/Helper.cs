using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Utility
{
    public static class Helper
    {
        static int fileProcessedCount;
        public static event EventHandler<StudyEventArgs> FileProcessed;

        public static List<string> Find(string searchText, List<string> files)
        {
            if (string.IsNullOrEmpty(searchText))
                return files;

            bool found = false;
            List<string> foundInFiles = new List<string>();
            foreach (var file in files)
            {
                try
                {
                    string ext = Path.GetExtension(file).ToUpperInvariant();
                    found = false;
                    switch (ext)
                    {
                        case ".DOCX":
                        case ".DOC":
                        case ".WEBARCHIVE":
                            found = MSWordDocReader.Find(file, (object)searchText);
                            break;
                        case ".PDF":
                            found = PDFReader.Find(file, searchText);
                            break;
                        case ".TXT":
                        case ".CS":
                        case ".ASPX":
                        case ".ASP":
                        case ".XML":
                        case ".XSLT":
                        case ".JS":
                        case ".CSS":
                        case ".HTML":
                        case ".PHP":
                        case ".CONFIG":
                        case ".ASAX":

                            found = TextFileReader.Find(file, searchText);
                            break;

                        default:
                            found = false;
                            break;
                    }

                    if (found)
                    {
                        foundInFiles.Add(file);

                    }


                    if (FileProcessed != null)
                        FileProcessed(null, new StudyEventArgs(++fileProcessedCount, files.Count));

                }
                catch (Exception ex) { }
            }
           

            return foundInFiles;
        }

        internal static void Close()
        {
            try
            {
                MSWordDocReader.Close();
            }
            catch { }
        }
    }


     

    public class StudyEventArgs : EventArgs
    {
        public int FileProcessed, FileCount;

        public StudyEventArgs(int _FileProcessed, int _FileCount)
        {
            this.FileProcessed = _FileProcessed;
            this.FileCount = _FileCount;

        }

    }

}
