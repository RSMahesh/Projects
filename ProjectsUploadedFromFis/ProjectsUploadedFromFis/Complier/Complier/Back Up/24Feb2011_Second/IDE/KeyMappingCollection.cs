using System;
using System.Collections.Generic;
using System.Text;
using KeyMappingPair = System.Collections.Generic.KeyValuePair<string, string>;
namespace IDE
{
    internal sealed  class KeyMappingCollection 
    {
        List<KeyMappingPair> keyMappingList;
        Comparison<KeyMappingPair> compare;
        static KeyMappingCollection instance;
        static KeyMappingCollection()
        {
            instance = new KeyMappingCollection("");
        }
        private KeyMappingCollection(string pp)
        {
            keyMappingList = new List<KeyMappingPair>();
            compare += compare;
        }
        public static KeyMappingCollection GetInstance()
        {
            return instance;
        }
        public int comparison(KeyMappingPair x, KeyMappingPair y)
        {
        return   String.Compare(x.Key, y.Key, StringComparison.InvariantCulture);
        }
        public void Add(string key, string value)
        {
            keyMappingList.Add(new KeyMappingPair(key, value));
        }
        public void Clear()
        {
            keyMappingList.Clear();
        }
        public bool Contains(string key)
        {
            foreach (KeyMappingPair item in keyMappingList)
            {
                if (String.Compare(item.Key, key, StringComparison.InvariantCulture) == 0)
                    return true;
            }

            return false;
        }
        public int Count
        {
           get
           {
               return keyMappingList.Count;
           }
            
        }
        public bool Remove(string key)
        {
               foreach (KeyMappingPair item in keyMappingList)
            {
                if (String.Compare(item.Key, key, StringComparison.InvariantCulture) == 0)
                {
                   return  keyMappingList.Remove(item);
                    
                     }
               }

               return false;
        }
        public void Sort()
        {
            keyMappingList.Sort(compare);
        }
        public string  FindFirstClosedMatch(string key)
        {
            foreach (KeyMappingPair item in keyMappingList)
            {
                if (item.Key.StartsWith(key, StringComparison.InvariantCulture))
                {
                    return item.Value;
                }
            }
            return "";
        }
        public KeyMappingPair[] ToArray()
        {
           return  keyMappingList.ToArray();
        }

        public KeyMappingPair[] ToDistinctValueArray()
        {
            keyMappingList.Sort(delegate(KeyMappingPair a1, KeyMappingPair a2) { return a1.Value.CompareTo(a2.Value); });

           return  keyMappingList.FindAll(delegate(KeyMappingPair a1) 
                                {
                                 int indx = keyMappingList.IndexOf(a1);
                                return  indx >0 ? ! keyMappingList[--indx].Value.Equals(a1.Value, StringComparison.OrdinalIgnoreCase):true ;
                                }
                            ).ToArray() ;
       
        }

    }

}
