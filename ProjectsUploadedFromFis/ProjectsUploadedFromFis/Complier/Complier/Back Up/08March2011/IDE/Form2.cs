using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using CompDLL;
using System.IO;

using KeyMappingPair = System.Collections.Generic.KeyValuePair<string, string>;

namespace IDE
{
    public partial class Form2 : Form
    {
        string userTypedKeys = "";
        KeyMappingCollection keyMappingCollection;
        readonly Font hindiFont, englishFont;

        public Form2()
        {
            InitializeComponent();
            keyMappingCollection = KeyMappingCollection.GetInstance();
            hindiFont = new Font("Shusha", (float)14, FontStyle.Regular);
            englishFont = new Font("Verdana", (float)9, FontStyle.Regular);
            listMapping.Font = hindiFont;
            label1.Font = hindiFont;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(listMapping.Items[0].ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listMapping.Visible = false;
            LaodKeyMapping();
        }

        private void LaodKeyMapping()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Mahesh\Projects\Complier\IDE\KeyMapping.xml");
            XmlNodeList list = doc.SelectNodes("/root/add");
            foreach (XmlNode node in list)
            {
              //  if (node.Attributes["ComplilerKeyWord"] != null && node.Attributes["ComplilerKeyWord"].Value  != "")
                keyMappingCollection.Add(node.Attributes["KeyBordKey"].Value, node.Attributes["DisplayString"].Value);
            }
            listMapping.DisplayMember = "value";
            listMapping.ValueMember = "key";
            listMapping.Sorted = true;
            listMapping.DataSource = keyMappingCollection.ToDistinctValueArray();
        }


        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            AddSelectedIntellisenseItem();
        }
        private void AddSelectedIntellisenseItem()
        {
            int start = rchTxtWindow.SelectionStart;
            int lastIndex = rchTxtWindow.Text.LastIndexOf(userTypedKeys, start);
            RemoveTypedCharacter(start - userTypedKeys.Length, userTypedKeys.Length);
            InsertText(start - userTypedKeys.Length, listMapping.SelectedItem);
            rchTxtWindow.Font = hindiFont;
            UpdateFont();
            listMapping.Visible = false;
            userTypedKeys = "";
        }

        private void TransalteAndAdd()
        {
            int start = rchTxtWindow.SelectionStart;
            int lastIndex = rchTxtWindow.Text.LastIndexOf(userTypedKeys, start);
            string transaltedText = Transalte(userTypedKeys);

            RemoveTypedCharacter(start - userTypedKeys.Length, userTypedKeys.Length);
            InsertTextEx(start - userTypedKeys.Length, transaltedText);
            rchTxtWindow.Font = hindiFont;
            UpdateFont();
            userTypedKeys = "";
            label1.Visible = false;
        }

        private void InsertText(int index, object value)
        {
            string key = ((KeyMappingPair)value).Value;
            rchTxtWindow.Text = rchTxtWindow.Text.Insert(index, key);
            rchTxtWindow.SelectionStart = index + key.Length;
        }

        private void InsertTextEx(int index,string value)
        {
            rchTxtWindow.Text = rchTxtWindow.Text.Insert(index, value );
            rchTxtWindow.SelectionStart = index + value.Length;
        }

        private string Transalte(string text)
        {
            StringBuilder sb = new StringBuilder(10);
            for (int indx = 0; indx < text.Length; indx++)
            {
               sb.Append( GetMappedCharacter(text.Substring(indx,1)));
            }
            return sb.ToString();
        }

        private string GetMappedCharacter(string chr)
        {
            string str = keyMappingCollection[chr];
            return str == null? chr : str;
        }
        
        
        private void RemoveTypedCharacter(int index, int len)
        {
            rchTxtWindow.Text = rchTxtWindow.Text.Remove(index, len);
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (checkBox1.Checked)
            {
                rchTxtWindow.SelectionFont = englishFont;
                return;
            }

            if (e.KeyCode == Keys.Up && listMapping.Visible)
            {
                if (listMapping.SelectedIndex > -1)
                    listMapping.SelectedIndex = listMapping.SelectedIndex - 1;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Down && listMapping.Visible)
            {
                if (listMapping.SelectedIndex < listMapping.Items.Count - 1)
                    listMapping.SelectedIndex = listMapping.SelectedIndex + 1;
                e.Handled = true;
            }
            rchTxtWindow.SelectionFont = englishFont;
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox1.Checked)
            {
                HandleKeyPress(e);
                return;
            }
          int asci = (int)e.KeyChar;
            if (asci == 32)
                return;
            if (asci == 8 && !listMapping.Visible)
                return;
            if (asci == 27 && listMapping.Visible)
            {
                listMapping.Visible = false;
                //userTypedKeys = "";
                checkBox1.Checked = true;
                return;
            }
            if (asci == 13 && listMapping.Visible)
            {
                OnEnterKey();
                return;
            }
            if (asci == 13)
                return;
            if (IsMetaCharacter(asci))
                return;
            if ((int)e.KeyChar == 8 && userTypedKeys.Length > 0)
                userTypedKeys = userTypedKeys.Substring(0, userTypedKeys.Length - 1);
            else
                userTypedKeys += e.KeyChar.ToString();

