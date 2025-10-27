using System;
using System.IO;

namespace StoreApp.Services
{
    public static class Logger
    {
        private static readonly string logFile = "log.txt";
        private static Queue<string> _logs = new Queue<string>();
        private static readonly int LogAreaHeight = 8;

        public static void Info(string message)
        {
            string logMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";

            // Добавляем лог в очередь
            _logs.Enqueue(logMessage);
            if (_logs.Count > LogAreaHeight)
                _logs.Dequeue();

            // Перерисовываем лог-область
            DrawLogArea();
        }

        private static void DrawLogArea()
        {
            int windowHeight = Console.WindowHeight;
            int logStart = windowHeight - LogAreaHeight;

            for (int i = 0; i < LogAreaHeight; i++)
            {
                Console.SetCursorPosition(0, logStart + i);
                Console.Write(new string(' ', Console.WindowWidth)); // очищаем строку
            }

            int row = 0;
            foreach (var log in _logs)
            {
                Console.SetCursorPosition(0, logStart + row);
                Console.Write(log);
                row++;
            }

            // Возврат курсора в верх UI
            Console.SetCursorPosition(0, 0);
        }
    }
}