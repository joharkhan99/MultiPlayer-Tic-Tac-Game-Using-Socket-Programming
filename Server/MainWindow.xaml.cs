using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TcpClient> clients = new List<TcpClient>();
        Thread thread;
        TcpListener listener;
        List<PlayerBound> Players = new List<PlayerBound>();
        static IList<ClientIDs> ClientList = new List<ClientIDs>();
        List<string> chatList = new List<string>();


        public MainWindow()
        {
            InitializeComponent();
        }
        static bool IsClientConnected(TcpClient client)
        {
            if (client.Client.Poll(0, SelectMode.SelectRead))
            {
                byte[] checkConn = new byte[1];
                if (client.Client.Receive(checkConn, SocketFlags.Peek) == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private void ThreadProc(object obj)
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            var client = (TcpClient)obj;
            try
            {


                while (client.Connected)
                {
                    if (!IsClientConnected(client))
                    {
                        Dispatcher.Invoke(() =>
                        {
                            listbox2.Items.Add("Client Left: " + client.Client.RemoteEndPoint);
                            for (int i = 0; i < listbox1.Items.Count; i++)
                            {
                                if (client.Client.RemoteEndPoint.ToString() == listbox1.Items[i].ToString())
                                {
                                    listbox1.Items.Remove(client.Client.RemoteEndPoint.ToString());
                                }
                            }
                        });

                        for (int i = 0; i < ClientList.Count; i++)
                        {
                            if (client.Client.RemoteEndPoint == ClientList[i].Ip)
                            {
                                ClientList.RemoveAt(i);
                                clients.RemoveAt(i);
                            }
                        }

                        string SendClientList = "up";
                        for (int i = 0; i < ClientList.Count; i++)
                        {
                            SendClientList += ClientList[i].ClientID + ",";
                        }
                        byte[] buff = Encoding.UTF8.GetBytes(SendClientList);
                        for (int i = 0; i < clients.Count; i++)
                        {
                            NetworkStream ClientsStrem = clients[i].GetStream();
                            ClientsStrem.Write(buff, 0, buff.Length);
                        }
                        client.Close();
                        break;

                    }
                    else
                    {
                        NetworkStream networkStream = client.GetStream();
                        byte[] buff = new byte[client.ReceiveBufferSize];
                        int bytesRead = networkStream.Read(buff, 0, client.ReceiveBufferSize);
                        string dataRecieved = Encoding.UTF8.GetString(buff, 0, bytesRead);

                        if (dataRecieved != "")
                        {
                            NetworkStream p1 = networkStream;
                            NetworkStream p2 = null;
                            string RecievedCommand = dataRecieved.Substring(0, 2);
                            string RecievedMessage = "";
                            if (dataRecieved.Length > 2)
                                RecievedMessage = dataRecieved.Remove(0, 2);

                            if (RecievedCommand == "lo")
                            {
                                ClientList.Add(new ClientIDs { ClientID = RecievedMessage, Ip = client.Client.RemoteEndPoint });
                                Dispatcher.Invoke(() =>
                                {
                                    listbox2.Items.Add(RecievedMessage + " Connected");
                                });
                                List<String> ingame = new List<String>();
                                for (int i = 0; i < Players.Count; i++)
                                {
                                    ingame.Add(Players[i].Player1);
                                    ingame.Add(Players[i].Player2);
                                }
                                string SendClientList = "up";
                                for (int i = 0; i < ClientList.Count; i++)
                                {
                                    if (!ingame.Contains(ClientList[i].ClientID))
                                    {
                                        SendClientList += ClientList[i].ClientID + ",";
                                    }
                                }

                                byte[] buffi = Encoding.UTF8.GetBytes(SendClientList);
                                for (int i = 0; i < clients.Count; i++)
                                {
                                    NetworkStream ClientsStream = clients[i].GetStream();
                                    ClientsStream.Write(buffi, 0, buffi.Length);
                                }
                            }


                            if (RecievedCommand == "pl")
                            {
                                string[] tmp = RecievedMessage.Split(',');
                                string Player1 = tmp[0];
                                string Player2 = tmp[1];
                                Players.Add(new PlayerBound { Player1 = Player1, Player2 = Player2 });
                                List<string> ingame = new List<string>();
                                for (int i = 0; i < Players.Count; i++)
                                {
                                    ingame.Add(Players[i].Player1);
                                    ingame.Add(Players[i].Player2);
                                }
                                string SendClientList = "up";
                                for (int i = 0; i < ClientList.Count; i++)
                                {
                                    if (!ingame.Contains(ClientList[i].ClientID))
                                    {
                                        SendClientList += ClientList[i].ClientID + ",";
                                    }
                                }
                                byte[] buffi = Encoding.UTF8.GetBytes(SendClientList);
                                for (int i = 0; i < clients.Count; i++)
                                {
                                    NetworkStream ClientsStream = clients[i].GetStream();
                                    ClientsStream.Write(buffi, 0, buffi.Length);
                                }

                                EndPoint ip = null;
                                byte[] buffer = Encoding.UTF8.GetBytes("pa" + Player1);
                                for (int i = 0; i < ClientList.Count; i++)
                                {
                                    if (ClientList[i].ClientID == Player2)
                                    {
                                        ip = ClientList[i].Ip;
                                    }
                                }

                                for (int i = 0; i < clients.Count; i++)
                                {
                                    if (clients[i].Client.RemoteEndPoint == ip)
                                    {
                                        p2 = clients[i].GetStream();
                                        p2.Write(buffer, 0, buffer.Length);
                                    }
                                }
                            }


                            if (RecievedCommand == "mv")
                            {
                                string Player1 = "";
                                string Player2 = "";
                                string[] tmp = RecievedMessage.Split(',');
                                string Player = tmp[0];
                                string Step = tmp[1];
                                for (int i = 0; i < Players.Count; i++)
                                {
                                    if (Players[i].Player1 == Player)
                                    {
                                        Player1 = Players[i].Player1;
                                        Player2 = Players[i].Player2;
                                    }
                                    if (Players[i].Player2 == Player)
                                    {
                                        Player1 = Players[i].Player1;
                                        Player2 = Players[i].Player2;
                                    }
                                }
                                EndPoint ip = null;
                                if (Player1 == Player)
                                {
                                    for (int i = 0; i < ClientList.Count; i++)
                                    {
                                        if (ClientList[i].ClientID == Player2)
                                        {
                                            ip = ClientList[i].Ip;
                                        }
                                    }
                                    for (int i = 0; i < clients.Count; i++)
                                    {
                                        if (clients[i].Client.RemoteEndPoint == ip)
                                        {
                                            p2 = clients[i].GetStream();
                                            string textToSend = "mv" + Step;
                                            byte[] bytesToSend = Encoding.UTF8.GetBytes(textToSend);
                                            p2.Write(bytesToSend, 0, bytesToSend.Length);
                                        }
                                    }
                                }
                                if (Player2 == Player)
                                {
                                    for (int i = 0; i < ClientList.Count; i++)
                                    {
                                        if (ClientList[i].ClientID == Player1)
                                        {
                                            ip = ClientList[i].Ip;
                                        }
                                    }
                                    for (int i = 0; i < clients.Count; i++)
                                    {
                                        if (clients[i].Client.RemoteEndPoint == ip)
                                        {
                                            p2 = clients[i].GetStream();
                                            string textToSend = "mv" + Step;
                                            byte[] bytesToSend = Encoding.UTF8.GetBytes(textToSend);
                                            p2.Write(bytesToSend, 0, bytesToSend.Length);
                                        }
                                    }
                                }
                            }

                            if (RecievedCommand == "ch")
                            {
                                chatList.Add(RecievedMessage);
                                List<String> ingame = new List<String>();
                                for (int i = 0; i < Players.Count; i++)
                                {
                                    ingame.Add(Players[i].Player1);
                                    ingame.Add(Players[i].Player2);
                                }

                                string SendClientList = "chs_!@()<.-#;";
                                for (int i = 0; i < chatList.Count; i++)
                                {
                                    SendClientList += chatList[i] + ",";
                                }

                                byte[] buffi = Encoding.UTF8.GetBytes(SendClientList);
                                for (int i = 0; i < clients.Count; i++)
                                {
                                    NetworkStream ClientsStream = clients[i].GetStream();
                                    ClientsStream.Write(buffi, 0, buffi.Length);

                                }

                            }


                        }
                    }
                }


            }
            catch (SocketException socketEx)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(socketEx.Message);
                });
            }
            catch (System.IO.IOException ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(ex.Message);
                });
            }


        }
        private void Listening()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            const int PORT_NO = 2899;
            listener = new TcpListener(ipAddress, PORT_NO);
            string stat = "";
            Dispatcher.Invoke(() =>
            {
                this.StatusLabel.Content = "ON";
                this.listbox2.Items.Add("Server started Listening...");
                stat = StatusLabel.Content.ToString();
            });
            TcpClient client;
            listener.Start();

            try
            {

                while (stat == "ON")
                {
                    client = listener.AcceptTcpClient();
                    clients.Add(client);
                    Dispatcher.Invoke(() =>
                    {
                        this.listbox2.Items.Add("Client Connected: " + client.Client.RemoteEndPoint);
                        this.listbox1.Items.Add(client.Client.RemoteEndPoint.ToString());

                        listbox2.SelectedIndex = listbox2.Items.Count - 1;
                        listbox2.ScrollIntoView(listbox2.SelectedItem);
                    });
                    int ind = clients.IndexOf(clients[clients.Count - 1]);
                    ThreadPool.QueueUserWorkItem(ThreadProc, clients[ind]);
                }
            }
            catch (SocketException socketEx)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(socketEx.Message);
                });
            }

        }
        private void StartButtonClick(object sender, EventArgs e)
        {
            thread = new Thread(() => Listening());
            thread.Start();
        }
        private void StopButtonClick(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                StatusLabel.Content = "OFF";
                listbox2.Items.Add("Server stopped");
            });
            listener.Stop();
            thread.Abort();

        }
        private void CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (listener != null || thread != null)
                {
                    listener.Stop();
                    thread.Abort();
                }
            }
            catch
            {

            }
            Environment.Exit(0);
        }
    }

    public class PlayerBound
    {
        public string Player1;
        public string Player2;
    }

    public class ClientIDs
    {
        public string ClientID { get; set; }
        public EndPoint Ip { get; set; }
    }

}
