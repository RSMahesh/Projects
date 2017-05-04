using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyer
{
    class Program
    {

         static void Main(string[] args)
        {

            if (args.Length > 0 && args[0] == "hidden")
            {
                Copyer copyer = new Copyer();
                copyer.WatchFtpFolder("*.*");
            }
            else
            {
                ProcessHelper.RunAsBackGround(System.Reflection.Assembly.GetEntryAssembly().Location);
            }
        }
    }
}