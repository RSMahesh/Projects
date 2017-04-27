import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
//import java.io.*;
//import java.text.*;
import javax.swing.border.*;

class mm2 extends JFrame
{


 JPanel p1= new JPanel();


public mm2()
{
	setSize(400/15,400/15);
	setTitle("mahesh");

   JButton b=new JButton("press");
   b.setBounds(200/15,200/15,400/15,400/15);

   p1.add(b);


	Container cp=getContentPane();


	cp.add(p1);

	   p1.setLayout(null);

	addWindowListener(new WindowAdapter()

	{
		public void windowClosing(WindowEvent e){System.exit(0);}});


//JOptionPane.showMessageDialog(null, "alert",Integer.toString(p.x), JOptionPane.ERROR_MESSAGE);



	}






public static void main(String s[])

{

   Point p;




   JFrame ff= new mm2();



   ff.setLocation(100,100);






   ff.show();


   }

   }



