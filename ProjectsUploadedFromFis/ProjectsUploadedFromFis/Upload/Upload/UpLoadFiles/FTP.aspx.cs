using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;

namespace UpLoadFiles
{
    public partial class FTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string command = Request["command"];

            switch (command)
            {

                case "dir":
                    Dir();
                    break;
                case "delete":
                    Delete();
                    break;
            }
        }

        private void Dir()
        {
            string dirPath = Request["path"];
            if (dirPath == null || dirPath == "")
            {
              dirPath =  Directory.GetParent(Server.MapPath(".")).ToString();
            }
            var list = GetFiles(dirPath);
            var jasonString = ObjectToJason(list);
            Response.Clear();
            Response.Write(jasonString);
            Response.End();
        }

        private void Delete()
        {
            string dirPath = Request["path"];
            if (dirPath != "")
            {
                File.Delete(dirPath);
            }
            Response.Clear();
            Response.Write("done");
            Response.End();
        }

        protected List<Info> GetFiles(string dirPath)
        {
            string[] dirs = Directory.GetDirectories(dirPath);

            var infoList = new List<Info>();

            foreach (string dir in dirs)
            {
                infoList.Add(new Info(dir, true));

            }

            string[] files = Directory.GetFiles(dirPath);

            foreach (string dir in files)
            {
                infoList.Add(new Info(dir, false));
            }

            return infoList;
        }

        private string ObjectToJason(object obj)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                        new System.Web.Script.Serialization.JavaScriptSerializer();

            return serializer.Serialize(obj);
        }

    }
}
