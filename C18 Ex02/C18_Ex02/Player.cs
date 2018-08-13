using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex02
{
    class Player
    {
        PrintConsoleUtils playerConsoleUtils = new PrintConsoleUtils();
        int m_NumOfPoints = 0;
        public int Points
        {
            get { return m_NumOfPoints; }
            set { m_NumOfPoints = value; }
        }
        public void GetMoveFromHuman(ref int o_MoveOfPlayer)
        {
            string userInput = null;
            int attempts = 0;
            consoleUtils.PrintMoveOfPlayersQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out o_MoveOfPlayer) == false || o_MoveOfPlayer < k_MinNumOfPlayer || o_MoveOfPlayer > k_MaxNumOfPlayers)
            {
                if (userInput=="Q")
                {

                }
                else
                {
                    consoleUtils.PrintNumOfPlayersQuestion(ref userInput, ++attempts);
                }
            }
        }
        public void GetMoveFromComputer(ref char [,] i_GameBoard) 
        {
            //Use random func or think about "smart" moves he said he will give 10 points extra for AI - thats why 
            // I pass the matrix board as well.
        }
        public void GameBoardUpdate(int i_Move, ref char[,] i_GameBoard)
        {
            // take the move of the player and update the board
        }
    }
}
