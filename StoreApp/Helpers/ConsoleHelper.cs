using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Helpers
{
    public static class ConsoleHelper
    {
        public static void ClearUIArea (int logHeight)
        {
            int windowHeight = Console.WindowHeight;
            int uiHeight = windowHeight - logHeight;

            for (int i = 0; i < uiHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, 0);
        }
    }
}
