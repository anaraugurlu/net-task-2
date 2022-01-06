using Microsoft.Win32;
using net_task_2.Model;
using net_task_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TcpClientt.ViewModel
{
    class MainViewModel:BaseViewModel
    {
       
        public string Path { get; set; }
     public RelayCommand SendCommand { get; set; }
     public   RelayCommand InstallCommand { get; set; }
        public MainViewModel(MainWindow mainWindow)
        {



            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ipAddress = IPAddress.Parse("192.168.100.12");
            var port = 27002;
            var ep = new IPEndPoint(ipAddress, port);
            try
            {
                socket.Connect(ep);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
            SendCommand = new RelayCommand((e) =>
            {
                MessageBox.Show("Connected to the server ");

                if (socket.Connected)
                {
                    socket.Send(ImageConvert.GetBytesOfImage(Path));
                    MessageBox.Show("Process ended succesfully");
                }

            });
            InstallCommand  = new RelayCommand((e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files(All files (*.*)|All files (*.*)|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    Path = openFileDialog.FileName;
                }
            });
        }
    }
    }
