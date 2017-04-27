using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages;
using System.Data.SqlClient;
using System.Data;
using DataBaseConnectionProvider.Interface;

namespace ResellerClub.DataAccess
{
   public class Domain :DALBase 
   {
       public Domain(IConnection connection)
           : base(connection)
       {
       }

       public List<ITopLevelDomianMessage> GetTopLevelDomian()
       {
           string cmdText = "Select Name,Category,PlanID from [TopLevelDomain] with(nolock)";
           connection.Select(cmdText);
           return DataReaderToMessage(connection.ExecuteDataReader());
       }

       public List<string> GetNameServer()
       {
           string cmdText = "Select Name from [NameServer] with(nolock)";
           connection.Select(cmdText);
           return DataReaderToNameServerMsg(connection.ExecuteDataReader());
       }
       public List<string> DataReaderToNameServerMsg(IDataReader dataReader)
       {
           var nameServer = new List<string>();
           try
           {
               while (dataReader.Read())
               {
                   nameServer.Add(dataReader.GetString(0));
               }

           }
           finally
           {
               if ((!dataReader.IsClosed) || (dataReader != null))
               {
                   dataReader.Close();
               }
           }
           return nameServer;
       }


       private List<ITopLevelDomianMessage> DataReaderToMessage(IDataReader dataReader)
       {
           var tlds = new List<ITopLevelDomianMessage>();
           try
           {
               while (dataReader.Read())
               {

                   var tld = new ResellerClub.Messages.TopLevelDomianMessage(
                                        dataReader.GetString(0),
                                        dataReader.GetString(1),
                                        dataReader.GetGuid(2));

                   tlds.Add(tld);
               }

           }
           finally
           {
               if ((!dataReader.IsClosed) || (dataReader != null))
               {
                   dataReader.Close();
               }
           }
           return tlds;
       }

       public void InsertTopLevelDomain(string productName, string planName, string topLevelDomain,string currency)
       {

           connection.Insert("sp_insert_TopLevelDomain");
           connection.AddParameter("ProductName", productName);
           connection.AddParameter("PlanName", planName);
           connection.AddParameter("TopLevelDomain", topLevelDomain);
              connection.AddParameter("CurrencyName", currency);
           connection.ExecuteNonQuery();
       }

   }
}
