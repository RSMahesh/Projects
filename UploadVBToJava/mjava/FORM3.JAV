import javax.swing.*; 
 import java.awt.*; 
 import java.awt.event.*;
  import javax.swing.border.*;
  import VbtoJava.VBfunction;class Form3 extends JFrame 
 { 
 	 JPanel p1=new VBtoJavaForm(); 
 	 public Form3()
 	{ 
	setSize(251,263);
	setTitle("Form3");
	Container cp=getContentPane();cp.add(p1);
	  addWindowListener(new WindowAdapter()
	 {public void windowClosing(WindowEvent e){System.exit(0);}});
	
	}
	public static void main(String s[])
	{
	Form3 ff=new Form3();
	ff.setLocation(4,23);
	ff.show();
	}
 }  
class mypanel extends JPanel
{
	Border etched;
	Border title;
	
	 JPanel Frame1;
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
	 public mypanel()
	{
	etched=BorderFactory.createEtchedBorder();
	 setLayout(null);title=BorderFactory.createTitledBorder(etched,"Calculator");
	 
	 Frame1 = new JPanel(null);
	Frame1.setBounds(0,0,249,257);Frame1.setBorder(title);
	this.add(Frame1);
	 Text1 = new TextField("Text1");
	 Text1.setBounds(16,24,225,25);
	Frame1.add(Text1);
	 Command1 = new JButton("1");
	 Command1.setBounds(16,168,41,33);
	Frame1.add(Command1);
	 Command2 = new JButton("2");
	 Command2.setBounds(64,168,41,33);
	Frame1.add(Command2);
	 Command3 = new JButton("3");
	 Command3.setBounds(117,168,41,33);
	Frame1.add(Command3);
	 Command4 = new JButton("4");
	 Command4.setBounds(16,125,41,33);
	Frame1.add(Command4);
	 Command5 = new JButton("5");
	 Command5.setBounds(64,128,41,33);
	Frame1.add(Command5);
	 Command6 = new JButton("6");
	 Command6.setBounds(117,128,41,33);
	Frame1.add(Command6);
	 Command7 = new JButton("7");
	 Command7.setBounds(16,80,41,33);
	Frame1.add(Command7);
	 Command8 = new JButton("8");
	 Command8.setBounds(64,80,41,33);
	Frame1.add(Command8);
	 Command9 = new JButton("9");
	 Command9.setBounds(117,80,41,33);
	Frame1.add(Command9);
	 Command10 = new JButton("+");
	 Command10.setBounds(176,216,41,33);
	Frame1.add(Command10);
	 Command11 = new JButton("-");
	 Command11.setBounds(176,168,41,33);
	Frame1.add(Command11);
	 Command12 = new JButton("=");
	 Command12.setBounds(117,216,41,33);
	Frame1.add(Command12);
	 Command13 = new JButton("*");
	 Command13.setBounds(176,128,41,33);
	Frame1.add(Command13);
	 Command14 = new JButton("/");
	 Command14.setBounds(176,80,41,33);
	Frame1.add(Command14);
	 Command15 = new JButton("0");
	 Command15.setBounds(16,216,41,33);
	Frame1.add(Command15);
	 Command16 = new JButton();
	 Command16.setBounds(64,216,41,33);
	Frame1.add(Command16);
	}
} class buttonaction extends mypanel implements ActionListener
	{
	 public buttonaction()
	{
	 Command1.addActionListener(this);
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
	 if (source==Command15)
	 {
	  Text1.setText(""+(Text1.getText()+"0"));
	 }
	 if (source==Command16)
	 {
	  Text1.setText(""+(Text1.getText()));
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
	  class VBtoJavaForm extends buttonaction 
	{ public VBtoJavaForm(){ }
	 }