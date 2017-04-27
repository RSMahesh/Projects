import java.io.*;

 interface sexy1
{

	public void go();
}
class myouter
{

	 private class inner implements sexy1
	{

  public void go()
  {

	System.out.println("Go");

}
   public	void show()
	{
	System.out.println("mahesh");

	}

	}

protected inner create()
{

return new inner();

}



}


class my1 extends myouter
{




public my1()
{
 inner in =create();
in.go();

}



}


class test1
{

	public static void main(String s[])

	{


		my1 mm=new my1();

	}
}

