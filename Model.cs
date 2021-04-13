using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Linq;
namespace app00
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
        private string time_passed;
        private double time_in_num;
        private List<String> xmlNames;
        private float aileron;
        private float elevator;
        private float rudder;
        //private float flaps;
        //private float slats;
        //private float speed_brake;
        private float heading;
        private float pitch;
        private float yaw;
        private float roll;
        private float airSpeed;
        private float altimeter;
        //engines
        private float throttle;
        //gear hydraulics
        // private float engine_pump;
        //private float electric_pump;
        //electric
        //private float external_power;
        //private float apu_generator;
        //autoflight position
        //private float latitude_deg;
        //private float longitude_deg;
        //private float altitude_ft;
        List<DataPoint> graphList = new List<DataPoint>();
        String selection;
        String correlated_att;
        // Line linear_reg;
        List<DataPoint> corre_list = new List<DataPoint>();
        private float a;
        private float b;
        private Dictionary<string, string> data;
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
        List<DataPoint> corre_points = new List<DataPoint>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Model(String csv, String xml)
        {
            data = new Dictionary<string, string>();
            file_path = csv;
            xml_path = xml;
            stop = false;
            current_line = 0;
            timeWait = 100;
            time_in_num = 0;
            file = new List<string>(42);
            new Thread(delegate ()
            {
                SaveFile();
                SaveXml();
                putInMap();
            }).Start();
            ConnectFg();
        }
        public void ConnectFg()
        {
            var client = new TcpClient("localhost", 5400);
            NetworkStream ns = client.GetStream();
            string line;
            new Thread(delegate ()
            {
                line = file[current_line] + "\r\n";
                while (current_line != num_of_lines + 1)
                {
                    if (current_line != num_of_lines)
                    {
                        line = file[current_line] + "\r\n";
                    }
                    if (!stop && (current_line != num_of_lines))
                    {
                            InitialProperties();
                            time_in_num = (current_line / 10);
                            time_passed = TimeSpan.FromSeconds(time_in_num).ToString();
                            NotifyPropertyChanged("TimePassed");
                            current_line++;
                            NotifyPropertyChanged("Current_line");
                    }
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
            NotifyPropertyChanged("XmlNames");
        }

        private void InitialProperties()
        {
            String[] arrProperties = file[current_line].Split(',');
            Aileron = float.Parse(arrProperties[xmlNames.IndexOf("aileron")]);
            Elevator = float.Parse(arrProperties[xmlNames.IndexOf("elevator")]);
            Rudder = float.Parse(arrProperties[xmlNames.IndexOf("rudder")]);
            Throttle = float.Parse(arrProperties[xmlNames.IndexOf("throttle")]);
            Altimeter = float.Parse(arrProperties[xmlNames.IndexOf("altitude-ft")]);
            AirSpeed = float.Parse(arrProperties[xmlNames.IndexOf("airspeed-kt")]);
            Heading = float.Parse(arrProperties[xmlNames.IndexOf("heading-deg")]);
            Pitch = float.Parse(arrProperties[xmlNames.IndexOf("pitch-deg")]);
            Yaw = float.Parse(arrProperties[xmlNames.IndexOf("side-slip-deg")]);
            Roll = float.Parse(arrProperties[xmlNames.IndexOf("roll-deg")]);
            if (selection != null)
            {
                new Thread(delegate ()
                {
                    getListOfPoints();
                }).Start();
            }
        }

        public void NotifyPropertyChanged(String propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void getListOfPoints()
        {
            List<DataPoint> l = new List<DataPoint>(num_of_lines);
            List<DataPoint> l1 = new List<DataPoint>(num_of_lines);
            int i;
            double y;
            DataPoint p;
            double y1;
            DataPoint p1;
            for (i = 0; i < current_line; i++)
            {
                String[] arrProperties = file[(int)i].Split(',');
                y = float.Parse(arrProperties[xmlNames.IndexOf(selection)]);
                p = new DataPoint(i, y);
                l.Add(p);
                y1 = float.Parse(arrProperties[xmlNames.IndexOf(correlated_att)]);
                p1 = new DataPoint(i, y1);
                l1.Add(p1);
            }
            GraphList = l;
            NotifyPropertyChanged("GraphList");
            Corre_points = l1;
            NotifyPropertyChanged("Corre_point");
        }
        public string findCorrelatedFeature(string feature)
        {
            float maxResult = 0;
            string nameOfCorrelated = "";
            float pearsonResult;
            float[] arr = new float[num_of_lines]; float[] brr = new float[num_of_lines];
            for (int j = 0; j < xmlNames.Count(); j++)
            {
                string currentfeature = xmlNames[j];
                if (feature != currentfeature)
                {
                    for (int i = 0; i < num_of_lines; i++)
                    {
                        String[] arrProperties = file[i].Split(',');
                        arr[i] = float.Parse(arrProperties[xmlNames.IndexOf(feature)]);
                        brr[i] = float.Parse(arrProperties[xmlNames.IndexOf(currentfeature)]);
                    }
                    pearsonResult = (float)(cov(arr, brr, num_of_lines) / (Math.Sqrt(var(arr, num_of_lines)) * Math.Sqrt(var(brr, num_of_lines))));
                    if (Math.Abs(pearsonResult) >= Math.Abs(maxResult)
                        || j == 0)
                    {
                        maxResult = pearsonResult;
                        nameOfCorrelated = currentfeature;
                    }
                }
            }
            return nameOfCorrelated;
        }

        public void putInMap()
        {
            string nameOfCorrelated;
            for (int i = 0; i < xmlNames.Count(); i++)
            {
                nameOfCorrelated = findCorrelatedFeature(xmlNames[i]);
                if (!data.ContainsKey(xmlNames[i]))
                {
                    data.Add(xmlNames[i], nameOfCorrelated);
                }
            }
        }

        public void getArrays()
        {
            corre_list.Clear();
            float sum1 = 0;
            float sum2 = 0;
            float sumCov = 0;
            float sumVar = 0;
            //sleep/10 is the number of lines in a second
            int k = xmlNames.IndexOf(selection);
            int j = xmlNames.IndexOf(correlated_att);
            float n, m;

            for (int t = Sleep / 10 * 30; t > 0; t--)
            {
                String[] arrProperties = file[num_of_lines - t].Split(',');
                n = float.Parse(arrProperties[k]);
                m = float.Parse(arrProperties[j]);
                corre_list.Add(new DataPoint(n, m));
                sum1 += n;
                sum2 += m;
                sumCov += n * m;
                sumVar += n * n;
            }
            NotifyPropertyChanged("Corre_list");
            sumCov /= (Sleep / 10 * 30);
            float x1 = sum1 / (Sleep / 10 * 30);
            float y1 = sum2 / (Sleep / 10 * 30);
            A = (sumCov - x1 * y1) / (sumVar / (Sleep / 10 * 30) - x1 * x1);
            B = y1 - (a * x1);
        }

        public float var(float[] x, int size)
        {
            float result = 0;
            float av;
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * x[i];
                result += x[i];
            }
            av = result / size;
            return sum / size - av * av;
        }
        // returns the covariance of X and Y
        public float cov(float[] x, float[] y, int size)
        {
            float sum = 0;
            float sum1 = 0;
            float sum2 = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * y[i]; sum1 += x[i];
                sum2 += y[i];
            }
            sum /= size;
            float x1 = sum1 / size;
            float y1 = sum2 / size;
            return sum - x1 * y1;
        }
        
        public float Aileron
        {
            get
            {
                return aileron * 60 + 50;
            }
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }
        public List<DataPoint> GraphList
        {
            get
            {
                return graphList;
            }
            set
            {
                graphList = value;
                NotifyPropertyChanged("GraphList");
            }
        }
        public String Selection
        {
            set
            {
                selection = value;
                NotifyPropertyChanged("Selection");
                correlated_att = data.FirstOrDefault(x => x.Key == selection).Value;
                NotifyPropertyChanged("Correlated_att");
                getArrays();
            }
            get
            {
                return selection;
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
                return elevator * 60 + 50;
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
                return rudder * 200 + 20;
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
                return throttle * 200 - 20;
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
        public List<String> XmlNames
        {
            get
            {
                return xmlNames;
            }
            set
            {
                xmlNames = value;
                NotifyPropertyChanged("XmlNames");
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
                return yaw + 20;
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
        public string TimePassed
        {
            get
            {
                return time_passed;
            }
            set
            {
                time_passed = value;
                NotifyPropertyChanged("TimePassed");
            }
        }
        public string Correlated_att
        {
            get
            {
                return correlated_att;
            }
            set
            {
                correlated_att = value;
                NotifyPropertyChanged("Correlated_att");
            }
        }
        public List<DataPoint> Corre_points
        {
            get
            {
                return corre_points;
            }
            set
            {
                corre_points = value;
                NotifyPropertyChanged("Corre_points");
            }
        }
        public List<DataPoint> Corre_list
        {
            get
            {
                return corre_list;
            }
            set
            {
                corre_list = value;
                NotifyPropertyChanged("Corre_list");
            }
        }
        public float A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
                NotifyPropertyChanged("A");
            }
        }
        public float B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
                NotifyPropertyChanged("B");
            }
        }

        //public string File_path { set; get; }
    }
}
