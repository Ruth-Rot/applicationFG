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

        float Aileron { set; get; }
        float Elevator { set; get; }
        float Rudder { set; get; }
        float Throttle { set; get; }

        float Altimeter { set; get; }
        float AirSpeed { set; get; }
        float Pitch { set; get; }
        float Roll { set; get; }
        float Yaw { set; get; }
        float Heading { set; get; }

    }
}
