using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Xml.Linq;
using System.Xml.Resolvers;


namespace app0
{
    /// <summary>
    /// Interaction logic for ConnectServer.xaml
    /// </summary>
    public class Model : IModel
    {
        //private LoadPath load;

        // flight controls
        private string file_path;
        private string xml_path;

        private List<String> xmlNames;

        private float aileron;
        private float elevator;
        private float rudder;
        private float flaps;
        private float slats;
        private float speed_brake;
        private float heading;
        private float pitch;
        private float yaw;
        private float roll;
        private float airSpeed;
        private float altimeter;

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

        int timeWait;
        private List<string> file;
        int current_line;
        int num_of_lines;
        Boolean stop;

        public event PropertyChangedEventHandler PropertyChanged;

        public Model(String csv, String xml)
        {
            file_path = csv;
            xml_path = xml;
            stop = false;
            current_line = 0;
            timeWait = 100;
            file = new List<string>(42);
            SaveFile();
            SaveXml();
            ConnectFg();
        }

        public void ConnectFg()
        {
            var client = new TcpClient("localhost", 5400);
            NetworkStream ns = client.GetStream();
            string line;


            new Thread(delegate ()
            {
                while (current_line != num_of_lines)
                {
                    line = file[current_line] + "\r\n";
                    if (!stop)
                    {
                        current_line++;
                    }
                    //add line to list
                    // change linenum
                    Console.WriteLine(line);
                    //InitialProperties();
                    ns.Write(System.Text.Encoding.ASCII.GetBytes(line), 0, System.Text.Encoding.ASCII.GetBytes(line).Length);
                    ns.Flush();
                    Thread.Sleep(timeWait);
                }
            }).Start();
        }


        public void SaveFile()
        {
            var bufferr = new System.IO.StreamReader(file_path);
            string line;
            while ((line = bufferr.ReadLine()) != null)
            {
                file.Add(line);
            }
            var lineCount = File.ReadLines(file_path).Count();
            num_of_lines = lineCount;
        }

        public void SaveXml()
        {
            XDocument xml = XDocument.Load(xml_path);
            IEnumerable<string> temp = xml.Descendants("output").Descendants("name").Select(name => (string)name);
            xmlNames = temp.ToList();
        }

        private void InitialProperties()
        {
            String[] arrProperties = file[current_line].Split(',');
            Aileron = float.Parse(arrProperties[xmlNames.IndexOf("aileron")]);
            Elevator = float.Parse(arrProperties[xmlNames.IndexOf("elevator")]);
            Rudder = float.Parse(arrProperties[xmlNames.IndexOf("rudder")]);
            Throttle = float.Parse(arrProperties[xmlNames.IndexOf("throttle")]);
        }

        public void NotifyPropertyChanged(String propName)
        {
            /* if (this.PropertyChanged != null)
             {
                 this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
             }*/
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public float Aileron
        {
            get
            {
                return aileron * 40 + 1;
            }
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }

        public float Elevator
        {
            set
            {
                elevator = value;
                NotifyPropertyChanged("Elevator");
            }
            get
            {
                return elevator * 40 + 3;
            }
        }
        public float Rudder
        {
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
            get
            {
                return rudder * 350 + 180;
            }
        }
        public float Throttle
        {
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
            }
            get
            {
                return throttle * 340 + 80;
            }
        }

        public float Altimeter
        {
            set
            {
                altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
            get
            {
                return altimeter;
            }
        }
        public float AirSpeed
        {
            set
            {
                airSpeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
            get
            {
                return airSpeed;
            }
        }
        public float Pitch
        {
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
            get
            {
                return pitch;
            }
        }
        public float Roll
        {
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
            get
            {
                return roll;
            }
        }
        public float Yaw
        {
            set
            {
                yaw = value;
                NotifyPropertyChanged("Yaw");
            }
            get
            {
                return yaw;
            }
        }
        public float Heading
        {
            set
            {
                heading = value;
                NotifyPropertyChanged("Heading");
            }
            get
            {
                return heading;
            }
        }

        public int Current_line
        {
            get
            {
                return current_line;
            }
            set
            {
                current_line = value;
                NotifyPropertyChanged("Current_line");
            }
        }
        public int Num_of_lines
        {
            get
            {
                return num_of_lines;
            }
            set
            {
                num_of_lines = value;
                NotifyPropertyChanged("Num_of_lines");
            }
        }
        public Boolean Stop
        {
            get
            {
                return stop;
            }
            set
            {
                stop = value;
                NotifyPropertyChanged("Stop");
            }
        }
        public int Sleep
        {
            get
            {
                return timeWait;
            }
            set
            {
                timeWait = value;
                NotifyPropertyChanged("Sleep");
            }
        }

        public string File_path { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public string File_path { set; get; }
    }
     
}
