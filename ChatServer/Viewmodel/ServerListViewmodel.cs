using ChatServer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using ChatServer.Message;
using Core.Model;

namespace ChatServer.Viewmodel
{
    public class ServerListViewmodel : ViewModelBase
    {
        
        public ServerListViewmodel()
        {
            Messenger.Register<ServerListViewmodel, ServersChangeMessage>(this, (r, m) => 
            {
                ServerStatusViewmodel temp = null;
                if (SelectedServer != null)
                    temp = SelectedServer.Copy();
                var ServerList = new ObservableCollection<ServerStatusViewmodel>();
                foreach (var item in m.Value)
                {
                    var ssvm = new ServerStatusViewmodel(item);
                    ServerList.Add(ssvm);
                }
                r.ServerList = ServerList;
                if (temp != null)
                    SelectedServer = ServerList.FirstOrDefault(x => x.Port == temp.Port);
            });
        }
        //List<ServerStatus> serverStatuses;
        private ServerStatusViewmodel selectedServer;
        public ServerStatusViewmodel SelectedServer 
        { 
            get { return selectedServer; }
            set 
            {
                if (value != null)
                {
                    selectedServer = value;
                    List<ServerStatus> serverStatuses = Messenger.Send<ServersRequestMessage>();
                    var selected = serverStatuses.FirstOrDefault(x => x.Port == selectedServer.Port);
                    Messenger.Send(new SelectedServerChangeMessage(selected));
                    OnPropertyChanged();
                }
                
            }
        }
        private ObservableCollection<ServerStatusViewmodel> serverList;
        public ObservableCollection<ServerStatusViewmodel> ServerList
        {
            get { return serverList; }
            set { serverList = value; OnPropertyChanged(); }
        }
    }
    public partial class ServerStatusViewmodel : ViewModelBase
    {
        public ServerStatusViewmodel(ServerStatus serverStatus)
        {
            Status = serverStatus.Status.ToString();
            RoomName = serverStatus.RoomName;
            ClientNum = serverStatus.ClientNum;
            Port = serverStatus.Port;
        }
        public ServerStatusViewmodel(ServerStatusViewmodel serverStatus)
        {
            Status = serverStatus.Status.ToString();
            RoomName = serverStatus.RoomName;
            ClientNum = serverStatus.ClientNum;
            Port = serverStatus.Port;
        }
        private string status;
        public string Status { get { return status; } set { status = value; OnPropertyChanged(); } }
        private string roomName;
        public string RoomName { get { return roomName; } set { roomName = value; OnPropertyChanged(); } }
        private string clientNum;
        public string ClientNum { get { return clientNum; } set { clientNum = value; OnPropertyChanged(); } }
        public int Port { get; set; }
        public ServerStatusViewmodel Copy()
        {
            return new ServerStatusViewmodel(this);
        }
    }
}
