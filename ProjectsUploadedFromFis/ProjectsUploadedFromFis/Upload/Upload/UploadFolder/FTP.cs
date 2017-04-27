using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace UploadFolder
{
    public partial class FTP : Form
    {
        List<TreeNode> selectedNodes = new List<TreeNode>();
        public FTP()
        {
            InitializeComponent();
        }

        private void FTP_Load(object sender, EventArgs e)
        {

        }

        private void btnGetRooTFiles_Click(object sender, EventArgs e)
        {
            var response = HTTPRequestHelper.PostUrl(txtServerUrl.Text + "?command=dir");
            var list = JasonToObject(response);


            treeView1.Nodes.Clear();
            string parentDir = Path.GetDirectoryName(list[0].FileName);
            var pNode = treeView1.Nodes.Add(parentDir, parentDir);
            AddNodes(pNode, list);
        }

        private void AddNodes(TreeNode parentNode, List<Info> list)
        {
            foreach (var ino in list)
            {
                if (txtExt.Text.Trim() != "")
                {
                    if (Path.GetExtension(ino.FileName).ToUpperInvariant() != txtExt.Text.ToUpperInvariant())
                    {
                        continue;
                    }
                }

                var node = parentNode.Nodes.Add(ino.FileName, ino.FileName.Replace(parentNode.FullPath + "\\", ""));
                
                if (ino.IsFolder)
                {
                    node.Nodes.Add("", "");
                }
            }

        }

        private List<Info> JasonToObject(string jason)
        {

            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                        new System.Web.Script.Serialization.JavaScriptSerializer();

            return serializer.Deserialize<List<Info>>(jason);
        }


        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "")
            {
                var response = HTTPRequestHelper.PostUrl(txtServerUrl.Text + "?command=dir&path=" + e.Node.FullPath);
                var list = JasonToObject(response);
                e.Node.Nodes[0].Remove();
                AddNodes(e.Node, list);

            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            selectedNodes = new List<TreeNode>();
            CallNodesSelector();
            string result = "";
            bool carryOn = false;

            foreach (var node in selectedNodes)
            {
                Application.DoEvents();

                if (result != "Yes To All")
                {
                    CutsomMessage customMsg = new CutsomMessage();
                    customMsg.msg = node.FullPath;
                    customMsg.ShowDialog();
                    result = customMsg.result;
                    customMsg.Dispose();
                    if (result == "Yes To All" || result == "Yes")
                    {
                        carryOn = true;
                    }
                    if (result == "No")
                    {
                        carryOn = false;
                    }

                    if (result == "Cancel")
                    {
                        return;
                    }
                }
                if (carryOn)
                {
                    var response = HTTPRequestHelper.PostUrl(txtServerUrl.Text + "?command=delete&path=" + node.FullPath);
                    if (response == "done")
                    {
                        node.Remove();
                    }
                    else
                    {
                        MessageBox.Show(response);
                    }
                }
            }
        }

        private void CallNodesSelector()
        {
            TreeNodeCollection nodes = this.treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                GetNodeRecursive(n);
            }
        }

        private void GetNodeRecursive(TreeNode treeNode)
        {
            if (treeNode.Checked == true)
            {
                selectedNodes.Add(treeNode);

            }
            foreach (TreeNode tn in treeNode.Nodes)
            {
                GetNodeRecursive(tn);
            }

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void CheckNodeRecursive(TreeNode treeNode,bool _checked)
        {
            treeNode.Checked = _checked;
            foreach (TreeNode tn in treeNode.Nodes)
            {
                tn.Checked = _checked;
                if (tn.Nodes.Count > 0)
                {
                    CheckNodeRecursive(tn, _checked);
                }
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode tn in e.Node.Nodes)
            {
                tn.Checked = e.Node.Checked;
            }
     
        }
    }
}