            ShowIntellisense();
        }

        private void HandleKeyPress(KeyPressEventArgs e)
        {
            int asci = (int)e.KeyChar;
         
            if (asci == 27)
            {
                return;
            }
            if (asci == 13 && userTypedKeys.Trim().Length >0)
            {
                OnEnterKey();
                checkBox1.Checked = false;
                return;
            }
            if (asci == 13)
                return;
            if (IsMetaCharacter(asci))
                return;
            if ((int)e.KeyChar == 8 )
            {
                if(userTypedKeys.Length > 0)
                userTypedKeys = userTypedKeys.Substring(0, userTypedKeys.Length - 1);
            }
            else
                userTypedKeys += e.KeyChar.ToString();

          Point p =   GetCursorLocation();
          if (!label1.Visible)
          {
              label1.Left = p.X+5;
              label1.Top = p.Y + 50;
              label1.Tag = "";
              label1.Visible = true;
          }
          label1.Text = Transalte(userTypedKeys);
     
        }


        private void OnEnterKey()
        {
            int indx = rchTxtWindow.SelectionStart - 1;
            rchTxtWindow.Text = rchTxtWindow.Text.Remove(indx, 1);
            rchTxtWindow.SelectionStart = indx;
            if (checkBox1.Checked)
            {

                TransalteAndAdd();
            }
            else
            {
                AddSelectedIntellisenseItem();
            }
        }


        private void ShowIntellisense()
        {
            Point p = rchTxtWindow.GetPositionFromCharIndex(rchTxtWindow.SelectionStart);
            if (!listMapping.Visible)
            {
                p.Y += (int)(hindiFont.GetHeight() + hindiFont.GetHeight() +5 );
                listMapping.Location = p;
                listMapping.Visible = true;
            }
            SelectIntellisenseItem();
        }

        private Point GetCursorLocation()
        {
            return rchTxtWindow.GetPositionFromCharIndex(rchTxtWindow.SelectionStart);
        }

        private bool IsMetaCharacter(int asci)
        {
            if (ConfigurationManager.AppSettings["MetaCharcter"].IndexOf((char)asci) > -1)
            {
                userTypedKeys = string.Empty;
                return true;
            }
            return false;
        }

        private void SelectIntellisenseItem()
        {
            string key = keyMappingCollection.FindFirstClosedMatch(userTypedKeys);
            int indx = listMapping.FindStringExact(key);
            if (indx > -1)
                listMapping.SelectedIndex = indx;
        }

        private void UpdateFont()
        {
            int indx = 0;
            char[] metaChar = ConfigurationManager.AppSettings["MetaCharcter"].ToCharArray();
            int currentP = rchTxtWindow.SelectionStart;
            do
            {
                indx = rchTxtWindow.Text.IndexOfAny(metaChar, indx + 1);
                if (indx > -1)
                {
                    rchTxtWindow.Select(indx, 1);
                    rchTxtWindow.SelectionFont = englishFont;
                }

            } while (indx != -1);

            rchTxtWindow.SelectionStart = currentP;
        }

      
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

    
        public  void Save(string filePath)
        {
            string str = rchTxtWindow.Text;
            rchTxtWindow.SaveFile(filePath, RichTextBoxStreamType.PlainText);
        }

        public void Refresh()
        {
            rchTxtWindow.SelectAll();
            rchTxtWindow.Font = hindiFont;
            UpdateFont();
    
        }

        public void UnDo()
        {
            rchTxtWindow.Undo();
        }

        public void ReDO()
        {
            rchTxtWindow.Redo();
        }

        public void OpenFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                rchTxtWindow.Text = sr.ReadToEnd();
            }

            Refresh();
        }
    
    
    }
}