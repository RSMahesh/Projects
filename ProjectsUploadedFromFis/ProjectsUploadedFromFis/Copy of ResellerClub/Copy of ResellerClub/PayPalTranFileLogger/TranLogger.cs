using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface;
using System.Data;
using System.Web;
using System.IO;

namespace ResellerClub.PayPalTranFileLogger
{
    public class TranLogger : IPayPalTranscationLogger
    {
        #region IPayPalTranscationLogger Members
        string datasetFile = "ReselClubDst.xml";

        public void InsertTransactionLog(string userEmailId, Guid transcationId, double transcationAmount)
        {
            DataSet dst = GetDataSet();
            DataRow dr = dst.Tables[0].NewRow();
            dr["UserEmailId"] = userEmailId;
            dr["TranscationId"] = transcationId;
            dr["TranscationAmount"] = transcationAmount;
            dst.Tables[0].Rows.Add(dr);

            dst.AcceptChanges();
            dst.WriteXml(HttpContext.Current.Server.MapPath(datasetFile));
        }

        public void UpdateTransactionLog(string transcationId, string status)
        {
            DataSet dst = GetDataSet();
          DataRow[] drs =  dst.Tables[0].Select("TranscationId='"+ transcationId +"'");
          if (drs.Length > 0)
          {
              drs[0]["Status"] = status;
          }

          dst.AcceptChanges();
          dst.WriteXml(HttpContext.Current.Server.MapPath(datasetFile));
        }


        private DataSet GetDataSet()
        {
            DataSet dst = new DataSet();
            if (File.Exists(HttpContext.Current.Server.MapPath(datasetFile)))
            {
                dst.ReadXml(HttpContext.Current.Server.MapPath(datasetFile));
                return dst;
            }
            DataTable dt = new DataTable("PayPalTranscationLog");
            dt.Columns.Add("UserEmailId");
            dt.Columns.Add("TranscationId", typeof(Guid));
            dt.Columns.Add("TranscationAmount", typeof(decimal));
            dt.Columns.Add("Status");

            dst.Tables.Add(dt);
            return dst;
        }

      

        #endregion
    }
}
