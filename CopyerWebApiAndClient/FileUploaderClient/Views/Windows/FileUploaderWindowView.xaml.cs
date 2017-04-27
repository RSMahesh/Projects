using MedwriteDesktopApp.ViewModels.UserControls;
using MedwriteDesktopApp.ViewModels.Windows;
using MedwriteDesktopApp.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MedwriteDesktopApp.Views.Windows
{
    /// <summary>
    /// Interaction logic for FileUploader.xaml
    /// </summary>
    public partial class FileUploaderWindowView: Window
    {
        double  oh = 0;
        double btnoh = 0;
        int minH = 40;
        public FileUploaderWindowView()
        {
            InitializeComponent();
            var vm = new FileUploaderWindowVM(119);
            this.DataContext = vm;
            vm.CheckPendingUploads(itemsControl1);
            oh = this.Height;
            //btnoh = btnMini.
            
        }

        public System.Collections.ObjectModel.ObservableCollection<FileUploaderControlVM> FileUploaders { get; set; }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(this.Height==oh)
            {

                this.Height = minH;
                this.Top = this.Top + (oh - minH);
                this.WindowStyle = System.Windows.WindowStyle.None; // removes top bar (icon, title, close buttons etc)
             //   this.AllowsTransparency = true; //removes the border around the outside
                grdMain.Visibility = System.Windows.Visibility.Collapsed;
                
            }
            else
            {
                this.Height = oh;
                this.Top = this.Top - (oh - minH);
                grdMain.Visibility = System.Windows.Visibility.Visible;
                this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow; // removes top bar (icon, title, close buttons etc)

            }
        }


        void CollapseGrid()
        {
            grdMain.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var item in itemsControl1.Items)
            {
                var itm = (FileUploaderControl)item;
                if (itm.btnPause.Visibility == System.Windows.Visibility.Visible)
                {
                    if (itm.btnPause.IsChecked == false)
                    {
                        MessageBox.Show("Working... Go and Stop");
                        e.Cancel = true;
                        return;
                    }
                }
                
            }
        }

       

       
    }

 
}
