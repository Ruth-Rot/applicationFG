using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static app0.Model;

namespace app0
{
    interface IModel : INotifyPropertyChanged
    {
        
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
        Boolean Stop { set; get; }
        int Sleep { set; get; }
        int Current_line { set; get; }
        int Num_of_lines { set; get; }

        string TimePassed { set; get; }
        List<String> XmlNames { get; set; }
       
        List<DataPoint> GraphList { get; set; }

        String Selection { get; set; }

    }
}
