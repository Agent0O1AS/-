using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace SMD_Game_PR3
{
    public class GameSettings
    {
        public Field Field;

        public GameSettings() { }

        public void SetField(Field Field)
        {
            this.Field = Field; // (char[,])field.Clone();
        }

        public void PlayerAction(char action)
        {
            foreach (Player p in Field.players)
            {
                if ((action == p.control[0, 0] || action == p.control[1, 0]) && p.y_player != 0)
                    PlayerMove(-1, 0, p);
                if ((action == p.control[0, 1] || action == p.control[1, 1]) && p.x_player != 0)
                    PlayerMove(0, -1, p);
                if ((action == p.control[0, 2] || action == p.control[1, 2]) && p.y_player != Field.y_field - 1)
                    PlayerMove(1, 0, p);
                if ((action == p.control[0, 3] || action == p.control[1, 3]) && p.x_player != Field.x_field - 1)
                    PlayerMove(0, 1, p);
                if (action == p.control[0, 4] || action == p.control[1, 4])
                {
                    if (Field.field[p.y_player, p.x_player] != '_' && p.items[p.itemNumber] != null)
                    {
                        char up = Field.field[p.y_player, p.x_player];
                        Field.field[p.y_player, p.x_player] = p.items[p.itemNumber].symbol;
                        if (up == '/') p.items[p.itemNumber] = new Item('/', "Факел");
                        else if (up == 'T') p.items[p.itemNumber] = new Item('T', "Кирка");
                        else if (up == '■') p.items[p.itemNumber] = new Item('■', "Блок");
                    }
                    else if (Field.field[p.y_player, p.x_player] != '_' && p.items[p.itemNumber] == null)
                    {
                        if (Field.field[p.y_player, p.x_player] == '/') p.items[p.itemNumber] = new Item('/', "Факел");
                        else if (Field.field[p.y_player, p.x_player] == 'T') p.items[p.itemNumber] = new Item('T', "Кирка");
                        else if (Field.field[p.y_player, p.x_player] == '■') p.items[p.itemNumber] = new Item('■', "Блок");
                        Field.field[p.y_player, p.x_player] = '_';
                    }
                    else if (Field.field[p.y_player, p.x_player] == '_' && p.items[p.itemNumber] != null)
                    {
                        Field.field[p.y_player, p.x_player] = p.items[p.itemNumber].symbol;
                        p.items[p.itemNumber] = null;
                    }
                }
                if (action == p.control[0, 5] || action == p.control[1, 5])
                {
                    if (p.itemNumber < 2) p.itemNumber++;
                    else p.itemNumber = 0;
                }
            }
        }

        public void PlayerMove(int cy, int cx, Player p)
        {
            if (Field.field[p.y_player + cy, p.x_player + cx] != '#')
            {
                if (p.items[p.itemNumber] != null)
                    if (p.items[p.itemNumber].symbol == '■' && Field.field[p.y_player, p.x_player] == '_')
                    {
                        bool block = true;
                        foreach (Player e in Field.players)
                            if (e.id != p.id && e.y_player == p.y_player && e.x_player == p.x_player)
                                block = false;
                        if (block)
                            Field.field[p.y_player, p.x_player] = '#';
                    }
                p.y_player = p.y_player + cy;
                p.x_player = p.x_player + cx;
                Field.PlayerCoordinates(p.id - '0');
            }
            else if (p.items[p.itemNumber] != null)
            {
                if (p.items[p.itemNumber].symbol == 'T')
                {
                    Field.field[p.y_player + cy, p.x_player + cx] = '_';
                    p.y_player = p.y_player + cy;
                    p.x_player = p.x_player + cx;
                    Field.PlayerCoordinates(p.id - '0');
                }
            }
        }

        public List<string> PlayerInventory()
        {
            List<string> answer = new List<string>();
            foreach (Player p in Field.players)
            {
                string inventory = " [ ] [ ] [ ] ";
                if (p.items[0] != null) inventory = inventory.Substring(0, 2) + p.items[0].symbol + inventory.Substring(3);
                if (p.items[1] != null) inventory = inventory.Substring(0, 6) + p.items[1].symbol + inventory.Substring(7);
                if (p.items[2] != null) inventory = inventory.Substring(0, 10) + p.items[2].symbol + inventory.Substring(11);
                if (p.itemNumber == 0) inventory = " >" + inventory[2] + "<" + inventory.Substring(4);
                else if (p.itemNumber == 1) inventory = inventory.Substring(0, 5) + ">" + inventory[6] + "<" + inventory.Substring(8);
                else if (p.itemNumber == 2) inventory = inventory.Substring(0, 9) + ">" + inventory[10] + "< ";
                if (p.items[p.itemNumber] != null) inventory += " (" + p.items[p.itemNumber].name + ")";
                answer.Add("- Инвентарь " + p.symbol + " -");
                answer.Add(inventory);
                answer.Add("- - - - - - - -");
            }
            return answer;
        }

        public List<char[,]> PlayerCamera()
        {
            List<char[,]> answer = new List<char[,]>();
            foreach (Player p in Field.players)
            {
                int n = 5;
                if (p.items[p.itemNumber] != null && p.items[p.itemNumber].symbol == '/') n += 2;
                char[,] camera = new char[n, n];
                for (int y = 0; y < n; y++)
                    for (int x = 0; x < n; x++)
                    {
                        if (p.x_player - (n / 2) + x >= 0 && p.x_player - (n / 2) + x < Field.x_field && p.y_player - (n / 2) + y >= 0 && p.y_player - (n / 2) + y < Field.y_field)
                        {
                            camera[y, x] = Field.field[p.y_player - (n / 2) + y, p.x_player - (n / 2) + x];
                            foreach (Player e in Field.players)
                            {
                                if (p.y_player - (n / 2) + y == e.y_player && p.x_player - (n / 2) + x == e.x_player)
                                    camera[y, x] = e.symbol;
                            }
                        }
                        else camera[y, x] = 'X';
                    }
                camera[n / 2, n / 2] = p.symbol;
                answer.Add(camera);
            }
            return answer;
        }
    }
}
