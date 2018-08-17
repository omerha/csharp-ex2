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
        int m_BoardCols = 0;
        int m_BoardRows = 0;
        int m_NumOfHumanPlayers = 0;
        bool m_ExitFromTheGame = false;
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
            RunGame();
        }

        public void RunGame()
        {
            System.Console.WriteLine(m_NumOfHumanPlayers);
            if (m_NumOfHumanPlayers==1)
            {
                m_Players[0].Sign = 'X';
                GameOfOneHumanPlayerVsCompuer();
            }
            else
            {
                m_Players[0].Sign = 'X';
                m_Players[1].Sign = 'O';
                while (!m_ExitFromTheGame)
                {
                    consoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
                    GameOfTwoHumanPlayers();
                }
            }
        }

        public void GameOfTwoHumanPlayers()
        {
            int moveOfPlayer=0;
            bool restartGame = false;
            System.Nullable<int> numberOfPlayerWhoWon = null ;
            while (!restartGame)
            {
                for (int numOfPlayer = 0; numOfPlayer<m_NumOfHumanPlayers && !restartGame ;numOfPlayer++)
                {
                    m_Players[numOfPlayer].GetMoveFromHuman(ref moveOfPlayer, m_BoardRows, ref m_ExitFromTheGame);

                    if (m_ExitFromTheGame)
                    {
                        numberOfPlayerWhoWon = System.Math.Abs(numOfPlayer - 1);
                        m_ExitFromTheGame = false;
                        restartGame = true;
                    }
                    else
                    {
                        GameBoardUpdate(moveOfPlayer, m_Players[numOfPlayer].Sign);
                        consoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
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
                        consoleUtils.PrintSatusGameMsg(numberOfPlayerWhoWon);
                        consoleUtils.PrintPointsStatus(m_NumOfHumanPlayers, m_Players);
                        GameOver();
                    }

                }
            }
        }

        public void GameOfOneHumanPlayerVsCompuer()
        {
            int moveOfPlayer = 0;
            bool restartGame = false;
            System.Nullable<int> numberOfPlayerWhoWon = null;
            while (!restartGame)
            {
                m_Players[0].GetMoveFromHuman(ref moveOfPlayer, m_BoardRows, ref m_ExitFromTheGame);
                if (m_ExitFromTheGame)
                {
                    numberOfPlayerWhoWon = 1;
                    m_ExitFromTheGame = false;
                    restartGame = true;
                }
                else
                {
                    GameBoardUpdate(moveOfPlayer, m_Players[0].Sign);
                    consoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
                    if (IsThereWinner(m_Players[0].Sign))
                    {
                        numberOfPlayerWhoWon = 0;
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
                        if (numberOfPlayerWhoWon.Value==0)
                        {
                            m_Players[0].Points++;
                            consoleUtils.PrintSatusGameMsgHumanVsComputer(numberOfPlayerWhoWon);
                            consoleUtils.PrintPointsStatus(m_NumOfHumanPlayers, m_Players);
                        }
                    }
                    GameOver();
                }

                // GetMoveFromComputer
                GameBoardUpdate(moveOfPlayer, 'C');
                consoleUtils.PrintBoard(m_BoardCols, m_BoardRows, m_GameBoard);
                if (IsThereWinner('C'))
                {
                    numberOfPlayerWhoWon = 1;
                    restartGame = true;
                }
                else if (IsFullBoard())
                {
                    restartGame = true;
                }
                if (restartGame)
                {
                    consoleUtils.PrintSatusGameMsgHumanVsComputer(numberOfPlayerWhoWon);
                    GameOver();
                }

            }
        }

        private void GameOver()
        {
            string userInput = null;
            int attempts = 0;
            consoleUtils.PrintContinueQuestion(ref userInput, attempts);
            while (userInput != "Y" && userInput != "N")
            {
                consoleUtils.PrintContinueQuestion(ref userInput, ++attempts);
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

        public void InitializationGameBoard()
        {
            for (int i=0;i<m_BoardRows;i++)
            {
                for (int j=0;j<m_BoardCols;j++)
                {
                    m_GameBoard[i, j] = '\0';
                }
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

        public void GameBoardUpdate(int i_Move, char i_Sign)
        {
            bool isMoveDone = false;
            for (int i = m_BoardRows; i > 0 && isMoveDone != true; i--)
            {
                if (m_GameBoard[i - 1, i_Move - 1] == '\0')
                {
                    m_GameBoard[i - 1, i_Move - 1] = i_Sign;
                    isMoveDone = true;
                }
            }
        }

        public bool IsFullBoard()
        {
            bool isFull = true;
            for (int i=0;i<m_BoardRows && isFull;i++)
            {
                for (int j=0;j<m_BoardCols && isFull;j++)
                {
                    if (m_GameBoard[i,j]=='\0')
                    {
                        isFull=false;
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

        public bool CheckRowsForWinner(char i_Sign)
        {
            int counterSingInRows = 0;
            for (int i=0;i<m_BoardRows;i++)
            {
                counterSingInRows = 0;
                for (int j=0;j<m_BoardCols;j++)
                {
                    if (m_GameBoard[i,j]==i_Sign)
                    {
                        counterSingInRows++;
                        if (counterSingInRows == 4)
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

        public bool CheckColsForWinner(char i_Sign)
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
                        if (counterSingInCols == 4)
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

        public bool CheckDiagonalForWinner(char i_Sign)
        {
            int counterSingDiagonal = 0;
            for (int k = 0; k < m_BoardCols - 3; k++)
            {

                for (int j = 0; (j + k) < m_BoardCols &&  j < m_BoardRows; j++)
                {
                    {
                        if (m_GameBoard[j, j+k] == i_Sign)
                        {
                            counterSingDiagonal++;
                            if (counterSingDiagonal == 4)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            counterSingDiagonal = 0;
                        }
                    }
                }
            }
            counterSingDiagonal = 0;
            for (int i=1;i<m_BoardRows-3;i++)
            {
                for (int j=0;j<m_BoardCols && (i+j)<m_BoardRows;j++)
                {
                    if (m_GameBoard[i + j, j] == i_Sign)
                    {
                        counterSingDiagonal++;
                        if (counterSingDiagonal == 4)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        counterSingDiagonal = 0;
                    }
                }
            }

            counterSingDiagonal = 0;
            int n = 7;
            for (int k=0;k<m_BoardCols-3;k++)
            {
                for (int j = 0; (j+k) < m_BoardCols && (n-j) > 0; j++)
                {
                    if (m_GameBoard[n - j, j + k] == i_Sign)
                    {
                        counterSingDiagonal++;
                        if (counterSingDiagonal == 4)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        counterSingDiagonal = 0;
                    }
                }
            }

            counterSingDiagonal = 0;
            for (int i = 6; i > 3 ; i--)
            {
                for (int j = 0;j < m_BoardCols && (i-j)>0; j++)
                {
                    if (m_GameBoard[i - j,j ] == i_Sign)
                    {
                        counterSingDiagonal++;
                        if (counterSingDiagonal == 4)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        counterSingDiagonal = 0;
                    }
                }
            }
            return false;
        }
    }
}
