using SMD_Game_PR3;
using SMD_2_3_IS_PR1;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SMD_2_3_IS_PR1
{
    class Program
    {
        static void Main()
        {
            List<IOperation> operations = new List<IOperation>();
            Menu Menu = new Menu();
            Menu.menuItems.Add(new MenuItem(1, "Добавить операцию"));
            Menu.menuItems.Add(new MenuItem(2, "Список операций"));
            Menu.menuItems.Add(new MenuItem(3, "Сохранить в файл"));
            Menu.menuItems.Add(new MenuItem(4, "Загрузить из файла"));
            Menu.menuItems.Add(new MenuItem(5, "Игра"));
            Menu.menuItems.Add(new MenuItem(6, "Выход"));

            while (true)
            {
                Console.WriteLine("=== Main Menu ===");
                foreach (MenuItem item in Menu.menuItems)
                    Console.WriteLine(item.id + ". " + item.text);
                Console.Write("- Введите команду: ");
                string key = Console.ReadLine();

                if (key == "6")
                {
                    Console.WriteLine("- Завершение работы!");
                    break;
                }
                else if (key == "1")
                {
                    Console.Write("- Введите строку: ");
                    string line = Console.ReadLine(); // Доходы: 2024.03.15 "Зарплата" 50000

                    string[] p = Functions.Spliter(line);

                    if (p[0] == "error")
                    {
                        Console.WriteLine("- Ошибка: неверный формат строки.");
                        Logger.Error("Неверный формат строки: " + line);
                        continue;
                    }

                    DateTime date = DateTime.ParseExact(p[1], "yyyy.MM.dd", null);
                    int sum = int.Parse(p[3]);

                    IOperation op = null;
                    if (p[0] == "Доходы") op = new Income(p[0], date, p[2], sum);
                    else if (p[0] == "Расходы") op = new Expense(p[0], date, p[2], sum);

                    if (op == null)
                    {
                        Console.WriteLine("- Ошибка: неизвестный тип.");
                        Logger.Error("Неизвестный тип: " + p[0]);
                    }
                    else
                    {
                        operations.Add(op);
                        Console.WriteLine("- Успешное добавление.");
                    }
                }
                else if (key == "2") Functions.PrintOperations(operations);
                else if (key == "3") Functions.SaveToFile(operations);
                else if (key == "4") Functions.LoadFromFile(operations);
                else if (key == "5")
                {
                    GameLauncher game = new GameLauncher();
                    game.StartGame();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("- Неверная команда!");
                    Logger.Error("Неверная команда меню: " + key);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            List<Tochka> listTochka = new List<Tochka>();
            listTochka.Add(new Tochka(1.0, 2.0));
            listTochka.Add(new Tochka(5.0, 4.5));
            listTochka.Add(new Tochka(15.0, 15.5));

            foreach (var item in StrokaTochka(listTochka))
            {
                Console.WriteLine("Точка (" + item.x + " ; " + item.y + ")");
            }
        }

        static List<Tochka> StrokaTochka(List<Tochka> list)
        {
            List<Tochka> answer = new List<Tochka>();
            foreach (Tochka tochka in list)
            {
                if ((Math.Abs(5 - tochka.x)) * (Math.Abs(5 - tochka.x)) + (Math.Abs(5 - tochka.y)) * (Math.Abs(5 - tochka.y)) <= 100)
                    answer.Add(tochka);
            }
            return answer;
        }
    }
}