using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsControlLibrary1;

namespace WindowsFormsApplication3
{
    public partial class LayOutSetting : Form
    {
        List<CheckBox> _inputs = new List<CheckBox>();
       public static List<ControlPostion> left = new List<ControlPostion>();
       public static List<ControlPostion> right = new List<ControlPostion>();
       
       public static DataColumnCollection _columns;
        public LayOutSetting()
        {
          
            InitializeComponent();
          
            rightPanel.CellBorderStyle = leftPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

        }

    private void InitKayOut()
        {
            if(left.Count ==0 && right.Count ==0)
            {
                for(var i =0; i < _columns.Count; i++)
                {
                    var pp = new ControlPostion();
                    pp.Index = i;
                    pp.Row = left.Count;
                    pp.dataColumn = _columns[i];

                    left.Add(pp);
                    i= i+1;
                    if (i == _columns.Count)
                        break;

                    pp = new ControlPostion();
                    pp.Index = i;
                    pp.Row = right.Count;
                    pp.dataColumn = _columns[i];

                    right.Add(pp);
                }
            }
        }
      
        private void LayOutSetting_Load(object sender, EventArgs e)
        {
            panel1.AutoScroll = true;
            panel2.AutoScroll = true;
            InitKayOut();
            AddControls();
         
           
        }

        

        private void AddControls()
        {
            var row = 0;
            leftPanel.AutoSize = rightPanel.AutoSize = true;
            foreach(var controlP in left )
            {
                var inputControl = new InputControl(controlP.dataColumn.ColumnName,0,0);
                inputControl.Tag = controlP.Index.ToString();
                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.AutoSize = true;
                panel.Tag = controlP.Index.ToString();

                var chk = new CheckBox();
                chk.CheckedChanged += Chk_Click;
                panel.Controls.Add(chk);
                panel.Controls.Add(inputControl);
                leftPanel.Controls.Add(panel, 0, row);
                leftPanel.RowCount += 1;
                row++;
            }

            row = 0;
            foreach (var controlP in right)
            {
                var inputControl = new InputControl(controlP.dataColumn.ColumnName,0,0);
                inputControl.Tag = controlP.Index.ToString();
                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.AutoSize = true;
                panel.Tag = controlP.Index.ToString();

                panel.Controls.Add(new CheckBox());
                panel.Controls.Add(inputControl);
                rightPanel.Controls.Add(panel, 0, row);
                rightPanel.RowCount += 1;
                row++;
            }

        }
        Panel currentPanel;
        private void Chk_Click(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            currentPanel =(Panel) chk.Parent;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {

        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if(currentPanel.Parent != leftPanel)
            {
                return;
            }
            leftPanel.Controls.Remove(currentPanel);
            rightPanel.RowCount += 1;
            rightPanel.Controls.Add (currentPanel,0, rightPanel.RowCount-1);
          //  parent.Controls.Add(inputControl, col, row);

            //    Control c1 = this.tableLayoutPanel1.GetControlFromPosition(0, 0);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class ControlPostion
    {
        public int Index { get; set; }
        public int Row { get; set; }
       // public int col { get; set; }
        public bool Readonly { get; set; }
        public int TextBoxWidth { get; set; }
        public int TesBoxHieght { get; set; }
        public bool Hidden { get; set; }

        public DataColumn dataColumn { get; set; }



    }
}
