using SMD_Game_PR3;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMD_2_3_IS_PR1
{
    public class GameLauncher
    {
        public GameLauncher() { }
        public void StartGame()
        {
            char mainKey; // = char.ToUpper(Console.ReadKey().KeyChar);
            bool game = true;
            string window = "menu";
            MenuGame Menu = new MenuGame();
            Menu.menuItems.Add(new MenuItemGame(1, "            Играть [P]            "));
            Menu.menuItems.Add(new MenuItemGame(2, "           Настройки [S]          "));
            Menu.menuItems.Add(new MenuItemGame(3, "         Выйти из игры [Q]        "));
            GameSettings settings = new GameSettings();
            //settings.SetField(new Field(Fields.fieldSurvival));
            settings.SetField(new Field(Fields.Fields100()));
            while (game)
            {

                // Главное меню
                while (window == "menu")
                {
                    Console.Clear();
                    Console.WriteLine("=== Главное меню ========= [X] ===");
                    Console.WriteLine();
                    foreach (MenuItemGame item in Menu.menuItems)
                        Console.WriteLine(item.text);
                    mainKey = char.ToUpper(Console.ReadKey().KeyChar);
                    if (mainKey == 'P')
                    {
                        window = "game";
                    }
                    //else if (mainKey == 'S') window = "settings";
                    else if (mainKey == 'Q' || mainKey == 'X')
                    {
                        game = false;
                        break;
                    }
                }

                // Игровое окно
                while (window == "game")
                {
                    Console.Clear();
                    Console.WriteLine("=== Игровое окно ========= [X] ===");

                    Console.WriteLine();
                    List<char[,]> cameras = settings.PlayerCamera();
                    foreach (char[,] camera in cameras)
                    {
                        for (int y = 0; y < camera.GetLength(0); y++)
                        {
                            string i = "";
                            for (int x = 0; x < camera.GetLength(1); x++) i += camera[y, x] + " ";
                            Console.WriteLine(i);
                        }
                        Console.WriteLine();
                    }

                    /*Console.WriteLine();
                    for (int y = 0; y < settings.Field.field.GetLength(0); y++)
                    {
                        string i = "";
                        for (int x = 0; x < settings.Field.field.GetLength(1); x++) i += settings.Field.field[y, x] + " ";
                        Console.WriteLine(i);
                    }*/

                    Console.WriteLine();
                    foreach (string s in settings.PlayerInventory()) Console.WriteLine(s);

                    Console.WriteLine();
                    mainKey = char.ToUpper(Console.ReadKey().KeyChar);
                    if (mainKey == 'X')
                    {
                        window = "menu";
                        break;
                    }
                    else settings.PlayerAction(mainKey);
                }
            }
        }
    }
}
