using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using ResellerClub.Common;
using System.Collections;
using System.Collections.Generic;

namespace ResellerClub.WebUI.code
{
    public abstract class LanguageSetting
    {

        public  static void LoadLanguageSetting(string language)
        {
            Hashtable langSetting = null;
            string path;
            if (Cache.Get("LanguageSetting") != null)
            {
                langSetting = (Hashtable)Cache.Get("LanguageSetting");
            }
            else
            {
                langSetting = new Hashtable();
            }

            //path = HttpContext.Current.Server.MapPath(".\\" + language + ".setting");
            //if (!File.Exists(path))
            //{
            //    langSetting[language] = null;
            //    return;
            //}

            langSetting[language] = ReadSettingsFromFolder(language);
            Cache.Set("LanguageSetting", langSetting);

        }

        private static Dictionary<string, string> ReadSettingsFromFolder(string language)
        {
            string path = HttpContext.Current.Server.MapPath("~\\Language\\" + language);
            if (!Directory.Exists(path))
            {
                return null;
            }

            var files = Directory.GetFiles(path);
            Dictionary<string, string> setting = new Dictionary<string, string>();
            foreach (var file in files)
            {
                ReadSettingsFromFile(file, ref setting);
            }

            return setting;
        }



        public static string GetString(string key)
        {
            string language = GetLanguage();
            Dictionary<string, string> langDic = null;
            string strSetting = "";
            if (Cache.Get("LanguageSetting") == null)
            {
                LoadLanguageSetting(language);
            }

            Hashtable langSetting = (Hashtable)Cache.Get("LanguageSetting");

            if (!langSetting.Contains(language))
            {
                LoadLanguageSetting(language);
            }

            if (langSetting.Contains(language))
            {
                langDic = (Dictionary<string, string>)langSetting[language];
                strSetting = langDic[key];
            }

            return strSetting;
        }

        private static void ReadSettingsFromFile(string filePath, ref Dictionary<string, string> setting)
        {
            char[] splitChar = { '=' };
            string[] arr;

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Trim() != "")
                    {
                        arr = line.Split(splitChar);
                        setting[arr[0].Trim()] = arr[1].Trim();
                    }
                }
            }
        }


        public static string GetLanguage()
        {
            string language = "";
            SessionManager SessionM = new SessionManager();
            if (SessionM["country"] == null)
              // return "HINDI";
            return "ENGLISH";

            switch (SessionM["country"].ToString().ToUpperInvariant())
            {
                case "USA":
                    language = "ENGLISH";
                    break;
                case "INDIA":
                    //language = "HINDI";
                    language = "ENGLISH";
                    break;

            }

            return language;
        }





    }
}
