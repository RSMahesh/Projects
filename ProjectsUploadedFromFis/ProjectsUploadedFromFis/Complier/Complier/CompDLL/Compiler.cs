using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CodeGenerater;

namespace CompDLL
{
   public class Compiler
    {
       public static void Complie(string codeFile)
        {
        
       
           ParserLanguageSetting.GetInstance(@"C:\Mahesh\Projects\Complier\IDE\KeyMapping.xml");
           Scanner scanner = null;
            string fileName = codeFile;
           Parser parser = null;
            using (TextReader input = File.OpenText(fileName))
            {
                scanner = new Scanner(input);
            }

            PreProcessor pp = new PreProcessor(@"C:\Mahesh\Projects\Complier\CompilerWriting\PreProcessor.xml");
            pp.RunPreProcessor(scanner.Tokens);

            try
            {
                parser = new Parser(scanner);
                parser.StartParser();
            }
            catch (ParserException ex)
            {

                throw new CompileException(ex.LineNumber, codeFile, ex.Message);
            }
            catch (Exception  ex)
            {
                throw new CompileException(parser.GetLastExceptionLine, codeFile, ex.Message);
            }
            
           AssemblyGen assmGen = new AssemblyGen(parser.Result, Path.GetFileNameWithoutExtension(fileName) + ".exe", @"C:\Mahesh\Projects\Complier\CompilerWriting\bin\Debug\");
        }
    }


    public class BaseException : System.Exception
    {
       protected  int _lineNumber;
       protected  string _message;

        public BaseException(int lineNumber, string message)
        {
            _lineNumber = lineNumber;
            _message = message;
        }

        public int LineNumber
        {
            get { return _lineNumber; }
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }



    }

    public class CompileException : BaseException
    {
        string file;
        public CompileException(int lineNumber, string file, string msg): base(lineNumber,msg)
        {
            this.file = file;
        }

        public string File
        {
            get { return file; }
            set { file = value; }
        }

    }

}
