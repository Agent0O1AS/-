using System;
using System.Collections.Generic;
using System.Text;

namespace SMD_Game_PR3
{
    public class Player
    {
        public char id;
        public char symbol;
        public int y_player;
        public int x_player;
        public Item[] items = new Item[3];
        public int itemNumber = 1;
        public char[,] control = new char[2,6];

        public Player(char id, char symbol, int y_player, int x_player, char[,] control)
        {
            this.id = id;
            this.symbol = symbol;
            this.y_player = y_player;
            this.x_player = x_player;
            this.control = control;
        }
    }
}
