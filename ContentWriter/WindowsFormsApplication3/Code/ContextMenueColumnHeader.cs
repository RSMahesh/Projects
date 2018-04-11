using EventPublisher;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace WindowsFormsApplication3
{
    class ContextMenueColumnHeader : BaseContextMenue
    {
        ContextMenu contextMenu = new ContextMenu();
       int headerColumnIndex;
        public ContextMenueColumnHeader(AppContext appContext):base(appContext)
        {
            AddContextMenu();
        }
        public void ShowMenu(Point p, int headerColumnIndex)
        {
            this.headerColumnIndex = headerColumnIndex;
            UpdateMenu();
            contextMenu.Show(appContext.dataGridView, p);
           
        }
        void AddContextMenu()
        {
            contextMenu.MenuItems.Add("Forze", ExcuteHandlerWithWaitCursor).Name = "ToggleColumnForzing";
            contextMenu.MenuItems.Add("Show Words Frequency", ExcuteHandlerWithWaitCursor).Name = "WordFrequency";
            contextMenu.MenuItems.Add("Show Character Count", ExcuteHandlerWithWaitCursor).Name = "ShowCharacterCount";
            appContext.dataGridView.ContextMenu = contextMenu;
        }

        void UpdateMenu()
        {
            contextMenu.MenuItems["ToggleColumnForzing"].Text = appContext.dataGridView.Columns[this.headerColumnIndex].Frozen ? "UnForze" : "Forze";
        }
        
     
        void ShowCharacterCount(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
(EventPublisher.Events.ShowCharacterCountForColumn.ToString(), new EventArg(this.headerColumnIndex));

        }

        void ToggleColumnForzing(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
(EventPublisher.Events.ToggleColumnForzing.ToString(), new EventArg(this.headerColumnIndex));

        }


        void WordFrequency(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
(EventPublisher.Events.WordsFrequency.ToString(), new EventArg(this.headerColumnIndex));

        }
    }
}
