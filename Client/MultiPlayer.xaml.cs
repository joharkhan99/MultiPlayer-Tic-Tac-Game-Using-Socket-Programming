using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    /// Interaction logic for MultiPlayer.xaml
    /// </summary>
    public partial class MultiPlayer : Window
    {
        private string ChoosedPlayer = "None";
        private static string currentPlayer = "X";
        private static Random random = new Random();
        private static TcpClient client = new TcpClient();
        int inandout = 0;
        Thread thread;
        int MyStep = 0;
        //IP ADDRESS WILL BE REPLACED PLEASE
        //IP ADDRESS WILL BE REPLACED PLEASE`
        string SERVER_IP_ADDRESS = "192.168.7.138";

        List<string> duplicateNames = new List<string>();

        public MultiPlayer()
        {
            InitializeComponent();
            playButton.IsEnabled = false;
            chatButton.IsEnabled = false;
        }

        public int CheckVictory()
        {
            if (button1.Content.ToString() == "X" && button2.Content.ToString() == "X" && button3.Content.ToString() == "X")
                return 1;
            if (button4.Content.ToString() == "X" && button5.Content.ToString() == "X" && button6.Content.ToString() == "X")
                return 1;
            if (button7.Content.ToString() == "X" && button7.Content.ToString() == "X" && button8.Content.ToString() == "X")
                return 1;
            if (button1.Content.ToString() == "X" && button4.Content.ToString() == "X" && button7.Content.ToString() == "X")
                return 1;
            if (button2.Content.ToString() == "X" && button5.Content.ToString() == "X" && button8.Content.ToString() == "X")
                return 1;
            if (button3.Content.ToString() == "X" && button6.Content.ToString() == "X" && button9.Content.ToString() == "X")
                return 1;
            if (button1.Content.ToString() == "X" && button5.Content.ToString() == "X" && button9.Content.ToString() == "X")
                return 1;
            if (button3.Content.ToString() == "X" && button5.Content.ToString() == "X" && button7.Content.ToString() == "X")
                return 1;

            if (button1.Content.ToString() == "O" && button2.Content.ToString() == "O" && button3.Content.ToString() == "O")
                return 2;
            if (button4.Content.ToString() == "O" && button5.Content.ToString() == "O" && button6.Content.ToString() == "O")
                return 2;
            if (button7.Content.ToString() == "O" && button7.Content.ToString() == "O" && button8.Content.ToString() == "O")
                return 2;
            if (button1.Content.ToString() == "O" && button4.Content.ToString() == "O" && button7.Content.ToString() == "O")
                return 2;
            if (button2.Content.ToString() == "O" && button5.Content.ToString() == "O" && button8.Content.ToString() == "O")
                return 2;
            if (button3.Content.ToString() == "O" && button6.Content.ToString() == "O" && button9.Content.ToString() == "O")
                return 2;
            if (button1.Content.ToString() == "O" && button5.Content.ToString() == "O" && button9.Content.ToString() == "O")
                return 2;
            if (button3.Content.ToString() == "O" && button5.Content.ToString() == "O" && button7.Content.ToString() == "O")
                return 2;

            //tie
            if (button1.Content.ToString() != "" && button2.Content.ToString() != "" && button3.Content.ToString() != "" && button4.Content.ToString() != "" && button5.Content.ToString() != "" && button6.Content.ToString() != "" && button7.Content.ToString() != "" && button8.Content.ToString() != "" && button9.Content.ToString() != "")
            {
                return 0;
            }
            return 3;
        }

        private void CheckEndGame()
        {
            this.Dispatcher.Invoke(() =>
            {
                if (CheckVictory() == 0)
                {
                    if (inandout == 0)
                    {
                        cPlayerLabel.Content = "O";
                        currentPlayer = "O";
                        inandout = 1;
                    }
                    else
                    {
                        cPlayerLabel.Content = "X";
                        currentPlayer = "X";
                        inandout = 0;
                    }
                    MessageBox.Show("Tie");
                    NewGame();
                }
                else if (CheckVictory() == 1)
                {
                    if (inandout == 0)
                    {
                        cPlayerLabel.Content = "O";
                        currentPlayer = "O";
                        inandout = 1;
                    }
                    else
                    {
                        cPlayerLabel.Content = "X";
                        currentPlayer = "X";
                        inandout = 0;
                    }
                    int score = int.Parse(XScoreLabel.Content.ToString());
                    score++;
                    XScoreLabel.Content = score.ToString();
                    MessageBox.Show("X Win");
                    NewGame();
                }
                else if (CheckVictory() == 2)
                {
                    if (inandout == 0)
                    {
                        cPlayerLabel.Content = "O";
                        currentPlayer = "O";
                        inandout = 1;
                    }
                    else
                    {
                        cPlayerLabel.Content = "X";
                        currentPlayer = "X";
                        inandout = 0;
                    }
                    int score = int.Parse(OScoreLabel.Content.ToString());
                    score++;
                    OScoreLabel.Content = score.ToString();
                    MessageBox.Show("O Win");
                    NewGame();
                }
            });
        }

        public void MarkO(int step)
        {
            this.Dispatcher.Invoke(() =>
            {
                switch (step)
                {
                    case 1:
                        if (button1.Content.ToString() != "1")
                        {
                            button1.Content = "O";
                            cPlayerLabel.Content = "X";
                            currentPlayer = "X";
                        }
                        break;
                    case 2:
                        if (button2.Content.ToString() != "1")
                        {
                            button2.Content = "O";
                            cPlayerLabel.Content = "X";
                            currentPlayer = "X";
                        }
                        break;
                    case 3:
                        if (button3.Content.ToString() != "1")
                        {
                            button3.Content = "O";
                            cPlayerLabel.Content = "X";
                            currentPlayer = "X";
                        }
                        break;
                    case 4:
                        if (button4.Content.ToString() != "1")
                        {
                            button4.Content = "O";
                            cPlayerLabel.Content = "X";
                            currentPlayer = "X";
                        }
                        break;
                    case 5:
                        if (button5.Content.ToString() != "1")
                        {
                            button5.Content = "O";
                            cPlayerLabel.Content = "X";
                            currentPlayer = "X";
                        }
                        break;
                    case 6:
                        if (button6.Content.ToString() != "1")
                        {
                            button6.Content = "O";
                            cPlayerLabel.Content = "X";
                            currentPlayer = "X";
                        }
                        break;
                    case 7:
                        if (button7.Content.ToString() != "1")
                        {
                            button7.Content = "O";
                            cPlayerLabel.Content = "X";
                            currentPlayer = "X";
                        }
                        break;
                    case 8:
                        if (button8.Content.ToString() != "1")
                        {
                            button8.Content = "O";
                            cPlayerLabel.Content = "X";
                            currentPlayer = "X";
                        }
                        break;
                    case 9:
                        if (button9.Content.ToString() != "1")
                        {
                            button9.Content = "O";
                            cPlayerLabel.Content = "X";
                            currentPlayer = "X";
                        }
                        break;
                    default:
                        break;
                }
            });
        }

        public void MarkX(int step)
        {
            this.Dispatcher.Invoke(() =>
            {
                switch (step)
                {
                    case 1:
                        if (button1.Content.ToString() != "1")
                        {
                            button1.Content = "X";
                            currentPlayer = "O";
                            cPlayerLabel.Content = "Circle";
                        }
                        break;
                    case 2:
                        if (button2.Content.ToString() != "1")
                        {
                            button2.Content = "X";
                            currentPlayer = "O";
                            cPlayerLabel.Content = "Circle";
                        }
                        break;
                    case 3:
                        if (button3.Content.ToString() != "1")
                        {
                            button3.Content = "X";
                            currentPlayer = "O";
                            cPlayerLabel.Content = "Circle";
                        }
                        break;
                    case 4:
                        if (button4.Content.ToString() != "1")
                        {
                            button4.Content = "X";
                            currentPlayer = "O";
                            cPlayerLabel.Content = "Circle";
                        }
                        break;
                    case 5:
                        if (button5.Content.ToString() != "1")
                        {
                            button5.Content = "X";
                            currentPlayer = "O";
                            cPlayerLabel.Content = "Circle";
                        }
                        break;
                    case 6:
                        if (button6.Content.ToString() != "1")
                        {
                            button6.Content = "X";
                            currentPlayer = "O";
                            cPlayerLabel.Content = "Circle";
                        }
                        break;
                    case 7:
                        if (button7.Content.ToString() != "1")
                        {
                            button7.Content = "X";
                            currentPlayer = "O";
                            cPlayerLabel.Content = "Circle";
                        }
                        break;
                    case 8:
                        if (button8.Content.ToString() != "1")
                        {
                            button8.Content = "X";
                            currentPlayer = "O";
                            cPlayerLabel.Content = "Circle";
                        }
                        break;
                    case 9:
                        if (button9.Content.ToString() != "1")
                        {
                            button9.Content = "X";
                            currentPlayer = "O";
                            cPlayerLabel.Content = "Circle";
                        }
                        break;
                    default:
                        break;
                }
            });
        }

        private void NewGame()
        {
            button1.Content = "";
            button2.Content = "";
            button3.Content = "";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            button7.Content = "";
            button8.Content = "";
            button9.Content = "";
        }

        private void GridButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            bool clicked = false;
            if (ChoosedPlayer == currentPlayer && btn.Equals(button1))
            {
                if (ChoosedPlayer == "X")
                {
                    MarkX(1);
                    MyStep = 1;
                }
                else
                {
                    MarkO(1);
                    MyStep = 1;
                }
                clicked = true;
            }

            if (ChoosedPlayer == currentPlayer && btn.Equals(button2))
            {
                clicked = true;
                if (ChoosedPlayer == "X")
                {
                    MarkX(2);
                    MyStep = 2;
                }
                else
                {
                    MarkO(2);
                    MyStep = 2;
                }
            }

            if (ChoosedPlayer == currentPlayer && btn.Equals(button3))
            {
                clicked = true;
                if (ChoosedPlayer == "X")
                {
                    MarkX(3);
                    MyStep = 3;
                }
                else
                {
                    MarkO(3);
                    MyStep = 3;
                }
            }

            if (ChoosedPlayer == currentPlayer && btn.Equals(button4))
            {
                clicked = true;
                if (ChoosedPlayer == "X")
                {
                    MarkX(4);
                    MyStep = 4;
                }
                else
                {
                    MarkO(4);
                    MyStep = 4;
                }
            }

            if (ChoosedPlayer == currentPlayer && btn.Equals(button5))
            {
                clicked = true;
                if (ChoosedPlayer == "X")
                {
                    MarkX(5);
                    MyStep = 5;
                }
                else
                {
                    MarkO(5);
                    MyStep = 5;
                }
            }

            if (ChoosedPlayer == currentPlayer && btn.Equals(button6))
            {
                clicked = true;
                if (ChoosedPlayer == "X")
                {
                    MarkX(6);
                    MyStep = 6;
                }
                else
                {
                    MarkO(6);
                    MyStep = 6;
                }
            }

            if (ChoosedPlayer == currentPlayer && btn.Equals(button7))
            {
                clicked = true;
                if (ChoosedPlayer == "X")
                {
                    MarkX(7);
                    MyStep = 7;
                }
                else
                {
                    MarkO(7);
                    MyStep = 7;
                }
            }

            if (ChoosedPlayer == currentPlayer && btn.Equals(button8))
            {
                clicked = true;
                if (ChoosedPlayer == "X")
                {
                    MarkX(8);
                    MyStep = 8;
                }
                else
                {
                    MarkO(8);
                    MyStep = 8;
                }
            }

            if (ChoosedPlayer == currentPlayer && btn.Equals(button9))
            {
                clicked = true;
                if (ChoosedPlayer == "X")
                {
                    MarkX(9);
                    MyStep = 9;
                }
                else
                {
                    MarkO(9);
                    MyStep = 9;
                }
            }

            if (clicked)
            {
                SendMove();
                CheckEndGame();
            }
        }

        private void SendMove()
        {
            var nwStream = client.GetStream();
            string textToSend = "mv" + nameTextBox.Text + "," + MyStep;
            byte[] bytesToSend = Encoding.UTF8.GetBytes(textToSend);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        private void ListenForCommad()
        {
            try
            {
                while (client.Connected)
                {
                    NetworkStream nwstream = client.GetStream();
                    byte[] buffer = new byte[client.ReceiveBufferSize];
                    int bytesRead = nwstream.Read(buffer, 0, client.ReceiveBufferSize);
                    string dataRecieved = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    if (dataRecieved != "")
                    {
                        string RecievedCommad = dataRecieved.Substring(0, 2);
                        string RecievedMessage = "";

                        if (dataRecieved.Length > 2)
                            RecievedMessage = dataRecieved.Remove(0, 2);    //remove first two chars bcz they r commnd

                        if (RecievedCommad == "up")
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                if (checkboxListBox.IsEnabled)
                                {
                                    checkboxListBox.Items.Clear();
                                    string[] tmp = RecievedMessage.Split(',');
                                    for (int i = 0; i < tmp.Length; i++)
                                    {
                                        if (tmp[i] != "")
                                        {
                                            checkboxListBox.Items.Add(tmp[i]);
                                        }
                                    }
                                }
                            });
                        }

                        if (RecievedCommad == "pa")
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Player1.Content = RecievedMessage;
                                Player2.Content = nameTextBox.Text;
                                nameTextBox.IsEnabled = false;
                                connectButton.IsEnabled = false;
                                playButton.IsEnabled = false;
                                checkboxListBox.IsEnabled = false;
                            });
                            currentPlayer = "X";
                            ChoosedPlayer = "O";
                            this.Dispatcher.Invoke(() =>
                            {
                                youWithLabel.Content = "Circle";
                                cPlayerLabel.Content = "X";
                            });
                        }

                        if (RecievedCommad == "ex") { }
                        if (RecievedCommad == "mv")
                        {
                            int Step = int.Parse(RecievedMessage);
                            switch (Step)
                            {
                                case 1:
                                    if (ChoosedPlayer == "X")
                                        MarkO(Step);
                                    else if (ChoosedPlayer == "O")
                                        MarkX(Step);
                                    break;
                                case 2:
                                    if (ChoosedPlayer == "X")
                                        MarkO(Step);
                                    else if (ChoosedPlayer == "O")
                                        MarkX(Step);
                                    break;
                                case 3:
                                    if (ChoosedPlayer == "X")
                                        MarkO(Step);
                                    else if (ChoosedPlayer == "O")
                                        MarkX(Step);
                                    break;
                                case 4:
                                    if (ChoosedPlayer == "X")
                                        MarkO(Step);
                                    else if (ChoosedPlayer == "O")
                                        MarkX(Step);
                                    break;
                                case 5:
                                    if (ChoosedPlayer == "X")
                                        MarkO(Step);
                                    else if (ChoosedPlayer == "O")
                                        MarkX(Step);
                                    break;
                                case 6:
                                    if (ChoosedPlayer == "X")
                                        MarkO(Step);
                                    else if (ChoosedPlayer == "O")
                                        MarkX(Step);
                                    break;
                                case 7:
                                    if (ChoosedPlayer == "X")
                                        MarkO(Step);
                                    else if (ChoosedPlayer == "O")
                                        MarkX(Step);
                                    break;
                                case 8:
                                    if (ChoosedPlayer == "X")
                                        MarkO(Step);
                                    else if (ChoosedPlayer == "O")
                                        MarkX(Step);
                                    break;
                                case 9:
                                    if (ChoosedPlayer == "X")
                                        MarkO(Step);
                                    else if (ChoosedPlayer == "O")
                                        MarkX(Step);
                                    break;
                                default:
                                    break;
                            }
                            CheckEndGame();
                        }


                        //fpor chat
                        if (dataRecieved.Contains("chs_!@()<.-#;"))
                        {
                            RecievedCommad = dataRecieved.Substring(0, 3);
                            if (dataRecieved.Length > 3)
                                RecievedMessage = dataRecieved.Remove(0, 14);    //remove first three for cat chars bcz they r commnd

                            if (RecievedCommad == "chs")
                            {
                                this.Dispatcher.Invoke(() =>
                                {
                                    if (chatListBox.IsEnabled)
                                    {
                                        chatListBox.Items.Clear();
                                        string[] tmp = RecievedMessage.Split(',');
                                        for (int i = 0; i < tmp.Length; i++)
                                        {
                                            if (tmp[i] != "")
                                            {
                                                chatListBox.Items.Add(tmp[i]);
                                            }
                                        }
                                    }

                                    chatListBox.SelectedIndex = chatListBox.Items.Count - 1;
                                    chatListBox.ScrollIntoView(chatListBox.SelectedItem);
                                });
                            }

                        }


                    }  //end first if
                }
            }
            catch (System.IO.IOException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Game Closed");
                    System.Windows.Application.Current.Shutdown();
                });
            }
            catch (System.Threading.Tasks.TaskCanceledException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Opponent Left");
                    System.Windows.Application.Current.Shutdown();
                });
            }
        }

        private void checkboxListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLabel.Content = checkboxListBox.SelectedItem.ToString();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (thread != null)
                    thread.Abort();
            }
            catch (Exception ex)
            {

            }
            System.Windows.Application.Current.Shutdown();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.nameTextBox.Text.ToString().Trim().Length > 0)
                {
                    //IP ADDRESS WILL BE REPLACED PLEASE
                    //IP ADDRESS WILL BE REPLACED PLEASE
                    client.Connect(SERVER_IP_ADDRESS, 2899);
                    //IP ADDRESS WILL BE REPLACED PLEASE
                    //IP ADDRESS WILL BE REPLACED PLEASE
                    if (client.Connected)
                    {
                        playButton.IsEnabled = true;
                        nameTextBox.IsEnabled = false;
                        connectButton.IsEnabled = false;
                        chatButton.IsEnabled = true;
                        thread = new Thread(() => ListenForCommad());
                        thread.Start();
                        string textToSend = "lo" + nameTextBox.Text.ToString();
                        byte[] bytesToSend = Encoding.UTF8.GetBytes(textToSend);
                        var networkStream = client.GetStream();
                        networkStream.Write(bytesToSend, 0, bytesToSend.Length);
                    }
                }
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                MessageBox.Show("Server is closed at the moment!");
            }

        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedLabel.Content.ToString() != nameTextBox.Text.ToString())
            {
                Player1.Content = nameTextBox.Text;
                Player2.Content = selectedLabel.Content;
                var nwStream = client.GetStream();
                string textToSend = "pl" + nameTextBox.Text + "," + selectedLabel.Content;
                byte[] bytesToSend = Encoding.UTF8.GetBytes(textToSend);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                nameTextBox.IsEnabled = false;
                connectButton.IsEnabled = false;
                playButton.IsEnabled = false;
                checkboxListBox.IsEnabled = false;
                currentPlayer = "X";
                ChoosedPlayer = "X";
                youWithLabel.Content = "X";
                cPlayerLabel.Content = "X";
            }
            else
            {
                MessageBox.Show("Cannot play against yourself");
            }
        }

        private void nameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (nameTextBox.Text.Length > 8)
            {
                nameTextBox.Text = nameTextBox.Text.Remove(8);
                MessageBox.Show("name can be max of 8 chars");
            }
        }
        private void chatButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(chatTextBox.Text.ToString().Trim()))
            {
                var nwStream = client.GetStream();
                string textToSend = "ch" + nameTextBox.Text + ": " + chatTextBox.Text;
                byte[] bytesToSend = Encoding.UTF8.GetBytes(textToSend);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                chatTextBox.Text = "";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.Close();
        }

    }
}