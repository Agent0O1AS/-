using System;

namespace SMD_2_3_IS_PR1
{
    class Program
    {
        static void Main()
        {
            string line1 = "Доходы: 2024.03.15 \"Зарплата\" 50000";

            List<Income> list = new List<Income>();
            string key = "";
            string line = "";
            while (key != "q")
            {
                Console.Write("Введите команду: ");
                key = Convert.ToString(Console.ReadLine());
                if (key == "q")
                {
                    break;
                }
                Console.Write("Введите доходы: ");
                line = Convert.ToString(Console.ReadLine());

                string[] lines = Spliter(line);
                DateTime date = DateTime.ParseExact(lines[1], "yyyy.MM.dd", null);
                int sum = int.Parse(lines[3]);

                list.Add(new Income(lines[0], date, lines[2], sum));

                int number = 0;
                foreach (Income inlist in list)
                {
                    Console.WriteLine();
                    number++;
                    Console.WriteLine("Number " + number);
                    Console.WriteLine("Type:   " + inlist.type);
                    Console.WriteLine("Date:   " + inlist.date.ToString("yyyy.MM.dd"));
                    Console.WriteLine("Source: " + inlist.source);
                    Console.WriteLine("Sum:    " + inlist.sum);
                }
            }

            Console.WriteLine();
            List<Tochka> listTochka = new List<Tochka>();
            listTochka.Add(new Tochka(1.0, 2.0));
            listTochka.Add(new Tochka(5.0, 4.5));
            listTochka.Add(new Tochka(15.0, 15.5));

            Console.WriteLine(StrokaTochka(listTochka));
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

        static string[] Spliter(string line)
        {
            string[] answer = new string[4];

            int x1 = line.IndexOf(':');
            answer[0] = line.Substring(0, x1).Trim();

            string line2 = line.Substring(x1 + 1).Trim();

            int i = 0;

            int start1 = i;
            while (line2[i] != ' ')
                i++;
            answer[1] = line2.Substring(start1, i - start1);

            while (line2[i] == ' ')
                i++;

            i++;
            int start2 = i;
            while (line2[i] != '"')
                i++;
            answer[2] = line2.Substring(start2, i - start2);
            i++;

            while (i < line2.Length && line2[i] == ' ')
                i++;

            answer[3] = line2.Substring(i).Trim();

            return answer;
        }
    }
}