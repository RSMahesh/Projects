import javax.swing.*;
 import java.awt.*;
 import java.awt.event.*;
  import javax.swing.border.*;

import javax.swing.JTabbedPane;
class inframe extends JFrame
{

JDesktopPane  desktop = new JDesktopPane();
public inframe()
{
	//In the constructor of InternalFrameDemo, a JFrame subclass:
		setSize(650,400);
	setTitle("JTable");
	   createFrame(); //Create first window
	   setContentPane(desktop);

	   //Make dragging a little faster but perhaps uglier.
	    desktop.setDragMode(JDesktopPane.OUTLINE_DRAG_MODE);
}
	protected void createFrame() {
	    MyInternalFrame frame = new MyInternalFrame();
	    frame.setVisible(true); //necessary as of 1.3
	    desktop.add(frame);
	    try {
	        frame.setSelected(true);
	    } catch (java.beans.PropertyVetoException e) {}
	}



		public static void main(String s[])
		{
		inframe ff=new inframe();
		ff.setLocation(137,90);
		ff.show();
		}



}





class MyInternalFrame extends JInternalFrame implements ActionListener
{

//In the constructor of MyInternalFrame, a JInternalFrame subclass:
static int openFrameCount = 0;
static final int xOffset = 30, yOffset = 30;
	 TextField Text1;
	 JButton Command1;
	 JButton Command2;
	JTabbedPane Mytab;
	public MyInternalFrame() {
	    super("mahesh",
	          true, //resizable
	          true, //closable
	          true, //maximizable
	          true);//iconifiable
	    //...Create the GUI and put it in the window...
	    //...Then set the window size or call pack...
	    //...
	    //Set the window's location.

 JComponent c = (JComponent) this.getContentPane();
     //c.add(new JButton(), BorderLayout.NORTH);
     //c.add(new JButton(), BorderLayout.CENTER);
	c.setLayout(null);

	 Mytab=new JTabbedPane(2,0);
	 Mytab.setBounds(10,10,200,200);
	 c.add(Mytab);

	 Text1 = new TextField("Text1");
	Text1.setBounds(8,16,225,25);
	Mytab.add(Text1,0);
	 Command1 = new JButton("1");
	 Command1.setBounds(8,160,41,33);
	Mytab.add(Command1,0);
	 Command2 = new JButton("2");
	 Command2.setBounds(56,160,41,33);
	Mytab.add(Command2);


 Command2.addActionListener(this);
	 Command2.addActionListener(this);

setSize(400,500);
	//setTitle("JTable");
	    setLocation(xOffset*openFrameCount, yOffset*openFrameCount);
	}



 public void actionPerformed(ActionEvent evt)
	{
	 Object source=evt.getSource();
	 if (source==Command1)
	 {
	 Text1.setText(""+(Text1.getText()+"1"));
	 }


}

}