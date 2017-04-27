import javax.swing.*; 
 import java.awt.*; 
 import java.awt.event.*;
  import javax.swing.border.*;
  import VbtoJava.VBfunction;class Form2 extends JFrame 
 { 
 	 JPanel p1=new VBtoJavaForm(); 
 	 public Form2()
 	{ 
	setSize(312,213);
	setTitle("Form2");
	Container cp=getContentPane();cp.add(p1);
	  addWindowListener(new WindowAdapter()
	 {public void windowClosing(WindowEvent e){System.exit(0);}});
	
	}
	public static void main(String s[])
	{
	Form2 ff=new Form2();
	ff.setLocation(4,23);
	ff.show();
	}
 }  
class mypanel extends JPanel
{
	Border etched;
	Border title;
	
	 JPanel Frame1;
	 JButton Command1;
	 TextField Text3;
	 TextField Text2;
	 TextField Text1;
	 public mypanel()
	{
	etched=BorderFactory.createEtchedBorder();
	 setLayout(null);title=BorderFactory.createTitledBorder(etched,"Frame1");
	 
	 Frame1 = new JPanel(null);
	Frame1.setBounds(88,112,361,201);Frame1.setBorder(title);
	this.add(Frame1);
	 Command1 = new JButton("Command1");
	 Command1.setBounds(120,144,145,25);
	Frame1.add(Command1);
	 Text3 = new TextField("Text3");
	 Text3.setBounds(112,80,129,41);
	Frame1.add(Text3);
	 Text2 = new TextField("Text2");
	 Text2.setBounds(200,32,137,33);
	Frame1.add(Text2);
	 Text1 = new TextField("Text1");
	 Text1.setBounds(24,32,121,33);
	Frame1.add(Text1);
	}
} class buttonaction extends mypanel implements ActionListener
	{
	 public buttonaction()
	{
	 Command1.addActionListener(this);
	 }
	 public void actionPerformed(ActionEvent evt)
	{
	 Object source=evt.getSource();
	 if (source==Command1)
	 {
	 Text3.setText(""+(VBfunction.Val(Text1.getText())+VBfunction.Val(Text2.getText())));
	 }
	 }
 }
	  class VBtoJavaForm extends buttonaction 
	{ public VBtoJavaForm(){ }
	 }