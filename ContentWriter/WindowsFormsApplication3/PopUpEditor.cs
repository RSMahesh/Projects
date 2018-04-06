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
using System.Speech.AudioFormat;
using System.IO;

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
            this.trackBar1.Maximum = 10;
            this.trackBar1.Minimum = -10;
         
            this.Resize += PopUpEditor_Resize;
            this.trackBar1.ValueChanged += TrackBar1_ValueChanged;
            this.AutoScroll = true;
            EventContainer.SubscribeEvent(EventPublisher.Events.RichTextBoxTextChanged.ToString(), RichTextBoxTextChanged);
            AddRichTextBox();
            BackColor = appContext.Theme.BackGroundColor;
            TextLength = appContext.dataGridViewTextBoxEditing.Text.Length;
            appContext.dataGridView.EndEdit();
            GetVoices();

        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            Restart();
        }

        private void Restart()
        {
            speechSynthesizerObj.Pause();
            speechSynthesizerObj.Dispose();
            speechSynthesizerObj = new SpeechSynthesizer();
            Toggle();
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
           // this.toolStripStatusLabel1.Text = "Characters : " + len.ToString();
        }

        private void PopUpEditor_Resize(object sender, EventArgs e)
        {
            this.panel1.Width = this.Width - 30;
            this.panel1.Height = this.Height - 70;
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

        //private void Speak()
        //{
        //    speechSynthesizerObj.Dispose();

        //    if (richTextBox.Text != "")
        //    {
        //        speechSynthesizerObj = new SpeechSynthesizer();
        //      //  speechSynthesizerObj.Rate = this.toolStripTraceBarItem1.tb.Value;
        //        speechSynthesizerObj.SpeakAsync(richTextBox.Text);
        //    }
        //}

        private void Toggle()
        {
            if (speechSynthesizerObj.State == SynthesizerState.Speaking)
            {
                speechSynthesizerObj.Pause();
                btnPlayPause.Text = "Speak";
            }
            else if (speechSynthesizerObj.State == SynthesizerState.Paused)
            {
                speechSynthesizerObj.Resume();
                btnPlayPause.Text = "Pause";
            }
            else if(speechSynthesizerObj.State == SynthesizerState.Ready)
            {
                speechSynthesizerObj.Dispose();

                if (richTextBox.Text != "")
                {
                    speechSynthesizerObj = new SpeechSynthesizer();
                    speechSynthesizerObj.Rate = this.trackBar1.Value;
                    SetVoice();
                    speechSynthesizerObj.SpeakAsync(richTextBox.Text);
                    btnPlayPause.Text = "Pause";
                }
            }
        }
      
        private void PopUpEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.speechSynthesizerObj.Pause();
            this.speechSynthesizerObj = null;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GetVoices();
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
            Grammar dictationGrammar = new DictationGrammar();
            recognizer.LoadGrammar(dictationGrammar);
            try
            {
                //button1.Text = "Speak Now";
                recognizer.SetInputToDefaultAudioDevice();
            //    recognizer.SpeechDetected += Recognizer_SpeechDetected;
                RecognitionResult result = recognizer.Recognize();
           //     button1.Text = result.Text;
            }
            catch (InvalidOperationException exception)
            {
             //   button1.Text = String.Format("Could not recognize input from default aduio device. Is a microphone or sound card available?\r\n{0} - {1}.", exception.Source, exception.Message);
            }
            finally
            {
                recognizer.UnloadAllGrammars();
            }

        }
        
        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            Toggle();
        }

      
        private void GetVoices()
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                foreach (InstalledVoice voice in synth.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    string AudioFormats = "";
                    foreach (SpeechAudioFormatInfo fmt in info.SupportedAudioFormats)
                    {
                        AudioFormats += String.Format("{0}\n",
                        fmt.EncodingFormat.ToString());
                    }
                    comboBox1.Items.Add(info.Gender);
                }
            }
            comboBox1.Text = "Change Voice";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Restart();
        }

        private void SetVoice()
        {
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString().Equals("Male", StringComparison.OrdinalIgnoreCase))
            {
                speechSynthesizerObj.SelectVoiceByHints(VoiceGender.Male);
            }
            else
            {
                speechSynthesizerObj.SelectVoiceByHints(VoiceGender.Female);
            }
        }
    }
}
