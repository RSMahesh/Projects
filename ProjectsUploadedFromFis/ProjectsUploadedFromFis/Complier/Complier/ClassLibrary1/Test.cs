using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryTest
{
    public class Test
    {
        public string classLevelVariable;
        public Test()
        {
            classLevelVariable = "Test123";
            Console.Write("===========" + classLevelVariable + "===============");
        }

        public Test(string ABC)
        {
            classLevelVariable = "Test123";
        }

        public void Check(string parameter)
        {
    classLevelVariable  = "localValue";
    Console.Write(classLevelVariable);     

            //parameter = "dsdsd";
            //localVariable = parameter; 
            //classLevelVariable = "Check12345";
        }

        public void NewFun()
        {
            Console.Write("Mahesh");
            classLevelVariable = "pppppppppppppppp";
            Console.Write(classLevelVariable);
        }
        public void  we_yu()
        {
            bool a= true, b= false;
            if (a == b)
            {
                int myint = 1;
                
            }


        }

        public static void  Fun(string A, string B)
        {
            string localVariable;
            localVariable = A;
         //   Console.WriteLine(A);
          //  Console.WriteLine("Line2" + B);
           // Console.WriteLine("Line3");
         // return "vv";
        }


    }
}
