using System;
using System.Collections.Generic;
using System.Text;

namespace SMD_Game_PR3
{
    public class Field
    {
        public char[,] field;
        public int y_field;
        public int x_field; //        { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public char[] playerSymbols = { '?', '!', '2', '3', '4', '5', '6', '7', '8', '9' };
        public List<Player> players = new List<Player>();

        public Field(char[,] field)
        {
            this.field = (char[,])field.Clone();
            this.y_field = field.GetLength(0); // строки
            this.x_field = field.GetLength(1); // столбцы
            PlayerSearch();
        }

        public void PlayerSearch()
        {
            for (int i = 0; i < 10; i++)
            {
                int[] coordinates = PlayerCoordinates(i);
                if (coordinates[0] > -1)
                {
                    char[,] control = new char[2, 6];
                    if (i == 0)
                    {
                        control = new char[2, 6] {
                            { 'W', 'A', 'S', 'D', 'Q', 'F'},
                            { 'Ц', 'Ф', 'Ы', 'В', 'Й', 'А'}
                        };
                    }
                    else
                    {
                        control = new char[2, 6] {
                            { 'I', 'J', 'K', 'L', 'U', ';'},
                            { 'Ш', 'О', 'Л', 'Д', 'Г', 'Ж'}
                        };
                    }
                    players.Add(new Player((char)('0' + i), playerSymbols[i], coordinates[0], coordinates[1], control));
                    field[coordinates[0], coordinates[1]] = '_';
                }
            }
        }

        public int[] PlayerCoordinates(int n)
        {
            int[] coordinates = { -1, -1 };
            for (int y = 0; y < field.GetLength(0); y++)
                for (int x = 0; x < field.GetLength(1); x++)
                    if (field[y, x] == (char)('0' + n))
                    {
                        coordinates[0] = y;
                        coordinates[1] = x;
                        return coordinates;
                    }
            return coordinates;
        }
    }
}
