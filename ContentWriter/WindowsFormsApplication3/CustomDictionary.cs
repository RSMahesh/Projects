using EventPublisher;
using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class CustomDictionary : Form
    {
        string customDictionaryFile;
        public CustomDictionary()
        {
            InitializeComponent();
            customDictionaryFile = Path.Combine(Utility.FreeLanceAppDataFolder, "custom-dictionary.lex");
            this.FormClosing += CustomDictionary_FormClosing;
            this.TopMost = true;
        }

        private void CustomDictionary_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            HideUI();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText(customDictionaryFile, textBox1.Text);
            EventContainer.PublishEvent
         (EventPublisher.Events.CustomDictionaryUpdate.ToString(), new EventArg(null));
        }

        public void AddToDictionary(string word)
        {
            File.AppendAllText(customDictionaryFile, Environment.NewLine + word);
            EventContainer.PublishEvent
                   (EventPublisher.Events.CustomDictionaryUpdate.ToString(), new EventArg(null));
        }
        
        public void ShowUI()
        {
            textBox1.Text = File.ReadAllText(customDictionaryFile);
            this.Show();
        }

        public void HideUI()
        {
            this.Hide();
        }

        public Uri CustomDictionaryUri
        {
            get
            {
                if(!File.Exists(customDictionaryFile))
                {
                    AddToDictionary("mmm");
                }

                return new Uri(customDictionaryFile);
            }
        }
    }
}
