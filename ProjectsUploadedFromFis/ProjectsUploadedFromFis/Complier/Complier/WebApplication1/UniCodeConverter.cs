using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using System.Web;


namespace HindiUnicode
{
     partial  class UniCodeConverter
    {
        static DataTable dt;
        static char postFix = '\0';

        public UniCodeConverter() { }

        public static DataTable MappingDataTable
        {
            get { return UniCodeConverter.dt; }
        }
        static string Kruti_UniCode_Mapping = "Kruti_UniCode_Mapping.xml";
   
        static UniCodeConverter()
        {
            Kruti_UniCode_Mapping = HttpContext.Current.Server.MapPath("Kruti_UniCode_Mapping.xml");
            dt = GetDataTable();
            LoadMapping();

        }
      
        private static void LoadMapping()
        {
            dt.ReadXml(Kruti_UniCode_Mapping);
        }
      

        public static string GetUniCodeString(string text)
        {
            char ch = (char)39;
            StringBuilder sb = new StringBuilder();
            DataRow[] drs = null;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ch)
                    drs = dt.Select("KurtiDev='" + ch + ch + "'");
                else
                    drs = dt.Select("KurtiDev='" + text[i] + "'");

                if (drs != null && drs.Length > 0)
                {
                    AppenUniCode(sb, drs);
                }
                else if (text[i] == '\n')
                {
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    sb.Append(text[i]);

                }
            }

            return sb.ToString();

        }

        private static void AppenUniCode(StringBuilder sb, DataRow[] drs)
        {
            bool flag = true;

            if (drs[0]["postFix"] != DBNull.Value && Convert.ToBoolean(drs[0]["postFix"]))
            {

                postFix = Convert.ToChar(drs[0]["UniCode"]);

            }
            else if (sb.Length > 0 && drs[0]["preFix"] != DBNull.Value && Convert.ToBoolean(drs[0]["preFix"]))
            {
                int skipLen = 0;
                string charToSkip = drs[0]["SkipCharPosPreFix"].ToString();
                for (int i = 0; i < charToSkip.Length; i++)
                {
                    if (sb[sb.Length - (i + 1)] == charToSkip[i])
                    {
                        skipLen++;
                    }
                    else
                    {
                        break;
                    }

                }

                 sb.Insert(sb.Length - (1+skipLen), drs[0]["UniCode"]);


            }
            else if (ReplaceSquence(sb, drs))
            {
                ;
            }
           
            else
            {
                sb.Append(drs[0]["UniCode"]);
                if (postFix != '\0')
                {
                    drs = dt.Select("UniCode='" + postFix + "'");
                    if (ReplaceSquence(sb, drs))
                    {
                        ;
                    }
                    else
                    {
                        sb.Append(postFix);
                    }
                        postFix = '\0';
                }

            }


        }

        private static  bool  ReplaceSquence(StringBuilder sb, DataRow[] drs)
        {
            char [] splitChr = {','};
            bool found = false;
            if (sb.Length > 0 && drs[0]["ReplaceCharacter"] != null &&
                drs[0]["ReplaceCharacter"].ToString().Trim() != "")
            {

                string[] rplSeq = drs[0]["ReplaceCharacter"].ToString().Trim().Split(splitChr);
                string[] rplWith = drs[0]["ReplaceWith"].ToString().Trim().Split(splitChr);

                for (int outer = 0; outer < rplSeq.Length; outer++)
                {
                    found = true;
                    string replaceCh = rplSeq[outer];
                    for (int i = 0; i < replaceCh.Length; i++)
                    {
                        if (replaceCh[i] != sb[sb.Length - (replaceCh.Length - i)])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        sb.Remove(sb.Length - replaceCh.Length, replaceCh.Length);

                        if(rplWith[outer].Trim().Length > 0)
                        sb.Append(rplWith[outer]);
                        
                        break;
                    }

                    
                }
               
            }
            return found;
        }

        public static string GetHexCode(string unicodeString)
        {
            StringBuilder sb = new StringBuilder();
            Int32 decimalVal;
            for (int i = 0; i < unicodeString.Length; i++)
            {
                decimalVal = (Int32)unicodeString[i];

                sb.Append("\\u" + Convert.ToString(decimalVal, 16));
            }

            return sb.ToString();
        }

        public static string HexCodeToUniCode(string hexCodeString)
        {
            string[] split = { @"\u" };
            string[] arr = hexCodeString.Split(split, StringSplitOptions.None);
            StringBuilder sb = new StringBuilder();

            foreach (string str in arr)
            {
                try
                {
                    sb.Append((char)Convert.ToInt32(str, 16));
                }
                catch { }
            }
            return sb.ToString();
        }

        private static DataTable GetDataTable()
        {
            DataTable dt = new DataTable("Mapping");
            dt.Columns.Add("SNo", typeof(int));
            dt.Columns.Add("KurtiDev");
            dt.Columns.Add("UniCode");
            dt.Columns.Add("UniCodeHex");
            dt.Columns.Add("PostFix", typeof(bool));
            dt.Columns.Add("PreFix", typeof(bool));
            dt.Columns.Add("ReplaceCharacter");
            dt.Columns.Add("ReplaceWith");
            dt.Columns.Add("SkipCharPosPreFix");
            dt.CaseSensitive = true;
            dt.Columns[0].Unique = true;
            return dt;
        }


        private void CreateInitalChart()
        {
            dt = GetDataTable();


            for (int i = 2304; i < 2304 + 128; i++)
            {
                DataRow dr = dt.NewRow();
                dr["SNo"] = dt.Rows.Count + 1;
                dr["KurtiDev"] = "";
                dr["UniCode"] = ((char)i).ToString();
                dr["UniCodeHex"] = Convert.ToString(i, 16);
                dt.Rows.Add(dr);
            }



            //dataGridView1.DataSource = dt;
            //dataGridView1.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //dataGridView1.Columns[0].ReadOnly = true;
            //dataGridView1.Columns[2].ReadOnly = true;
            //dataGridView1.Columns[3].ReadOnly = true;
        }

        private void AddHalf()
        {

        }

        public static void Save()
        {
            dt.WriteXml(Kruti_UniCode_Mapping);
            LoadMapping();
        }


    }
}
