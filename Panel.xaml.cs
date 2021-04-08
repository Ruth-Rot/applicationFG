using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace app0.controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Panel : UserControl
    {
        Model model;
        ViewModel vm;
        View view;

        public Panel()
        {
            InitializeComponent();
            model = new Model();
            vm = new ViewModel(model);
            this.DataContext = vm;
        }

        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
       
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_Sleep = 100;

        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_Sleep = -1;
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlaySpeed_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Speed_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        

        private void Clock_changed(object sender, TextChangedEventArgs e)
        {
            double time = 0.1 * vm.VM_Line_num;
            string strTime = time.ToString();
            DateTime dt = DateTime.ParseExact(strTime, "HHmm", CultureInfo.InvariantCulture);
            string timestring = dt.ToString("H:mm:ss");
        }
    }
}
