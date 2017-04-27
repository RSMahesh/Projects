/* * TabbedPaneDemo.java is a 1.4 example that requires one additional file: * images/middle.gif. */
import javax.swing.*;
 import java.awt.*;
 import java.awt.event.*;
  import javax.swing.border.*;
  import VbtoJava.VBfunction;
//************************
import javax.swing.JTabbedPane;
import javax.swing.ImageIcon;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JFrame;
import javax.swing.JComponent;
import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.event.KeyEvent;

class TabbedPaneDemo extends JPanel
{ public TabbedPaneDemo()

{ super(new GridLayout(1, 1));
JTabbedPane tabbedPane = new JTabbedPane();
ImageIcon icon = createImageIcon("images/HLPBELL.gif");
JComponent panel1 = makeTextPanel("Panel #1");
tabbedPane.addTab("Tab 1", icon, panel1, "Does nothing");
tabbedPane.setMnemonicAt(0, KeyEvent.VK_1);
//********

buttonaction bb1=new buttonaction();

//********

JComponent panel2 = makeTextPanel("Panel #2");
tabbedPane.addTab("Tab 2", icon, bb1, "Does twice as much nothing");
tabbedPane.setMnemonicAt(1, KeyEvent.VK_2);
JComponent panel3 = makeTextPanel("Panel #3");
tabbedPane.addTab("Tab 3", icon, panel3, "Still does nothing");
tabbedPane.setMnemonicAt(2, KeyEvent.VK_3);
JComponent panel4 = makeTextPanel( "Panel #4 (has a preferred size of 410 x 50).");
panel4.setPreferredSize(new Dimension(410, 50));
tabbedPane.addTab("Tab 4", icon, panel4, "Does nothing at all");
tabbedPane.setMnemonicAt(3, KeyEvent.VK_4);
//Add the tabbed pane to this panel.
add(tabbedPane);
//Uncomment the following line to use scrolling tabs.
//tabbedPane.setTabLayoutPolicy(JTabbedPane.SCROLL_TAB_LAYOUT);
}
protected JComponent makeTextPanel(String text)
{ JPanel panel = new JPanel(false);
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


/** * Create the GUI and show it. For thread safety, * this method should be invoked from the * event-dispatching thread. */
private static void createAndShowGUI()
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
public static void
main(String[] args) {
	//Schedule a job for the event-dispatching thread:
	//creating and showing this application's GUI.
	javax.swing.SwingUtilities.invokeLater(new Runnable()
	{
		public void run()
		{ createAndShowGUI(); } });

		}
		}



//****************************************************


class mypanel extends JPanel
{
	long fvalue;
	String opreation;
	Border etched;
	Border title;

