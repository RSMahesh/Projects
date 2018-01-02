using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    public class Theme
    {
        public string Name { get; set; }

        public bool Default { get; set; }
        public Color BackGroundColor { get; set; }

        public Color ForeColor { get; set; }

        public Color CurrentRowColor { get; set; }

        public Color HighlightedRowColor { get; set; }

        public Color SelectionBackColor { get; set; }

        public Color SelectionForeColor { get; set; }

        public Color XmlBackGroundColor { get; set; }

        public Color XmlForeColor { get; set; }

        public Color CurrentCellBorderColor { get; set; }

        public static Theme GetDefaultTheme()
        {
            var theme = new Theme();
            theme.BackGroundColor = Color.LightGray;
            theme.ForeColor = Color.Black;
            theme.SelectionBackColor = Color.LightGray;
            theme.SelectionForeColor = Color.Black;
            theme.CurrentRowColor = Color.Cornsilk;
            theme.HighlightedRowColor = Color.CadetBlue;
            theme.CurrentCellBorderColor = Color.White;

            return theme;
        }

    }
}
