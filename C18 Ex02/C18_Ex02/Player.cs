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
        public void GetMoveFromHuman(ref int o_MoveOfPlayer, int i_boardRows)
        {
            string userInput = null;
            int attempts = 0;
            playerConsoleUtils.PrintMoveOfPlayersQuestion(ref userInput, attempts);
            while (int.TryParse(userInput, out o_MoveOfPlayer) == false || o_MoveOfPlayer > i_boardRows || o_MoveOfPlayer<1)
            {
                if (userInput=="Q")
                {
                    //
                }
                else
                {
                    playerConsoleUtils.PrintNumOfPlayersQuestion(ref userInput, ++attempts);
                }
            }

        }
        public void GetMoveFromComputer(ref char [,] i_GameBoard) 
        {
            int avaliableRow = 0;
            int avaliableCol = 0;
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
