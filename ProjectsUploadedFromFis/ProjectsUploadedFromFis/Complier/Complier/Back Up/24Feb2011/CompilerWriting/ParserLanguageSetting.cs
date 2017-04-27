using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;

sealed class ParserLanguageSetting
{
    Dictionary<string, string> languageSetting;
    private static ParserLanguageSetting instance;
    private static object lockObj = new object();
    private ParserLanguageSetting(string languageSettingFilePath)
    {
        languageSetting = new Dictionary<string, string>();
        XmlDocument doc = new XmlDocument();
        doc.Load(languageSettingFilePath);
        XmlNodeList list = doc.SelectNodes("/root/add");
        foreach (XmlNode node in list)
        {
            languageSetting.Add(node.Attributes["key"].Value, node.Attributes["value"].Value);
        }
    }
    public string this[string token]
    {
        get
        {
            try
            {
                return languageSetting[token];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
            return null;
        }
    }
    public static ParserLanguageSetting GetInstance(string languageSettingFilePath)
    {
        if (instance == null)
        {
            Monitor.Enter(lockObj);
            if (instance == null)
            {
                instance = new ParserLanguageSetting(languageSettingFilePath);
            }
            Monitor.Exit(lockObj);
        }
        return instance;
    }
}

