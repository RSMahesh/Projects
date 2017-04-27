import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
//import VBtoJava.VBfunction.*;

//import java.io.*;
//import java.text.*;
import javax.swing.border.*;
class VBfunction
{


public static long Val(String no)

{

long lno;
lno=Long.parseLong(no);

return lno;
}

}

class mm1 extends JFrame
{


	//JPanel p1= new mypane();
	JPanel p2= new mypane1();


public mm1()
{
	setSize(400,400);
	setTitle("mahesh");

   //setBounds(200,200,400,400);

//	p1.add(t);


	Container cp=getContentPane();

//	cp.add(p1);
cp.add(p2);
	addWindowListener(new WindowAdapter()

	{
		public void windowClosing(WindowEvent e){System.exit(0);}});








	}






public static void main(String s[])

{

   Point p;




   JFrame ff= new mm1();



   ff.setLocation(100,100);

   p=ff.getLocation();



   JOptionPane.showMessageDialog(null, "alert",Integer.toString(p.x), JOptionPane.ERROR_MESSAGE);


   ff.show();


   }

   }




class mypane1 extends JPanel implements ActionListener,TextListener



{

TextField t;
TextField t1;
TextField t2;
JButton b;
Border etched;
Border title;
JPanel pp1;
public mypane1 ()
{

 etched=BorderFactory.createEtchedBorder();

 title=BorderFactory.createTitledBorder(etched,"Category");

 t=new TextField("67",20);

 t1=new TextField("68",20);

 t2=new TextField("T2",20);

 b=new JButton(" press     ");

pp1=new JPanel();




setLayout(null);

pp1.setLayout(null);


t.setBounds(20,10,100,30);
t1.setBounds(20,50,100,15);
t2.setBounds(20,100,100,50);
b.setBounds(20,250,100,300
);


pp1.add(t);

pp1.add(t1);
pp1.add(t2);

add(b);

pp1.setBounds(100,100,200,150);


pp1.setBorder(title);

add(pp1);

//t.addTextListener();

b.addActionListener(this);

t.addTextListener(this);

t1.addTextListener(this);

}


public void textValueChanged(TextEvent e)
{

//JOptionPane.showMessageDialog(this, "alert", "alert", JOptionPane.ERROR_MESSAGE);

long n,n1;
long n2;

 try
 {
 n=VBfunction.Val(t.getText());
 n1=VBfunction.Val(t1.getText());

 }


catch(Exception exe)
{

   n=0;n1=0;


}

n2=n*n1;

t2.setText(""+n*n1);

}


public void actionPerformed(ActionEvent evt)
{

if(pp1.isVisible()==true)
pp1.setVisible(false);
else
pp1.setVisible(true);


//JOptionPane.showMessageDialog(this, "alert", "alert", JOptionPane.ERROR_MESSAGE);

}

}




