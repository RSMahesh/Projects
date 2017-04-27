using MedwriteDesktopApp.ViewModels.UserControls;
using MedwriteDesktopApp.ViewModels.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedwriteDesktopApp.Views.UserControls
{
    /// <summary>
    /// Interaction logic for FileUploaderControl.xaml
    /// </summary>
    public partial class FileUploaderControl : UserControl
    {
        public FileUploaderControl(int userId, string file,string uploadMethod )
        {
            InitializeComponent();
            this.DataContext = new FileUploaderControlVM(userId, file, uploadMethod);
        }

    
        private void UserControl_Unloaded_1(object sender, RoutedEventArgs e)
        {
            var vm = (FileUploaderControlVM)this.DataContext;
            vm.StopUploadCommand.Execute(null);
       
        }

    }
}
