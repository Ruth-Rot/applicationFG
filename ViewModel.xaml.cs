using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ViewModel.xaml
    /// </summary>
    public partial class ViewModel : INotifyPropertyChanged
    {
        private IModel model;

        public ViewModel(Model m)
        {
            this.model = m;
            InitializeComponent();
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            // if (this.PropertyChanged != null)
            //   this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public float VM_Throttle
        {
            get
            {
                return model.Throttle;
            }
            set
            {
                model.Throttle = value;
            }
        }

        public float VM_Rudder
        {
            get
            {
                return model.Rudder;
            }
            set
            {
                model.Rudder = value;
            }
        }

        public float VM_Elevator
        {
            get
            {
                return model.Elevator;
            }
            set
            {
                model.Elevator = value;
            }
        }


        public int VM_Current_line
        {
            get
            {
                return model.Current_line;
            }
            set
            {
                model.Current_line = value;
            }
        }

        public int VM_Num_of_lines
        {
            get
            {
                return model.Num_of_lines;
            }
            set
            {
                model.Num_of_lines = value;
            }
        }

        public int VM_Sleep
        {
            get
            {
                return model.Sleep;
            }
            set
            {
                model.Sleep = value;
            }
        }

        public string VM_File_path
        {
            get
            {
                return model.File_path;
            }
            set
            {
                model.File_path = value;
            }
        }

        public Boolean VM_Stop
        {
            get
            {
                return model.Stop;
            }
            set
            {
                model.Stop = value;
            }
        }
    
        public float VM_Ailrone
        {
            get
            {
                return model.Aileron;
            }
        }

        public float VM_Altimeter
        {
            get
            {
                return model.Altimeter;
            }
        }
        public float VM_AirSpeed
        {
            get
            {
                return model.AirSpeed;
            }
        }
        public float VM_Pitch
        {
            get
            {
                return model.Pitch;
            }
        }
        public float VM_Roll
        {
            get
            {
                return model.Roll;
            }
        }
        public float VM_Yaw
        {
            get
            {
                return model.Yaw;
            }
        }
        public float VM_Heading
        {
            get
            {
                return model.Heading;
            }
        }
    }
}
    

