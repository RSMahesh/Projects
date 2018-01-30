using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class ImageLoader
    {
        List<int> imageColIndexs = new List<int>();
        List<DataGridViewColumn> imgcolumns = new List<DataGridViewColumn>();
        DataGridView dataGridView;
        string excelFilePath;
        public ImageLoader(DataGridView dataGridView, string excelFilePath)
        {
            this.dataGridView = dataGridView;
            this.excelFilePath = excelFilePath;
        }

        private void GetImageColumns()
        {
            for (var i = 0; i < dataGridView.Columns.Count; i++)
            {
                try
                {
                    var uu = new Uri(dataGridView.Rows[0].Cells[i].Value.ToString());
                    if (uu.Scheme.StartsWith("http"))
                    {
                        imageColIndexs.Add(i);
                        dataGridView.Columns[i].Visible = false;
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

      
        public void AddImageColumn()
        {
            GetImageColumns();

            List<DataGridViewImageColumn> lst = new List<DataGridViewImageColumn>();

            foreach (var cell in imageColIndexs)
            {
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn.HeaderText = "Image";
                imgColumn.Width = Constants.ImageIconSize + 20;
           
                dataGridView.Columns.Insert(1, imgColumn);
                imgcolumns.Add(imgColumn);
                lst.Add(imgColumn);
                // imgColumn.Frozen = true;
            }

            foreach (DataGridViewImageColumn col in lst)
            {
                col.Frozen = true;
            }


        }
        public void LoadImageInCell()
        {
            if (ConfigurationManager.AppSettings["DontNotLoadImage"] != null
                  && bool.Parse(ConfigurationManager.AppSettings["DontNotLoadImage"]))
            {
                return;
            }

            WebClient wc = new WebClient();
            for (int rowIndex = 0; rowIndex < dataGridView.Rows.Count; rowIndex++)
            {

                for (var i = 0; i < imageColIndexs.Count; i++)
                {
                    try
                    {

                        if (dataGridView.Rows[rowIndex].Cells[imageColIndexs[i]].Value != null)
                        {
                            var url = dataGridView.Rows[rowIndex].Cells[imageColIndexs[i] + imageColIndexs.Count].Value.ToString();

                            if (string.IsNullOrEmpty(url))
                            {
                                continue;
                            }

                            var uri = new Uri(url);
                            var filePAth = GetLocalImagePath(uri);

                            if (!Directory.Exists(Path.GetDirectoryName(filePAth)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(filePAth));
                            }

                            if (!File.Exists(filePAth))
                            {
                                wc.DownloadFile(url, filePAth);
                            }

                            var thumbFilePath = Path.Combine(Path.GetDirectoryName(filePAth), Path.GetFileNameWithoutExtension(filePAth) + "_thumb" + ".ico");
                            Image thumNailImage = null;

                            if (File.Exists(thumbFilePath))
                            {
                                thumNailImage = Image.FromFile(thumbFilePath);
                            }
                            else
                            {
                                var img = Image.FromFile(filePAth);
                                thumNailImage = ImageThumbnailDataGridView.Helper.ResizeImage(img, Constants.ImageIconSize, Constants.ImageIconSize, false);
                                thumNailImage.Save(thumbFilePath, ImageFormat.Icon);
                                img.Dispose();
                            }

                            dataGridView.Rows[rowIndex].Cells[imgcolumns[i].Index].Value = thumNailImage;
                            dataGridView.Rows[rowIndex].Cells[imgcolumns[i].Index].Tag = filePAth;

                        }

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        private string GetLocalImagePath(Uri uri)
        {
            var tt = excelFilePath.Split(new string[] { "_dataBackup" }, StringSplitOptions.None)[0];
            return Path.GetDirectoryName(tt) + "\\" +
                       Path.GetFileNameWithoutExtension(tt) + "-images"
                       + uri.LocalPath.Replace("/", "\\");
        }
    }
}
