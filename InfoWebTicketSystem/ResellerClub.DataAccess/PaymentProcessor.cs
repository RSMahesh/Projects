using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseConnectionProvider.Interface;

namespace ResellerClub.DataAccess
{
   public class PaymentProcessor : DALBase 
    {
       public PaymentProcessor(IConnection connection)
            : base(connection)
        {
        }

       public void InsertTransactionLog(string request, Guid orderId)
       {
           string cmdText = "INSERT INTO PaymentTransactionLog ([ID],[OrderID],[Request],[UpdateDate]) VALUES (NewID()," +
           "'" + orderId.ToString() + "'," +
           "'" + request.Replace("'","''")  + "',GetDate())"; 
      
           connection.ExecuteSQL(cmdText);
       }

       public void UpdateTransactionLog(string response, Guid orderId)
       {
           string cmdText = "UPDATE  PaymentTransactionLog set Response='" + response.Replace("'", "''") + "' where orderId='" + orderId.ToString() + "'";
           connection.ExecuteSQL(cmdText);
       }

        public void UpdateTable_1()
       {
           string cmdText = "update table_1 set count = count +1";
           connection.ExecuteSQL(cmdText);
       }

       
    }
}
