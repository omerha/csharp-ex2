using System;
using System.Collections.Generic;
using System.Text;
namespace C18_Ex02
{
    class PrintConsoleUtils
    {

        public void PrintBoard(int i_Cols, int i_Rows)
        {
            StringBuilder board = new StringBuilder();
            Ex02.ConsoleUtils.Screen.Clear();
            for (int i = 0; i < i_Rows; i++)
            {
                if (i == 0)
                {
                    for (int k = 1; k <= i_Cols; k++)
                    {
                        board.Append("  " + k + "  ");
                    }
                    board.Append("\n");
                }
                for (int j = 0; j < i_Cols; j++)
                {
                    board.Append("|    ");
                }
                board.Append("|\n");
                board.Append('=', 5 * i_Cols + 1);
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
    }
}
