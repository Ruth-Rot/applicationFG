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
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : UserControl
    {
        private ViewModel vm;
        

        public Graph(ViewModel v)
        {
            vm = v;
            InitializeComponent();
            DataContext = vm;
        }

        private void Properties_SelectionChanged(object sender, SelectionChangedEventArgs e){}
    }
}
