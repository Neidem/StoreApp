using StoreApp.Services;
using StoreApp.Models;
using StoreApp.Enums;
using System.Security;
using StoreApp.Interface;
using StoreApp.Helpers;


namespace StoreApp
{


   sealed class Program
    {
        private static void Main(string[] args)
        {
            AppState currentState = AppState.MainMenu;

            IDataManager dataManager = DependencyContainer.GetDataManager();
            IAuthService authService = DependencyContainer.GetAuthService();
            IUserRegistrationService registrationService = DependencyContainer.GetRegistrationService();
            IStoreService storeService = DependencyContainer.GetStoreService();


            UserAccount user = null;
            var dataPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,"Data","users.json");
            string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);

            string path = Path.Combine(dataDir, "products.json");
            if (!File.Exists(path))
                File.WriteAllText(path, "[]"); // создаем пустой JSON, если нет

           

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
            //var authService = new AuthService();

           

            while (true)
            {
                int choice = UIHelper.MenuSelect(new[] 
                { 
                    "Войти",
                    "Зарегистрироваться",
                    "Выход" 
                }, "Магазинчик");// 0 - Войти и т.д

                if (choice == 0) // Войти
                {
                    Console.Clear();
                    Console.WriteLine($"📂 Используется файл: {dataPath}");
                    Console.Write("Логин: ");
                    string username = Console.ReadLine();
                    Console.Write("Пароль: ");
                    string password = Console.ReadLine();
                    //  user = authService.Authenticate(username, password);

                    var result = authService.AuthenticateUser(username, password);
                    if (!result.Succes)
                    {
                        if (result.IsBanned)
                            Console.WriteLine("Пользователь был заблокирован администратором!");
                        else
                            Console.WriteLine("Неверный логин или пароль!");
                        Console.ReadKey();
                        return;
                       
                        
                   

                    }
    
                    
                    Console.WriteLine($"Добро пожаловать, {username}");
                    Console.ReadKey();
                    UIHelper.ShowLoadingAnimation(3.55);
                    result.UserMenu.ShowMenu();
                    
                }
                else if (choice == 1) // Зарегистрироваться
                {
                    Console.Clear();
                    registrationService.RegisterUser(); // реализуй регистрацию через AuthService
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