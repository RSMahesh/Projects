using EventPublisher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class ContextMenueColumnHeader
    {
        ContextMenu contextMenu = new ContextMenu();
        AppContext appContext;
        MenuItem menuItemSynonyms;
        public ContextMenueColumnHeader(AppContext appContext)
        {
            this.appContext = appContext;
            AddContextMenu();
        }
        public void ShowMenu(Point p)
        {
          //  DisableMenuItemsIfRequired();
            contextMenu.Show(appContext.dataGridView, p);
        }

      

        void AddContextMenu()
        {
           // contextMenu.MenuItems.Add("Forze", OnCut).Name = "Cut";
            contextMenu.MenuItems.Add("Show Character Count", OnCopy).Name = "Copy";
          //  contextMenu.MenuItems.Add("Show Word Frequency", OnPast).Name = "Paste";
            appContext.dataGridView.ContextMenu = contextMenu;
        }

        private void MenuItemSynonyms_Select(object sender, EventArgs e)
        {
            appContext.synonymProvider.AddSynoms(appContext.dataGridViewTextBoxEditing.SelectedText,
                   menuItemSynonyms, OnSynonymClick);
        }

        private void OnSynonymClick(object sender, EventArgs e)
        {
            var menuIltem = (MenuItem)sender;
            appContext.dataGridViewTextBoxEditing.Paste(menuIltem.Text);
        }

        void SearchBackup(object sender, EventArgs e)
        {
            var searchText = GetSearchText();

            if (string.IsNullOrEmpty(searchText))
            {
                return;
            }

            EventContainer.PublishEvent
(Events.SearchTextInBackUp.ToString(), new EventArg(Guid.NewGuid(), searchText));

        }

        private string GetSearchText()
        {

            return appContext.dataGridViewTextBoxEditing.SelectedText.Trim();
        }

        void SearchInternet(object sender, EventArgs e)
        {
            var searchText = GetSearchText();

            if (string.IsNullOrEmpty(searchText))
            {
                return;
            }
            Process.Start("https://www.google.com/search?q=" + searchText);
        }

        void OnFilter(object sender, EventArgs e)
        {
            Filter filter = new Filter(appContext);

            filter.WindowState = FormWindowState.Normal;
            filter.Show();
            var text = appContext.dataGridViewTextBoxEditing.SelectedText;
            filter.ColumnToFilter = appContext.dataGridView.CurrentCell.OwningColumn.Name;
            filter.TextToFilter = text;
            filter.Operation = "Contains";

        }

        void OnFind(object sender, EventArgs e)
        {
            var text = appContext.dataGridViewTextBoxEditing.SelectedText;
            FindText frm = new FindText(appContext);
            frm.FindString = text;
            frm.TopMost = true;
            frm.Show();
            frm.FindNext();
        }

        void OnEditor(object sender, EventArgs e)
        {
            PopUpEditor frm = new PopUpEditor(appContext);
            frm.Show();
        }

        void OnCut(object sender, EventArgs e)
        {
            appContext.dataGridViewTextBoxEditing.Cut();
        }
        void OnCopy(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
(EventPublisher.Events.ShowCharacterCountForColumn.ToString(), new EventArg(Guid.NewGuid(), null));

        }
        void OnPast(object sender, EventArgs e)
        {
            appContext.dataGridViewTextBoxEditing.Paste();
        }
    }
}
