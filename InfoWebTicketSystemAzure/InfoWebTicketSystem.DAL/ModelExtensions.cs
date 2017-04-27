//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================

using System.Data;
using System.Reflection;

namespace AExpense.Model
{
    using System;
    using System.Text;
    using DataAccessLayer;

    public static class ModelExtensions
    {
        public static DataTable GetDataTableFromObjects(object[] objects)
        {

            if (objects != null && objects.Length > 0)
            {

                Type t = objects[0].GetType();

                DataTable dt = new DataTable(t.Name);

                foreach (PropertyInfo pi in t.GetProperties())
                {

                    dt.Columns.Add(new DataColumn(pi.Name,pi.PropertyType));

                }

                foreach (var o in objects)
                {

                    DataRow dr = dt.NewRow();

                    foreach (DataColumn dc in dt.Columns)
                    {

                        dr[dc.ColumnName] = o.GetType().GetProperty(dc.ColumnName).GetValue(o, null);

                    }

                    dt.Rows.Add(dr);

                }

                return dt;

            }

            return null;

        }
    }
}