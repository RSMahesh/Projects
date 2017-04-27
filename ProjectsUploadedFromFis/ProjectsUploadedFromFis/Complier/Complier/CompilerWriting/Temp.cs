using System;
using System.Collections.Generic;
using System.Text;

 

	public sealed  class Student 
	{
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
	}

public sealed class Temp
{
    Student student = new Student();

    void GetData()
    {
        student.Name1 = "mahesh";
        int a = student.Age1;
    }

}