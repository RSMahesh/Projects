using EventPublisher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MenuItem = System.Windows.Controls.MenuItem;
using RichTextBox = System.Windows.Controls.RichTextBox;


namespace WindowsFormsApplication3
{
    class ContextMenueWpfRichTextBox
    {
        ContextMenu contextMenu = new ContextMenu();
        MenuItem menuItemSynonyms;
        RichTextBox richTextBox;
        SynonymProvider synonymProvider;
        AppContext appContext;
        public ContextMenueWpfRichTextBox(RichTextBox richTextBox, AppContext appContext)
        {
            this.appContext = appContext;
            this.richTextBox = richTextBox;
            this.synonymProvider = appContext.synonymProvider;
            richTextBox.ContextMenu = contextMenu;
            richTextBox.ContextMenuOpening += RichTextBox_ContextMenuOpening;
        }

        void AddContextMenu()
        {
            contextMenu.Items.Clear();
            GetCurrentSpellSuggestion();
            var enabled = !string.IsNullOrEmpty(richTextBox.Selection.Text);

            AddMenuItem("Cut", () => { richTextBox.Cut(); }, enabled);
            AddMenuItem("Copy", () => { richTextBox.Copy(); }, enabled);
            AddMenuItem("Paste", () => { richTextBox.Paste(); }, true);
            AddMenuItem("Delete", () => { richTextBox.Cut(); }, enabled);

            contextMenu.Items.Add(new Separator());

            menuItemSynonyms = new MenuItem();
            menuItemSynonyms.Header = "Synonyms";
            menuItemSynonyms.IsEnabled = enabled;
            menuItemSynonyms.GotFocus += MenuItemSynonyms_GotFocus;
            contextMenu.Items.Add(menuItemSynonyms);

            contextMenu.Items.Add(new Separator());

            AddMenuItem("Editor", () => { new PopUpEditor(appContext).Show(); }, true);

            //contextMenu.MenuItems.Add("Cut", OnCut).Name = "Cut";
            //contextMenu.MenuItems.Add("Copy", OnCopy).Name = "Copy";
            //contextMenu.MenuItems.Add("Paste", OnPast).Name = "Paste";
            //contextMenu.MenuItems.Add("Delete", OnCut).Name = "Delete";

            //contextMenu.MenuItems.Add("-");
            //menuItemSynonyms = contextMenu.MenuItems.Add("Synonyms");
            //menuItemSynonyms.Name = "Synonyms";
            //menuItemSynonyms.Select += MenuItemSynonyms_Select;

            //contextMenu.MenuItems.Add("-");
            //contextMenu.MenuItems.Add("Editor", OnEditor).Name = "Editor";
            //contextMenu.MenuItems.Add("-");

            //contextMenu.MenuItems.Add("Find", OnFind).Name = "Find";
            //contextMenu.MenuItems.Add("Filter", OnFilter).Name = "Filter";

            //MenuItem searchMenu = new MenuItem("Search");
            //searchMenu.Name = "Search";

            //searchMenu.MenuItems.Add("Backup", ExcuteHandlerWithWaitCursor).Name = "SearchBackup";
            //searchMenu.MenuItems.Add(new MenuItem("Internet", SearchInternet));

            //contextMenu.MenuItems.Add(searchMenu);
            //appContext.dataGridView.ContextMenu = contextMenu;
        }

        private void RichTextBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            AddContextMenu();
        }
        public IEnumerable<string> GetCurrentSpellSuggestion()
        {
            List<string> suggestions = new List<string>();
            var splError = richTextBox.GetSpellingError(richTextBox.CaretPosition);

            if (splError != null)
            {
                foreach (string suggestion in splError.Suggestions)
                {
                    var menuItem = AddMenuItem(suggestion, () => { splError.Correct(suggestion); }, true);
                    menuItem.FontWeight = FontWeights.UltraBold;
                }

                if (splError.Suggestions.Any())
                {
                    contextMenu.Items.Add(new Separator());
                }

                AddMenuItem("Ignore", () => { splError.IgnoreAll(); }, true);
                contextMenu.Items.Add(new Separator());
            }

            return suggestions;
        }

        private void MenuItemSynonyms_GotFocus(object sender, RoutedEventArgs e)
        {
            if (menuItemSynonyms.Items.Count > 0 && menuItemSynonyms.Tag.ToString() == richTextBox.Selection.Text)
            {
                return;
            }

            menuItemSynonyms.Tag = richTextBox.Selection.Text;

            synonymProvider.AddSynoms(richTextBox.Selection.Text,
                  menuItemSynonyms, OnSynonymClick);

        }

        private void OnSynonymClick(string text)
        {
            this.richTextBox.Selection.Text = text;
        }

        private MenuItem AddMenuItem(string name, Action action, bool enabled, bool addAtStart = false)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Header = name;
            menuItem.IsEnabled = enabled;

            menuItem.Click += new RoutedEventHandler(delegate (Object o, RoutedEventArgs a)
            {
                action();
            });

            contextMenu.Items.Insert(addAtStart ? 0 : contextMenu.Items.Count, menuItem);

            return menuItem;
        }
    }
}
