import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.border.*;

class PunderP1 extends JFrame
{


	JPanel mainp= new mainpane();


public PunderP1()
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

   JFrame ff= new PunderP();
   ff.show();


   }

   }



class mainpane extends JPanel implements ActionListener

{

Border etched;
Border title;

JPanel pp;
JPanel pp1;

JButton b1,b2;

public mainpane()
{

pp=new JPanel(null);

pp1=new JPanel(null);

b1=new JButton("B1");
b2=new JButton("B2");

b1.setBounds(20,20,20,20);
b2.setBounds(20,20,20,20);

b1.addActionListener(this);
b2.addActionListener(this);

pp.setBounds(20,20,60,60);

pp1.setBounds(100,100,60,60);


pp.add(b1);

pp1.add(b2);

this.add(pp);

this.add(pp1);

	etched=BorderFactory.createEtchedBorder();

 	title=BorderFactory.createTitledBorder(etched,"PP");

		pp.setBorder(title);




	//etched=BorderFactory.createEtchedBorder();

 	title=BorderFactory.createTitledBorder(etched,"PP1");

		pp1.setBorder(title);





setLayout(null);

}

public void actionPerformed(ActionEvent evt)
{

Object source=evt.getSource();

/*Container temp;
temp=(Container) pp1.getComponent(0);

if (source== temp.getComponent(0))
{
JOptionPane.showMessageDialog(this,"PRODUCT", "alert", JOptionPane.ERROR_MESSAGE);

}*/

/*if (source== ((Container)pp1.getComponent(0)).getComponent(0))
{
JOptionPane.showMessageDialog(this,"PRODUCT", "alert", JOptionPane.ERROR_MESSAGE);

}
*/


if (source==b1)
{
//JOptionPane.showMessageDialog(this,"b1", "alert", JOptionPane.ERROR_MESSAGE);

if (pp1.isVisible())
pp1.setVisible(false);
else
pp1.setVisible(true);
}


if (source==b2)
{
//JOptionPane.showMessageDialog(this,"b2", "alert", JOptionPane.ERROR_MESSAGE);

if (pp.isVisible())
pp.setVisible(false);
else
pp.setVisible(true);
}




}






}


