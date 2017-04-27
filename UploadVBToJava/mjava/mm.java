import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.border.*;

class mm extends JFrame
{


	JPanel mainp= new mainpane();


public mm()
{
	setSize(400,400);
	setTitle("mahesh");

	Container cp=getContentPane();
	cp.add(mainp);

	addWindowListener(new WindowAdapter()

	{
		public void windowClosing(WindowEvent e){System.exit(0);}});

	}


public static void main(String s[])

{

   JFrame ff= new mm();
   ff.show();


   }

   }



class mypane extends JPanel

{

JButton b;
Border etched;
Border title;
	public mypane (mainpane temp)
	{

 	etched=BorderFactory.createEtchedBorder();

 	title=BorderFactory.createTitledBorder(etched,"Category");

	b=new JButton(" press     ");

	setLayout(null);

	b.setBounds(20,20,110,110);

	add(b);

	setBounds(50,50,150,150);

	setBorder(title);

	b.addActionListener(temp);

	}

}





class mypane1 extends JPanel

{

 JButton b;
Border etched;
Border title;
	public mypane1 (mainpane temp)
	{

 	etched=BorderFactory.createEtchedBorder();

 	title=BorderFactory.createTitledBorder(etched,"Product");

	b=new JButton(" press     ");

	setLayout(null);

	b.setBounds(20,20,110,110);

	add(b);

	setBounds(100,200,150,150);

	setBorder(title);

	b.addActionListener(temp);

	}

}







class mainpane extends JPanel implements ActionListener

{

JPanel pp;
JPanel pp1;
public mainpane()
{

 pp=new mypane(this);

 pp1=new mypane1(this);

add(pp);

add(pp1);

setLayout(null);

}

public void actionPerformed(ActionEvent evt)
{

Object source=evt.getSource();

if (source==pp1.getComponent(0))
{
JOptionPane.showMessageDialog(this,"PRODUCT", "alert", JOptionPane.ERROR_MESSAGE);

}

if (source==pp.getComponent(0))
{
JOptionPane.showMessageDialog(this,"CATEGORY", "alert", JOptionPane.ERROR_MESSAGE);
}
}
}


