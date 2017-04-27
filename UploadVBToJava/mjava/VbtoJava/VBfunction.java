package VbtoJava;

public class VBfunction
{


public static long Val(String no)

{

long lno;
try
{
lno=Long.parseLong(no);
}


catch(Exception exe)
{

   lno=0;
}


return lno;
}

}







