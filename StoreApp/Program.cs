using StoreApp.Services;
using StoreApp.Models;

class Program
{
    static void Main()
    {
        var auth = new AuthService();
        UserAccount user = null;

        while (true)
        {
            Console.Clear();
            int selected = MenuSelect(new[] { "Вход", "Выход" });

            if (selected == 0)
            {
                user = auth.Login();
                if (user != null)
                {
                    if (user.Role == "admin")
                        AdminMenu(auth);
                    else if (user.Role == "customer")
                        CustomerMenu();
                }
            }
            else
            {
                Console.WriteLine("Выход из программы...");
                break;
            }
        }
    }

    

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

    
    
        static void AdminMenu(AuthService auth)
        {
            while (true)
            {
                Console.Clear();
                int choice = MenuSelect(new[] { "Добавить пользователя", "Назад" });

                if (choice == 0)
                    auth.RegisterUser();
                else
                    break;
            }
        }

   

    static void CustomerMenu()
    {
        Console.Clear();
        Console.WriteLine("Меню клиента (позже добавим покупки и возвраты)");
        Console.ReadKey();
    }
}
