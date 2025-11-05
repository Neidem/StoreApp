using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StoreApp.Interface;

namespace StoreApp.Services
{
    internal class UserRegistrationService : IUserRegistrationService
    {

        private readonly IDataManager _dataManager;
        private List<UserAccount> _users;

        public UserRegistrationService(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _users = _dataManager.LoadUsers();
        }

        public void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("=== Добавление нового пользователя ===");

            string login;
            do
            {
                Console.Write("Введите логин: ");
                login = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(login))
                    Console.WriteLine(" Логин не может быть пустым!");
                else if (_users.Any(u => u.Username.Equals(login, StringComparison.OrdinalIgnoreCase)))
                    Console.WriteLine(" Такой пользователь уже существует!");
                else
                    break;

            } while (true);

            string password;
            do
            {
                Console.Write("Введите пароль (мин. 4 символа): ");
                password = Console.ReadLine();

                if (string.IsNullOrEmpty(password) || password.Length < 4)
                    Console.WriteLine(" Слишком короткий пароль!");
                else
                    break;

            } while (true);

            string role;
            do
            {
                Console.Write("Введите роль (admin / customer): ");
                role = Console.ReadLine()?.ToLower().Trim();

                if (role == "admin" || role == "customer")
                    break;
                else
                    Console.WriteLine(" Некорректная роль!");
            } 
            while (true);

            var newUser = new UserAccount
            {
                Username = login,
                PasswordHash = AuthService.GetMd5Hash(password),
                Role = role,
                IsBanned = false

            };

            _users.Add(newUser);
            _dataManager.SaveUsers(_users);

            var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json");
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });

            Console.ReadKey();
            File.WriteAllText(dataPath, json);

            Console.WriteLine(" Пользователь успешно добавлен!");
            Console.ReadKey();
        }


    }
}
