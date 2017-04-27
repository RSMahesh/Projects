using System;
using System.IO;
using System.Windows.Forms;

using EventPublisher;

namespace WindowsFormsControlLibrary1
{
    public partial class Editor : UserControl
    {
        public Guid Id;

        private bool _leftBoxDirty;

        public Editor(string txtLeftText, string txtRightText)
        {
            InitializeComponent();
            Id = Guid.NewGuid();
            SubScribeEvents();
            txtRight.Text = txtRightText;
            txtLeft.Text = txtLeftText;
            txtLeft_TextChanged(null, null);

        }

        public string LeftTextBoxText{
            get
            {
                return txtLeft.Text;
            }
        }

        public string RightTextBoxText
        {
            get
            {
                return txtRight.Text;
            }
        }


        private void txtLeft_TextChanged(object sender, EventArgs e)
        {
            var eventArg = new EventArg(Id, txtLeft.Text);

            EventContainer.PublishEvent(EventPublisher.Events.LeftTextBoxChanged.ToString(), eventArg);
        }

        private void txtRight_TextChanged(object sender, EventArgs e)
        {

        }

        private void SubScribeEvents()
        {
            EventContainer.SubscribeEvent(EventPublisher.Events.SetRightTextBoxText.ToString(), SetRightTextBoxText);
            EventContainer.SubscribeEvent(EventPublisher.Events.SetBottomTextBoxText.ToString(), SetBottomTextBoxText);
            EventContainer.SubscribeEvent(EventPublisher.Events.SaveLeftTextBoxChanges.ToString(), SaveLeftBoxText);
        }


        private void SaveLeftBoxText(EventArg eventArg)
        {
          
        }

        private void SetRightTextBoxText(EventArg eventArg)
        {
            if (!this.ValidEvent(eventArg))
            {
                return;
            }

            BeginInvoke((Action)(() => txtRight.Text = eventArg.Arg.ToString()));
        }

        private void SetBottomTextBoxText(EventArg eventArg)
        {
            if (!this.ValidEvent(eventArg))
            {
                return;
            }

            BeginInvoke((Action)(() => txtBotttom.Text = eventArg.Arg.ToString()));
        }

        private bool ValidEvent(EventArg eventArg)
        {
            return eventArg.EventId == Id;
        }

        private void txtBotttom_TextChanged(object sender, EventArgs e)
        {

        }

   }
}
