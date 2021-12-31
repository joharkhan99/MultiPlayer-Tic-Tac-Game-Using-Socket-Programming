using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Client
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayer : Window
    {
        /**
            TicTacToe Data Position
             0|1|2
             3|4|5
             6|7|8
        */
        int[] GameMatrixData = new int[9];
        int playerTurn = -1;     //-1=X, 1=O, 0=Stop
        int gameDifficulty = 2;      //0=easy,1=medium,2=Impossible
        bool isBotEnable = false;
        int LastMovePosition = -1;     //last move pos
        int playerXScores = 0, playerOScores = 0;     //players scores

        public SinglePlayer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (playerTurn == 0 || isBotEnable)
                return;

            if (sender.Equals(button1))
            {
                if (GameMatrixData[0] == 0)
                {
                    LastMovePosition = 0;
                    GameMatrixData[0] = playerTurn;
                    ChangeButtonText(LastMovePosition);
                    SetNextTurn();
                    RunBot();
                }
            }
            else if (sender.Equals(button2))
            {
                if (GameMatrixData[1] == 0)
                {
                    LastMovePosition = 1;
                    GameMatrixData[1] = playerTurn;
                    ChangeButtonText(LastMovePosition);
                    SetNextTurn();
                    RunBot();
                }
            }
            else if (sender.Equals(button3))
            {
                if (GameMatrixData[2] == 0)
                {
                    LastMovePosition = 2;
                    GameMatrixData[2] = playerTurn;
                    ChangeButtonText(LastMovePosition);
                    SetNextTurn();
                    RunBot();
                }
            }
            else if (sender.Equals(button4))
            {
                if (GameMatrixData[3] == 0)
                {
                    LastMovePosition = 3;
                    GameMatrixData[3] = playerTurn;
                    ChangeButtonText(LastMovePosition);
                    SetNextTurn();
                    RunBot();
                }
            }
            else if (sender.Equals(button5))
            {
                if (GameMatrixData[4] == 0)
                {
                    LastMovePosition = 4;
                    GameMatrixData[4] = playerTurn;
                    ChangeButtonText(LastMovePosition);
                    SetNextTurn();
                    RunBot();
                }
            }
            else if (sender.Equals(button6))
            {
                if (GameMatrixData[5] == 0)
                {
                    LastMovePosition = 5;
                    GameMatrixData[5] = playerTurn;
                    ChangeButtonText(LastMovePosition);
                    SetNextTurn();
                    RunBot();
                }
            }
            else if (sender.Equals(button7))
            {
                if (GameMatrixData[6] == 0)
                {
                    LastMovePosition = 6;
                    GameMatrixData[6] = playerTurn;
                    ChangeButtonText(LastMovePosition);
                    SetNextTurn();
                    RunBot();
                }
            }
            else if (sender.Equals(button8))
            {
                if (GameMatrixData[7] == 0)
                {
                    LastMovePosition = 7;
                    GameMatrixData[7] = playerTurn;
                    ChangeButtonText(LastMovePosition);
                    SetNextTurn();
                    RunBot();
                }
            }
            else if (sender.Equals(button9))
            {
                if (GameMatrixData[8] == 0)
                {
                    LastMovePosition = 8;
                    GameMatrixData[8] = playerTurn;
                    ChangeButtonText(LastMovePosition);
                    SetNextTurn();
                    RunBot();
                }
            }
        }

        public void SetNextTurn()
        {
            int iGameStatus = CheckStatus();
            if (iGameStatus == 2)
            {
                if (playerTurn == -1)
                {
                    playerTurn = 1;
                }
                else
                {
                    if (gameDifficulty != 3 && LastMovePosition == -1)
                    {
                        playerTurn = -1;
                    }
                    else
                    {
                        playerTurn = -1;
                    }
                }
            }
            else
            {
                if (iGameStatus == -1)
                {
                    playerXScores++;
                    MessageBox.Show("X wins: " + playerXScores.ToString());
                }
                else if (iGameStatus == 1)
                {
                    playerOScores++;
                    MessageBox.Show("O wins: " + playerOScores.ToString());
                }
                else
                {
                    /*                    playerTurn = 0;*/
                    MessageBox.Show("Game Draw");
                }

                playerTurn = 0;
                RestartGame(null, null);
            }
        }

        void RunBot()
        {
            //0=gameover
            if (playerTurn == 0)
                return;

            isBotEnable = true;
            WaitNSeconds(1);
            if (gameDifficulty == 2)
            {
                RunImpossibleBot();
            }
        }

        void RunImpossibleBot()
        {
            int iOpponent;

            //check win and dont lose
            if (playerTurn == -1) 
                iOpponent = 1;
            else
                iOpponent = -1;

            if (LastMovePosition == -1)
            {
                //paste on center
                if (GameMatrixData[4] == 0)
                {
                    LastMovePosition = 4;
                }
            }
            else if (GameMatrixData[LastMovePosition] != 0)
            {
                CheckWinAndDontLose(iOpponent);     //check dont lose
                CheckWinAndDontLose(playerTurn);     //check win
                if (GameMatrixData[LastMovePosition] != 0)
                {
                    if (LastMovePosition == 4) //last move is center
                    {
                        //paste on corner
                        if (GameMatrixData[0] == 0)
                            LastMovePosition = 0;
                        else if (GameMatrixData[2] == 0)
                            LastMovePosition = 2;
                        else if (GameMatrixData[6] == 0)
                            LastMovePosition = 6;
                        else if (GameMatrixData[8] == 0)
                            LastMovePosition = 8;
                        else
                            LastMovePosition = RandomPosition();
                    }
                    //Lastmove is corner
                    else if (LastMovePosition == 0 || LastMovePosition == 2 || LastMovePosition == 6 || LastMovePosition == 8)
                    {
                        //Paste on center
                        if (GameMatrixData[4] == 0)
                        {
                            LastMovePosition = 4;
                        }
                        //Paste on opposite corner
                        else if (GameMatrixData[0] == 0 && GameMatrixData[8] == iOpponent)
                        {
                            LastMovePosition = 0;
                        }
                        else if (GameMatrixData[2] == 0 && GameMatrixData[6] == iOpponent)
                        {
                            LastMovePosition = 2;
                        }
                        else if (GameMatrixData[6] == 0 && GameMatrixData[2] == iOpponent)
                        {
                            LastMovePosition = 6;
                        }
                        else if (GameMatrixData[8] == 0 && GameMatrixData[0] == iOpponent)
                        {
                            LastMovePosition = 8;
                        }
                        //If opposite corners are opponent
                        else if ((GameMatrixData[0] == iOpponent && GameMatrixData[8] == iOpponent) || (GameMatrixData[2] == iOpponent && GameMatrixData[6] == iOpponent))
                        {
                            //Paste on Edge
                            if (GameMatrixData[1] == 0)
                            {
                                LastMovePosition = 1;
                            }
                            else if (GameMatrixData[3] == 0)
                            {
                                LastMovePosition = 3;
                            }
                            else if (GameMatrixData[5] == 0)
                            {
                                LastMovePosition = 5;
                            }
                            else if (GameMatrixData[7] == 0)
                            {
                                LastMovePosition = 7;
                            }
                        }
                        //Paste on corner
                        else if (GameMatrixData[0] == 0)
                        {
                            LastMovePosition = 0;
                        }
                        else if (GameMatrixData[2] == 0)
                        {
                            LastMovePosition = 2;
                        }
                        else if (GameMatrixData[6] == 0)
                        {
                            LastMovePosition = 6;
                        }
                        else if (GameMatrixData[8] == 0)
                        {
                            LastMovePosition = 8;
                        }
                        else LastMovePosition = RandomPosition();
                    }
                    //Lastmove is Edge
                    else if (LastMovePosition == 1 || LastMovePosition == 3 || LastMovePosition == 5 || LastMovePosition == 7)
                    {
                        //Paste on center
                        if (GameMatrixData[4] == 0)
                        {
                            LastMovePosition = 4;
                        }
                        //Paste on corner when edges are opponent
                        else if (GameMatrixData[0] == 0 && GameMatrixData[1] == iOpponent && GameMatrixData[3] == iOpponent)
                        {
                            LastMovePosition = 0;
                        }
                        else if (GameMatrixData[2] == 0 && GameMatrixData[1] == iOpponent && GameMatrixData[5] == iOpponent)
                        {
                            LastMovePosition = 2;
                        }
                        else if (GameMatrixData[6] == 0 && GameMatrixData[3] == iOpponent && GameMatrixData[7] == iOpponent)
                        {
                            LastMovePosition = 6;
                        }
                        else if (GameMatrixData[8] == 0 && GameMatrixData[5] == iOpponent && GameMatrixData[7] == iOpponent)
                        {
                            LastMovePosition = 8;
                        }
                        //Paste the corner near the edge
                        else if (GameMatrixData[0] == 0 && ((GameMatrixData[1] == iOpponent) || (GameMatrixData[3] == iOpponent)))
                        {
                            LastMovePosition = 0;
                        }
                        else if (GameMatrixData[2] == 0 && ((GameMatrixData[1] == iOpponent) || (GameMatrixData[5] == iOpponent)))
                        {
                            LastMovePosition = 2;
                        }
                        else if (GameMatrixData[6] == 0 && ((GameMatrixData[3] == iOpponent) || (GameMatrixData[7] == iOpponent)))
                        {
                            LastMovePosition = 6;
                        }
                        else if (GameMatrixData[8] == 0 && ((GameMatrixData[5] == iOpponent) || (GameMatrixData[7] == iOpponent)))
                        {
                            LastMovePosition = 8;
                        }
                        //Paste on corner
                        else if (GameMatrixData[0] == 0)
                        {
                            LastMovePosition = 0;
                        }
                        else if (GameMatrixData[2] == 0)
                        {
                            LastMovePosition = 2;
                        }
                        else if (GameMatrixData[6] == 0)
                        {
                            LastMovePosition = 6;
                        }
                        else if (GameMatrixData[8] == 0)
                        {
                            LastMovePosition = 8;
                        }
                        //Paste on Edge
                        else if (GameMatrixData[1] == 0)
                        {
                            LastMovePosition = 1;
                        }
                        else if (GameMatrixData[3] == 0)
                        {
                            LastMovePosition = 3;
                        }
                        else if (GameMatrixData[5] == 0)
                        {
                            LastMovePosition = 5;
                        }
                        else if (GameMatrixData[7] == 0)
                        {
                            LastMovePosition = 7;
                        }
                        else LastMovePosition = RandomPosition();
                    }
                }
            }
            GameMatrixData[LastMovePosition] = playerTurn;
            ChangeButtonText(LastMovePosition);
            isBotEnable = false;
            SetNextTurn();

        }

        /**
            TicTacToe Data Position
             0|1|2
             3|4|5
             6|7|8
        */
        //-1=X win, 0=Draw, 1=O win, 2=continue play
        private int CheckStatus()
        {
            // position 0|1|2
            if (GameMatrixData[0] != 0 && GameMatrixData[0] == GameMatrixData[1] && GameMatrixData[1] == GameMatrixData[2])
            {
                return GameMatrixData[0];
            }
            // position 3|4|5
            else if (GameMatrixData[3] != 0 && GameMatrixData[3] == GameMatrixData[4] && GameMatrixData[4] == GameMatrixData[5])
            {
                return GameMatrixData[3];
            }
            // position 6|7|8
            else if (GameMatrixData[6] != 0 && GameMatrixData[6] == GameMatrixData[7] && GameMatrixData[7] == GameMatrixData[8])
            {
                return GameMatrixData[6];
            }
            // position 0|3|6
            else if (GameMatrixData[0] != 0 && GameMatrixData[0] == GameMatrixData[3] && GameMatrixData[3] == GameMatrixData[6])
            {
                return GameMatrixData[0];
            }
            // position 1|4|7
            else if (GameMatrixData[1] != 0 && GameMatrixData[1] == GameMatrixData[4] && GameMatrixData[4] == GameMatrixData[7])
            {
                return GameMatrixData[1];
            }
            // position 2|5|8
            else if (GameMatrixData[2] != 0 && GameMatrixData[2] == GameMatrixData[5] && GameMatrixData[5] == GameMatrixData[8])
            {
                return GameMatrixData[2];
            }
            // position 0|4|8
            else if (GameMatrixData[0] != 0 && GameMatrixData[0] == GameMatrixData[4] && GameMatrixData[4] == GameMatrixData[8])
            {
                return GameMatrixData[0];
            }
            // position 2|4|6
            else if (GameMatrixData[2] != 0 && GameMatrixData[2] == GameMatrixData[4] && GameMatrixData[4] == GameMatrixData[6])
            {
                return GameMatrixData[2];
            }
            //means draw
            else if (GameMatrixData[0] != 0 && GameMatrixData[1] != 0 && GameMatrixData[2] != 0 && GameMatrixData[3] != 0 && GameMatrixData[4] != 0 && GameMatrixData[5] != 0 && GameMatrixData[6] != 0 && GameMatrixData[7] != 0 && GameMatrixData[8] != 0)
            {
                return 0;
            }

            return 2;
        }

        void CheckWinAndDontLose(int turn)
        {
            if (GameMatrixData[0] == 0 && ((GameMatrixData[1] == turn && GameMatrixData[2] == turn) || (GameMatrixData[3] == turn && GameMatrixData[6] == turn) || (GameMatrixData[4] == turn && GameMatrixData[8] == turn)))
                LastMovePosition = 0;
            else if (GameMatrixData[1] == 0 && ((GameMatrixData[0] == turn && GameMatrixData[2] == turn) || (GameMatrixData[4] == turn && GameMatrixData[7] == turn)))
                LastMovePosition = 1;
            else if (GameMatrixData[2] == 0 && ((GameMatrixData[0] == turn && GameMatrixData[1] == turn) || (GameMatrixData[5] == turn && GameMatrixData[8] == turn) || (GameMatrixData[4] == turn && GameMatrixData[6] == turn)))
                LastMovePosition = 2;
            else if (GameMatrixData[3] == 0 && ((GameMatrixData[0] == turn && GameMatrixData[6] == turn) || (GameMatrixData[4] == turn && GameMatrixData[5] == turn)))
                LastMovePosition = 3;
            else if (GameMatrixData[4] == 0 && ((GameMatrixData[0] == turn && GameMatrixData[8] == turn) || (GameMatrixData[2] == turn && GameMatrixData[6] == turn) || (GameMatrixData[1] == turn && GameMatrixData[7] == turn) || (GameMatrixData[3] == turn && GameMatrixData[5] == turn)))
                LastMovePosition = 4;
            else if (GameMatrixData[5] == 0 && ((GameMatrixData[2] == turn && GameMatrixData[8] == turn) || (GameMatrixData[3] == turn && GameMatrixData[4] == turn)))
                LastMovePosition = 5;
            else if (GameMatrixData[6] == 0 && ((GameMatrixData[0] == turn && GameMatrixData[3] == turn) || (GameMatrixData[7] == turn && GameMatrixData[8] == turn) || (GameMatrixData[2] == turn && GameMatrixData[4] == turn)))
                LastMovePosition = 6;
            else if (GameMatrixData[7] == 0 && ((GameMatrixData[1] == turn && GameMatrixData[4] == turn) || (GameMatrixData[6] == turn && GameMatrixData[8] == turn)))
                LastMovePosition = 7;
            else if (GameMatrixData[8] == 0 && ((GameMatrixData[2] == turn && GameMatrixData[5] == turn) || (GameMatrixData[6] == turn && GameMatrixData[7] == turn) || (GameMatrixData[0] == turn && GameMatrixData[4] == turn)))
                LastMovePosition = 8;
        }
        private int RandomPosition()
        {
            int count = 0;
            for (int i = 0; i <= 8; i++)
            {
                if (GameMatrixData[i] == 0)
                {
                    count++;
                }
            }
            Random random = new Random();
            int iRandom = random.Next(1, count);

            count = 0;
            for (int i = 0; i <= 8; i++)
            {
                if (GameMatrixData[i] == 0)
                {
                    count++;
                    if (count == iRandom)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= 8; i++)
            {
                GameMatrixData[i] = 0;
            }

            LastMovePosition = -1;
            isBotEnable = false;
            //X first
            playerTurn = 1;
            SetNextTurn();

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

        void ChangeButtonText(int iPosition)
        {
            if (GameMatrixData[iPosition] == -1)
            {
                switch (iPosition)
                {
                    case (0): button1.Content = "X"; break;
                    case (1): button2.Content = "X"; break;
                    case (2): button3.Content = "X"; break;
                    case (3): button4.Content = "X"; break;
                    case (4): button5.Content = "X"; break;
                    case (5): button6.Content = "X"; break;
                    case (6): button7.Content = "X"; break;
                    case (7): button8.Content = "X"; break;
                    case (8): button9.Content = "X"; break;
                }
            }
            else if (GameMatrixData[iPosition] == 1)
            {
                switch (iPosition)
                {
                    case (0): button1.Content = "O"; break;
                    case (1): button2.Content = "O"; break;
                    case (2): button3.Content = "O"; break;
                    case (3): button4.Content = "O"; break;
                    case (4): button5.Content = "O"; break;
                    case (5): button6.Content = "O"; break;
                    case (6): button7.Content = "O"; break;
                    case (7): button8.Content = "O"; break;
                    case (8): button9.Content = "O"; break;
                }
            }
            else
            {
                switch (iPosition)
                {
                    case (0): button1.Content = ""; break;
                    case (1): button2.Content = ""; break;
                    case (2): button3.Content = ""; break;
                    case (3): button4.Content = ""; break;
                    case (4): button5.Content = ""; break;
                    case (5): button6.Content = ""; break;
                    case (6): button7.Content = ""; break;
                    case (7): button8.Content = ""; break;
                    case (8): button9.Content = ""; break;
                }
            }
        }

        private void CloseButtonCLick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //UTILITY
        private void WaitNSeconds(int secs)
        {
            if (secs < 1)
                return;
            DateTime dateTime = DateTime.Now.AddSeconds(secs);
            while (DateTime.Now < dateTime)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate { }));
            }
        }


    }
}
