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
    public partial class UploadFolder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> fileToCopy = new List<string>();
            List<string> uploadedFiles = new List<string>();
            char[] split = { '\n' };
            string[] files = txtFiles.Text.Split(split);
            fileToCopy.AddRange(files);
            Session["fileToCopy"] = fileToCopy;
            Session["uploadedFiles"] = uploadedFiles;
            Session["ServerRootPath"] = txtServerPath.Text;
            Session["ClientRootPath"] = txtClientPath.Text;

        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {


            if (FileUpload1.FileName != null)
            {
               string serverFilePath =  txtCopyingFile.Text.Replace(Session["ClientRootPath"].ToString(), Session["ServerRootPath"].ToString());

               if (File.Exists(serverFilePath))
               {
                   File.Copy(serverFilePath, Server.MapPath("BackUp") + "\\" + FileUpload1.FileName + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", ""));
               }
               else if(!Directory.Exists(Path.GetDirectoryName(serverFilePath)))
               {
                   Directory.CreateDirectory(serverFilePath);
               }
                FileUpload1.SaveAs(serverFilePath);
            }


            List<string> fileToCopy = (List<string>)Session["fileToCopy"];
            List<string> uploadedFiles = (List<string>)Session["uploadedFiles"];
            fileToCopy.Remove(txtCopyingFile.Text);
            uploadedFiles.Add(txtCopyingFile.Text);
            txtCopyingFile.Text  = fileToCopy[0];
           

            GridView1.DataSource = fileToCopy;
            GridView2.DataSource = uploadedFiles;
            

        }


    }
}
