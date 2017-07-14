using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    public class JarFileItem
    {

        private static readonly object Locker = new object();

        //In C# and Java as well, "volatile" means not only "make sure that the compiler and the jitter do not perform any code reordering or register caching optimizations on this variable". 
        //It also means "tell the processors to do whatever it is they need to do to ensure that I am reading the latest value, even if that means halting 
        //other processors and making them synchronize main memory with their caches".
        private static volatile JarFileItem _emptyInstance;

        public JarFileItem(Dictionary<string, string> headers, string filePath)
            : this(headers, File.ReadAllBytes(filePath), -1)
        {
            
        }

        public JarFileItem(Dictionary<string, string> headers, byte[] containt, long offSetInJarFile)
        {
            //To DO: Ensure 
            Headers = headers;
            Containt = containt;
            OffSetInJarFile = offSetInJarFile;
        }
       
        private JarFileItem()
        {

        }

        public Dictionary<string, string> Headers { get; private set; }

        public byte[] Containt { get; private set; }

        public long OffSetInJarFile { get; private set; }


        //Empty for avoiding Null anti patern instead for return null retun Empty
        public static JarFileItem Empty
        {
            get { return BuildOrRetrieveEmptyInstance(); }
        }

        private static JarFileItem BuildOrRetrieveEmptyInstance()
        {
            if (_emptyInstance == null)
            {
                lock (Locker)
                {
                    // double check in case _emptyInstance was instantiated while this thread was locked.
                    if (_emptyInstance == null)
                    {
                        _emptyInstance = new JarFileItem();
                    }
                }
            }

            return _emptyInstance;
        }
    }
}
