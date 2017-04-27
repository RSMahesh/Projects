using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Temp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ClassLibraryTest.Test oj = new ClassLibraryTest.Test();
            //oj.NewFun();
            FirstType ff = new FirstType();
           
            ff.MyFunTwo();
            //InvokeMethodSlow(@"C:\Mahesh\Projects\Complier\CompilerWriting\bin\Debug\ClassLibraryTest.dll","Test","NewFun",new object[0] {});
        }

        public void Test()
        {

          

            //ClassLibraryTest.Test oj = new ClassLibraryTest.Test();
            //oj.NewFun();
    
        }


        public static Object InvokeMethodSlow(string AssemblyName,
          string ClassName, string MethodName, Object[] args)
        {
            // load the assemly
            Assembly assembly = Assembly.LoadFrom(AssemblyName);

            // Walk through each type in the assembly looking for our class
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass == true)
                {
                    if (type.FullName.EndsWith("." + ClassName))
                    {
                        // create an instance of the object
                        object ClassObj = Activator.CreateInstance(type);


                        MethodInfo[] mi = type.GetMethods();
                        foreach (MethodInfo m in mi)
                        {
                            Console.WriteLine(m.Name);
                        }

                        // Dynamically Invoke the method
                        object Result = type.InvokeMember(MethodName,
                          BindingFlags.Default | BindingFlags.InvokeMethod,
                               null,
                               ClassObj,
                               args);
                        return (Result);
                    }
                }
            }
            throw (new System.Exception("could not invoke method"));
        }
    }
}
