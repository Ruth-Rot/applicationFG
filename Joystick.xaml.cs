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

      /*  Boolean CheckDist(float x, float y)
        {
            if (Math.Sqrt(Math.Pow(x - 1, 2)+ Math.Pow(y - 3, 2))<=40)
            {
                return true;
            }
            return false;
        }*/
        void change()
        {
            Move()
        }
        void MoveKnob(float x,float y)
        {
            float normalX = x * 40 + 1;
            float normalY = y * 40 + 3;
                knobPosition.Y = normalY ;
                knobPosition.X = normalX;


            //}
        }
        void MoveThrottle(float t)
        {
            float normalT = t * 340 + 80;
            throttlePosition = normalT; //move zir y
            //check if smaller then 420;
        }

        void MoveRudder(float r)
        {
            float normalT = r * 350 + 180;
            throttlePosition = normalT; //move zir x
            //check if smaller than 530;
        }

    }
}
