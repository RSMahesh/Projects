using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Xml;

	class PreProcessor
	{
        Dictionary<string, string> dictionary;
        public PreProcessor(string preProcessorInPutFile) {
            dictionary = new Dictionary<string, string>();
            
            XmlDocument doc = new XmlDocument();
            doc.Load(preProcessorInPutFile);
            XmlNodeList list = doc.SelectNodes("/root/add");
            foreach (XmlNode node in list)
            {
                if (node.Attributes["Find"] != null && node.Attributes["Find"].Value.Trim() != "")
                    dictionary.Add(node.Attributes["Find"].Value, node.Attributes["Replace"].Value);
            }

        }
        public void  RunPreProcessor(IList<object> tokens)
        {
                for (int indx = 0; indx < tokens.Count;indx++)
                {
                    if (tokens[indx] is string)
                    {
                        if (dictionary.ContainsKey(tokens[indx] as string))
                        {
                            tokens[indx] = dictionary[tokens[indx] as string];
                        }
                    }
                }
              
        }

	}
