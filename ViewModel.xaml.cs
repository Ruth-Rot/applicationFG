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
        private Model model;

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
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
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

        public float VM_Ailrone
        {
            get
            {
                return model.Aileron;
            }
            set
            {
                model.Aileron = value;
            }
        }

        public float VM_Line_num
        {
            get
            {
                return model.Line_num;
            }
            set
            {
                model.Line_num = value;
            }
        }

        public float VM_Sleep
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

        public void VM_ConnectFg()
        {
            model.ConnectFg();
        }


        public void VM_Continue()
        {
            model.Continue();
        }

        public void VM_Back()
        {
            model.Back();
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
    }
}
    

