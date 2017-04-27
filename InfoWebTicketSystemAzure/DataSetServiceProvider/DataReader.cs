using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataSetServiceProvider
{
    internal class DataReader : IDataReader
    {
        #region IDataReader Members
        DataTable dt;
        int curPos = 0;



        internal DataReader(DataTable dataTable)
        {
            this.dt = dataTable;
        }

        public void Close()
        {
            
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { return curPos < dt.Rows.Count; }
        }

        public bool NextResult()
        {
            return false;
        }

        public bool Read()
        {
            if (curPos < dt.Rows.Count - 1)
            {
                curPos++;
                return true;
            }
            return false;
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion

        #region IDataRecord Members

        public int FieldCount
        {
            get { return dt.Columns.Count; }
        }

        public bool GetBoolean(int i)
        {
            return (bool)this.dt.Rows[curPos][i];
        }

        public byte GetByte(int i)
        {
            return (byte)GetFiledData(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            return (char)GetFiledData(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            return (DateTime)GetFiledData(i);
        }

        public decimal GetDecimal(int i)
        {
            return (decimal)GetFiledData(i);
        }

        public double GetDouble(int i)
        {
            return (double)GetFiledData(i);
        }

        public Type GetFieldType(int i)
        {
            return GetFiledData(i).GetType();
        }

        public float GetFloat(int i)
        {
            return (float)GetFiledData(i); ;
        }

        public Guid GetGuid(int i)
        {
          return (Guid)GetFiledData(i);
        }

        public short GetInt16(int i)
        {
            return (Int16)GetFiledData(i);
        }

        public int GetInt32(int i)
        {
            return (Int32)GetFiledData(i);
        }

        public long GetInt64(int i)
        {
            return (Int64)GetFiledData(i);
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
          return  (string)dt.Rows[curPos][i];
        }

        public object GetValue(int i)
        {
            return GetFiledData(i);
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            return GetFiledData(i)== DBNull.Value;
        }

        public object this[string name]
        {
            get { return GetFiledData(name); }
        }

        public object this[int i]
        {
            get { return GetFiledData(i); }
        }

        private object GetFiledData(int filedIndx)
        {
            return dt.Rows[curPos][filedIndx];
        }
        private object GetFiledData(string  filedName)
        {
            return dt.Rows[curPos][filedName];
        }
       
        #endregion
    }
}
