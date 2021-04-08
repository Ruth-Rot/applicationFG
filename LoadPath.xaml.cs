using Microsoft.Win32;
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

namespace app0
{
    /// <summary>
    /// Interaction logic for LoadPath.xaml
    /// </summary>
    public partial class LoadPath : Page
    {
        public string filePath;
        List<String> File;
        int LineNum;

        Model model;

        public LoadPath()
        {
            InitializeComponent();
            LineNum = 0;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = System.IO.Path.GetFullPath(openFileDialog.FileName);
                model = new Model();
                //model = new Model(filePath);
            }
        }

        public string Filepath
        {
            get { return filePath; }
        }
    }
}
