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
using Microsoft.Win32;


namespace app0
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Page
    {
        private ViewModel vm;
        private string filePath;
       

        public View()
        {
            InitializeComponent();

        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                //NavigationService.Navigate(home);
                this.filePath = System.IO.Path.GetFullPath(openFileDialog.FileName);

                vm = new ViewModel(new Model());
                vm.VM_File_path = filePath;
                DataContext = vm;
                

                //vm = new ViewModel(new Model(filePath));
                //DataContext = vm;
            }
        }

        /*public void set_VM(ViewModel viewModel)
        {
            vm = viewModel;
            DataContext = vm;
            vm.VM_File_path = filePath;
        }*/

        public string Filepath
        {
            get { return filePath; }
        }
    }
}
