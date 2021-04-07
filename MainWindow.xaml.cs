using System;
using System.Collections.Generic;
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
using System.Net.Sockets;
using System.Threading;
using Microsoft.Win32;



namespace app0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        //LoadPath loadPath;
        Panel panel;


        public MainWindow()
        {
            InitializeComponent();
            //loadPath = new LoadPath();
            //Navigate(loadPath);
            //panel = new Panel();
        }


        /* private void btnOpenFile_Click(object sender, RoutedEventArgs e)
         {
             OpenFileDialog openFileDialog = new OpenFileDialog();
             if (openFileDialog.ShowDialog() == true)
             {
                 filePath = System.IO.Path.GetFullPath(openFileDialog.FileName);
             }
             //this.Close();
         }*/


        /*public void ConnectFg()
        {

            var client = new TcpClient("localhost", 5400);
            NetworkStream ns = client.GetStream();
            var file = new System.IO.StreamReader(@"C:\Users\noam\Desktop\reg_flight.csv");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                line += "\r\n";
                Console.WriteLine(line);
                ns.Write(System.Text.Encoding.ASCII.GetBytes(line), 0, System.Text.Encoding.ASCII.GetBytes(line).Length);
                ns.Flush();
                Thread.Sleep(100);
            }
        }*/
    }
}
