﻿using EventPublisher;
using RecordSession;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recorder
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;
        frmControlBox _frmControlBoxObject;
        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenControlBox();
            EventContainer.PublishEvent(RecordSession.Events.CloseCurrentSession.ToString(), new EventArg(Guid.NewGuid(), e));
        }

     

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

      
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            OpenControlBox();
        }

        private void OpenControlBox()
        {
            if(_frmControlBoxObject != null)
            {
                _frmControlBoxObject.Dispose();
            }

            _frmControlBoxObject = new frmControlBox();
            _frmControlBoxObject.MdiParent = this;
            _frmControlBoxObject.Show();
        }
        private void controlBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmControlBoxObject.WindowState = FormWindowState.Normal;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent(RecordSession.Events.CloseCurrentSession.ToString(), new EventArg(Guid.NewGuid(), e));
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
