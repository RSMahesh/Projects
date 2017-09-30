using System;
using System.IO;

namespace StatusMaker.Data
{
    public static class Excel
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


    }
}
