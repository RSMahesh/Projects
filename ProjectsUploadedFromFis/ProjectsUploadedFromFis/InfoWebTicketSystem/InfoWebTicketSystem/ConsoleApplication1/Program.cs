using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            DataSetServiceProvider.QueryParser qq = new DataSetServiceProvider.QueryParser(GetaDataTable());
            //qq.Insert(" Insert into Ticket (name, age, address) values('mahesh','34','B-109,Pathik Vihar')");
            qq.Insert(" Insert into Ticket (FID,Subject) values("+Guid.NewGuid().ToString()+",'Testing Subject')");


        }

        private static  DataTable GetaDataTable()
        {
            ///Initializes a new instance of the DALBase class with the connection object provided.

            DataTable dt = new DataTable();
            string filePath = @"C:\DataBaseBackUp\Ticket.xml";
            using (StreamReader sr = new StreamReader(filePath))
            {
                dt.ReadXml(sr);
            }

            return dt;
        }
    }
}
