using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameFiles
{
   internal class FileRenamer
    {
        private static string[] exludeFiles = { ".exe" };

       private const string sequenceFile = "sequence.txt";

        public void RenameFiles(string dirPath)
        {
            var filesToRename = Directory.GetFiles(dirPath, "*.*");
            var seq = GetSequence(dirPath);

            foreach (var fileToRename in filesToRename)
            {
                var file = Path.GetFileNameWithoutExtension(fileToRename);

                if (IsInValidFile(fileToRename))
                {
                    continue;
                }

                RenameFile(fileToRename, seq);
            }
        }

       private bool IsInValidFile(string file)
       {
           if (exludeFiles.Contains(Path.GetExtension(file)))
           {
              return true;
           }

           if (Path.GetFileName(file) == sequenceFile)
           {
               return true;
           }

           return false;
       }

        private void RenameFile(string fileToRename, string seq)
        {

            if (exludeFiles.Contains(Path.GetExtension(fileToRename)))
            {
                return;
            }

            File.Move(fileToRename,  GetNewFileName(fileToRename, seq));

        }

        private string GetNewFileName(string fileToRename, string seq)
        {
            var file = Path.GetFileNameWithoutExtension(fileToRename);

            var startIndx = file.IndexOf("--");

            string newFileName = string.Empty;

            if (startIndx > -1)
            {
                newFileName = file.Substring(0, startIndx) + seq;
            }

            string newFilePath = string.Empty;


            if (!string.IsNullOrEmpty(newFileName))
            {
                newFilePath = fileToRename.Replace(file, newFileName);
            }
            else
            {
                newFilePath = fileToRename.Replace(file, file + seq);
            }

            return newFilePath;
        }

        private string GetSequence(string dirPath)
        {
            var sequenceFilePath = Path.Combine(dirPath, sequenceFile);

            if (!File.Exists(sequenceFilePath))
            {
                File.WriteAllText(sequenceFilePath, "--0");
            }

            var seq = File.ReadAllText(sequenceFilePath);

            var intSeq = int.Parse(seq.Replace("--", ""));

            var newSeq = "--" + (++intSeq);

            File.WriteAllText(sequenceFilePath, newSeq);

            return newSeq;
        }
    }
}
