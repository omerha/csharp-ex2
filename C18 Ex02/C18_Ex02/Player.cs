using System;
using System.Collections.Generic;
using System.Text;
namespace C18_Ex02
{
    class Player
    {
        PrintConsoleUtils playerConsoleUtils = new PrintConsoleUtils();
        int m_NumOfPoints = 0;
        char m_Sign;
        public char Sign
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }
        public int Points
        {
            get { return m_NumOfPoints; }
            set { m_NumOfPoints = value; }
        }
        public void GetMoveFromHuman(ref int o_MoveOfPlayer, int i_boardRows, ref bool o_ExitFromTheGame)
        {
            string userInput = null;
            int attempts = 0;
            playerConsoleUtils.PrintMoveOfPlayersQuestion(ref userInput, attempts);
            while ((int.TryParse(userInput, out o_MoveOfPlayer) == false || o_MoveOfPlayer > i_boardRows || o_MoveOfPlayer<1) && (!o_ExitFromTheGame) )
            {
                if (userInput=="Q")
                {
                    o_ExitFromTheGame = true;
                }
                else
                {
                    playerConsoleUtils.PrintNumOfPlayersQuestion(ref userInput, ++attempts);
                }
            }

        }
        static public void GetMoveFromComputer(ref int o_MoveOfPlayer, int i_boardRows) 
        {
            int avaliableRow = 0;
            int avaliableCol = 0;
            Random rander = new Random();
            string computerMove = null;
            int randomComputerMove = rander.Next(0, i_boardRows);
            int.TryParse(computerMove, out randomComputerMove);
        }

        public bool IsThereRowAvaliable(ref int o_row, char[,] i_GameBoard)
        {
            for (int i=0;i<i_GameBoard.Length;i++)
            {

            }
            return false;
        }


    }
}
