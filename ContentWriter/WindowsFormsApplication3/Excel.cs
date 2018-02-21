using System;
using System.IO;
using Microsoft.Office.Interop;
using System.Collections.Generic;

namespace StatusMaker.Data
{
    public static class ExcelProvider
    {
        public static string GetExcelConnectionString(string filePath)
        {
            switch (Path.GetExtension(filePath).ToUpperInvariant())
            {
                case ".XLS":
                    return string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;'", filePath);
                case ".XLSX":
                    return string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=Yes;'", filePath);
            }

            throw new Exception("Invalid Excel file extension" + Path.GetExtension(filePath));
        }

        public static string GetExcelConnectionString_(string filePath)
        {
            switch (Path.GetExtension(filePath).ToUpperInvariant())
            {
                case ".XLS":
                    return string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;'", filePath);
                case ".XLSX":
                    return @"Driver={ Microsoft Excel Driver(*.xls, *.xlsx, *.xlsm, *.xlsb)}; DBQ = "+ filePath+";";


                //    return string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=Yes;'", filePath);
            }

            throw new Exception("Invalid Excel file extension" + Path.GetExtension(filePath));
        }

    //    public List<string> static GetSheets(string filePath)
    //    {
    //        Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
    //        excelApp.Visible = true;

    //        Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open("C:\MyWorkbook.xls",
    //            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    //            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    //            Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            
    //        for(int i=0; i < workbook.Sheets.Count; i ++ )
    //        {
    //            workbook.Sheets.Item.
    //        }


    //        foreach(var sheet in workbook.Sheets.GetEnumerator())
    //        {
                
    //        }


      }
    }
