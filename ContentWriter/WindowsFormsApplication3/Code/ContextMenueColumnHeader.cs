using EventPublisher;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace WindowsFormsApplication3
{
    class ContextMenueColumnHeader
    {
        ContextMenu contextMenu = new ContextMenu();
        AppContext appContext;
        int headerColumnIndex;
        public ContextMenueColumnHeader(AppContext appContext)
        {
            this.appContext = appContext;
            AddContextMenu();
        }
        public void ShowMenu(Point p, int headerColumnIndex)
        {
            //  DisableMenuItemsIfRequired();
            this.headerColumnIndex = headerColumnIndex;
            UpdateMenu();
            contextMenu.Show(appContext.dataGridView, p);
           
        }
        void AddContextMenu()
        {
            contextMenu.MenuItems.Add("Forze", ExcuteHandlerAction).Name = "ToggleColumnForzing";
            contextMenu.MenuItems.Add("Show Words Frequency", ExcuteHandlerAction).Name = "WordFrequency";
            contextMenu.MenuItems.Add("Show Character Count", ExcuteHandlerAction).Name = "ShowCharacterCount";
            //  contextMenu.MenuItems.Add("Show Word Frequency", OnPast).Name = "Paste";
            appContext.dataGridView.ContextMenu = contextMenu;
        }

        void UpdateMenu()
        {
            contextMenu.MenuItems["ToggleColumnForzing"].Text = appContext.dataGridView.Columns[this.headerColumnIndex].Frozen ? "UnForze" : "Forze";
        }
        
        private void CallMethod(string methodName, object sender, EventArgs e)
        {
            MethodInfo methodInfo = this.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo.Invoke(this, new[] { sender, e });
        }


        void ExcuteHandlerAction(object sender, EventArgs e)
        {
            appContext.ChangeCursor(Cursors.WaitCursor);

            var menuItem = (MenuItem)sender;

            CallMethod(menuItem.Name, sender, e);

            appContext.ChangeCursor(Cursors.Default);

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
