/* * TabbedPaneDemo.java is a 1.4 example that requires one additional file: * images/middle.gif. */
import javax.swing.event.*;
import javax.swing.JTabbedPane;
import javax.swing.JFrame;
import javax.swing.*;
import java.awt.BorderLayout;
import javax.swing.JPanel;
import java.awt.event.KeyEvent;
import java.awt.Dimension;

import java.awt.GridLayout;
 class TabbedPaneDemo  extends  JTabbedPane implements ChangeListener

{


   	public TabbedPaneDemo()

   {

addChangeListener(this);


   ImageIcon icon = createImageIcon("images/HLPBELL.gif");
   JComponent panel1 = makeTextPanel("Panel #1");
   addTab("Tab 1", icon, panel1, "Does nothing");
   setMnemonicAt(0, KeyEvent.VK_1);
   //********

   //buttonaction bb1=new buttonaction();

   //addChangeListener(this);

   //********

   JComponent panel2 = makeTextPanel("Panel #2");
   addTab("Tab 2", icon, panel2, "Does twice as much nothing");
   setMnemonicAt(1, KeyEvent.VK_2);
   JComponent panel3 = makeTextPanel("Panel #3");
   addTab("Tab 3", icon, panel3, "Still does nothing");
   setMnemonicAt(2, KeyEvent.VK_3);
   JComponent panel4 = makeTextPanel( "Panel #4 (has a preferred size of 410 x 50).");
   panel4.setPreferredSize(new Dimension(410, 50));
   addTab("Tab 4", icon, panel4, "Does nothing at all");
   setMnemonicAt(3, KeyEvent.VK_4);
   }


protected JComponent makeTextPanel(String text)
{
	JPanel panel = new JPanel(false);
JLabel filler = new JLabel(text);
filler.setHorizontalAlignment(JLabel.CENTER);
panel.setLayout(new GridLayout(1, 1));
panel.add(filler); return panel; }
/** Returns an ImageIcon, or null if the path was invalid. */
protected static ImageIcon createImageIcon(String path)
{ java.net.URL imgURL = TabbedPaneDemo.class.getResource(path);
if (imgURL != null) { return new ImageIcon(imgURL); } else
{ System.err.println("Couldn't find file: " + path);
return null;
}
}

   public void stateChanged(ChangeEvent e) {

            JOptionPane.showMessageDialog(null,"BB");

           // fireStateChanged();
        }




public void createAndShowGUI()
{ //Make sure we have nice window decorations.
JFrame.setDefaultLookAndFeelDecorated(true);
//Create and set up the window.
JFrame frame = new JFrame("TabbedPaneDemo");
frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
//Create and set up the content pane.
JComponent newContentPane = new TabbedPaneDemo();
newContentPane.setOpaque(true);
//content panes must be opaque
frame.getContentPane().add(new TabbedPaneDemo(), BorderLayout.CENTER);
//Display the window.


frame.pack();
frame.setVisible(true);
}

}



/* protected ChangeListener createChangeListener()
    {

        //JOptionPane.showMessageDialog(null,"BB");
        return new ModelListener();
    }*/
//}
//***************************************************

class TabbedPaneDemo2
{

public static void
main(String[] args) {
	//Schedule a job for the event-dispatching thread:
	//creating and showing this application's GUI.
	javax.swing.SwingUtilities.invokeLater(new Runnable()
	{

		TabbedPaneDemo dm=new TabbedPaneDemo();

		public void run()
		{
		dm.createAndShowGUI(); } });

		}


		}

//****************************************************

