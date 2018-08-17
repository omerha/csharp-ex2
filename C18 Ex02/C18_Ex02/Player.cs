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
        public void GetMoveFromHuman(ref int o_MoveOfPlayer, int i_boardRows)
        {
            string userInput = null;
            int attempts = 0;
            playerConsoleUtils.PrintMoveOfPlayersQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out o_MoveOfPlayer) == false || o_MoveOfPlayer > i_boardRows || o_MoveOfPlayer<1)
            {
                if (userInput=="Q")
                {
                    //the other player is win
                }
                else
                {
                    playerConsoleUtils.PrintNumOfPlayersQuestion(ref userInput, ++attempts);
                }
            }

        }
        public void GetMoveFromComputer(ref char [,] i_GameBoard) 
        {
            //Use random func or think about "smart" moves he said he will give 10 points extra for AI - thats why 
            // I pass the matrix board as well.
        }
        public void GameBoardUpdate(int i_Move, ref char[,] io_GameBoard, int i_BoardRows, int i_BoardCols)
        {
           bool isMoveDone = false;
           for (int i= i_BoardRows; i >0 && isMoveDone!=true;i--)
            {
                if (io_GameBoard[i-1,i_Move-1]!=' ')
                {
                    io_GameBoard[i-1, i_Move-1] = 'X';
                    isMoveDone = true;
                }
            }
        }
    }
}
