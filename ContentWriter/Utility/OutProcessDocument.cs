using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utility
{
   public static class OutProcessDocument 
    {
       static  IOutProcessDoc currentDoc;
       static Dictionary<string, IOutProcessDoc> handleTable;
       static OutProcessDocument()
       {
           handleTable = new Dictionary<string, IOutProcessDoc>();
       }
       public static bool OpenDocument(string path)
       {
           IOutProcessDoc doc = GetDocHandler(path);
           if (doc != null)
           {
               if (doc != currentDoc)
               {
                   if (currentDoc != null)
                   {
                       currentDoc.Visible = false;
                   }
                   currentDoc = doc;
               }

               doc.OpenDoc(path);
               return true;
           }
           else
           {
               if (currentDoc != null)
               currentDoc.Visible = false;
               return false;
           }
       }

       public static void ReSize(int left, int top, int width, int height)
       {
           if(currentDoc != null && currentDoc.Visible)
           currentDoc.Resize(left, top, width, height);
       }

       private static IOutProcessDoc GetDocHandler(string path)
       {
           string ext = Path.GetExtension(path).ToUpperInvariant();
           IOutProcessDoc doc = null;
           if (handleTable.ContainsKey(ext))
           {
               doc = handleTable[ext];
           }

           switch (ext)
           {
               case ".DOCX":
               case ".DOC":
                   doc = MSWordDocHandler.Instance;
                   break;
               case ".WEBARCHIVE":
                   doc = SafariDocHandler.Instance;
                   break;
               default:
                   doc = null;
                   break;
           }
           
           if (doc != null)
           {
               handleTable[ext] = doc;
           }

           return doc;
       }

       public static void Close()
       {
           foreach (KeyValuePair<string, IOutProcessDoc> item in handleTable)
           {
               item.Value.Close();
               handleTable[item.Key] = null;
                handleTable.Remove(item.Key);
                break;
           }

           Helper.Close();

            ProcessHelper.CloseProcess("WINWORD.EXE");
       }

       public static bool Visible
       {
           get
           {
               if (currentDoc != null)
                   return currentDoc.Visible;
               else
                   return false;

           }
           set
           {
               if (currentDoc != null)
               currentDoc.Visible = value;
           }
       }

        public static string Text
        {
            get
            {
                if (currentDoc != null)
                    return currentDoc.Text;
                else
                    return string.Empty;

            }
            set
            {
                if (currentDoc != null)
                    currentDoc.Text = value;
            }
        }
    }
}
