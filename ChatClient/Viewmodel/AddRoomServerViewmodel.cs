using ChatClient.Message;
using CommunityToolkit.Mvvm.Input;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Net;
using System.Net.Sockets;

namespace ChatClient.Viewmodel
{
    public class AddRoomServerViewmodel:ViewModelBase
    {
        public AddRoomServerViewmodel()
        {
            server = new ServerStatus();
            AddServerEventCmd = new RelayCommand<Window>(w => AddServerEvent(w));
            InputIP = "127.0.0.1";
            InputPort = "1234";
            InputName = "Jimmy";
            PromptVisibility = Visibility.Hidden;
        }
        ServerStatus server;
        private string inputIP;
        
        public string InputIP { get { return inputIP; } set { inputIP = value; server.IP = value; OnPropertyChanged(); } }
        private string inputPort;

        public string InputPort { get { return inputPort; } set { inputPort = value; server.Port = string.IsNullOrEmpty(value) ? 0 : int.Parse(value); OnPropertyChanged(); } }
        private string inputName;

        public string InputName { get { return inputName; } set { inputName = value; OnPropertyChanged(); } }

        private Visibility promptVisibility;
        public Visibility PromptVisibility { get { return promptVisibility; } set { promptVisibility = value; OnPropertyChanged(); } }
        public RelayCommand<Window> AddServerEventCmd { get; set; }
        void AddServerEvent(Window window)
        {
            PromptVisibility = Visibility.Hidden;

            IPAddress ip = IPAddress.Parse(InputIP);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(new IPEndPoint(ip, int.Parse(InputPort)));
                try
                {
                    // server.Send(Encoding.ASCII.GetBytes("Connect test"));
                    if (!SocketExtensions.IsConnected(server))
                    {
                        PromptVisibility=Visibility.Visible;
                        return;
                    }
                }
                catch (Exception e)
                {
                    PromptVisibility = Visibility.Hidden;
                    return;
                }
                ClientData me = new ClientData() { Name = InputName, IP = SocketExtensions.GetLocalIPAddress() };
                ServerStatus newServer = new ServerStatus() { UserName = me, IP = InputIP, Port = int.Parse(InputPort), Status = ServerStatus.EServerStatus.Open, Connection = server };

                Messenger.Send(new NewChatRoomMessage(newServer));

                InputIP = string.Empty;
                InputPort = string.Empty;
                InputName = string.Empty;
                window.Close();
            }
            catch (Exception e)
            {
                PromptVisibility = Visibility.Visible;
            }            
        }
    }           
}
