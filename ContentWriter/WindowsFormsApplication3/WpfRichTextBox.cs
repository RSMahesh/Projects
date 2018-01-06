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

namespace WindowsFormsApplication3
{
    public class WpfRichTextBox
    {
        System.Windows.Controls.RichTextBox richTextBox = new System.Windows.Controls.RichTextBox();

        ElementHost host = new ElementHost();

        bool addingText = false;

        public WpfRichTextBox(System.Windows.Forms.Panel panel)
        {
            richTextBox.SpellCheck.IsEnabled = true;
            richTextBox.FontFamily = new FontFamily("Verdana");
            richTextBox.FontStyle = System.Windows.FontStyles.Normal;
            richTextBox.FontSize = 14.25F;
            richTextBox.FontWeight = System.Windows.FontWeights.Regular;
            richTextBox.TextChanged += RichTextBox_TextChanged;
            richTextBox.KeyUp += RichTextBox_KeyUp;
            host.Dock = DockStyle.Fill;
            host.Child = richTextBox;
            panel.Controls.Add(host);
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
