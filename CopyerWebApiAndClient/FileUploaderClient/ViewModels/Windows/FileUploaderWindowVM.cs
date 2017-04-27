using MedwriteDesktopApp.Commands;
using MedwriteDesktopApp.Models.Windows;
using MedwriteDesktopApp.Views.UserControls;
using MedwriteServiceInterface.DataModel.FileUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedwriteDesktopApp.ViewModels.Windows
{
    class FileUploaderWindowVM : ViewModelBase
    {
        FileUploaderWindowModel fileUploaderWindowModel;
        int _userId;

        public FileUploaderWindowVM(int userId)
        {
            HTTP = true;
            SelectedFile = "Click Here To Browse";
            _userId = userId;
            fileUploaderWindowModel = new FileUploaderWindowModel(userId);
        }
        #region Commands
        private RelayCommand _UploadFileCommand;
        /// <summary>
        /// Command Bind with  Upload File Button
        /// </summary>
        public ICommand UploadFileCommand
        {
            get
            {
                if (_UploadFileCommand == null) _UploadFileCommand = new RelayCommand(param => this.UploadFile(param));
                return _UploadFileCommand;
            }
        }

        private void UploadFile(object param)
        {

            ItemsControl itemsControl = (ItemsControl)param;
            FileUploaderControl uploaderControl = new FileUploaderControl(119, SelectedFile, GetUploadMethod());
            itemsControl.Items.Add(uploaderControl);
            SelectedFile = "Click Here To Browse";
        }

        private string GetUploadMethod()
        {
            string uploadMethod = "HTTP";
            if (HTTP)
                uploadMethod = "HTTP";
            else if (FTP)
                uploadMethod = "FTP";
            else if (VNC)
                uploadMethod = "VNC";

            return uploadMethod;
        }

        private RelayCommand _BrowserFileCommand;
        /// <summary>
        /// Command Bind with  Upload File Button
        /// </summary>
        public ICommand BrowserFileCommand
        {
            get
            {
                if (_BrowserFileCommand == null) _BrowserFileCommand = new RelayCommand(param => this.BrowserFile(param));
                return _BrowserFileCommand;
            }
        }

        private void BrowserFile(object param)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            // dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                SelectedFile = filename;
            }

            //fileUploaderControlModel.CancelUpload();
        }
        #endregion
        #region Properties
        public bool HTTP { get; set; }
        public bool FTP { get; set; }
        public bool VNC { get; set; }

        string _SelectedFile;
        public string SelectedFile
        {
            get
            {
                return _SelectedFile;
            }
            set
            {
                _SelectedFile = value;
                NotifyPropertyChanged("SelectedFile");
            }
        }
        #endregion

        public void CheckPendingUploads(ItemsControl itemsControl)
        {
            PendingUploadsResponse pendingUploadsResponse = fileUploaderWindowModel.GetPendingUploads(_userId);
            foreach (var pUpload in pendingUploadsResponse.PendingUploads)
            {
                FileUploaderControl uploaderControl = new FileUploaderControl(pUpload.UserId, pUpload.ClientFilePath, pUpload.UploadMethod);
                itemsControl.Items.Add(uploaderControl);
                SelectedFile = "Click Here To Browse";
            }
        }
    }
}
