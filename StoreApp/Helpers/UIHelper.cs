using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Helpers
{
    public static class UIHelper
    {
        public static bool TryReadInt(string promt, out int value)
        {
            value = 0;
            while (true)
            {
                ConsoleHelper.ClearUIArea(3);
                Console.WriteLine($"{promt} (или Esc для выхода):");

                var key= Console.ReadKey(intercept:true);
                if(key.Key==ConsoleKey.Escape)
                    return false;

                if(!char.IsDigit(key.KeyChar))
                {
                    Console.WriteLine("\n Введите корректное число");
                    Console.ReadKey();
                    continue;

                } 
                
                Console.WriteLine(key.KeyChar);
                string input = key.KeyChar +Console.ReadLine();
                if (int.TryParse(input, out int result ))
                {
                    value = result;
                    return true;

                }

                Console.WriteLine("\n Ошибка ввода, попробуйте снова");
                Console.ReadKey();

             
            }
            
        }

        public static int MenuSelect(string[] options, string title = "")
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                ConsoleHelper.ClearUIArea(3);

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

        public static bool TryReadString(string promt, out string result)
        {
            result = string.Empty;
            ConsoleHelper.ClearUIArea(3);
            Console.WriteLine($"{promt} (или ESc для выхода)");

            var key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Escape)
            {
                return false;
            }

            Console.Write(key.KeyChar);
            result = key.KeyChar + Console.ReadLine();

            if (string.IsNullOrEmpty(result))
            {
                Console.WriteLine("Пустая строка недопустима!");
                Console.ReadKey();
                return TryReadString(promt, out result);
            }
            return true;
        }

        public static void ShowLoadingAnimation(double seconds)
        {
            var spinner = new[] { "|", "/", "-", "\\" };
            var start = DateTime.Now;
            int counter = 0;

            while ((DateTime.Now - start).TotalSeconds<seconds)
            {
                Console.Write($"\r Загрузка {spinner[counter++ %spinner.Length]}");
                Thread.Sleep(100);
            }

            Console.WriteLine("\r Загрузка завершена! \n");
            Thread.Sleep(400);
        }

        public static bool Confirm(string message)
        {
            int choice = MenuSelect(new[]
            {
                "Да",
                "Нет"
            }, message);
            return choice == 0;
        }
    }
}
