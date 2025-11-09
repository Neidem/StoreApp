using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Log;
using StoreApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public class Application : IApp
    {
        private readonly IAuthService _authService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly ILogger _logger;

        public Application(
            IAuthService authService,
            IUserRegistrationService userRegistrationService,
            ILogger logger)
        {
            _authService = authService;
            _userRegistrationService = userRegistrationService;
            _logger = logger;
        }

        public void Run()
        {
            while (true)
            {
                int choice = UIHelper.MenuSelect(new[]
                {
                    "Войти",
                    "Зарегистрироваться",
                    "Выход"
                }, "Магазинчик");

                switch (choice)
                {
                    case 0:
                        Login();
                        break;
                    case 1:
                        Register();
                        break;
                    case 2:
                        return;

                }

            }

        }

        private void Login()
        {
            ConsoleHelper.ClearUIArea(3);
            Console.Write("Логин: ");
            string username = Console.ReadLine();

            Console.Write("Пароль: ");
            string password = Console.ReadLine();
           

            var result = _authService.AuthenticateUser(username, password);
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
            _logger.Info(LogEvents.UserLogin(username));
            UIHelper.ShowLoadingAnimation(0.35);
            result.UserMenu.ShowMenu();

        }

        private void Register()
        {
            ConsoleHelper.ClearUIArea(3);
            _userRegistrationService.RegisterUser();
        }


    }
}
