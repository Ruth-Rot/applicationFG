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

namespace app0
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        
        ViewModel vm;
        
        public UserControl1(ViewModel v)
        {
            InitializeComponent();

            this.vm = v; ;
            this.DataContext = vm;
        }
 



        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.VM_Sleep = (float)(100 / (Slider2.Value));
            speed.Text = Slider2.Value.ToString();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_Stop = false;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_Stop = true;

        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            if ((vm.VM_Num_of_lines - vm.VM_Current_line) >= 50)
            {
                vm.VM_Current_line += 50;
            }
            else
            {
                vm.VM_Current_line = vm.VM_Num_of_lines;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if ((vm.VM_Current_line - 50) >= 0)
            {
                vm.VM_Current_line -= 50;
            }
            else
            {
                vm.VM_Current_line = 0;
            }
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
           /* double time = 0.1 * vm.VM_Current_line;
            string strTime = time.ToString();
            DateTime dt = DateTime.ParseExact(strTime, "HHmm", CultureInfo.InvariantCulture);
            string timestring = dt.ToString("H:mm:ss");*/
        }
    }
}
