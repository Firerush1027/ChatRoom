using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.ObjectModel;
using ChatServer.Message;
using Core.Model;

namespace ChatServer.Viewmodel
{
    public class ChatroomViewmodel : ViewModelBase
    {
        public ChatroomViewmodel()
        {
            Messenger.Register<ChatroomViewmodel, SelectedServerChangeMessage>(this, (r, m) =>
            {
                var mdvm = new ObservableCollection<MessageDataViewmodel>();
                foreach (var item in m.Value.MessageList)
                {
                    var msg = new MessageDataViewmodel(item);
                    mdvm.Add(msg);
                }
                r.MessageList = mdvm;
            });
        }
        private ObservableCollection<MessageDataViewmodel> messageList;
        public ObservableCollection<MessageDataViewmodel> MessageList
        {
            get { return messageList; }
            set { messageList = value; OnPropertyChanged(); }
        }
    }
    public class ClientDataViewmodel : ViewModelBase
    {
        public ClientDataViewmodel(ClientData Data)
        {
            FullName=Data.FullName;
            Name=Data.Name;
        }
        public ClientData Data { get; set; }
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } }
        private string fullName;
        public string FullName { get { return fullName; } set { fullName = value; OnPropertyChanged(); } }

    }
    public class MessageDataViewmodel : ViewModelBase
    {
        public MessageDataViewmodel(MessageData Data)
        {
            Message = Data.Message;
            ClientData = new ClientDataViewmodel(Data.Client);
        }
        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(); } }
        private ClientDataViewmodel clientData;
        public ClientDataViewmodel ClientData
        {
            get { return clientData; }
            set { clientData = value; OnPropertyChanged(); }
        }
    }
}