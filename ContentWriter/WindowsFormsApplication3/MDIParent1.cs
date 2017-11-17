using EventPublisher;
using StatusMaker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsControlLibrary1;

namespace WindowsFormsApplication3
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {

                OpenFile(openFileDialog.FileName);
            }
        }

        private string GetDataFile()
        {
            var _dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _dataFolder = Path.Combine(_dataFolder, "FreeLance");

            if (!Directory.Exists(_dataFolder))
            {
                Directory.CreateDirectory(_dataFolder);
            }


            return Path.Combine(_dataFolder, "LastFile.txt");

        }
        private void SaveFileLastOpenFilePath(string filePath)
        {
            File.WriteAllText(GetDataFile(), filePath);
        }

        private string GetLastOpenedFile()
        {
            if (File.Exists(GetDataFile()))
            {
                return File.ReadAllText(GetDataFile());
            }
            return string.Empty;
        }


        string lastFileName = "";
        Form1 form1 = null;
        void OpenFile(string filePath)
        {


            if (SingleInstance.IsFileAlreadyOpen(filePath)) 
            {
                MessageBox.Show("File :" + filePath + " is already opend");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            form1 = new Form1(filePath);
            form1.MdiParent = this;
            form1.FormClosed += Form1_FormClosed;
            form1.Show();

            if (Path.GetExtension(filePath) != ".xml")
            {
                SaveFileLastOpenFilePath(filePath);
                lastFileName = filePath;
            }
            this.Cursor = Cursors.Default;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form1 = (Form)sender;

            form1.Hide();

            form1.Dispose();
            form1 = null;

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

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            this.AllowDrop = true;
            this.DragEnter += MDIParent1_DragEnter;
            this.DragDrop += MDIParent1_DragDrop;

            //OLDBConnection12 connection = new OLDBConnection12(@"D:\\WordList.xlsx");
            //var dt = connection.ExecuteDatatable("Select * from [Sheet1$]");
            //List<string> lst = new List<string>();
            //foreach(DataRow row in dt.Rows)
            //{
            //    lst.Add(row[0].ToString());
            //}
            //InputControl.Words = lst;

           

            // OpenFile(@"D:\11.xlsx");
        }

        private void MDIParent1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void MDIParent1_DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                OpenFile(file);
                return;
            }
        }

        private void changeOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ColumnsOrder();
            frm.Show();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
                (EventPublisher.Events.Save.ToString(), new EventArg(Guid.NewGuid(), null));

        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void tableFullViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backUpDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var backUpFolder = Path.GetDirectoryName(lastFileName) + "\\" +
                       Path.GetFileNameWithoutExtension(lastFileName)
                       + "_dataBackup";

            var files = Directory.GetFiles(backUpFolder, "*.xml").OrderByDescending(x => x.ToString());
            OpenFile(files.FirstOrDefault());
        }

        private void mSWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
        (EventPublisher.Events.OpenWord.ToString(), new EventArg(Guid.NewGuid(), null));

        }

        private void descriptionCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
       (EventPublisher.Events.DescriptionCount.ToString(), new EventArg(Guid.NewGuid(), null));

        }

        private void fullBackUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var backUpFolder = Path.GetDirectoryName(lastFileName) + "\\" +
                      Path.GetFileNameWithoutExtension(lastFileName)
                      + "_dataBackup\\Full";

            var files = Directory.GetFiles(backUpFolder, "*.xml").OrderByDescending(x => x.ToString());
            OpenFile(files.FirstOrDefault());
        }

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
(EventPublisher.Events.ShowFilter.ToString(), new EventArg(Guid.NewGuid(), null));

        }

        private void importBackUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
(EventPublisher.Events.StartDataImport.ToString(), new EventArg(Guid.NewGuid(), null));

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
(EventPublisher.Events.Undo.ToString(), new EventArg(Guid.NewGuid(), null));

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EventContainer.PublishEvent
(EventPublisher.Events.ReDo.ToString(), new EventArg(Guid.NewGuid(), null));
        }

        private void statToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
(EventPublisher.Events.Statistics.ToString(), new EventArg(Guid.NewGuid(), null));


        }

        bool lasteFileLaoded;
        private void MDIParent1_Activated(object sender, EventArgs e)
        {
            if (!lasteFileLaoded)
            {
                lasteFileLaoded = true;

                var lastOpenedFile = GetLastOpenedFile();

                if (!string.IsNullOrEmpty(lastOpenedFile))
                {
                    OpenFile(lastOpenedFile);
                }
            }
        }

        private void formulaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EventContainer.PublishEvent
(EventPublisher.Events.Formula.ToString(), new EventArg(Guid.NewGuid(), null));
        }

        private void sentenceCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
(EventPublisher.Events.SetenceCount.ToString(), new EventArg(Guid.NewGuid(), null));
        }
    
    }
}
