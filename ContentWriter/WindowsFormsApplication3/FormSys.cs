using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class FormSys : Form
    {
        ContextMenu contextMenu = new ContextMenu();
        MenuItem menuItemSyno1;
        SynonymProvider synonymProvider = new SynonymProvider();
        public FormSys()
        {
            InitializeComponent();
            menuItemSyno1 = new MenuItem("Syno");
            menuItemSyno1.Select += MenuItemSyno_Select;
            contextMenu.MenuItems.Add(menuItemSyno1);
            tbWord.ContextMenu = contextMenu;
        }

        private void MenuItemSyno_Select(object sender, EventArgs e)
        {
            synonymProvider.AddSynoms(tbWord.Text, menuItemSyno1, OnSynSelected);
            
      //      AddSynoms(menuItemSyno1, OnSynSelected);
        }

        private void OnSynSelected(object sender, EventArgs e)
        {

        }


      

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormSys_Load(object sender, EventArgs e)
        {

        }

        private void tbWord_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbSynonym_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
   
}
