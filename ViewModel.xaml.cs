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

        public double VM_Throttle
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

        public double VM_Rudder
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

        public double VM_Elevator
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

        public double VM_Ailrone
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

        public void VM_ConnectFg(string path)
        {
            model.ConnectFg(path);
        }

        public bool VM_Stop
        {
            set
            {
                model.Stop = value;
            }
        }

        /*public void VM_Start()
        {
            model.Start();
        }

        public void VM_Stop()
        {
            model.Stop();
        }*/

        public void VM_Continue()
        {
            model.Continue();
        }

        public void VM_Back()
        {
            model.Back();
        }


    }
}
    

