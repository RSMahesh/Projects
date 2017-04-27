import javax.swing.*; 
 import java.awt.*; 
 import java.awt.event.*;
  import javax.swing.border.*;
  import VbtoJava.VBfunction;class Testing extends JFrame 
 { 
 	 JPanel p1=new VBtoJavaForm(); 
 	 public Testing()
 	{ 
	setSize(379,443);
	setTitle("Testing");
	Container cp=getContentPane();cp.add(p1);
	  addWindowListener(new WindowAdapter()
	 {public void windowClosing(WindowEvent e){System.exit(0);}});
	
	}
	public static void main(String s[])
	{
	Testing ff=new Testing();
	ff.setLocation(4,23);
	ff.show();
	}
 }  
class mypanel extends JPanel
{
	Border etched;
	Border title;
	
	 JPanel Frame1;
	 JPanel Frame6;
	 JCheckBox Check3;
	 JCheckBox Check2;
	 JCheckBox Check1;
	 JPanel Frame5;
	 TextField Text2;
	 JPanel Frame4;
	 TextField Text1;
	 JPanel Frame3;
	 JRadioButton Option3;
	 JRadioButton Option2;
	 JRadioButton Option1;
	 JPanel Frame2;
	 JButton Command1;
	 JList List1;
	 JComboBox Combo1;
	 public mypanel()
	{
	etched=BorderFactory.createEtchedBorder();
	 setLayout(null);title=BorderFactory.createTitledBorder(etched,"Let's See");
	 
	 Frame1 = new JPanel(null);
	Frame1.setBounds(8,0,361,409);Frame1.setBorder(title);
	this.add(Frame1);title=BorderFactory.createTitledBorder(etched,"Have Your Age");
	 
	 Frame6 = new JPanel(null);
	Frame6.setBounds(24,104,313,41);Frame6.setBorder(title);
	Frame1.add(Frame6);
	 Check3 = new JCheckBox("Old",false);
	 Check3.setBounds(208,16,81,17);
	Frame6.add(Check3);
	
	 Check2 = new JCheckBox("Adoloscent",false);
	 Check2.setBounds(104,16,73,17);
	Frame6.add(Check2);
	
	 Check1 = new JCheckBox("Young",false);
	 Check1.setBounds(16,16,57,17);
	Frame6.add(Check1);
	title=BorderFactory.createTitledBorder(etched,"CheckBox Result");
	 
	 Frame5 = new JPanel(null);
	Frame5.setBounds(24,352,313,41);Frame5.setBorder(title);
	Frame1.add(Frame5);
	 Text2 = new TextField("Text2");
	 Text2.setBounds(8,16,297,19);
	Frame5.add(Text2);title=BorderFactory.createTitledBorder(etched,"Radio Result");
	 
	 Frame4 = new JPanel(null);
	Frame4.setBounds(24,304,321,41);Frame4.setBorder(title);
	Frame1.add(Frame4);
	 Text1 = new TextField("Text1");
	 Text1.setBounds(16,16,289,19);
	Frame4.add(Text1);title=BorderFactory.createTitledBorder(etched,"Have Your Choice");
	 
	 Frame3 = new JPanel(null);
	Frame3.setBounds(24,24,321,41);Frame3.setBorder(title);
	Frame1.add(Frame3);
	 ButtonGroup Frame3_G=new ButtonGroup();
	
	 Option3 = new JRadioButton("Gay",false);
	 Option3.setBounds(232,16,41,13);
	Frame3.add(Option3);
	Frame3_G.add(Option3);
	
	 Option2 = new JRadioButton("Gril",true);
	 Option2.setBounds(164,16,41,13);
	Frame3.add(Option2);
	Frame3_G.add(Option2);
	
	 Option1 = new JRadioButton("Boy",false);
	 Option1.setBounds(96,16,41,13);
	Frame3.add(Option1);
	Frame3_G.add(Option1);
	
	 Frame2 = new JPanel(null);
	Frame2.setBounds(24,240,313,49);Frame2.setBorder(etched);
	Frame1.add(Frame2);
	 Command1 = new JButton("Press Me");
	 Command1.setBounds(24,16,281,25);
	Frame2.add(Command1);
	 List1 = new JList(new String[]{"Mahesh","Bailwal",});
	 List1.setBounds(24,152,321,80);
	Frame1.add(List1);
	 Combo1 = new JComboBox(new String[]{"XXXXXXXXXXXXXXXXXXXX","YYYYYYYYYYYYYYYYYYY",});
	 Combo1.setBounds(24,72,321,21);
	Frame1.add(Combo1);
	}
} class buttonaction extends mypanel implements ActionListener
	{
	 public buttonaction()
	{
	 Check1.addActionListener(this);
	 Check2.addActionListener(this);
	 Check3.addActionListener(this);
	 Option1.addActionListener(this);
	 Option2.addActionListener(this);
	 Option3.addActionListener(this);
	 }
	 public void actionPerformed(ActionEvent evt)
	{
	 Object source=evt.getSource();
	 if (source==Check1)
	 {
	 Text2.setText(""+(Text2.getText()+"+Young"));
	 }
	 if (source==Check2)
	 {
	  Text2.setText(""+(Text2.getText()+"+Adoloscent"));
	 }
	 if (source==Check3)
	 {
	  Text2.setText(""+(Text2.getText()+"+Old"));
	 }
	 if (source==Option1)
	 {
	  Text1.setText(""+("BOY"));
	 }
	 if (source==Option2)
	 {
	  Text1.setText(""+("Gril"));
	 }
	 if (source==Option3)
	 {
	  Text1.setText(""+("Gay"));
	 }
	 }
 }
	  class VBtoJavaForm extends buttonaction 
	{ public VBtoJavaForm(){ }
	 }