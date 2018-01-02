using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    public class CellData
    {
        public CellData(object value, Point location)
        {
            Value = value;
            Location = location;
        }

        public CellData() { }
        public object Value { get; set; }
        public Point Location { get; set; }
    }
}
