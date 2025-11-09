using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Log
{
    public static class ConsoleLogPanel
    {
        private static readonly Queue<string> _logs = new();
        private const int LogAreaHeight = 3;
        private static readonly object _lockObj = new();

        public static void Write(string msg)
        {
            lock(_lockObj)
            {
                _logs.Enqueue(msg);
                if(_logs.Count > LogAreaHeight) 
                    _logs.Dequeue();

                Redraw();

            }
        }

        private static void Redraw()
        {
            int windowHeight = Console.WindowHeight;
            int start = windowHeight - LogAreaHeight;

            for (int i = 0; i < LogAreaHeight; i++)
            {
                Console.SetCursorPosition(0, start + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            int row = 0;
            foreach (var log in _logs)
            {
                Console.SetCursorPosition(0, start + row);
                Console.Write(log);
                row++;
            }

            Console.SetCursorPosition(0, 0);

        }

    }
}
