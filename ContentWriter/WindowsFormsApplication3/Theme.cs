using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            theme.BackGroundColor = SavedBackGroundColor;
            theme.ForeColor = Color.Black;
            theme.SelectionBackColor = SavedBackGroundColor;
            theme.SelectionForeColor = Color.Black;
            theme.CurrentRowColor = SavedBackGroundColor;
            theme.HighlightedRowColor = Color.CadetBlue;
            theme.CurrentCellBorderColor = Color.White;

            return theme;
        }


        public static Color SavedBackGroundColor
        {
            get
            {
                if (File.Exists(BackGroundColorFile))
                {
                    return ColorTranslator.FromHtml(File.ReadAllText(BackGroundColorFile));
                }
                else
                {
                    return Color.LightGray;
                }
            }

            set
            {
                File.WriteAllText(BackGroundColorFile, ColorTranslator.ToHtml(value));
            }
        }


        private static string BackGroundColorFile
        {
            get
            {
                return Path.Combine(Utility.FreeLanceAppDataFolder, "_BackGroundColor.txt");
            }
        }


        public static Theme GetReadOnlyTheme()
        {
            var theme = new Theme();
            theme.BackGroundColor = Color.White;
            theme.ForeColor = Color.Black;
            theme.SelectionBackColor = Color.White;
            theme.SelectionForeColor = Color.Black;
            theme.CurrentRowColor = Color.White;
            theme.HighlightedRowColor = Color.YellowGreen;
            theme.CurrentCellBorderColor = Color.Yellow;

            return theme;
        }

    }
}
