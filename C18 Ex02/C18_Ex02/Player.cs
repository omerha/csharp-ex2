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
        public void GetMoveFromHuman(ref int o_Move)
        {
            //Use the printconsole utils to print the question only and keep the logic under this function.
        }
        public void GetMoveFromComputer(ref int o_Move, ref char [,] i_GameBoard)
        {
            //Use random func or think about "smart" moves he said he will give 10 points extra for AI - thats why 
            // I pass the matrix board as well.
        }
    }
}
