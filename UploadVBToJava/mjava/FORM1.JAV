import javax.swing.*; 
 import java.awt.*; 
 import java.awt.event.*;
  import javax.swing.border.*;
class Form1 extends JFrame 
 { 
 	 JPanel p1=new mypanel(); 
 	 public Form1()
 	{ 
	setSize(415,367);
	setTitle("Form1");
	Container cp=getContentPane();cp.add(p1);
	  addWindowListener(new WindowAdapter()
	 {public void windowClosing(WindowEvent e){System.exit(0);}});
	
	}
	public static void main(String s[])
	{
	Form1 ff=new Form1();
	ff.setLocation(137,90);
	ff.show();
	}
 }  
class mypanel extends JPanel
{
	Border etched;
	Border title;
	
	 JPanel Frame1;
	 JButton Command14;
	 JButton Command13;
	 JButton Command12;
	 JButton Command11;
	 JButton Command10;
	 JButton Command9;
	 JButton Command8;
	 JButton Command7;
	 JButton Command6;
	 JButton Command5;
	 JButton Command4;
	 JButton Command3;
	 JButton Command2;
	 JButton Command1;
	 TextField Text1;
	 public mypanel()
	{
	etched=BorderFactory.createEtchedBorder();
	 setLayout(null);title=BorderFactory.createTitledBorder(etched,"Calculator");
	 
	 Frame1 = new JPanel(null);
	Frame1.setBounds(104,32,249,321);Frame1.setBorder(title);
	this.add(Frame1);
	 Command14 = new JButton("/");
	 Command14.setBounds(88,264,65,17);
	Frame1.add(Command14);
	 Command13 = new JButton("*");
	 Command13.setBounds(24,264,65,17);
	Frame1.add(Command13);
	 Command12 = new JButton("=");
	 Command12.setBounds(160,256,65,17);
	Frame1.add(Command12);
	 Command11 = new JButton("-");
	 Command11.setBounds(88,248,65,17);
	Frame1.add(Command11);
	 Command10 = new JButton("+");
	 Command10.setBounds(24,248,65,17);
	Frame1.add(Command10);
	 Command9 = new JButton("9");
	 Command9.setBounds(160,176,73,33);
	Frame1.add(Command9);
	 Command8 = new JButton("8");
	 Command8.setBounds(88,176,73,33);
	Frame1.add(Command8);
	 Command7 = new JButton("7");
	 Command7.setBounds(16,176,73,33);
	Frame1.add(Command7);
	 Command6 = new JButton("6");
	 Command6.setBounds(160,120,73,33);
	Frame1.add(Command6);
	 Command5 = new JButton("5");
	 Command5.setBounds(88,120,73,33);
	Frame1.add(Command5);
	 Command4 = new JButton("4");
	 Command4.setBounds(16,120,73,33);
	Frame1.add(Command4);
	 Command3 = new JButton("3");
	 Command3.setBounds(160,80,73,33);
	Frame1.add(Command3);
	 Command2 = new JButton("2");
	 Command2.setBounds(88,80,73,33);
	Frame1.add(Command2);
	 Command1 = new JButton("1");
	 Command1.setBounds(16,80,73,33);
	Frame1.add(Command1);
	 Text1 = new TextField("Text1");
	 Text1.setBounds(16,24,225,33);
	Frame1.add(Text1);
	}
}