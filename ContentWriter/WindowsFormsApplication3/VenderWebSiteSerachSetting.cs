using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    partial class VenderWebSiteSerachSetting : Form
    {
        AppContext appContext;
        public VenderWebSiteSerachSetting(AppContext appContext)
        {
            this.appContext = appContext;
            InitializeComponent();
            this.FormClosing += VenderWebSiteSerachSetting_FormClosing;
        }

        public VenderWebSiteSerachSettingInfo CurrentSetting
        {
            get
            {
                return VenderWebSiteSerachSettingInfo.GetSetting();
            }
        }

        public VenderWebSiteSerachSettingInfo GetSettings()
        {
            return VenderWebSiteSerachSettingInfo.GetSetting();
        }

        private void VenderWebSiteSerachSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void VenderWebSiteSerachSetting_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void LoadSetting()
        {
            foreach (DataGridViewColumn col in appContext.dataGridView.Columns)
            {
                if (col.Visible && !(col is DataGridViewImageColumn))
                {
                    cmbFields.Items.Add(col.Name);
                }
            }
            cmbFields.SelectedIndex = 0;

            var seetings = VenderWebSiteSerachSettingInfo.GetSetting();
            txtUrl.Text = seetings.Url;
            cmbFields.SelectedItem = seetings.ColumnName;
            txtReplaceText.Text = seetings.ReplaceWords;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            VenderWebSiteSerachSettingInfo setting = new VenderWebSiteSerachSettingInfo();
            setting.Url = txtUrl.Text;
            setting.ColumnName = cmbFields.SelectedItem.ToString();
            setting.ReplaceWords = txtReplaceText.Text;
            VenderWebSiteSerachSettingInfo.SaveSetting(setting);
            MessageBox.Show("Setting Saved");
        }

        private void txtReplaceText_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class VenderWebSiteSerachSettingInfo
    {
        public string Url { get; set; }

        public string ColumnName { get; set; }

        public string ReplaceWords { get; set; }

        static string dataFile = Path.Combine(Utility.FreeLanceAppDataFolder, "VenderWebSiteSerachSetting.json");

        public static VenderWebSiteSerachSettingInfo GetSetting()
        {

            if (File.Exists(dataFile))
            {
                return Utility.Deserialize<VenderWebSiteSerachSettingInfo>(File.ReadAllText(dataFile));
            }

            return new VenderWebSiteSerachSettingInfo();
        }

        public static void SaveSetting(VenderWebSiteSerachSettingInfo settings)
        {
            File.WriteAllText(dataFile, Utility.Serialize(settings));
        }
    }

}
