using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsControlLibrary1
{
    public class LeftTextEventChangeDetail
    {
        public LeftTextEventChangeDetail(string text, string tag)
        {
            Text = text;
            Tag = tag;
        }
        public string Text { get; private set; }
        public string Tag { get; private set; }
    }
}
