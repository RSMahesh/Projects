using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MediaValet.Exercise.Common
{
    public static class SingleInstance
    {
        static Mutex mutexObj;

        public static bool IsApplicationAlreadyRunning(string applicationKey)
        {
            bool createdNew;
             mutexObj = new Mutex(true, applicationKey, out createdNew);
            return createdNew;
        }
    }
}
