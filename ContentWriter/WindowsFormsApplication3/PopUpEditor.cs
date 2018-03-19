using EventPublisher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace WindowsFormsApplication3
{
    partial class PopUpEditor : Form
    {
        SpeechSynthesizer speechSynthesizerObj;
        AppContext appContext;
        WpfRichTextBox richTextBox;
        public PopUpEditor(AppContext appContext)
        {
            this.appContext = appContext;
            InitializeComponent();
            speechSynthesizerObj = new SpeechSynthesizer();
            TopMost = true;
        }

        private void PopUpEditor_Load(object sender, EventArgs e)
        {

            this.toolStripTraceBarItem1.tb.Maximum = 10;
            this.toolStripTraceBarItem1.tb.Minimum = -10;
            this.toolStripTraceBarItem1.tb.ValueChanged += Tb_ValueChanged;

            this.Resize += PopUpEditor_Resize;
            //this.panel1.AutoScroll = true;
            this.AutoScroll = true;
            this.toolStrip1.BackColor = Color.Black;
            //this.toolStrip1.Visible = false;
            this.statusStrip1.Visible = false;
            EventContainer.SubscribeEvent(EventPublisher.Events.RichTextBoxTextChanged.ToString(), RichTextBoxTextChanged);
            AddRichTextBox();
            BackColor = appContext.Theme.BackGroundColor;
            TextLength = appContext.dataGridViewTextBoxEditing.Text.Length;
            appContext.dataGridView.EndEdit();

        }

        private void AddRichTextBox()
        {
            richTextBox = new WpfRichTextBox(this.panel1);
            richTextBox.BackgroundColor = Utility.ToMediaColor(appContext.Theme.BackGroundColor);
            richTextBox.FontSize = 18;
            richTextBox.Text = appContext.dataGridViewTextBoxEditing.Text;
            richTextBox.LineSpacing = 30;
        }

        private void RichTextBoxTextChanged(EventArg arg)
        {
            ShowCharLen(arg.Arg.ToString().Length);
        }

        private void ShowCharLen(int len)
        {
            this.toolStripStatusLabel1.Text = "Characters : " + len.ToString();
        }

        private void PopUpEditor_Resize(object sender, EventArgs e)
        {
            this.panel1.Width = this.Width - 30;
            this.panel1.Height = this.Height - 70;
            this.panel1.BorderStyle = BorderStyle.None;
        }

        public Panel TextBoxPanel
        {
            get
            {
                return panel1;
            }
        }

        public int TextLength
        {
            set
            {
                ShowCharLen(value);
            }
        }
       

        private void PalyPause_Click(object sender, EventArgs e)
        {
            speechSynthesizerObj.Dispose();

            if (richTextBox.Text != "")
            {
                speechSynthesizerObj = new SpeechSynthesizer();
                speechSynthesizerObj.Rate = this.toolStripTraceBarItem1.tb.Value;
                speechSynthesizerObj.SpeakAsync(richTextBox.Text);
            }
        }

        private void Pause_Click(object sender, System.EventArgs e)
        {
            if (speechSynthesizerObj.State == SynthesizerState.Speaking)
            {
                speechSynthesizerObj.Pause();
            }
            else if (speechSynthesizerObj.State == SynthesizerState.Paused)
            {
                speechSynthesizerObj.Resume();
            }

        }


        private void toolStripTraceBarItem1_Click(object sender, EventArgs e)
        {

        }

        private void Tb_ValueChanged(object sender, EventArgs e)
        {
            speechSynthesizerObj.Rate = this.toolStripTraceBarItem1.tb.Value;
        }

        private void PopUpEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.speechSynthesizerObj.Pause();
            this.speechSynthesizerObj = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
              }

        string abc = "";
        private void Recognizer_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
            Grammar dictationGrammar = new DictationGrammar();
            recognizer.LoadGrammar(dictationGrammar);
            try
            {
                button1.Text = "Speak Now";
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.SpeechDetected += Recognizer_SpeechDetected;
                RecognitionResult result = recognizer.Recognize();
                button1.Text = result.Text;
            }
            catch (InvalidOperationException exception)
            {
                button1.Text = String.Format("Could not recognize input from default aduio device. Is a microphone or sound card available?\r\n{0} - {1}.", exception.Source, exception.Message);
            }
            finally
            {
                recognizer.UnloadAllGrammars();
            }

        }
    }
}