	 TextField Text1;
	 JButton Command1;
	 JButton Command2;
	 JButton Command3;
	 JButton Command4;
	 JButton Command5;
	 JButton Command6;
	 JButton Command7;
	 JButton Command8;
	 JButton Command9;
	 JButton Command10;
	 JButton Command11;
	 JButton Command12;
	 JButton Command13;
	 JButton Command14;
	 JButton Command15;
	 JButton Command16;
	 TextField Text2;
	 TextField Text3;
	 TextField Text4;
	 public mypanel()
	{
	etched=BorderFactory.createEtchedBorder();
	title=BorderFactory.createTitledBorder(etched,"Calculator");


	setBounds(0,0,249,257);
	setBorder(title);

	 setLayout(null);
	 Text1 = new TextField("Text1");
	 Text1.setBounds(8,16,225,25);
	this.add(Text1);
	 Command1 = new JButton("1");
	 Command1.setBounds(8,160,41,33);
	this.add(Command1);
	 Command2 = new JButton("2");
	 Command2.setBounds(56,160,41,33);
	this.add(Command2);
	 Command3 = new JButton("3");
	 Command3.setBounds(109,160,41,33);
	this.add(Command3);
	 Command4 = new JButton("4");
	 Command4.setBounds(8,117,41,33);
	this.add(Command4);
	 Command5 = new JButton("5");
	 Command5.setBounds(56,120,41,33);
	this.add(Command5);
	 Command6 = new JButton("6");
	 Command6.setBounds(109,120,41,33);
	this.add(Command6);
	 Command7 = new JButton("7");
	 Command7.setBounds(8,72,41,33);
	this.add(Command7);
	 Command8 = new JButton("8");
	 Command8.setBounds(56,72,41,33);
	this.add(Command8);
	 Command9 = new JButton("9");
	 Command9.setBounds(109,72,41,33);
	this.add(Command9);
	 Command10 = new JButton("+");
	 Command10.setBounds(168,208,41,33);
	this.add(Command10);
	 Command11 = new JButton("-");
	 Command11.setBounds(168,160,41,33);
	this.add(Command11);
	 Command12 = new JButton("=");
	 Command12.setBounds(109,208,41,33);
	this.add(Command12);
	 Command13 = new JButton("*");
	 Command13.setBounds(168,120,41,33);
	this.add(Command13);
	 Command14 = new JButton("/");
	 Command14.setBounds(168,72,41,33);
	this.add(Command14);
	 Command15 = new JButton("0");
	 Command15.setBounds(8,208,41,33);
	this.add(Command15);
	 Command16 = new JButton(" ");
	 Command16.setBounds(56,208,41,33);
	this.add(Command16);
	 Text2 = new TextField("Text2");
	 Text2.setBounds(40,48,49,19);
	this.add(Text2);
	 Text3 = new TextField("Text3");
	 Text3.setBounds(88,48,49,19);
	this.add(Text3);
	 Text4 = new TextField("Text4");
	 Text4.setBounds(136,48,49,19);
	this.add(Text4);
	}
} class buttonaction extends mypanel implements ActionListener
	{
	 public buttonaction()
	{
	 Command1.addActionListener(this);
	 Command10.addActionListener(this);
	 Command11.addActionListener(this);
	 Command12.addActionListener(this);
	 Command13.addActionListener(this);
	 Command14.addActionListener(this);
	 Command15.addActionListener(this);
	 Command16.addActionListener(this);
	 Command2.addActionListener(this);
	 Command3.addActionListener(this);
	 Command4.addActionListener(this);
	 Command5.addActionListener(this);
	 Command6.addActionListener(this);
	 Command7.addActionListener(this);
	 Command8.addActionListener(this);
	 Command9.addActionListener(this);
	 }
	 public void actionPerformed(ActionEvent evt)
	{
	 Object source=evt.getSource();
	 if (source==Command1)
	 {
	 Text1.setText(""+(Text1.getText()+"1"));
	 }
	 if (source==Command10)
	 {
	  fvalue=VBfunction.Val(Text1.getText());opreation="PLUS";Text1.setText(""+(""));
	 }
	 if (source==Command11)
	 {
	  fvalue=VBfunction.Val(Text1.getText());opreation="MINUS";Text1.setText(""+(""));
	 }
	 if (source==Command12)
	 {
	  if ( opreation=="MINUS" )
{Text1.setText(""+(fvalue-VBfunction.Val(Text1.getText())));}if ( opreation=="PLUS" )
{Text1.setText(""+(fvalue+VBfunction.Val(Text1.getText())));}if ( opreation=="MULTIPLY" )
{Text1.setText(""+(fvalue*VBfunction.Val(Text1.getText())));}if ( opreation=="DIVIDE" )
{Text1.setText(""+(fvalue/VBfunction.Val(Text1.getText())));}
	 }
	 if (source==Command13)
	 {
	  fvalue=VBfunction.Val(Text1.getText());opreation="MULTIPLY";Text1.setText(""+(""));
	 }
	 if (source==Command14)
	 {
	  fvalue=VBfunction.Val(Text1.getText());Text2.setText(""+(Text1.getText()));Text3.setText(""+("/"));opreation="DIVIDE";Text1.setText(""+(""));
	 }
	 if (source==Command15)
	 {
	  Text1.setText(""+(Text1.getText()+"0"));
	 }
	 if (source==Command16)
	 {
	  Text1.setText(""+(""));
	 }
	 if (source==Command2)
	 {
	  Text1.setText(""+(Text1.getText()+"2"));
	 }
	 if (source==Command3)
	 {
	  Text1.setText(""+(Text1.getText()+"3"));
	 }
	 if (source==Command4)
	 {
	  Text1.setText(""+(Text1.getText()+"4"));
	 }
	 if (source==Command5)
	 {
	  Text1.setText(""+(Text1.getText()+"5"));
	 }
	 if (source==Command6)
	 {
	  Text1.setText(""+(Text1.getText()+"6"));
	 }
	 if (source==Command7)
	 {
	  Text1.setText(""+(Text1.getText()+"7"));
	 }
	 if (source==Command8)
	 {
	  Text1.setText(""+(Text1.getText()+"8"));
	 }
	 if (source==Command9)
	 {
	  Text1.setText(""+(Text1.getText()+"9"));
	 }
	 }
 }