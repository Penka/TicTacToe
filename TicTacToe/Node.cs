using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Node
    {
        private int x;
        private int y;
        private char symbol;
        private bool player;

        public Node(int newX, int newY, bool newPlayer)
        {
            x = newX;
            y = newY;
            player = newPlayer;
            if (player)
            {
                symbol = 'X';
            }
            else
            {
                symbol = 'O';
            }
        }
        public char Symbol()
        {
            return symbol;
        }

        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }

        public bool Player()
        {
            return player;
        }
    }
}
