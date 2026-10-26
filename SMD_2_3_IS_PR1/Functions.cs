using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SMD_2_3_IS_PR1
{
    class Functions
    {
        const string FileName = "data.txt";

        static public void SaveToFile(List<IOperation> operations)
        {
            List<string> lines = new List<string>();
            foreach (IOperation op in operations) lines.Add(op.type + ": " + op.date.ToString("yyyy.MM.dd") + " \"" + op.source + "\" " + op.sum);

            try
            {
                File.WriteAllLines(FileName, lines);
                Console.WriteLine("- Сохранено строк: " + lines.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("- Ошибка при сохранении файла: " + ex.Message);
                Logger.Error("Ошибка сохранения: " + ex.Message); // пишем в лог
            }
            finally
            {
                Console.WriteLine("- Операция сохранения завершена.");
            }
        }

        static public void LoadFromFile(List<IOperation> operations)
        {
            try
            {
                if (!File.Exists(FileName))
                {
                    Console.WriteLine("- Файл " + FileName + " не найден.");
                    return;
                }

                string[] lines = File.ReadAllLines(FileName);
                int loaded = 0;

                foreach (string line in lines)
                {
                    string[] p = Spliter(line);
                    if (p[0] == "error")
                    {
                        Logger.Error("Битая строка в файле: " + line);
                        continue;
                    }

                    DateTime date = DateTime.ParseExact(p[1], "yyyy.MM.dd", null);
                    int sum = int.Parse(p[3]);

                    if (p[0] == "Доходы") operations.Add(new Income(p[0], date, p[2], sum));
                    else if (p[0] == "Расходы") operations.Add(new Expense(p[0], date, p[2], sum));

                    loaded++;
                }

                Console.WriteLine("- Загружено строк: " + loaded);
            }
            catch (Exception ex)
            {
                Console.WriteLine("- Ошибка при загрузке файла: " + ex.Message);
                Logger.Error("Ошибка загрузки: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("- Операция загрузки завершена.");
            }
        }

        static public void PrintOperations(List<IOperation> operations)
        {
            int number = 0;
            foreach (IOperation op in operations)
            {
                number++;
                Console.WriteLine("------------");
                Console.WriteLine("Number  " + number);
                Console.WriteLine("Type:   " + op.type);
                Console.WriteLine("Date:   " + op.date.ToString("yyyy.MM.dd"));
                Console.WriteLine("Source: " + op.source);
                Console.WriteLine("Sum:    " + op.sum);
            }
            Console.WriteLine("------------");
        }

        static public string[] Spliter(string line)
        {
            string[] error = new string[] { "error", "", "", "" };

            if (string.IsNullOrWhiteSpace(line)) return error;

            int x1 = line.IndexOf(':');
            if (x1 < 0) return error;

            string type = line.Substring(0, x1).Trim();
            if (type.Length == 0) return error;

            string line2 = line.Substring(x1 + 1).Trim();
            if (line2.Length == 0) return error;

            int i = 0;

            // дата
            int start1 = i;
            while (i < line2.Length && line2[i] != ' ') i++;
            if (i == start1) return error;
            string dateStr = line2.Substring(start1, i - start1);

            DateTime date;
            if (!DateTime.TryParseExact(dateStr, "yyyy.MM.dd", null,
                    DateTimeStyles.None, out date))
                return error;

            while (i < line2.Length && line2[i] == ' ') i++;
            if (i >= line2.Length) return error;

            // источник в кавычках
            if (line2[i] != '"') return error;
            i++;
            int start2 = i;
            while (i < line2.Length && line2[i] != '"') i++;
            if (i >= line2.Length) return error;
            string source = line2.Substring(start2, i - start2);
            i++;

            while (i < line2.Length && line2[i] == ' ') i++;
            if (i >= line2.Length) return error;

            // сумма
            string sumStr = line2.Substring(i).Trim();
            int sum;
            if (!int.TryParse(sumStr, out sum)) return error;

            return new string[] { type, dateStr, source, sumStr };
        }
    }
}
