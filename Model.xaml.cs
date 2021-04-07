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
        private LoadPath load;

        private double aileron;
        private double elevator;
        private double rudder;
        private double throttle;

        volatile Boolean stop;

        public event PropertyChangedEventHandler PropertyChanged;

        public Model(string path)
        {
            stop = false;
            ConnectFg(path);
        }



        public void ConnectFg(string fullPath)
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
        }

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

        public Boolean Stop
        { 
            set
            {
                stop = true;
            }
        }
    }
}
