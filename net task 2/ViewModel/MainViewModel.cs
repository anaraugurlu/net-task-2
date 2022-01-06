using net_task_2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace net_task_2.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Imagee> allImages;

        public ObservableCollection<Imagee> AllImages
        {
            get { return allImages ; }
            set { allImages  = value; OnPropertyChanged(); }
        }

        public List<Imagee> Images { get; set; }
        public MainViewModel(MainWindow mainWindow)
        {
            Task.Run(() => {
                var ipAddress = IPAddress.Parse("192.168.100.12");
                var port = 27002;
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    var ep = new IPEndPoint(ipAddress, port);
                    socket.Bind(ep);

                    socket.Listen(10);
                    Console.WriteLine($"Listening on {socket.LocalEndPoint}");

                    int i = 0;

                    while (true)
                    {
                        var client = socket.Accept();
                        Task.Run(() =>
                        {

                            MessageBox.Show($"{client.RemoteEndPoint}   ");
                            var length = 0;
                            var bytes = new byte[6000000
                            ];

                            do
                            {
                                i++;
                                length = client.Receive(bytes);
                                var msg = ImageConvert.GetImagePath(bytes, i);
                                Images.Add(new Imagee
                                {
                                    
                                    Path = msg
                                });
                                AllImages = new ObservableCollection<Imagee>(Images);
                                if (msg == "ok")
                                {
                                    client.Shutdown(SocketShutdown.Both);
                                    client.Dispose();
                                    break;
                                }


                            } while (true);
                        });
                    }

                }

            });
            Images = new List<Imagee>();
            AllImages  = new ObservableCollection<Imagee>();
        }
     
    }
}
