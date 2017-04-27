using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages;
using ResellerClub.Messages;
using System.Data.SqlClient;
using System.Data;
using DataBaseConnectionProvider.Interface;

namespace ResellerClub.DataAccess
{
    //TO DO:
    //Need Despose Connection
   public class Plan : DALBase 
    {
       public List<IPlanMessage> GetPlans()
       {
           string cmdText = "Select ProductID,PlanID,SubPlanID,ProductName,PlanSequence,[Year],Price,PlanName,CurrencyName,CurrencySymbol,DisplayName,Selling from [VW_Plan] with(nolock)";
           connection.Select(cmdText);
          return  DataReaderToMessage(connection.ExecuteDataReader());
       }

       public void InsertPlan(string productName, int PlanSequence, int year, decimal price, string planName, string currencyName, string currencySymbol, Nullable<Guid> subPlanID, Nullable<Guid> planID, string displayName,bool selling)
       {

           connection.Insert("sp_insert_plan");
           connection.AddParameter("ProductName",productName);
           connection.AddParameter("planSquence", PlanSequence);
           connection.AddParameter("year", year);
           connection.AddParameter("price", price);
           connection.AddParameter("planName", planName);
           connection.AddParameter("currencyName", currencyName);
           connection.AddParameter("currencySymbol", currencySymbol);
           
           connection.AddParameter("subPlanId",subPlanID == null ? (object)DBNull.Value : subPlanID.Value);
           connection.AddParameter("planId", planID == null ? (object)DBNull.Value : planID.Value);

           connection.AddParameter("displayName", displayName);
           connection.AddParameter("selling", selling);
           
           connection.ExecuteNonQuery();
       }


       public Plan(IConnection connection)
           : base(connection)
       {

       }

       private List<IPlanMessage> DataReaderToMessage(IDataReader dataReader)
       {
           var plans = new List<IPlanMessage>();
           try
           {
               while (dataReader.Read())
               {

                   IPlanMessage plan = new ResellerClub.Messages.PlanMessage(
                                        dataReader.GetGuid(0),
                                        dataReader.GetGuid(1),
                                        dataReader.GetGuid(2),
                                        dataReader.GetString(3),
                                        dataReader.GetInt16(4),
                                        dataReader.GetInt16(5),
                                        dataReader.GetDecimal(6),
                                        dataReader.GetString(7),
                                        dataReader.GetString(8),
                                        dataReader.GetString(9),
                                        dataReader.GetString(10),
                                        dataReader.GetBoolean(11));

                   plans.Add(plan);
               }

           }
           finally
           {
               if ((!dataReader.IsClosed) || (dataReader != null))
               {
                   dataReader.Close();
               }
           }
           return plans;
       }

   }
}
