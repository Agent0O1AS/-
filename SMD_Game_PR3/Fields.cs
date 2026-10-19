using System;
using System.Collections.Generic;
using System.Text;

namespace SMD_Game_PR3
{
    public class Fields
    {
        static public char[,] fieldSurvival = {
            { '0', '_', '_', '_', '_', '_', '_', '_', '_', '_'},
            { '_', '_', '_', '_', '_', '_', '1', '_', '_', '_'},
            { '_', '_', '_', '_', '#', '_', '_', '_', 'T', '_'},
            { '_', '_', '3', '_', '#', '_', '_', '_', '_', '_'},
            { '_', '_', '_', '_', '#', '_', '#', '#', '_', '_'},
            { '#', '_', '_', '_', '_', '_', '_', '#', '_', '_'},
            { '#', '#', '#', '_', '_', '_', '#', '#', '_', '_'},
            { '_', '■', '#', '_', '_', '_', '_', '#', '_', '_'},
            { '_', '_', '_', '_', '/', '_', '_', '_', '/', '_'},
            { '_', '_', '_', '_', '_', '_', '_', '_', '_', '_'}
        };
        //static public Field FieldSurvival = new Field(fieldSurvival);

        static public char[,] field100 = new char[100, 100];
        static public char[,] Fields100()
        {
            field100 = new char[100, 100];
            for (int y = 0; y < 100; y++)
                for (int x = 0; x < 100; x++)
                    field100[y, x] = '_';

            field100[0, 0] = '0';
            field100[2, 2] = '1';
            field100[1, 0] = '/';
            field100[1, 1] = 'T';
            field100[1, 2] = '■';
            return field100;
        }
    }
}
