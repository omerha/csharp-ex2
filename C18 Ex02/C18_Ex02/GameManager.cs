﻿using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex02
{
    public class GameManager
    {
        private const int k_MaxSizeOfTable = 8;
        private const int k_MinSizeOfTable = 4;
        private const int k_MaxNumOfPlayers = 2;
        private const int k_MinNumOfPlayer = 1;
        private const int k_HumanPlayer = 0;
        private const int k_ComputerPlayer = 1;
        private const int k_NumOfSignsSequenceToWin = 4;
        private const char k_XSign = 'X';
        private const char k_OSign = 'O';
        private const char k_EmptySquare = '\0';
        private Player[] m_Players = new Player[2];
        private PrintConsoleUtils m_ConsoleUtils = new PrintConsoleUtils();
        private int m_BoardCols = 0;
        private int m_BoardRows = 0;
        private int m_NumOfHumanPlayers = 0;
        private bool m_ExitFromTheGame = false;
        private char[,] m_GameBoard = null;

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
            m_Players[0].Sign = k_XSign;
            m_Players[1].Sign = k_OSign;
            RunGame();
        }

        public void RunGame()
        {
            while (!m_ExitFromTheGame)
            {
                m_ConsoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
                if (m_NumOfHumanPlayers == 1)
                {
                    GameOfOneHumanPlayerVsCompuer();
                }
                else
                {
                    GameOfTwoHumanPlayers();
                }
            }
        }

        public void GameOfTwoHumanPlayers()
        {
            bool restartGame = false;
            int? numberOfPlayerWhoWon = null;
            while (restartGame == false)
            {
                for (int numOfPlayer = 0; numOfPlayer < m_NumOfHumanPlayers && restartGame == false; numOfPlayer++) 
                {
                    GetHumanMoveAndUpdateBoard(numOfPlayer);
                    if (m_ExitFromTheGame == true)
                    {
                        numberOfPlayerWhoWon = System.Math.Abs(numOfPlayer - 1);
                        m_ExitFromTheGame = false;
                        restartGame = true;
                    }
                    else
                    {
                        if (IsThereWinner(m_Players[numOfPlayer].Sign))
                        {
                            numberOfPlayerWhoWon = numOfPlayer;
                            restartGame = true;
                        }
                        else if (IsFullBoard())
                        {
                            restartGame = true;
                        }
                    }

                    if (restartGame)
                    {
                        if (numberOfPlayerWhoWon.HasValue)
                        {
                            m_Players[numberOfPlayerWhoWon.Value].Points++;
                        }

                        m_ConsoleUtils.PrintSatusGameMsg(numberOfPlayerWhoWon);
                        m_ConsoleUtils.PrintPointsStatus(m_Players);
                        numberOfPlayerWhoWon = null;
                        GameOver();
                    }
                }
            }
        }

        public void GetHumanMoveAndUpdateBoard(int i_PlayerNum)
        {
            int moveOfPlayer = 0;
            bool moveIsLegal = false;
            while (moveIsLegal == false)
            {
                m_Players[i_PlayerNum].GetMoveFromHuman(ref moveOfPlayer, m_BoardCols, ref m_ExitFromTheGame);
                if(m_ExitFromTheGame == true)
                {
                    moveIsLegal = true;
                }
                else if (GameBoardUpdate(moveOfPlayer, m_Players[i_PlayerNum].Sign) == true)
                {
                    moveIsLegal = true;
                    m_ConsoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
                }
                else
                {
                    m_ConsoleUtils.PrintColIsFull();
                }             
            }      
        }

        public void GetComputerMoveAndUpdateBoard()
        {
            int moveOfComputer = 0;
            bool moveIsLegal = false;
            while (moveIsLegal == false)
            {
                m_Players[1].GetMoveFromComputer(ref moveOfComputer, m_BoardCols);
                if (GameBoardUpdate(moveOfComputer, m_Players[1].Sign) == true) 
                {
                    moveIsLegal = true;
                    m_ConsoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
                }
            }
        }

        public void GameOfOneHumanPlayerVsCompuer()
        {
            bool restartGame = false;
            int? numberOfPlayerWhoWon = null;
            while (restartGame == false)
            {
                for (int i = 0; i < m_Players.Length && restartGame == false; i++)
                {
                    if (i == k_HumanPlayer)
                    {
                        GetHumanMoveAndUpdateBoard(k_HumanPlayer);
                        if (m_ExitFromTheGame == true)
                        {
                            numberOfPlayerWhoWon = k_ComputerPlayer;
                            restartGame = true;
                        }
                    }
                    else
                    {
                        GetComputerMoveAndUpdateBoard();
                    }

                    if (m_ExitFromTheGame == false)
                    {
                        if (IsThereWinner(m_Players[i].Sign))
                        {
                            numberOfPlayerWhoWon = i;
                            restartGame = true;
                        }
                        else if (IsFullBoard())
                        {
                            restartGame = true;
                        }
                    }

                    if (restartGame == true)
                    {
                        m_ExitFromTheGame = false;
                        if (numberOfPlayerWhoWon.HasValue)
                        {
                            m_Players[numberOfPlayerWhoWon.Value].Points++;
                            m_ConsoleUtils.PrintSatusGameMsgHumanVsComputer(numberOfPlayerWhoWon);
                        }
                        
                        m_ConsoleUtils.PrintPointsStatus(m_Players);
                        numberOfPlayerWhoWon = null;
                        GameOver();
                    }
                }
            }
        }

        private void GameOver()
        {
            string userInput = null;
            int attempts = 0;
            m_ConsoleUtils.PrintContinueQuestion(ref userInput, attempts);
            userInput = userInput.ToUpper();
            while (userInput != "Y" && userInput != "N")
            {
                m_ConsoleUtils.PrintContinueQuestion(ref userInput, ++attempts);
            }

            if (userInput == "N")
            {
                m_ExitFromTheGame = true;
            }
            else
            {
                InitializationGameBoard();
            }
        }
        
        public void GetBoardSizes()
        {
            string userInput = null;
            int attempts = 0;
            m_ConsoleUtils.PrintBoardRowsQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out m_BoardRows) == false || m_BoardRows > k_MaxSizeOfTable || m_BoardRows < k_MinSizeOfTable)
            {
                m_ConsoleUtils.PrintBoardRowsQuestion(ref userInput, ++attempts);
            }

            attempts = 0;
            m_ConsoleUtils.PrintBoardColsQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out m_BoardCols) == false || m_BoardCols > k_MaxSizeOfTable || m_BoardCols < k_MinSizeOfTable)
            {
                m_ConsoleUtils.PrintBoardColsQuestion(ref userInput, ++attempts);
            }
        }

        public void InitializationGameBoard()
        {
            for (int i = 0; i < m_BoardRows; i++) 
            {
                for (int j = 0; j < m_BoardCols; j++) 
                {
                    m_GameBoard[i, j] = k_EmptySquare;
                }
            }
        }

        public void GetNumOfHumanPlayers()
        {
            string userInput = null;
            int attempts = 0;
            m_ConsoleUtils.PrintNumOfPlayersQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out m_NumOfHumanPlayers) == false || m_NumOfHumanPlayers < k_MinNumOfPlayer || m_NumOfHumanPlayers > k_MaxNumOfPlayers)
            {
                m_ConsoleUtils.PrintNumOfPlayersQuestion(ref userInput, ++attempts);
            }
        }

        public bool GameBoardUpdate(int i_Move, char i_Sign)
        {
            for (int i = m_BoardRows; i > 0; i--) 
            {
                if (m_GameBoard[i - 1, i_Move - 1] == k_EmptySquare)
                {
                    m_GameBoard[i - 1, i_Move - 1] = i_Sign;
                    return true;
                }
            }

            return false;
        }

        public bool IsFullBoard()
        {
            bool isFull = true;
            for (int i = 0; i < m_BoardRows && isFull; i++) 
            {
                for (int j = 0; j < m_BoardCols && isFull; j++) 
                {
                    if (m_GameBoard[i, j] == k_EmptySquare) 
                    {
                        isFull = false;
                    }
                }
            }

            return isFull;
        }
        
        public bool IsThereWinner(char i_Sign)
        {
            if (CheckRowsForWinner(i_Sign))
            {
                return true;
            }

            if (CheckColsForWinner(i_Sign))
            {
                return true;
            }

            if (CheckDiagonalForWinner(i_Sign))
            {
                return true;
            }

            return false;
        }

        private bool CheckRowsForWinner(char i_Sign)
        {
            int counterSingInRows = 0;
            for (int i = 0; i < m_BoardRows; i++) 
            {
                counterSingInRows = 0;
                for (int j = 0; j < m_BoardCols; j++) 
                {
                    if (m_GameBoard[i, j] == i_Sign) 
                    {
                        counterSingInRows++;
                        if (counterSingInRows == k_NumOfSignsSequenceToWin) 
                        {
                            return true;
                        }
                    }
                    else
                    {
                        counterSingInRows = 0;
                    }
                }
            }

            return false;
        }

        private bool CheckColsForWinner(char i_Sign)
        {
            int counterSingInCols = 0;
            for (int j = 0; j < m_BoardCols; j++)
            {
                counterSingInCols = 0;
                for (int i = 0; i < m_BoardRows; i++)
                {
                    if (m_GameBoard[i, j] == i_Sign)
                    {
                        counterSingInCols++;
                        if (counterSingInCols == k_NumOfSignsSequenceToWin)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        counterSingInCols = 0;
                    }
                }
            }

            return false;
        }

        private bool CheckDiagonalForWinner(char i_Sign)
        {
            int rowUpIndex = 0, rowDownIndex = 0, colIndex = 0, counterUpDiagonal = 0, counterDownDiagonal = 0;
            for (int i = 0; i < m_BoardRows - 3; i++) 
            {
                for (int j = 0; j < m_BoardCols - 3; j++) 
                {
                    rowUpIndex = m_BoardRows - 1 - i;
                    rowDownIndex = i;
                    colIndex = j;
                    counterDownDiagonal = 0;
                    counterUpDiagonal = 0;
                    while (colIndex < m_BoardCols && rowUpIndex >= 0 && rowDownIndex < m_BoardRows)
                    {
                        if (m_GameBoard[rowUpIndex, colIndex] == i_Sign && m_GameBoard[rowDownIndex, colIndex] == i_Sign) 
                        {
                            counterDownDiagonal++;
                            counterUpDiagonal++; 
                        }
                        else
                        {
                            if(m_GameBoard[rowUpIndex, colIndex] == i_Sign)
                            {
                                counterUpDiagonal++;
                                counterDownDiagonal = 0;
                            }
                            else if (m_GameBoard[rowDownIndex, colIndex] == i_Sign)
                            {
                                counterDownDiagonal++;
                                counterUpDiagonal = 0;
                            }
                            else
                            {
                                counterUpDiagonal = 0;
                                counterDownDiagonal = 0;
                            }
                        }
                     
                        if(counterUpDiagonal == k_NumOfSignsSequenceToWin || counterDownDiagonal == k_NumOfSignsSequenceToWin)
                        {
                            return true;
                        }

                        colIndex++;
                        rowDownIndex++;
                        rowUpIndex--;
                    }
                }
            }

            return false;
        }
    }
}
