using System;
using System.IO;

namespace StoreApp.Services
{
    public static class Logger
    {
        private static readonly string logFile = "log.txt";

        public static void Info(string message)
        {
            Write("INFO", message);
        }

        public static void Warning(string message)
        {
            Write("WARN", message);
        }

        public static void Error(string message)
        {
            Write("ERROR", message);
        }

        private static void Write(string level, string message)
        {
            string log = $"[{DateTime.Now:HH:mm:ss}] [{level}] {message}";
            Console.ForegroundColor = level switch
            {
                "INFO" => ConsoleColor.Gray,
                "WARN" => ConsoleColor.Yellow,
                "ERROR" => ConsoleColor.Red,
                _ => ConsoleColor.White
            };
            Console.WriteLine(log);
            Console.ResetColor();

            File.AppendAllText(logFile, log + Environment.NewLine);
        }
    }
}