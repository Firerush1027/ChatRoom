using ChatClient.Viewmodel;
using ChatClient.Message;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ChatClient.Model
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ServerTemplate { get; set; }
        public DataTemplate ClientTemplate { get; set; }
        public DataTemplate MyTemplate { get; set; }

        public ClientData curUser { get; set; }

        public ChatTemplateSelector()
        {
            //Messenger.Register<ClientData>(this, "CurrentUser", user => {
            //    curUser = user;
            //});
            //Messenger.Register<ChatTemplateSelector, CurrentUserChangeMessage>(this, (r, m) => r.OnlineClient = new ObservableCollection<ClientData>(m.Value.OnlineClient));
        }

        public override DataTemplate SelectTemplate(object item,
          DependencyObject container)
        {
            MessageDataViewmodel data = (MessageDataViewmodel)item;
            if (data.ClientData.Name.Equals("System"))
                return ServerTemplate;
            else if (data.ClientData.FullName == ChatroomViewmodel.CurrentUser.FullName)
                return MyTemplate;
            return ClientTemplate;
        }
    }
}
