using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CodeProject
{
    public partial class Form1 : Form
    {
        WebBrowser webControl = new WebBrowser();
        List<QuestionInfo> newQuestions;
        string[] domainKeyWords = { "ASP", ".NET", "ASP.net", "C#", "VB.Net", "SQL" };
        public static  Hashtable questionHash = new Hashtable();
        PopUp popF = null;
      
        public Form1()
        {
            InitializeComponent();
            webControl.ScriptErrorsSuppressed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ParseResponse(1);
        }

        private void webBrowser1_Navigating(object sender,
    WebBrowserNavigatingEventArgs e)
        {
            HtmlDocument document =
                this.webControl.Document;
        }

        private void GetLatestQuestion(HtmlElement questionTable)
        {
            var eleList = questionTable.GetElementsByTagName("tr");
            newQuestions = new List<QuestionInfo>();

            for (int i = 0; i < questionTable.Children[0].Children.Count; i++)
            {
                var h3 = questionTable.Children[0].Children[i].GetElementsByTagName("h3")[0];
                var ele = h3.GetElementsByTagName("a")[0];
                var href = "http://www.codeproject.com" + ele.GetAttribute("href").Replace("about:", "");

                if (!questionHash.Contains(href))
                {

                    var technologies = GetQuestionTechnologie(questionTable.Children[0].Children[i]);
                    //if (IsDomainQuestion(questionTable.Children[0].Children[i]))
                    {
                        QuestionInfo qi = new QuestionInfo();
                        qi.link = href;
                        qi.text = ele.InnerHtml;
                        qi.technologie = technologies;
                        qi.timeStamp = DateTime.Now;
                        qi.readed = false;
                        questionHash[href] = qi;
                        newQuestions.Add(qi);
                        //PopUp popup = new PopUp();
                        //popup.label1.Text = ele.InnerHtml;
                        //popup.label1.Tag ="http://www.codeproject.com"+ ele.GetAttribute("href").Replace("about:","");
                        //popup.Show();
                    }

                }
            }
        }


        private HtmlElement GetQuestionTable(HtmlDocument document)
        {
            var tableList = document.GetElementsByTagName("table");
            HtmlElement table = null;
            GotClass(tableList, "question-list", out table);
            return table;
        }
        private bool IsDomainQuestion(HtmlElement tableRow)
        {
            bool rtn = false;
            var divList = tableRow.GetElementsByTagName("div");
            HtmlElement div = null;

            GotClass(divList, "tags", out div);
            var eleList = div.GetElementsByTagName("a");
            for (int i = 0; i < eleList.Count; i++)
            {
                var txt = eleList[i].InnerText;

                if (domainKeyWords.Any(x => x.IndexOf(txt.ToUpperInvariant()) > -1))
                {
                    rtn = true;
                }
            }

            return rtn;
        }

        private string GetQuestionTechnologie(HtmlElement tableRow)
        {
            string rtn = "";
            var divList = tableRow.GetElementsByTagName("div");
            HtmlElement div = null;

            GotClass(divList, "tags", out div);
            var eleList = div.GetElementsByTagName("a");
            for (int i = 0; i < eleList.Count; i++)
            {
                rtn += " " + eleList[i].InnerText;
            }

            return rtn;
        }
        private bool GotClass(HtmlElement ele, string className)
        {
            return ele.GetAttribute("className") == className;
        }

        private bool GotClass(HtmlElementCollection eleList, string className, out HtmlElement rntEle)
        {
            bool rtn = false;
            rntEle = null;

            for (int i = 0; i < eleList.Count; i++)
            {
                if (GotClass(eleList[i], className))
                {
                    rtn = true;
                    rntEle = eleList[i];
                    break;
                }
            }
            return rtn;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HitServer();
            timer1.Interval = 1000 * 10;
            timer1.Enabled = true;
            timer1.Start();
            ParameterizedThreadStart st = new ParameterizedThreadStart(HideShowWindow);
            

            Thread th = new Thread(st);

          th.Start(0);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            HitServer();
        }

        private void HitServer()
        {
            string response = HTTPHelper.PostUrl("http://www.codeproject.com/script/Answers/List.aspx?tab=unanswered&alltags=true");
            webControl.Navigating +=
        new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
            webControl.DocumentText = response;
            timer2.Interval = 1000 * 5;
            timer2.Enabled = true;
            timer2.Start();

        }

        private void ParseResponse(object obj)
        {
            HtmlDocument document =
                this.webControl.Document;
            if (document != null)
            {
                var table = GetQuestionTable(document);
                GetLatestQuestion(table);
                if (newQuestions.Count > 0)
                {

                    popF = new PopUp();
                    popF.timer1 = timer1;
                    popF.newQuestions = newQuestions;
                    popF.Show();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                ParseResponse(1);
                timer2.Enabled = false;
            }
            catch { }
        }

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        public static void HideShowWindow(object  show)
        {
            int hWnd;
           // Thread.Sleep(2000);
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName.ToUpperInvariant() == "CODEPROJECT")
                {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, (int)show);
                }
            }
        }

     

        private void Form1_Resize(object sender, EventArgs e)
        {
            //listView1.Size = this.Size;
        }

        // ColumnClick event handler. 
        private void ColumnClick(object o, ColumnClickEventArgs e)
        {
            // Set the ListViewItemSorter property to a new ListViewItemComparer  
            // object. Setting this property immediately sorts the  
            // ListView using the ListViewItemComparer object. 
         
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
       
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            HideShowWindow(0);

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (popF != null)
                {
                    popF.Visible = true;
                }
            }
            else
            {
                if (popF != null)
                {
                    popF.Visible = false;
                }
            }
        }
    }

    public class QuestionInfo
    {
        public string text;
        public string link;
        public string technologie;
        public DateTime timeStamp;
        public bool readed;
    }
}
