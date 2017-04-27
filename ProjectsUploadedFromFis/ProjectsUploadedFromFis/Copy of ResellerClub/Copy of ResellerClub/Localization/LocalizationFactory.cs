using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Xml;


namespace ResellerClub.Localization
{
   public abstract class LocalizationFactory
    {

       static LocalizationFactory()
       {
          
       }

      
       public static Setting GetSettings(string configFilePath)
       {
           XmlDocument doc = new XmlDocument();
           doc.Load(configFilePath);
           Setting setting = new Setting(doc);
           return setting;

       }

    }
}
