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
    class ContextMenueCurrentCell
    {
        ContextMenu contextMenu = new ContextMenu();
        AppContext appContext;
        MenuItem menuItemSynonyms;
        public ContextMenueCurrentCell(AppContext appContext)
        {
            this.appContext = appContext;
            AddContextMenu();
        }
        public void ShowMenu(Point p)
        {
            contextMenu.Show(appContext.dataGridView, p);
        }
        void AddContextMenu()
        {
            contextMenu.MenuItems.Add("Cut", OnCut);
            contextMenu.MenuItems.Add("Copy", OnCopy);
            contextMenu.MenuItems.Add("Past", OnPast);
            contextMenu.MenuItems.Add("Delete", OnCut);


            contextMenu.MenuItems.Add("-");
            menuItemSynonyms = contextMenu.MenuItems.Add("Synonyms");
            menuItemSynonyms.Select += MenuItemSynonyms_Select;
       
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add("Editor", OnEditor);
            contextMenu.MenuItems.Add("-");

            contextMenu.MenuItems.Add("Find", OnFind);

            contextMenu.MenuItems.Add("Filter", OnFilter);

            MenuItem addDevice = new MenuItem("Search");
            addDevice.MenuItems.Add(new MenuItem("Backup", SearchBackup));
            addDevice.MenuItems.Add(new MenuItem("Internet", SearchInternet));

            contextMenu.MenuItems.Add(addDevice);
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
