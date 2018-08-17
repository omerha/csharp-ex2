using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex02
{
    class GameManager
    {
        const int k_MaxSizeOfTable = 8;
        const int k_MinSizeOfTable = 4;
        const int k_MaxNumOfPlayers = 2;
        const int k_MinNumOfPlayer = 1;
        Player[] m_Players = new Player[2];
        PrintConsoleUtils consoleUtils = new PrintConsoleUtils();
        //Do not need to set an authorization level for each variable?
        int m_BoardCols = 0;
        int m_BoardRows = 0;
        int m_NumOfHumanPlayers = 0;
        int m_WhoWins = 0;
        bool m_IsTheBoardFull = false;
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
            IntilizationGameBoard();
            GetNumOfHumanPlayers();
            consoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
            RunGame();
        }
        public void IntilizationGameBoard()
        {
            for (int i = 0; i < m_BoardRows; i++)
            {
                for (int j = 0; j < m_BoardCols; j++)
                {
                    m_GameBoard[i, j] = 'Y';
                }
            }
        }
        public void RunGame()
        {
            if (m_NumOfHumanPlayers==1)
            {
                GameOfOneHumanPlayerVsCompuer();
            }
            else
            {
                GameOfTwoHumanPlayers();
            }
        }
        public void GameOfTwoHumanPlayers()
        {
            int moveOfPlayer=0;
            while (m_IsTheBoardFull==false && m_WhoWins==0)
            {
                m_Players[0].GetMoveFromHuman(ref moveOfPlayer, m_BoardRows);
                m_Players[0].GameBoardUpdate(moveOfPlayer, ref m_GameBoard, m_BoardRows, m_BoardCols);
                consoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
                if (m_IsTheBoardFull == false && m_WhoWins == 0)
                {
                    m_Players[1].GetMoveFromHuman(ref moveOfPlayer, m_BoardRows);
                    m_Players[1].GameBoardUpdate(moveOfPlayer, ref m_GameBoard, m_BoardRows, m_BoardCols);
                    consoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
                }
            }
        }
        public void GameOfOneHumanPlayerVsCompuer()
        {
            int moveOfPlayer = 0;
            while (m_IsTheBoardFull == false && m_WhoWins == 0)
            {
                m_Players[0].GetMoveFromHuman(ref moveOfPlayer, m_BoardRows);
                m_Players[0].GameBoardUpdate(moveOfPlayer, ref m_GameBoard, m_BoardRows, m_BoardCols);
                if (m_IsTheBoardFull == false && m_WhoWins == 0)
                {
                    m_Players[1].GetMoveFromComputer(ref m_GameBoard);
                }
            }
        }
        public void GetBoardSizes()
        {
            string userInput = null;
            int attempts = 0;
            consoleUtils.PrintBoardRowsQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out m_BoardRows) == false || m_BoardRows > k_MaxSizeOfTable || m_BoardRows < k_MinSizeOfTable)
            {
                consoleUtils.PrintBoardRowsQuestion(ref userInput, ++attempts);
            }
            attempts = 0;
            consoleUtils.PrintBoardColsQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out m_BoardCols) == false || m_BoardCols > k_MaxSizeOfTable || m_BoardCols < k_MinSizeOfTable)
            {
                consoleUtils.PrintBoardColsQuestion(ref userInput, ++attempts);
            }
        }
        public void GetNumOfHumanPlayers()
        {
            string userInput = null;
            int attempts = 0;
            consoleUtils.PrintNumOfPlayersQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out m_NumOfHumanPlayers) == false || m_NumOfHumanPlayers < k_MinNumOfPlayer || m_NumOfHumanPlayers > k_MaxNumOfPlayers)
            {
                consoleUtils.PrintNumOfPlayersQuestion(ref userInput, ++attempts);
            }
        }
    }
}
