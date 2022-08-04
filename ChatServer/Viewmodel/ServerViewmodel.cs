using Core.Model;
using ChatServer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using ChatServer.Message;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using Newtonsoft.Json;

namespace ChatServer.Viewmodel
{
    public class ServerViewmodel : ViewModelBase
    {        
        public ServerViewmodel()
        {
            ServerList = new List<ServerStatus>();
            OpenServerDialogCmd = new RelayCommand(OpenServerDialog);
            ChangeRoomStatusCmd = new RelayCommand(ChangeRoomStatus);
            Messenger.Register<ServerViewmodel, SelectedServerChangeMessage>(this, OnSelectedServerChange);
            Messenger.Register<ServerViewmodel, NewChatRoomMessage>(this, RecieveNewChatRoom);
            Messenger.Register<ServerViewmodel, ServersRequestMessage>(this, (r, m) => m.Reply(r.ServerList));
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
        public RelayCommand OpenServerDialogCmd { get; set; }
        public RelayCommand ChangeRoomStatusCmd { get; set; }
        ManualResetEvent _event = new ManualResetEvent(true);
        private void OpenServerDialog()
        {
            AddRoomServerView addRoomServerView = new AddRoomServerView();
            addRoomServerView.Show();
        }
        private void OnSelectedServerChange(ServerViewmodel r, SelectedServerChangeMessage m)
        {
            r.currentRoom = m.Value;
            RoomStatus= (currentRoom != null && currentRoom.Status == ServerStatus.EServerStatus.Open) ? "Stop Server" : " Start Server";
            RoomIP= currentRoom == null ? "" : currentRoom.RoomName;
        }
        private void ChangeRoomStatus()
        {
            if (currentRoom.Status == ServerStatus.EServerStatus.Open)
            {
                string text = "Server stop";
                ClientData system = new ClientData() { Name = "System" };
                MessageData message = new MessageData() { Client = system, Message = text };
                currentRoom.MessageList.Add(message);
                currentRoom.Status = ServerStatus.EServerStatus.Close;
                UpdateRoom(currentRoom);

                foreach (ClientData client in currentRoom.OnlineClient)
                {
                    client.Socket.Shutdown(SocketShutdown.Both);
                    client.Socket.Close();
                }
                currentRoom.OnlineClient.Clear();

                try
                {
                    currentRoom.ListenThread.Suspend();
                    try
                    {
                        currentRoom.Connection.Shutdown(SocketShutdown.Both);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    currentRoom.Connection.Disconnect(true);
                }
                catch (Exception e)
                {
                    // CurrentRoom.connection.Close();
                    Console.WriteLine(e);
                }
            }
            else
            {
                currentRoom.Status = ServerStatus.EServerStatus.Open;
                string text = "Server start";
                ClientData system = new ClientData() { Name = "System" };
                MessageData message = new MessageData() { Client = system, Message = text };
                currentRoom.MessageList.Add(message);
                currentRoom.ListenThread.Resume();
            }
            roomStatus = (currentRoom.Status == ServerStatus.EServerStatus.Open) ? "Stop Server" : " Start Server";
            UpdateRoom(currentRoom);
        }
        public void UpdateRoom(ServerStatus room)
        {
            var item = ServerList.FirstOrDefault(i => i.Port == room.Port && i.IP == room.IP);
            item.Status = room.Status;
            item.OnlineClient = room.OnlineClient;
            item.MessageList = room.MessageList;

            string allJson = JsonConvert.SerializeObject(item);
            foreach (ClientData client in item.OnlineClient)
            {
                if (client.Socket != null)
                {
                    byte[] send = Encoding.Default.GetBytes(allJson);
                    client.Socket.Send(send);
                }
            }
            RoomStatus = (currentRoom != null && currentRoom.Status == ServerStatus.EServerStatus.Open) ? "Stop Server" : " Start Server";
            RoomIP = currentRoom == null ? "" : currentRoom.RoomName;
            //Messenger.Send(new SelectedServerChangeMessage(currentRoom));
            Messenger.Send(new ServersChangeMessage(ServerList));
        }
        private void RecieveNewChatRoom(ServerViewmodel r, NewChatRoomMessage m)
        {
            var roomStatus = m.Value.Copy();
            try
            {
                IPAddress ip = IPAddress.Parse(roomStatus.IP);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                server.Bind(new IPEndPoint(ip, roomStatus.Port));
                server.Listen(10);
                roomStatus.Connection = server;

                string text = "Server start";
                ClientData system = new ClientData() { Name = "System" };
                MessageData message = new MessageData() { Client = system, Message = text };
                roomStatus.MessageList.Add(message);

                Thread thread = new Thread(() => Listen(server, roomStatus));
                roomStatus.ListenThread = thread;
                thread.Start();
                ServerList.Add(roomStatus);
                Messenger.Send(new ServersChangeMessage(ServerList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void Listen(Socket curServer, ServerStatus curStatus)
        {
            while (true)
            {
                Socket client = curServer.Accept();

                Thread receive = new Thread(() => ReceiveMsg(client, curStatus));

                receive.Start();
            }
        }
        public void ReceiveMsg(object client, ServerStatus curStatus)
        {
            Socket connection = (Socket)client;
            IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
            ClientData clientdata = null;
            ClientData system = new ClientData() { Name = "System" };
            while (SocketExtensions.IsConnected(connection))
            {
                try
                {
                    byte[] result = new byte[connection.Available];

                    int receive_num = connection.Receive(result);
                    String receive_str = Encoding.ASCII.GetString(result, 0, receive_num);
                    if (receive_num > 0)
                    {
                        if (clientdata == null)
                        {
                            clientdata = JsonConvert.DeserializeObject<ClientData>(receive_str);
                            clientdata.Socket = connection;

                            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                curStatus.OnlineClient.Add(clientdata);
                            });

                            string text = $"{clientdata.FullName} is entered the chatroom!";
                            MessageData message = new MessageData() { Client = system, Message = text };

                            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                curStatus.MessageList.Add(message);
                            });
                        }
                        else
                        {
                            MessageData message = JsonConvert.DeserializeObject<MessageData>(receive_str);
                            string send_str = $"{message.Client.FullName}:{message.Message}";

                            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                curStatus.MessageList.Add(message);
                            });
                        }

                        UpdateRoom(curStatus);
                    }
                }
                catch (Exception e)
                {
                    //exception close()
                    Console.WriteLine(e);
                    if (connection.Connected)
                    {
                        connection.Shutdown(SocketShutdown.Both);
                        connection.Close();
                    }
                    break;
                }
            }

            if (clientdata != null)
            {
                string text = clientdata.FullName + " left the chatroom!";
                MessageData message = new MessageData() { Client = system, Message = text };
                // connection.Send(Encoding.ASCII.GetBytes(text + "\n"));
                System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    curStatus.MessageList.Add(message);
                    curStatus.OnlineClient.Remove(clientdata);
                });

                UpdateRoom(curStatus);
            }
        }
    }
}
