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
using UserActivityLogger;

namespace Recorder
{
    public partial class CommentsScreen : Form
    {
        QueueWithCapacity _queueWithCapacity = new QueueWithCapacity(80);
        public CommentsScreen()
        {
            InitializeComponent();
        }

        private void CommentsScreen_Load(object sender, EventArgs e)
        {
            EventContainer.SubscribeEvent(RecordSession.Events.OnCommentsFetched.ToString(), OnCommentsFetched);
        }

        private void OnCommentsFetched(EventArg eventArg)
        {
            if (eventArg.Arg == null)
                return;

            string comments =eventArg.Arg.ToString();

            if (String.IsNullOrEmpty(comments))
            {
                txtKeysLogged.Text = string.Empty;
                return;
            }

            KeyProcessor processor = new KeyProcessor();

            var processedKeyData = processor.ProcessKeys(comments);
            txtKeysLogged.Text += processedKeyData.ProcessedData + processedKeyData.UnProcessedData;
            _queueWithCapacity.Add(processedKeyData.ProcessedData);
            txtCurrentText.Text = _queueWithCapacity.GetText();
            txtKeysLogged.SelectionStart = txtKeysLogged.Text.Length;
            txtKeysLogged.ScrollToCaret();
        }
    }
}
