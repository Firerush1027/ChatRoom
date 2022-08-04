using ChatClient.Message;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.ObjectModel;

namespace ChatClient.Viewmodel
{
    public class OnlineUserViewmodel : ViewModelBase
    {
        public OnlineUserViewmodel()
        {
            Messenger.Register<OnlineUserViewmodel, SelectedServerChangeMessage>(this, (r, m) => r.OnlineClient = new ObservableCollection<ClientData>(m.Value.OnlineClient));
            Messenger.Register<OnlineUserViewmodel, CurrentUserChangeMessage>(this, (r, m) =>
            {
                if (m.Value == null)
                    OnlineClient.Clear();
            });
        }
        private ObservableCollection<ClientData> onlineClient;
        public ObservableCollection<ClientData> OnlineClient
        {
            get { return onlineClient; }
            set { onlineClient = value; OnPropertyChanged(); }
        }
    }
}
