using ChatServer.Message;
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

namespace ChatServer.Viewmodel
{
    public class AddRoomServerViewmodel:ViewModelBase
    {
        public AddRoomServerViewmodel()
        {
            server = new ServerStatus();
            AddServerEventCmd = new RelayCommand<Window>(w => AddServerEvent(w));
            InputIP = "127.0.0.1";
            InputPort = "1234";
            PromptVisibility = Visibility.Hidden;
        }
        ServerStatus server;
        private string inputIP;
        
        public string InputIP { get { return inputIP; } set { inputIP = value; server.IP = value; OnPropertyChanged(); } }
        private string inputPort;

        public string InputPort { get { return inputPort; } set { inputPort = value; server.Port = string.IsNullOrEmpty(value) ? 0 : int.Parse(value); OnPropertyChanged(); } }
        private Visibility promptVisibility;
        public Visibility PromptVisibility { get { return promptVisibility; } set { promptVisibility = value; OnPropertyChanged(); } }
        public RelayCommand<Window> AddServerEventCmd { get; set; }
        void AddServerEvent(Window window)
        {
            PromptVisibility =Visibility.Hidden;
            List<ServerStatus> listFiltered = Messenger.Send<ServersRequestMessage>();

            bool isExist = false;
            foreach (ServerStatus item in listFiltered)
            {
                if (item.Port.ToString() == InputPort)
                {
                    isExist = true;
                    break;
                }
            }

            if (isExist) PromptVisibility = Visibility.Visible;
            else
            {
                PromptVisibility = Visibility.Hidden;
                Messenger.Send(new NewChatRoomMessage(server));
                InputIP = "";
                InputPort = "";
                window.Close();
            }
        }
    }           
}
