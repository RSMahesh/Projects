using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServerDLL
{
    internal  class SyncFileServer : FileServer 
    {
        public SyncFileServer(double interval, string commmunicationFolder): base(interval,  commmunicationFolder)
        {

        }

        public override void Start()
        {
            while (true)
            {
                if (!ReadCommand())
                    Thread.Sleep(3);
                Thread.Sleep(1);
                //break;
            }

        }
    }
}
