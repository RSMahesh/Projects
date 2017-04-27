	import javax.swing.*;
 	import java.awt.*;
 	import java.awt.event.*;
 	import javax.swing.border.*;
    import java.sql.*;
    import javax.swing.JOptionPane;
    import java.util.Vector;
	import javax.swing.JTable.*;
	import javax.swing.table.*;
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
	 JScrollPane scroll;
	 JScrollPane scroll1;
	 JTable mytable;
	 ResultSet books;
	 Vector col,ro;
	 public mypanel()
	{
	etched=BorderFactory.createEtchedBorder();
	 //setLayout(null);







	      TableModel dataModel = new AbstractTableModel() {
	          public int getColumnCount() { return 10; }
	          public int getRowCount() { return 10;}
	          public Object getValueAt(int row, int col) { return new Integer(row*col); }
	      };
	      JTable table = new JTable(dataModel);
	      JScrollPane scrollpane = new JScrollPane(table);





	// List1 = new JList(new String[]{"One","Two","Three","Four","Five","Six","Seven"});
	 //List1.setBounds(248,24,129,56);

//scroll= new JScrollPane(List1);
//scroll.setBounds(248,24,200,56);

//	this.add(scroll);

//Vector v1= new Vector ("One","Two","Three","Four","Five","Six");
//Vector v2= {"One","Two","Three","Four","Five","Six"};
     connect();

	try{
	 col= getColumnNames(books);
       ro = getRows(books);
}


	        catch (SQLException e){
	            JOptionPane.showMessageDialog(null, e.getMessage());
	        }



	mytable= new JTable(ro,col);
mytable.setAutoResizeMode(0);
//mytable.setBounds(10,30,800,200);

scroll1= new JScrollPane(mytable);
scroll1.setBounds(10,30,100,200);

this.add(scroll1);

//this.add(mytable);





	 Combo1 = new JComboBox(new String[]{"One","Two","Three","Four","Five"});
	 Combo1.setBounds(40,144,161,21);
	  Combo1.setEditable(true);
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




     public void connect()

      {
      try
      {
      Connection connection;
         Class.forName("sun.jdbc.odbc.JdbcOdbcDriver");
         String url = "jdbc:odbc:mahesh";
         String user = "Admin";
         String password = "";
         connection = DriverManager.getConnection(url, user, password);
         System.out.println("Connection made.");

         Statement statement = connection.createStatement(
         ResultSet.TYPE_SCROLL_SENSITIVE,
         ResultSet.CONCUR_UPDATABLE);
         String query = "SELECT * FROM realtemp";
         books = statement.executeQuery(query);




	}

     catch (ClassNotFoundException e){
           JOptionPane.showMessageDialog(null, e.getMessage());
           System.exit(1);
        }
        catch (SQLException e){
            JOptionPane.showMessageDialog(null, e.getMessage());
        }






  }



public static Vector getColumnNames(ResultSet results) throws SQLException{
      Vector columnNames = new Vector();
      ResultSetMetaData metaData = results.getMetaData();
      int columnCount = metaData.getColumnCount();
      for (int i = 1; i <= columnCount; i++)
         columnNames.add(metaData.getColumnName(i));
      return columnNames;
   }

  public static Vector getRows(ResultSet results) throws SQLException{
      Vector rows = new Vector();
      ResultSetMetaData metaData = results.getMetaData();
      int columnCount = metaData.getColumnCount();
      while (results.next()){
         Vector row = new Vector();
         for (int i = 1; i <= columnCount; i++){
            if (metaData.getColumnType(i) == Types.VARCHAR)
               row.add(results.getString(i));
            else if (metaData.getColumnType(i) == Types.INTEGER)
               row.add(new Integer(results.getInt(i)));
            else if (metaData.getColumnType(i) ==Types.DOUBLE)
               row.add(new Double(results.getDouble(i)));
         }
         rows.add(row);
      }
      return rows;
   }


}






class buttonaction extends mypanel implements ActionListener
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