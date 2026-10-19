using System;
using System.IO;

namespace SMD_2_3_IS_PR1
{
    static class Logger
    {
        static string fileName = "log.txt";

        public static void Error(string message)
        {
            string line = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] ERROR " + message;
            File.AppendAllText(fileName, line + Environment.NewLine);
        }
    }
}