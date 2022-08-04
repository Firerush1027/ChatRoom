using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Core.Model;


namespace ChatServer.Message
{
    public class ServersChangeMessage : ValueChangedMessage<List<ServerStatus>>
    {
        public ServersChangeMessage(List<ServerStatus> value) : base(value)
        {
        }
    }
    public class SelectedServerChangeMessage : ValueChangedMessage<ServerStatus>
    {
        public SelectedServerChangeMessage(ServerStatus value) : base(value)
        {
        }
    }
    public class NewChatRoomMessage : ValueChangedMessage<ServerStatus>
    {
        public NewChatRoomMessage(ServerStatus value) : base(value)
        {
        }
    }
    public class ServersRequestMessage : RequestMessage<List<ServerStatus>>
    {

    }
}