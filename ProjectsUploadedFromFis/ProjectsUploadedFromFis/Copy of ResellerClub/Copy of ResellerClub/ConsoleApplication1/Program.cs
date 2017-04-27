using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.BusinessLogic;
using ResellerClub.Common;
using System.IO;
using System.Data;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
          //Email.SendMailEx("maheshbailwal@gmail.com","Test","Testing","","jai-hind@infowebservices.in","Admin");
           // Customer cust = new Customer();
          // string msg ="";
          // bool vb = cust.GenerateTempPasswordSendEmail("maheshbailwal2@gmail.com",out  msg);

          // bool vb = cust.ChangePassword("maheshbailwal2@gmail.com", out  msg);
            
              DataTable dt = new DataTable();
          //  string filePath = @"C:\DataBaseBackUp\PlanDataTable.xml";
           // using (StreamReader sr = new StreamReader(filePath))
           // {
           //     dt.ReadXml(sr);
            //}
            
            DataSetServiceProvider.QueryParser q = new DataSetServiceProvider.QueryParser();
            var dataReader = q.ExecuteReader(dt, "Select * from Plans");
            var list = new List<string>();
            while (dataReader.Read())
            {
              list.Add(  dataReader.GetString(3));

            }
            
        }
    }
}
