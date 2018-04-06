using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WindowsFormsApplication3
{
    public static class SingleInstance
    {
        public static bool IsFileAlreadyOpen(string filePath)
        {
            filePath = filePath.Replace("\\", "");
            Semaphore sem;
            try
            {
                sem = Semaphore.OpenExisting(filePath);
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                sem = new Semaphore(1, 1, filePath);
            }

            var acquired = sem.WaitOne(0);
            return !acquired;
        }

        public static void CloseFileMutex(string filePath)
        {

            try
            {
                filePath = filePath.Replace("\\", "");
                var sem = Semaphore.OpenExisting(filePath);
                if (sem != null)
                {
                    sem.Release();
                    sem.Close();
                    sem.Dispose();
                }
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                
            }
        }
    }

}
