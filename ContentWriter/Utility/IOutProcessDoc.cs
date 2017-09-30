using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    interface IOutProcessDoc
    {
     
        void OpenDoc(string _fileName);
        void Resize(int left, int top, int width, int height);
        bool Visible { get; set; }
        void Close();

        string Text { get; set; }
    }
}
