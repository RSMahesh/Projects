using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class SynonymProvider
    {
        Microsoft.Office.Interop.Word.Application appWord;
        object objLanguage = Microsoft.Office.Interop.Word.WdLanguageID.wdEnglishUS; // or appropritate lang!

        public SynonymProvider()
        {
            appWord = new Microsoft.Office.Interop.Word.Application();
        }

        public void AddSynoms(string word, MenuItem menuItemSyno, EventHandler callBack)
        {
            var response = GetSynims(word);
            menuItemSyno.MenuItems.Clear();

            if(!response.Item1.Any() && !response.Item2.Any())
            {
                menuItemSyno.MenuItems.Add("None");
                return;
            }

            foreach (var syno in response.Item1)
            {
                menuItemSyno.MenuItems.Add(syno, callBack);
            }

            menuItemSyno.MenuItems.Add("-");

            foreach (var syno in response.Item2)
            {
                menuItemSyno.MenuItems.Add(syno, callBack);
            }
        }
        
        private Tuple<List<string>, List<string>> GetSynims(string word)
        {
          
            var synonymList = new List<string>();
            var antonims = new List<string>();

         
            Microsoft.Office.Interop.Word.SynonymInfo si =
                     appWord.get_SynonymInfo(word, ref (objLanguage));

            // first find out how many meanings were found for word
            int iMeanings = (int)si.MeaningCount;


            if (si.MeaningCount > 0)
            {
                // one or more meanings were found... loop over each
                // (notice SynonymInfo.MeaningList is type System.ArrayList!)
                var strMeanings = si.MeaningList as Array;
                if (strMeanings != null)
                    foreach (var strMeaning in strMeanings)
                    {
                        // get Synonym List for each meaning... note that
                        // get_SynonymList takes an object ref, thus we
                        // must create objMeaning object
                        var objMeaning = strMeaning;

                        var aSynonyms = si.AntonymList as Array;

                        var strSynonyms = si.SynonymList[objMeaning] as Array;
                        if (strSynonyms != null)
                            foreach (string strSynonym in strSynonyms)
                            {
                                // loop over each synonym in ArrayList
                                // and add to lbSynonym ListBox
                                //     lbSynonym.Items.Add(strSynonym);
                                if(!synonymList.Contains(strSynonym))
                                synonymList.Add(strSynonym);
                            }

                        foreach (string strSynonym in aSynonyms)
                        {
                            // loop over each synonym in ArrayList
                            // and add to lbSynonym ListBox
                            //  lbSynonym.Items.Add(":" + strSynonym);

                            if (!antonims.Contains(strSynonym))
                                antonims.Add(strSynonym);

                        }
                    }
            }
         
            return new Tuple<List<string>, List<string>>(synonymList, antonims); ;
        }

        ~SynonymProvider()
        {
            object objNull = null;
            object objFalse = false;
            appWord.Quit(ref objFalse, ref objNull, ref objNull);
        }

    }
}
