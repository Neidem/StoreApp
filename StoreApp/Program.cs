using StoreApp.Services;
using StoreApp.Data;
using StoreApp.Models;


namespace StoreApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            UserAccount user = null;

            Logger.Info("Приложение запущено");

            // --- Меню навигации стрелками ---
            static int MenuSelect(string[] options)
            {
                int index = 0;
                ConsoleKey key;

                do
                {
                    Console.Clear();
                    Console.WriteLine("Используйте ↑ ↓ и Enter для выбора:\n");

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
            var authService = new AuthService();

            while (user == null)
            {
                int choice = MenuSelect(new[] { "Войти", "Зарегистрироваться", "Выход" });

                if (choice == 0) // Войти
                {
                    Console.Clear();
                    Console.Write("Логин: ");
                    string username = Console.ReadLine();
                    Console.Write("Пароль: ");
                    string password = Console.ReadLine();

                    user = authService.Authenticate(username, password);
                    if (user != null)
                    {
                        Console.WriteLine($"Добро пожаловать, {user.Username}!");
                        user.ShowMenu(); // Полиморфизм
                        
                    }
                    else
                    {
                        Console.WriteLine("Неверный логин или пароль.");
                        Console.ReadKey();
                    }
                }
                else if (choice == 1) // Зарегистрироваться
                {
                    Console.Clear();
                    authService.RegisterUser(); // реализуй регистрацию через AuthService
                }
                else // Выход
                {
                    break;
                }
            }

            

            //static void AdminMenu(AuthService auth)
            //{
            //    while (true)
            //    {
            //        Console.Clear();
            //        int choice = MenuSelect(new[] { "Добавить пользователя", "Назад" });

            //        if (choice == 0)
            //            auth.RegisterUser();
            //        else
            //            break;
            //    }
            //}


        }
    }
}