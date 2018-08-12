using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex02
{
    class GameManager
    {
        Player[] m_Players = new Player[2];
        PrintConsoleUtils consoleUtils = new PrintConsoleUtils();
        int m_BoardCols = 0;
        int m_BoardRows = 0;
        int m_NumOfHumanPlayers = 0;
        char[,] m_GameBoard = null;
        public static void Main()
        {
            GameManager omer = new GameManager();
            omer.StartGame();
        }
        public void StartGame()
        {
            for(int i = 0; i < m_Players.Length; i++)
            {
                m_Players[i] = new Player();
            }
            GetBoardSizes();
            m_GameBoard = new char[m_BoardRows, m_BoardCols];
            GetNumOfHumanPlayers();
            consoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
            consoleUtils.PrintScores(m_Players);
        }
        public int Cols
        {
            get { return m_BoardCols; }
            set { m_BoardCols = value; }
        }
        public int Rows
        {
            get { return m_BoardRows; }
            set { m_BoardRows = value; }
        }
        public int HumanPlayers
        {
            get { return m_NumOfHumanPlayers; }
            set { m_NumOfHumanPlayers = value; }
        }
        public void GetBoardSizes()
        {
            string userInput = null;
            int attempts = 0;
            consoleUtils.PrintBoardRowsQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out m_BoardRows) == false || m_BoardRows > 8 || m_BoardRows < 4)
            {
                consoleUtils.PrintBoardRowsQuestion(ref userInput, ++attempts);
            }
            attempts = 0;
            consoleUtils.PrintBoardColsQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out m_BoardCols) == false || m_BoardCols > 8 || m_BoardCols < 4)
            {
                consoleUtils.PrintBoardColsQuestion(ref userInput, ++attempts);
            }
        }
        public void GetNumOfHumanPlayers()
        {
            string userInput = null;
            int attempts = 0;
            consoleUtils.PrintNumOfPlayersQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out m_NumOfHumanPlayers) == false || m_NumOfHumanPlayers <= 0 || m_NumOfHumanPlayers > 2)
            {
                consoleUtils.PrintNumOfPlayersQuestion(ref userInput, ++attempts);
            }
        }
    }
}
