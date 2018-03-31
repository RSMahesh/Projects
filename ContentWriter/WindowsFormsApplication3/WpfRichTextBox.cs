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
        RichTextBox internalRichTextBox = new RichTextBox();
        ElementHost host = new ElementHost();
        public System.Windows.Controls.SpellingError spellingError;
        public string SpellErrorText;

        bool doNotPublishChangeText = false;
        public WpfRichTextBox(Control panel)
        {
            ChangeLanguage();

            richTextBox.PreviewMouseRightButtonDown += RichTextBox_PreviewMouseRightButtonDown;
            richTextBox.MouseDown += RichTextBox_MouseDown;
            richTextBox.SpellCheck.IsEnabled = true;
            internalRichTextBox.SpellCheck.IsEnabled = true;
            richTextBox.FontFamily = new FontFamily("Verdana");
            richTextBox.FontSize = 14.25F;
            richTextBox.TextChanged += RichTextBox_TextChanged;
            richTextBox.BorderBrush = new SolidColorBrush(Colors.White);
            richTextBox.BorderThickness = new Thickness(3);
            host.Dock = DockStyle.Fill;
            host.Child = richTextBox;
            panel.Controls.Add(host);
        }

        public string Text
        {
            get
            {
                return new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            }
            set
            {
                Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
                doNotPublishChangeText = true;
                richTextBox.Document.Blocks.Clear();
                richTextBox.Document.Blocks.Add(new Paragraph(new Run(value)));
                doNotPublishChangeText = false;
            }
        }

        public double FontSize
        {
            set
            {
                richTextBox.FontSize = value;
            }
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
        
        public bool IsSpellErrors()
        {
            TextRange spellErrorRange;
            TextPointer start_pointer, end_pointer;
            int chrPos = 0;

            richTextBox.SelectAll();
            int txtLen = richTextBox.Selection.Text.Length;
           
            for (int i = chrPos; i < txtLen; i++)
            {
                start_pointer = richTextBox.Document.ContentStart.GetNextInsertionPosition
                    (LogicalDirection.Forward).GetPositionAtOffset(i, LogicalDirection.Forward);

                spellingError = richTextBox.GetSpellingError(start_pointer);
                if (spellingError != null)
                {

                    spellErrorRange = richTextBox.GetSpellingErrorRange(start_pointer);
                    SpellErrorText = spellErrorRange.Text;
                    int errRange = spellErrorRange.Text.Length;
                    
                    string textRun = start_pointer.GetTextInRun(LogicalDirection.Forward);
                    string trimmedString = string.Empty;
                    end_pointer = richTextBox.Document.ContentStart.GetNextInsertionPosition
                        (LogicalDirection.Forward).GetPositionAtOffset(i + errRange, LogicalDirection.Forward);
                    richTextBox.Selection.Select(start_pointer, start_pointer);
                   // richTextBox.Focus();


                    Rect screenPos = richTextBox.Selection.Start.GetCharacterRect(LogicalDirection.Forward);
                    double offset = screenPos.Top + richTextBox.VerticalOffset;
                    richTextBox.ScrollToVerticalOffset(offset - richTextBox.ActualHeight / 2);

                    return true;
                }
            }

            return false;

        }

        public bool IsSpellErrorsEx(string text)
        {
            TextRange spellErrorRange;
            TextPointer start_pointer, end_pointer;
            int chrPos = 0;
            Text = text;

            richTextBox.SelectAll();
            int txtLen = richTextBox.Selection.Text.Length;

            for (int i = chrPos; i < txtLen; i++)
            {
                start_pointer = richTextBox.Document.ContentStart.GetNextInsertionPosition
                    (LogicalDirection.Forward).GetPositionAtOffset(i, LogicalDirection.Forward);

                spellingError = richTextBox.GetSpellingError(start_pointer);
                if (spellingError != null)
                {

                    spellErrorRange = richTextBox.GetSpellingErrorRange(start_pointer);
                    SpellErrorText = spellErrorRange.Text;
                    int errRange = spellErrorRange.Text.Length;

                    string textRun = start_pointer.GetTextInRun(LogicalDirection.Forward);
                    string trimmedString = string.Empty;
                    end_pointer = richTextBox.Document.ContentStart.GetNextInsertionPosition
                        (LogicalDirection.Forward).GetPositionAtOffset(i + errRange, LogicalDirection.Forward);
                    richTextBox.Selection.Select(start_pointer, start_pointer);
                    // richTextBox.Focus();


                    Rect screenPos = richTextBox.Selection.Start.GetCharacterRect(LogicalDirection.Forward);
                    double offset = screenPos.Top + richTextBox.VerticalOffset;
                    richTextBox.ScrollToVerticalOffset(offset - richTextBox.ActualHeight / 2);

                    return true;
                }
            }

            return false;

        }

        public bool DoesContainSpellErrors(string text)
        {
            //this.richTextBox.Visibility = Visibility.Hidden;
        
            TextPointer start_pointer, end_pointer;
            int chrPos = 0;

            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));

            richTextBox.SelectAll();
            int txtLen = richTextBox.Selection.Text.Length;
          
            for (int i = chrPos; i < txtLen; i++)
            {
                start_pointer = richTextBox.Document.ContentStart.GetNextInsertionPosition
                    (LogicalDirection.Forward).GetPositionAtOffset(i, LogicalDirection.Forward);

              var  spellingError12 = richTextBox.GetSpellingError(start_pointer);
                if (spellingError12 != null)
                {
                   var spellErrorRange = richTextBox.GetSpellingErrorRange(start_pointer);
                    return true;
                }
            }

            return false;
        }

        public bool DoesContainSpellErrors123(string text)
        {
            TextPointer start_pointer, end_pointer;
            int chrPos = 0;

            internalRichTextBox.Document.Blocks.Clear();
            internalRichTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));

            internalRichTextBox.SelectAll();
            int txtLen = internalRichTextBox.Selection.Text.Length;

            for (int i = chrPos; i < txtLen; i++)
            {
                start_pointer = internalRichTextBox.Document.ContentStart.GetNextInsertionPosition
                    (LogicalDirection.Forward).GetPositionAtOffset(i, LogicalDirection.Forward);

                var spellingError12 = internalRichTextBox.GetSpellingError(start_pointer);
                if (spellingError12 != null)
                {
                    var spellErrorRange = internalRichTextBox.GetSpellingErrorRange(start_pointer);
                    return true;
                }
            }

            return false;
        }

        public void CorrectSpellError(string text)
        {
            spellingError.Correct(text);
        }

        public void IgnoreSpellError()
        {
            spellingError.IgnoreAll();
        }

        public void LoadCustomDic(Uri customDictionaryUri)
        {
            System.Collections.IList dictionaries = System.Windows.Controls.SpellCheck.GetCustomDictionaries(richTextBox);
            if(dictionaries.Contains(customDictionaryUri))
            {
                dictionaries.Remove(customDictionaryUri);
            }

            dictionaries.Add(customDictionaryUri);
        }

        private void ChangeLanguage()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
        }
        private void RichTextBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void RichTextBox_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void RichTextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            richTextBox.ContextMenu = null;
        }

        private void RichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!doNotPublishChangeText)
            {
                EventContainer.PublishEvent(Events.RichTextBoxTextChanged.ToString(), new EventArg(Text));
            }
        }
        
        public int LineSpacing
        {
            set
            {
                Paragraph p = richTextBox.Document.Blocks.FirstBlock as Paragraph;
                p.LineHeight = value;
            }
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
