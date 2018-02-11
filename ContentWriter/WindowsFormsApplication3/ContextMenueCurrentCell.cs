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
        public ContextMenueCurrentCell()
        {
            AddContextMenu();
        }
        public void ShowMenu(Point p)
        {
            contextMenu.Show(AppContext.dataGridView, p);
        }
        void AddContextMenu()
        {
            contextMenu.MenuItems.Add("Cut", OnCut);
            contextMenu.MenuItems.Add("Copy", OnCopy);
            contextMenu.MenuItems.Add("Past", OnPast);
            contextMenu.MenuItems.Add("Delete", OnCut);
            MenuItem addDevice = new MenuItem("Search");
            addDevice.MenuItems.Add(new MenuItem("Backup", SearchBackup));
            addDevice.MenuItems.Add(new MenuItem("Internet", SearchInternet));

            contextMenu.MenuItems.Add(addDevice);
            AppContext.dataGridView.ContextMenu = contextMenu;
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
            
            return AppContext.dataGridViewTextBoxEditing.SelectedText.Trim();
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

        void OnCut(object sender, EventArgs e)
        {
            AppContext.dataGridViewTextBoxEditing.Cut();
        }
        void OnCopy(object sender, EventArgs e)
        {
            AppContext.dataGridViewTextBoxEditing.Copy();
        }
        void OnPast(object sender, EventArgs e)
        {
            AppContext.dataGridViewTextBoxEditing.Paste();
        }

    }
}
