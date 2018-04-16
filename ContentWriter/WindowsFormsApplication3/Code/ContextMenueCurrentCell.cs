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
    class ContextMenueCurrentCell : BaseContextMenue
    {
        ContextMenu contextMenu = new ContextMenu();
        MenuItem menuItemSynonyms;
        public ContextMenueCurrentCell(AppContext appContext): base(appContext)
        {
            AddContextMenu();
        }
        public void ShowMenu(Point p)
        {
            DisableMenuItemsIfRequired();
            contextMenu.Show(appContext.dataGridView, p);
        }

        private void DisableMenuItemsIfRequired()
        {
            var enabled = !string.IsNullOrEmpty(appContext.dataGridViewTextBoxEditing.SelectedText);
            contextMenu.MenuItems["Cut"].Enabled =
            contextMenu.MenuItems["Copy"].Enabled =
            contextMenu.MenuItems["Delete"].Enabled =
            contextMenu.MenuItems["Synonyms"].Enabled =
            contextMenu.MenuItems["Find"].Enabled =
            contextMenu.MenuItems["Filter"].Enabled =
            contextMenu.MenuItems["Search"].Enabled =
            enabled;
        }

        void AddContextMenu()
        {
            contextMenu.MenuItems.Add("Cut", OnCut).Name = "Cut";
            contextMenu.MenuItems.Add("Copy", OnCopy).Name = "Copy";
            contextMenu.MenuItems.Add("Paste", OnPast).Name = "Paste";
            contextMenu.MenuItems.Add("Delete", OnCut).Name = "Delete";

            contextMenu.MenuItems.Add("-");
            menuItemSynonyms = contextMenu.MenuItems.Add("Synonyms");
            menuItemSynonyms.Name = "Synonyms";
            menuItemSynonyms.Select += MenuItemSynonyms_Select;

            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add("Editor", OnEditor).Name = "Editor";
            contextMenu.MenuItems.Add("-");

            contextMenu.MenuItems.Add("Find", OnFind).Name = "Find";
            contextMenu.MenuItems.Add("Filter", OnFilter).Name = "Filter";

            MenuItem searchMenu = new MenuItem("Search");
            searchMenu.Name = "Search";
         
            searchMenu.MenuItems.Add("Backup", ExcuteHandlerWithWaitCursor).Name = "SearchBackup";
            searchMenu.MenuItems.Add(new MenuItem("Internet", SearchInternet));

            contextMenu.MenuItems.Add(searchMenu);
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
            appContext.dataGridViewTextBoxEditing.Copy();
        }
        void OnPast(object sender, EventArgs e)
        {
            appContext.dataGridViewTextBoxEditing.Paste();
        }
    }
}
