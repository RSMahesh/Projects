using FileUploaderClient;
using MedwriteDesktopApp.Commands;
using MedwriteDesktopApp.Models.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace MedwriteDesktopApp.ViewModels.UserControls
{
    public class FileUploaderControlVM : ViewModelBase
    {
        FileUploaderControlModel fileUploaderControlModel;
        public FileUploaderControlVM(int userID, string file,string uploadMethod)
        {
            this.UploadMethod = uploadMethod;
            fileUploaderControlModel = new FileUploaderControlModel(userID, file, new Action<int, string>(UpdateProgress),uploadMethod);
            MaxProgress = fileUploaderControlModel.GetProgressLength();
            FileName = System.IO.Path.GetFileName(file);
            fileUploaderControlModel.OnUploadCompleted += OnUploadCompleted;
            fileUploaderControlModel.StartUpload();

        }

        private void UpdateProgress(int progress, string progressText)
        {

            App.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                 new Action(() => UpdateUIonProgress(progress, progressText)));
        }


        private void UpdateUIonProgress(int progess, string progressText)
        {
            CurrentProgress += progess;
            UploadedData = progressText;
        }

        private void OnUploadCompleted()
        {
            UploadCompleted = true;
            NotifyPropertyChanged("UploadCompleted");
        }

        #region Commands
        private RelayCommand _StopUploadCommand;
        /// <summary>
        /// Command Bind with Stop Upload Button
        /// </summary>
        public ICommand StopUploadCommand
        {
            get
            {
                if (_StopUploadCommand == null) _StopUploadCommand = new RelayCommand(param => this.StopUpload(param));
                return _StopUploadCommand;
            }
        }

        private void StopUpload(object param)
        {
            fileUploaderControlModel.CancelUpload();
            while (fileUploaderControlModel.Running)
                Thread.Sleep(100);
        }

        private RelayCommand _ResumeUploadCommand;
        /// <summary>
        /// Command Bind with Resume Upload Button
        /// </summary>
        public ICommand ResumeUploadCommand
        {
            get
            {
                if (_ResumeUploadCommand == null) _ResumeUploadCommand = new RelayCommand(param => this.ResumeUpload(param));
                return _ResumeUploadCommand;
            }
        }

        private void ResumeUpload(object param)
        {
            fileUploaderControlModel.ResumeUpload();
        }

        #endregion

        #region properties

        public string  UploadMethod { get; set; }
        public bool UploadCompleted { get; set; }
        private int currentProgress;
        public int CurrentProgress
        {
            get { return this.currentProgress; }
            private set
            {
                if (this.currentProgress != value)
                {
                    this.currentProgress = value;
                    NotifyPropertyChanged("CurrentProgress");
                }
            }
        }


        private string fileName;
        public string FileName
        {
            get { return this.fileName; }
            private set
            {
                if (this.fileName != value)
                {
                    this.fileName = value;
                    NotifyPropertyChanged("FileName");
                }
            }
        }

        private string uploadedData;
        public string UploadedData
        {
            get { return this.uploadedData; }
            private set
            {
                if (this.uploadedData != value)
                {
                    this.uploadedData = value;
                    NotifyPropertyChanged("UploadedData");
                }
            }
        }

        private int maxProgress;
        public int MaxProgress
        {
            get { return this.maxProgress; }
            private set
            {
                if (this.maxProgress != value)
                {
                    this.maxProgress = value;
                    NotifyPropertyChanged("MaxProgress");
                }
            }
        }
        #endregion
    }
}
