using System;
using System.Collections.Generic;
using System.Text;
namespace C18_Ex02
{
    class PrintConsoleUtils
    {
        const char k_XSign = 'X';
        const char k_OSign = 'O';
        public void PrintBoard(int i_Cols, int i_Rows,char[,] i_GameBoard)
        {
            StringBuilder board = new StringBuilder();
            Ex02.ConsoleUtils.Screen.Clear();
            for (int i = 0; i < i_Rows; i++)
            {
                if (i == 0)
                {
                    for (int k = 1; k <= i_Cols; k++)
                    {
                        board.Append("   " + k + "  ");
                    }
                    board.Append("\n");
                }
                for (int j = 0; j < i_Cols; j++)
                {
                    if (i_GameBoard[i, j] == k_XSign)
                    {
                        board.Append("|  X  ");
                    }
                    else if (i_GameBoard[i, j] == k_OSign)
                    {
                        board.Append("|  O  ");
                    }
                    else
                    {
                        board.Append("|     ");
                    }
                }
                board.Append("|\n");
                board.Append('=', 6 * i_Cols + 1);
                board.Append("\n");
            }
            System.Console.WriteLine(board);

        }
        public void PrintBoardRowsQuestion(ref string io_Rows,int i_Attempts)
        {
            if (i_Attempts == 0) {
                System.Console.WriteLine("Type the height of the board (can be 4 to 8)");
            }
            else
            {
                System.Console.WriteLine("Wrong input, please try again.\nType the height of the board (can be 4 to 8)");
            }
            io_Rows = System.Console.ReadLine();
        }
        public void PrintBoardColsQuestion(ref string io_Cols, int i_Attempts)
        {
            if (i_Attempts == 0)
            {
                System.Console.WriteLine("Type the width of the board (can be 4 to 8)");
            }
            else
            {
                System.Console.WriteLine("Wrong input, please try again.\nType the width of the board (can be 4 to 8)");
            }
            io_Cols = System.Console.ReadLine();
        }
        public void PrintNumOfPlayersQuestion(ref string io_NumOfPlayers,int i_Attemps)
        {
          if(i_Attemps == 0)
            {
                System.Console.WriteLine("Type the number of players");
            }
            else
            {
                System.Console.WriteLine("Wrong input, please try again.\nType the number of players");
            }
            io_NumOfPlayers = System.Console.ReadLine();
        }
        public void PrintMoveOfPlayersQuestion(ref string io_MoveOfPlayer, int i_Attemps)
        {
            if (i_Attemps == 0)
            {
                System.Console.WriteLine("Type the number of column");
            }
            else
            {
                System.Console.WriteLine("Wrong input, please try again.\nType the number of column");
            }
            io_MoveOfPlayer = System.Console.ReadLine();
        }
        public void PrintScores(Player[] i_Players)
        {
            System.Console.WriteLine("=== Scores Board ===");
            string outputPlayerScore = null;
            for(int i = 0; i < i_Players.Length; i++)
            {
                outputPlayerScore = String.Format("Player Num.{0} has {1} points", i + 1, i_Players[i].Points);
                System.Console.WriteLine(outputPlayerScore);
            }
        }
        public void PrintContinueQuestion(ref string io_Answer, int i_Attempts)
        {
            if (i_Attempts == 0)
            {
                System.Console.WriteLine("Do you want to continue playing ?(Y / N)");
            }
            else
            {
                System.Console.WriteLine("Wrong input, please try again.\nDo you want to continue playing ?(Y / N)");
            }
            io_Answer = System.Console.ReadLine();
        }
        public void PrintSatusGameMsg(System.Nullable<int> i_Winner)
        {
            if (i_Winner.HasValue)
            {
                string msgWinner = String.Format("The winner is player number {0} !!!", i_Winner.Value + 1);
                System.Console.WriteLine(msgWinner);
            }
            else
            {
                System.Console.WriteLine("The game has ended in a tie");
            }
        }
        public void PrintSatusGameMsgHumanVsComputer(System.Nullable<int> i_Winner)
        {
            if (i_Winner.HasValue)
            {
                if (i_Winner.Value==0)
                {
                    System.Console.WriteLine("The winner is the human player!!!");
                }
                else
                {
                    System.Console.WriteLine("The winner is the computer!!!");
                }
            }
            else
            {
                System.Console.WriteLine("The game has ended in a tie");
            }
        }
        public void PrintPointsStatus(int i_NumOfHumanPlayers, Player[] i_Players)
        {
            System.Console.WriteLine("The points status:");
            for (int i=0;i<i_NumOfHumanPlayers;i++)
            {
                string msgPointStatus = String.Format("The points of player number {0} are: {1} ", i+1, i_Players[i].Points);
                System.Console.WriteLine(msgPointStatus);
            }
        }
    }
}
