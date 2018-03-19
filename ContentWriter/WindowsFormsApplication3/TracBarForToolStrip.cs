using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace WindowsFormsApplication3
{
    /// <summary>
    /// Adds trackbar to toolstrip stuff
    /// </summary>
    [
    ToolStripItemDesignerAvailability
        (ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)
    ]

    public class ToolStripTraceBarItem : ToolStripControlHost
    {
        public TrackBar tb;
        public ToolStripTraceBarItem() : base(new TrackBar())
        {
            tb = (TrackBar)this.Control;
        }
    }
}
