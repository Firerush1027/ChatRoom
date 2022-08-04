using ChatServer.Message;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.ObjectModel;

namespace ChatServer.Viewmodel
{
    public class OnlineUserViewmodel : ViewModelBase
    {
        public OnlineUserViewmodel()
        {
            Messenger.Register<OnlineUserViewmodel, SelectedServerChangeMessage>(this, (r, m) => r.OnlineClient = new ObservableCollection<ClientData>(m.Value.OnlineClient));
        }
        private ObservableCollection<ClientData> onlineClient;
        public ObservableCollection<ClientData> OnlineClient
        {
            get { return onlineClient; }
            set { onlineClient = value; OnPropertyChanged(); }
        }
    }
}
