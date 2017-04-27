import javax.swing.*; 
 import java.awt.*; 
 import java.awt.event.*;
  import javax.swing.border.*;
  import VbtoJava.VBfunction;class myform extends JFrame 
 { 
 	 JPanel p1=new VBtoJavaForm(); 
 	 public myform()
 	{ 
	setSize(397,213);
	setTitle("Form1");
	Container cp=getContentPane();cp.add(p1);
	  addWindowListener(new WindowAdapter()
	 {public void windowClosing(WindowEvent e){System.exit(0);}});
	
	}
	public static void main(String s[])
	{
	myform ff=new myform();
	ff.setLocation(4,23);
	ff.show();
	}
 }  
class mypanel extends JPanel
{
	Border etched;
	Border title;
	
	 JList List1;
	 JComboBox Combo1;
	 JButton Command2;
	 JButton Command1;
	 TextField Text1;
	 public mypanel()
	{
	etched=BorderFactory.createEtchedBorder();
	 setLayout(null);
	 List1 = new JList(new String[]{"One","Two","Three","Four","Five","Six","Seven",});
	 List1.setBounds(248,24,129,56);
	this.add(List1);
	 Combo1 = new JComboBox(new String[]{"One","Two","Three","Four","Five",});
	 Combo1.setBounds(40,144,161,21);
	this.add(Combo1);
	 Command2 = new JButton("Command2");
	 Command2.setBounds(168,96,121,41);
	this.add(Command2);
	 Command1 = new JButton("Command1");
	 Command1.setBounds(48,96,97,33);
	this.add(Command1);
	 Text1 = new TextField("Text1");
	 Text1.setBounds(56,24,193,33);
	this.add(Text1);
	}
} class buttonaction extends mypanel implements ActionListener
	{
	 public buttonaction()
	{
	 }
	 public void actionPerformed(ActionEvent evt)
	{
	 Object source=evt.getSource();
	 }
 }
	  class VBtoJavaForm extends buttonaction 
	{ public VBtoJavaForm(){ }
	 }