using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages;
using System.Data.SqlClient;
using ResellerClub.Common.Logging;
using System.Data;
using DataBaseConnectionProvider.Interface;

namespace ResellerClub.DataAccess
{
    public class Order : DALBase
    {
        public Order(IConnection connection)
            : base(connection)
        {
        }

        public Guid SaveOrder(IList<IOrderItemMessage> orderItems, Guid sessionId, decimal orderAmount, int status)
        {
            Guid orderID = Guid.NewGuid();
            string cmdText = "insert into [Order] (ID,SessionID,OrderAmount,Status,InsertDate,UpdateDate)  values (" +
              "'" + orderID.ToString() + "'," +
                "'" + sessionId + "'," +
              "'" + orderAmount + "'," +
              "" + status.ToString() + "," +
              "'" + DateTime.Now.ToString() + "'," +
              "'" + DateTime.Now.ToString() + "')";
            connection.ExecuteSQL(cmdText);

            foreach (var item in orderItems)
            {
                SaveOrderItem(item, orderID, status);
            }

            return orderID;
        }

        private void SaveOrderItem(IOrderItemMessage item, Guid orderID, int status)
        {
            string cmdText = "insert into [OrderItem] (ID,orderID,SubPlanID,DomainName,Status,EnableSsl,EnableMaintenance,InvoiceNumber,Description,UpdateDate)  values (NewID()," +
            "'" + orderID.ToString() + "'," +
            "'" + item.SubPlanID + "'," +
            "'" + item.DomainName + "'," +
            "" + status.ToString() + "," +
            "" + (item.EnableSsl ? "1" : "0") + "," +
            "" + (item.EnableMaintenance ? "1" : "0") + "," +
            "'" + item.InvoiceNumber + "'," +
            "'" + item.Description + "'," +
            "GetDate())";

            connection.ExecuteSQL(cmdText);
        }

        public void UpdateOrderItem(Guid orderId, Guid subPlanId, string domainName, string invoiceNumber, string description, int status, string response)
        {
            XmlTraceWriter.Trace("--------1-------");
            string cmdText = "update [OrderItem] set status=" + status.ToString() +
            ",response='" + response.Replace("'", "''") + "'" +
            ",InvoiceNumber='" + invoiceNumber + "'" +
            ",Description='" + description + "'" +
            " where OrderId='" + orderId.ToString() + "' and SubPlanId='" + subPlanId.ToString() + "'" +
            " and DomainName='" + domainName + "'";
            XmlTraceWriter.Trace(cmdText);
            XmlTraceWriter.Trace("--------2-------");
            connection.ExecuteSQL(cmdText);
        }

        public void UpdateOrderStatus(Guid orderId, int status)
        {
            string cmdText = "update [Order] set status=" + status.ToString() + " where ID='" + orderId.ToString() + "'";
            connection.ExecuteSQL(cmdText);
        }

        public void UpdatePaymentTranNumber(Guid orderId, string tranNumber)
        {
            connection.Insert("sp_update_order_Payemt_Tran_Number");
            connection.AddParameter("OrderId", orderId);
            connection.AddParameter("paymentTranNumber", tranNumber);
            connection.ExecuteNonQuery();
        }

        public void UpdatePaymentMode(Guid orderId, string paymentMode)
        {
            string cmdText = "update [Order] set PaymentMode='" + paymentMode + "' where ID='" + orderId.ToString() + "'";
            connection.ExecuteSQL(cmdText);
        }

        public void UpdateOrderStatus(Guid orderId)
        {
            connection.Insert("sp_update_order_status");
            connection.AddParameter("OrderId", orderId);
            connection.ExecuteNonQuery();
        }

        public List<IOrderItemMessage> GetOrderItem(Guid orderId)
        {
            string cmdText = "select SubPlanID,DomainName,EnableSsl,EnableMaintenance,InvoiceNumber,Description,Status from OrderItem with(nolock) where OrderId='" + orderId.ToString() + "'";
            connection.Select(cmdText);
            var dataReader = connection.ExecuteDataReader();
            return DataReaderToMessage(dataReader);
        }

        public List<IOrderItemMessage> GetUnProcessedOrderItem(Guid orderId)
        {
            string cmdText = "select SubPlanID,DomainName,EnableSsl,EnableMaintenance,InvoiceNumber,Description,Status from OrderItem with(nolock) where OrderId='" + orderId.ToString() + "' and status<>" + Common.Constant.OrderItemStatusProcessed;
            connection.Select(cmdText);
            var dataReader = connection.ExecuteDataReader();
            return DataReaderToMessage(dataReader);
        }
     
        private List<IOrderItemMessage> DataReaderToMessage(IDataReader dataReader)
        {
            var orderItems = new List<IOrderItemMessage>();
            try
            {
                while (dataReader.Read())
                {
                    var item = new ResellerClub.Messages.OrderItemMessage(
                                         dataReader.GetGuid(0),
                                         dataReader.GetString(1),
                                         dataReader.GetBoolean(2),
                                         dataReader.GetBoolean(3),
                                         dataReader.GetString(4),
                                         dataReader.GetString(5),
                                         dataReader.GetInt16(6));

                    orderItems.Add(item);
                }

            }
            finally
            {
                if ((!dataReader.IsClosed) || (dataReader != null))
                {
                    dataReader.Close();
                }
            }
            return orderItems;
        }

        public Guid GetSessionId(Guid orderID)
        {
            string cmdText = "select sessionID from [order] with(nolock) where ID='" + orderID.ToString() + "'";
            connection.Select(cmdText);
            return (Guid) connection.ExecuteScalar();
        }

        public DataTable GetOrder(Guid orderId)
        {
            string query = "select * from [Order] with(nolock) where Id='" + orderId.ToString() + "'";
            connection.Select(query);
            return connection.ExecuteDataTable(false);
        }
    }
}
