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
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Page
    {
        ViewModel vm;
        public View()
        {
            InitializeComponent();
            vm = new ViewModel(new Model(""));
            DataContext = vm;
        }
    }
}
