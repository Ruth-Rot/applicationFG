using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ConnectServer.xaml
    /// </summary>
    public partial class Model : IModel
    {
        //private LoadPath load;

        // flight controls

        private float aileron;
        private float elevator;
        private float rudder;
        private float flaps;
        private float slats;
        private float speed_brake;

        //engines
        private float throttle;

        //gear hydraulics
        private float engine_pump;
        private float electric_pump;
        
        //electric
        private float external_power;
        private float apu_generator;

        //autoflight position
        private float latitude_deg;
        private float longitude_deg;
        private float altitude_ft;

        //orientation
        /*private float roll_deg;
        private float pitch_deg;
        private float heading_deg;
        private float side_slip_deg;

        //velocities
        private float airspeed_kt;
        private float glideslope;
        private float vertical_speed_fps;

        //accelerations, surface positions, instruments
        private float airspeed_indicator_indicated_speed_kt;
        private float altimeter_indicated_altitude_ft;
        private float altimeter_pressure_alt_ft;
        private float attitude_indicator_indicated_pitch_deg;
        private float attitude_indicator_indicated_roll_deg;
        private float attitude_indicator_internal_pitch_deg;
        private float attitude_indicator_internal_roll_deg;
        private float encoder_indicated_altitude_ft;
        private float encoder_pressure_alt_ft;
        private float gps_indicated_altitude_ft;
        private float gps_indicated_ground_speed_kt;
        private float gps_indicated_vertical_speed;
        private float indicated_heading_deg;
        private float magnetic_compass_indicated_heading_deg;
        private float slip_skid_ball_indicated_slip_skid;*/


        //volatile Boolean stop;
        int fileLine;
        int line_num;
        int timeWait;
        private List<string> file;
        public event PropertyChangedEventHandler PropertyChanged;

        public Model(string path)
        {
            line_num = 0;
            timeWait = 100;
            file = new List<string>(42);
            SaveFile(path);
            ConnectFg();
        }

        public void ConnectFg()
        {
            var client = new TcpClient("localhost", 5400);
            NetworkStream ns = client.GetStream();
            // var file = new System.IO.StreamReader(@"C:\Users\משתמש1\Desktop\reg_flight.csv");
            string line;
            while (line_num != file.Capacity)
            {
                line = file[line_num] + "\r\n";
                line_num++;
                //add line to list
                // change linenum
                Console.WriteLine(line);
                ns.Write(System.Text.Encoding.ASCII.GetBytes(line), 0, System.Text.Encoding.ASCII.GetBytes(line).Length);
                ns.Flush();
                Thread.Sleep(timeWait);
            }
        }
        

        
        public void SaveFile(string fullPath)
        {
           
            var bufferr = new System.IO.StreamReader(fullPath);
            string line;
            while ((line = bufferr.ReadLine()) != null)
            {
                file.Add(line);
            }
            fileLine = file.Capacity;
        }

        /*public void ConnectFg(string fullPath)
        {
            if (fullPath != null)
            {
                var client = new TcpClient("localhost", 5400);
                NetworkStream ns = client.GetStream();
                var file = new System.IO.StreamReader(fullPath);
                string line;
                while ((line = file.ReadLine()) != null && !stop)
                {
                    line += "\r\n";
                    Console.WriteLine(line);
                    ns.Write(System.Text.Encoding.ASCII.GetBytes(line), 0, System.Text.Encoding.ASCII.GetBytes(line).Length);
                    ns.Flush();
                    Thread.Sleep(100);
                }
            }
        }*/

        /*public void Start()
        {
            stop = false;
        }

        public void Stop()
        {
            stop = true;
        }*/

        public void Continue()
        {
        }

        public void Back()
        {
        }

        public double Aileron { set; get; }
        public double Elevator { set; get; }
        public double Rudder { set; get; }
        public double Throttle { set; get; }

        public double Line_num { set; get; }
        public double Sleep { set; get; }

    }
}
