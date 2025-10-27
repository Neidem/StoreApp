using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Helpers
{
    public static class UIHelper
    {
        public static int MenuSelect(string[] options, string title = "")
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();

                if (!string.IsNullOrEmpty(title))
                    Console.WriteLine(title + "\n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && index > 0) index--;
                else if (key == ConsoleKey.DownArrow && index < options.Length - 1) index++;

            } while (key != ConsoleKey.Enter);

            return index;
        }
    }
}
