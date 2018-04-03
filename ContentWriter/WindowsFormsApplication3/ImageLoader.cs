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
        public ImageLoader(DataGridView dataGridView) : this(dataGridView, string.Empty)
        {
        }
        public ImageLoader(DataGridView dataGridView, string excelFilePath)
        {
            this.dataGridView = dataGridView;
            this.excelFilePath = excelFilePath;
        }

        public static bool IsValidImageUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                if (!string.IsNullOrEmpty(url.Trim()) && uri.Scheme.StartsWith("http"))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
            }

            return false;
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
            }

            foreach (DataGridViewImageColumn col in lst)
            {
                col.Frozen = true;
                col.Tag = col.Index;
            }


        }
        public void LoadImageInCell()
        {
            if (ConfigurationManager.AppSettings["DontNotLoadImage"] != null
                  && bool.Parse(ConfigurationManager.AppSettings["DontNotLoadImage"]))
            {
                return;
            }

            var filePAth = string.Empty;

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
                            filePAth = GetLocalImagePath(uri, rowIndex);

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
                    catch (WebException ex)
                    {
                        if (ex.Status == WebExceptionStatus.ProtocolError &&
                            ex.Response != null)
                        {
                            var resp = (HttpWebResponse)ex.Response;
                            if (resp.StatusCode == HttpStatusCode.NotFound)
                            {
                                File.Copy(Path.Combine(Application.StartupPath, "Image-not-found.png"), filePAth);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void GetImageColumns()
        {
            for (var colIndex = 0; colIndex < dataGridView.Columns.Count; colIndex++)
            {
                for (var rowInd = 0; rowInd < dataGridView.Rows.Count; rowInd++)
                {
                    var obj = dataGridView.Rows[rowInd].Cells[colIndex].Value;
                    if (obj == null || string.IsNullOrEmpty(obj.ToString().Trim()))
                    {
                        continue;
                    }

                    if (IsValidImageUrl(obj.ToString()))
                    {
                        imageColIndexs.Add(colIndex);
                        dataGridView.Columns[colIndex].Visible = false;
                    }
                    break;
                }
            }
        }

        private string GetLocalImagePath(Uri uri, int rowIndex)
        {
            var filePath = excelFilePath;
            if (string.IsNullOrEmpty(filePath))
            {
                if (dataGridView.Columns.Contains("FilePath"))
                {
                    filePath = dataGridView["FilePath", rowIndex].Value.ToString();
                }
            }

            //if file is backup file then split the path
            var tt = filePath.Split(new string[] { "_dataBackup" }, StringSplitOptions.None)[0];

            return Path.GetDirectoryName(tt) + "\\" +
                       Path.GetFileNameWithoutExtension(tt) + "-images"
                       + uri.LocalPath.Replace("/", "\\");
        }
    }
}
