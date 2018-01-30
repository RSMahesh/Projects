using EventPublisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using RichTextBox = System.Windows.Controls.RichTextBox;
using ContextMenu = System.Windows.Controls.ContextMenu;
using System.Windows;
using System.Threading;
using System.Windows.Markup;

namespace WindowsFormsApplication3
{
    public class WpfRichTextBox
    {
        RichTextBox richTextBox = new RichTextBox();

        System.Windows.Controls.ContextMenu contextMenu = new System.Windows.Controls.ContextMenu();
        ElementHost host = new ElementHost();

        bool addingText = false;

        void AddContextMenu()
        {

            contextMenu.Items.Add("Copy");
            //contextMenu.MenuItems.Add("Past", OnPast);
            //contextMenu.MenuItems.Add("Delete", OnDelete);
            //contextMenu.MenuItems.Add("ApplyFormula", OnApplyFormula);

        }

        private void RichTextBox_ContextMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            //RichTextBox rtb = sender as RichTextBox;


            //if (rtb == null) return;

            //ContextMenu contextMenu = rtb.ContextMenu;

            //contextMenu.PlacementTarget = rtb;
            //FormSys frm = new FormSys();
            //frm.Show();
        }

        public WpfRichTextBox(System.Windows.Forms.Panel panel)
        {
            // AddContextMenu();
            ChangeLanguage();
            richTextBox.PreviewMouseRightButtonDown += RichTextBox_PreviewMouseRightButtonDown;
            richTextBox.MouseDown += RichTextBox_MouseDown;
            richTextBox.ContextMenuOpening += RichTextBox_ContextMenuOpening;
            richTextBox.SpellCheck.IsEnabled = true;
            richTextBox.FontFamily = new FontFamily("Verdana");
          //  richTextBox.FontStyle = System.Windows.FontStyles.Normal;
            richTextBox.FontSize = 14.25F;
           // richTextBox.FontWeight = System.Windows.FontWeights.Regular;
            richTextBox.TextChanged += RichTextBox_TextChanged;
            richTextBox.KeyUp += RichTextBox_KeyUp;
            richTextBox.BorderBrush = new SolidColorBrush(Colors.White);
            richTextBox.BorderThickness = new Thickness(3);
            host.Dock = DockStyle.Fill;
            host.Child = richTextBox;
            panel.Controls.Add(host);
        }

        public Color BackgroundColor
        {
            get
            {
                return (richTextBox.Background as SolidColorBrush).Color;
            }
            set
            {
                SolidColorBrush brush = new SolidColorBrush(value);
                richTextBox.Background = brush;
            }
        }
        
        public Color ForegroundColor
        {
            get
            {
                return (richTextBox.Foreground as SolidColorBrush).Color;
            }
            set
            {
                SolidColorBrush brush = new SolidColorBrush(value);
                richTextBox.Foreground = brush;
            }
        }


        public Color BorderColor
        {
            get
            {
                return (richTextBox.BorderBrush as SolidColorBrush).Color;
            }
            set
            {
                richTextBox.BorderBrush = new SolidColorBrush(value);
            }
        }

        private void ChangeLanguage()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");

            //FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
            //    new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name)));
        }
        private void RichTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //FormSys frm = new FormSys();
            //frm.ShowDialog();
        }

        private void RichTextBox_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void RichTextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            richTextBox.ContextMenu = null;
        }

        private void RichTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //   throw new NotImplementedException();
        }

        private void RichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!addingText)
            {
                var text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;

                EventContainer.PublishEvent(Events.RichTextBoxTextChanged.ToString(), new EventArg(text));
            }
        }

        public void AddText(string text)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
            addingText = true;
            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));
            addingText = false;
        }

        public void HighlightWholeWordAll(string word)
        {
            if (word == string.Empty)
                return;

            word = word.Trim();

            var wordPattern = new Regex(@"\b" + word + "\\b", RegexOptions.IgnoreCase);
            var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            foreach (Match match in wordPattern.Matches(textRange.Text))
            {
                SelectAndChangeColor(match.Index, match.Length);
            }
        }

        public void SelectText(string word, int startIndex)
        {
            if (word == string.Empty)
                return;

            var text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;

            var index = text.IndexOf(word, startIndex, StringComparison.OrdinalIgnoreCase);

            if (index != -1)
            {
                SelectAndChangeColor(index, word.Length);
                //myRtb.Select(index, word.Length);
            }
        }


        public bool ReplaceText(string textToReplace)
        {
            if (richTextBox.Selection.Text.Length > 0)
            {
                richTextBox.Selection.Text = textToReplace;
                return true;
            }
            return false;
        }


        private static TextPointer GetTextPointAt(TextPointer from, int pos)
        {
            TextPointer ret = from;
            int i = 0;

            while ((i < pos) && (ret != null))
            {
                if ((ret.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.Text) || (ret.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.None))
                    i++;

                if (ret.GetPositionAtOffset(1, LogicalDirection.Forward) == null)
                    return ret;

                ret = ret.GetPositionAtOffset(1, LogicalDirection.Forward);
            }

            return ret;
        }

        private void SelectAndChangeColor(int offset, int length)
        {

            // Get text selection:
            TextSelection textRange = richTextBox.Selection;

            // Get text starting point:
            TextPointer start = richTextBox.Document.ContentStart;

            // Get begin and end requested:
            TextPointer startPos = GetTextPointAt(start, offset);
            TextPointer endPos = GetTextPointAt(start, offset + length);

            // New selection of text:
            textRange.Select(startPos, endPos);

            // Apply property to the selection:
            textRange.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(System.Windows.Media.Colors.YellowGreen));

            // Return selection text:
            //return rtb.Selection.Text;
        }

    }
}
