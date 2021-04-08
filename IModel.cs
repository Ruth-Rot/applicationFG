using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app0
{
    interface IModel : INotifyPropertyChanged
    {
        //void ConnectFg(string fullPath);
        // void Start();
        // void Stop();
        /*void Continue();
        void Back();
        double Aileron { set; get; }
        double Elevator { set; get; }
        double Rudder { set; get; }
        double Throttle { set; get; }*/
        string File_path { set; get; }
    }
}
