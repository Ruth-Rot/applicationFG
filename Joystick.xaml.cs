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

namespace ass1
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        public Joystick()
        {
            InitializeComponent();
        }

        void Move(Double x,Double y)
        {

        }

        void up()
        {
            knobPosition.Y += 20;
        }

        void Down()
        {
            knobPosition.Y += 20;
        }

        void Left()
        {
            knobPosition.X += 20;
        }

        void Right()
        {
            knobPosition.X += 20;
        }

    }
}
