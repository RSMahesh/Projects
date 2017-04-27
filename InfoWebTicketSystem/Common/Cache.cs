using System;
using System.Collections.Generic;
using System.Collections;

using System.Linq;
using System.Text;

namespace ResellerClub.Common
{
    public static class Cache
    {
        static Hashtable hashT;

        static Cache()
        {
            hashT = new Hashtable();
        }

        public static object Get(object key)
        {
            return hashT[key];
        }

        public static void Set(object key, object value)
        {
            hashT[key] = value;
        }

    }
}
