using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryTest
{
 
        public sealed class  Student
        {

            private string[] names = {"ccc","bbb"};
            public string this[int index]
            {
                get
                {
                    return names[index];

                }
                set
                {
                    names[index] = value;
                }

            }
            private string Name;

            public string Name1
            {
                get { return Name; }
                set { Name = value; }
            }
            private int Age;

            public int Age1
            {
                get { return Age; }
                set { Age = value; }
            }

            void fun123()
            {
                Name1 = "mmmmmmm";

            }
        }

        public sealed class Temp
        {
            Student student = new Student();

             void GetData()
            {
                student[0] = "bailwal";
                string mm = student[1];

                student.Name1 = "mahesh";
                int a = student.Age1;
            }

        }
 
}
