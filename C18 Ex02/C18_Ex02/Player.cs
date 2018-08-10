using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex02
{
    class Player
    {
        int m_NumOfPoints = 0;
        public int Points
        {
            get { return m_NumOfPoints; }
            set { m_NumOfPoints = value; }
        }
    }
}
