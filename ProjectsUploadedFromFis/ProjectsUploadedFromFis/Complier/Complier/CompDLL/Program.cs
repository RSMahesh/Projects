// Copyright © Microsoft Corporation.  All rights reserved.”  
// This posting is provided “AS IS” with no warranties of any kind and confers no rights.  
// Use of samples included in this posting are subject to the terms specified at http://www.microsoft.com/info/cpyright.htm.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CodeGenerater;
namespace GfnCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
            //    Console.WriteLine("Usage: gfn.exe program.gfn");
           //     return;
            }

        //    try
          //  {
                ParserLanguageSetting.GetInstance(@"C:\Mahesh\Projects\Complier\IDE\KeyMapping.xml");
                Scanner scanner = null;
                string fileName = "StartIDE1.gfn";
                using (TextReader input = File.OpenText(fileName))
                {
                    scanner = new Scanner(input);
                }

            //PreProcessor pp = new PreProcessor(@"C:\Mahesh\Projects\Complier\CompilerWriting\PreProcessor.xml");
            //Parser parser = new Parser(pp.RunPreProcessor(scanner.Tokens));
            //AssemblyGen assmGen = new AssemblyGen(parser.Result, Path.GetFileNameWithoutExtension(fileName) + ".exe", @"C:\Mahesh\Projects\Complier\CompilerWriting\bin\Debug\");
            }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(e.Message);
        //    }
        //}
    }
}
