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
            outerPanel.AutoSize = true;
            outerPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

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
                    i++;
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
            InitKayOut();
            AddControls();
            outerPanel.AutoScroll = true;
            outerPanel.AutoSize = false;
            outerPanel.Height = 400;
           
        }

        

        private void AddControls()
        {
            var row = 0;
            leftPanel.AutoSize = rightPanel.AutoSize = true;
            foreach(var controlP in left )
            {
                var inputControl = new InputControl(controlP.Index.ToString());
                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.AutoSize = true;
                panel.Tag = "L" + row.ToString();
                
                panel.Controls.Add(new CheckBox());
                panel.Controls.Add(inputControl);
                leftPanel.Controls.Add(panel, 0, row);
                leftPanel.RowCount += 1;
                row++;
            }

            row = 0;
            foreach (var controlP in right)
            {
                var inputControl = new InputControl(controlP.Index.ToString());
                Panel panel = new Panel();
                panel.Tag = "R" + row.ToString();

                panel.Controls.Add(new CheckBox());
                panel.Controls.Add(inputControl);
                rightPanel.Controls.Add(panel, 0, row);
                rightPanel.RowCount += 1;
                row++;
            }

        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {

        }

        private void btnMoveRight_Click(object sender, EventArgs e)
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
