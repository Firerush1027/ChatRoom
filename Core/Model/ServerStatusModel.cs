using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Model
{
    public class ServerStatus
    {
        [JsonIgnore]
        public string IP { get; set; }
        [JsonIgnore]
        public int Port { get; set; }

        public EServerStatus Status { get; set; }
        [JsonIgnore]
        public Socket Connection { get; set; }
        [JsonIgnore]
        public Thread ListenThread { get; set; }

        public List<ClientData> OnlineClient { get; set; } = new List<ClientData>();

        public List<MessageData> MessageList { get; set; } = new List<MessageData>();
        public ClientData UserName { get; set; }
        [JsonIgnore]
        public string RoomName
        {
            get
            {
                return $"{IP}:{Port}";
            }
        }
        [JsonIgnore]
        public string ClientNum
        {
            get
            {
                return OnlineClient.Count.ToString();
            }
        }
        public enum EServerStatus
        {
            Open,
            Close
        }
        public ServerStatus Copy()
        {
            ServerStatus status = new ServerStatus();
            status.IP = IP;
            status.Port = Port;
            status.Connection = Connection;
            status.Status = Status;
            status.OnlineClient=OnlineClient.ToList();
            status.MessageList = MessageList.ToList();
            status.UserName = UserName;
            return status;
        }
    }
}
