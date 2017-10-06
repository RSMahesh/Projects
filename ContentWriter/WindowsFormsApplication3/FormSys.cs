using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class FormSys : Form
    {
        public FormSys()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Microsoft.Office.Interop.Word.Application appWord;      // word application var
            object objNull = null;      // word object method calls require
                                        // references to objects... create
                                        // object for null and
            object objFalse = false;      // false entries and language

            object objLanguage = Microsoft.Office.Interop.Word.WdLanguageID.wdEnglishUS; // or appropritate lang!

            try
            {
                // Try opening Word app
                appWord = new Microsoft.Office.Interop.Word.Application();
            }
            catch (System.Exception exc)
            {
                // could not open word... show error message and return
                MessageBox.Show(exc.Message);
                return;
            }

            // clear synonym listbox lbSynonym
            lbSynonym.Items.Clear();

            // now call get_SynonymInfo to get SynonymInfo structure for
            // word entered in TextBox tbWord
            Microsoft.Office.Interop.Word.SynonymInfo si =
                     appWord.get_SynonymInfo(tbWord.Text, ref (objLanguage));


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
                                lbSynonym.Items.Add(strSynonym);
                            }

                        foreach (string strSynonym in aSynonyms)
                        {
                            // loop over each synonym in ArrayList
                            // and add to lbSynonym ListBox
                            lbSynonym.Items.Add(":" + strSynonym);
                        }
                    }
            }
            else
            {
                // no meanings/synonyms found... set ListBox value to "NONE"
                lbSynonym.Items.Add("NONE");
            } // Clean up COM object
           // c.Destroy(si);

            // quit WINWORD app
            appWord.Quit(ref objFalse, ref objNull, ref objNull);
            // clean up COM object
          //  c.Destroy(appWord);

        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
   
}
