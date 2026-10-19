using System;
using System.Collections.Generic;
using System.Text;

namespace SMD_Game_PR3
{
    public class Item
    {
        public char symbol;
        public string name;
        public Item(char symbol, string name)
        {
            this.symbol = symbol;
            this.name = name;
        }
    }
}
