using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex02
{
    public class Player
    {
        private PrintConsoleUtils playerConsoleUtils = new PrintConsoleUtils();
        private int m_NumOfPoints = 0;
        private char m_Sign;

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

        public void GetMoveFromHuman(ref int o_MoveOfPlayer, int i_BoardCols, ref bool o_ExitFromTheGame)
        {
            string userInput = null;
            int attempts = 0;
            playerConsoleUtils.PrintMoveOfPlayersQuestion(ref userInput, attempts);
            userInput = userInput.ToUpper();
            while ((int.TryParse(userInput, out o_MoveOfPlayer) == false || o_MoveOfPlayer > i_BoardCols || o_MoveOfPlayer < 1) && (!o_ExitFromTheGame)) 
            {
                if (userInput == "Q") 
                {
                    o_ExitFromTheGame = true;
                }
                else
                {
                    playerConsoleUtils.PrintNumOfPlayersQuestion(ref userInput, ++attempts);
                }
            }
        }

        public void GetMoveFromComputer(ref int o_MoveOfPlayer, int i_BoardCols) 
        {
            Random rander = new Random();
            o_MoveOfPlayer = rander.Next(1, i_BoardCols);
        }
    }
}
