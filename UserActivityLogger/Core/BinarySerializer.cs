using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BinarySerializer
    {
        public static byte[] Serilazi(object obj)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, obj);
            return mStream.ToArray();
        }

        public static T DeSerilazi<T>(byte[] objectBytes)
        {
            var mStream = new MemoryStream();
            var binFormatter = new BinaryFormatter();

            mStream.Write(objectBytes, 0, objectBytes.Length);
            mStream.Position = 0;

            return (T)binFormatter.Deserialize(mStream) ;
        }
    }
}
