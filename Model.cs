using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Shapes;
using System.Xml.Linq;


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
        private string time_passed;
        private double time_in_num;

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

        List<DataPoint> graphList;
        String selection;
        String correlated_att;
        Line linear_reg;
        List<DataPoint> corre_list = new List<DataPoint>();
        float a;
        float b;

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
        List<DataPoint> corre_points;


        public event PropertyChangedEventHandler PropertyChanged;

        public Model(String csv, String xml)
        {
            file_path = csv;
            xml_path = xml;
            stop = false;
            current_line = 0;
            timeWait = 100;
            time_in_num = 0;
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
                    //add line to list
                    // change linenum
                    //Console.WriteLine(line);
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
                GraphList = getListOfPoints(selection);
                Corre_points = getListOfPoints(findCorrelatedFeature());
                getLinearReg(corre_list, corre_list.Count());
            }

        }

        public void NotifyPropertyChanged(String propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public List<DataPoint> getListOfPoints(string proprety)
        {
            List<DataPoint> l = new List<DataPoint>(num_of_lines);
            double i;
            double y;
            DataPoint p;
            for (i = 0; i < current_line; i++)
            {
                String[] arrProperties = file[(int)i].Split(',');
                y = float.Parse(arrProperties[xmlNames.IndexOf(proprety)]);
                p = new DataPoint(i, y);
                l.Add(p);
            }

            return l;
        }


        public string findCorrelatedFeature()
        {
            float maxResult = 0;
            string nameOfCorrelated = "";
            float pearsonResult;

            float[] arr = new float[num_of_lines];
            float[] brr = new float[num_of_lines];

            for (int j = 0; j < xmlNames.Count(); j++)
            {
                string currentfeature = xmlNames[j];
               
                if (selection != currentfeature)
                {
                    for (int i = 0; i < num_of_lines; i++)
                    {
                        String[] arrProperties = file[i].Split(',');
                        arr[i] = float.Parse(arrProperties[xmlNames.IndexOf(selection)]);
                        brr[i] = float.Parse(arrProperties[xmlNames.IndexOf(currentfeature)]);
                    }

                  /*  for (int t = 0; t < num_of_lines; t++)
                    {
                        String[] arrProperties = file[t].Split(',');
                        brr[t] = float.Parse(arrProperties[xmlNames.IndexOf(currentfeature)]);
                    }*/

                    pearsonResult = pearson(arr, brr, num_of_lines);

                    if (Math.Abs(pearsonResult) >= Math.Abs(maxResult)
                        || j == 0)
                    {
                        maxResult = pearsonResult;
                        nameOfCorrelated = currentfeature;
                        
                        for (int t = 0; t < num_of_lines; t++)
                        {
                            String[] arrProperties = file[t].Split(',');
                            brr[t] = float.Parse(arrProperties[xmlNames.IndexOf(currentfeature)]);
                            corre_list.Add(new DataPoint(arr[t], brr[t]));
                        }
                    }
                }
            }
            correlated_att = nameOfCorrelated;
            NotifyPropertyChanged("Correlated_att");
            NotifyPropertyChanged("Corre_list");
            // NotifyPropertyChanged("Corre_points");
            Console.WriteLine(corre_list);
            return nameOfCorrelated;
        }

        // returns the Pearson correlation coefficient of X and Y
        float pearson(float[] x, float[] y, int size)
        {
            return (float)(cov(x, y, size) / (Math.Sqrt(var(x, size)) * Math.Sqrt(var(y, size))));
        }

        float avg(float[] x, int size)
        {
            float sum = 0;
            for (int i = 0; i < size; sum += x[i], i++) ;
            return sum / size;
        }

        // returns the variance of X and Y
        float var(float[] x, int size)
        {
            float av = avg(x, size);
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * x[i];
            }
            return sum / size - av * av;
        }

        // returns the covariance of X and Y
        float cov(float[] x, float[] y, int size)
        {
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * y[i];
            }
            sum /= size;

            return sum - avg(x, size) * avg(y, size);
        }

        public void getLinearReg(List<DataPoint> points, int size)
        {
            float[] x = new float[size];
            float[] y = new float[size];
            for (int i = 0; i < size; i++)
            {
                x[i] = (float)points[i].X;
                y[i] = (float)points[i].Y;
            }
            a = cov(x, y, size) / var(x, size);
            b = avg(y, size) - (a * (avg(x, size)));
            NotifyPropertyChanged("A");
            NotifyPropertyChanged("B");

            /*Line l = new Line();

            Console.WriteLine(a);
            Console.WriteLine(b);
            l.X1 = 0;
            l.Y1 = b;
            l.X2 = 1;
            l.Y2 = a + b;
            linear_reg = l;*/
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
                GraphList = getListOfPoints(selection);
                Corre_points = getListOfPoints(findCorrelatedFeature());
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

        public string Correlated_att { 
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

        public List<DataPoint> Corre_list { 
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