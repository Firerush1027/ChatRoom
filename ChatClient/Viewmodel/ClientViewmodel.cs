using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ChatClient.View;
using ChatClient.Message;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace ChatClient.Viewmodel
{
    public class ClientViewmodel : ViewModelBase
    {
        public ClientViewmodel()
        {
            isBtnEnable = false;
            ServerList = new List<ServerStatus>();
            OpenServerDialogCmd = new RelayCommand(OpenServerDialog);
            ChangeRoomStatusCmd = new RelayCommand(ChangeRoomStatus);
            SendMessageCmd = new RelayCommand(SendMessage);
            Messenger.Register<ClientViewmodel, SelectedServerChangeMessage>(this, OnSelectedServerChange);
            Messenger.Register<ClientViewmodel, NewChatRoomMessage>(this, RecieveNewChatRoom);
            Messenger.Register<ClientViewmodel, ServersRequestMessage>(this, (r, m) => m.Reply(r.ServerList));
        }

        private ServerStatus currentRoom;
        private List<ServerStatus> ServerList { get; set; }
        private string roomStatus;
        public string RoomStatus
        {
            get { return roomStatus; }
            set { roomStatus = value; OnPropertyChanged(); }
        }
        private string roomIP;
        public string RoomIP
        {
            get { return roomIP; }
            set { roomIP = value; OnPropertyChanged(); }
        }
        private string inputText;
        public string InputText
        {
            get { return inputText; }
            set { inputText = value; OnPropertyChanged(); }
        }
        private bool isBtnEnable;
        public bool IsBtnEnable
        {
            get { return isBtnEnable; }
            set { isBtnEnable = value; OnPropertyChanged(); }
        }
        public RelayCommand OpenServerDialogCmd { get; set; }
        public RelayCommand ChangeRoomStatusCmd { get; set; }
        public RelayCommand SendMessageCmd { get; set; }
        ManualResetEvent _event = new ManualResetEvent(true);
        private void OpenServerDialog()
        {
            AddRoomServerView addRoomServerView = new AddRoomServerView();
            addRoomServerView.Show();
        }
        private void OnSelectedServerChange(ClientViewmodel r, SelectedServerChangeMessage m)
        {
            if(m.Value != null)
            {
                r.currentRoom = m.Value;
                RoomStatus = (currentRoom != null && currentRoom.Status == ServerStatus.EServerStatus.Open) ? "Stop Server" : " Start Server";
                RoomIP = currentRoom == null ? "" : currentRoom.RoomName;
                IsBtnEnable = currentRoom.Status == ServerStatus.EServerStatus.Open;
            }           
        }
        private void ChangeRoomStatus()
        {
            if (currentRoom.Connection.Connected)
            {
                currentRoom.Connection.Shutdown(SocketShutdown.Both);
                currentRoom.Connection.Close();
            }

            UpdateRoom(currentRoom, true);
        }
        private void SendMessage()
        {
            try
            {
                MessageData data = new MessageData() { Client = currentRoom.UserName, Message = InputText };
                string sendMsg = JsonConvert.SerializeObject(data);

                currentRoom.Connection.Send(Encoding.ASCII.GetBytes(sendMsg));
                Console.WriteLine(currentRoom.Connection.Available);
                InputText = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void UpdateRoom(ServerStatus room, bool isDelete)
        {
            ClientData userName = null;
            if (!isDelete)
            {
                var item = ServerList.FirstOrDefault(i => i.Port == room.Port && i.IP == room.IP);
                if (item != null)
                {
                    item.Status = room.Status;
                    item.OnlineClient = room.OnlineClient;
                    item.MessageList = room.MessageList;
                    IsBtnEnable = item.Status == ServerStatus.EServerStatus.Open;
                    if(item.Status == ServerStatus.EServerStatus.Close)
                        room.OnlineClient.Clear();
                    userName = item.UserName;
                    //Messenger.Send(new CurrentUserChangeMessage(item.UserName));
                    //Messenger.Send(new ServersChangeMessage(ServerList));
                }
            }
            else
            {
                ServerList.Remove(room);
                userName= null;
                IsBtnEnable = false;
                RoomIP = "";
            }
            Messenger.Send(new CurrentUserChangeMessage(userName));
            Messenger.Send(new ServersChangeMessage(ServerList));           
        }
        private void RecieveNewChatRoom(ClientViewmodel r, NewChatRoomMessage m)
        {
            var roomStatus = m.Value.Copy();
            try
            {
                Thread thread = new Thread(() => ReceiveMessage(roomStatus.Connection, roomStatus));
                roomStatus.ListenThread = thread;

                string user = JsonConvert.SerializeObject(roomStatus.UserName);

                roomStatus.Connection.Send(Encoding.ASCII.GetBytes(user));

                thread.Start();
                ServerList.Add(roomStatus);
                Messenger.Send(new ServersChangeMessage(ServerList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void ReceiveMessage(Socket client, ServerStatus curStatus)
        {
            while (true)
            {
                try
                {
                    byte[] result = new byte[client.Available];
                    int receiveNumber = client.Receive(result);
                    String recStr = Encoding.ASCII.GetString(result, 0, receiveNumber);
                    ServerStatus roomlist = JsonConvert.DeserializeObject<ServerStatus>(recStr);
                    if (roomlist != null)
                    {
                        curStatus.MessageList = roomlist.MessageList;
                        curStatus.Status = roomlist.Status;
                        curStatus.OnlineClient = roomlist.OnlineClient;
                    }
                    UpdateRoom(curStatus, false);
                }
                catch (Exception ex)
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                    }
                    break;
                }
            }
        }               
    }
}
