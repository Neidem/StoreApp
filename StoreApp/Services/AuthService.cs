using StoreApp.Data;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace StoreApp.Services
{
    class AuthService
    {
        private List<UserAccount> users;

        public AuthService()
        {
            users = DataManager.LoadUsers();
        }

        public static string GetMd5Hash(string input)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
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
                    Console.WriteLine("❌ Логин не может быть пустым!");
                else if (users.Any(u => u.Username.Equals(login, StringComparison.OrdinalIgnoreCase)))
                    Console.WriteLine("❌ Такой пользователь уже существует!");
                else
                    break;

            } while (true);

            string password;
            do
            {
                Console.Write("Введите пароль (мин. 4 символа): ");
                password = Console.ReadLine();

                if (string.IsNullOrEmpty(password) || password.Length < 4)
                    Console.WriteLine("❌ Слишком короткий пароль!");
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
                    Console.WriteLine("❌ Некорректная роль!");
            } while (true);

            var newUser = new UserAccount
            {
                Username = login,
                PasswordHash = GetMd5Hash(password),
                Role = role
            };

            users.Add(newUser);

            // ✅ Сохраняем в реальный путь
            var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json");
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataPath, json);

            Console.WriteLine("✅ Пользователь успешно добавлен!");
            Console.ReadKey();
        }



        public UserAccount Login()
        {
            Console.Write("Логин: ");
            string login = Console.ReadLine();
            Console.Write("Пароль: ");
            string password = Console.ReadLine();

            string hash = GetMd5Hash(password);

            var user = users.Find(u => u.Username == login && u.PasswordHash == hash);

            if (user != null)
            {
                Console.WriteLine($" Успешный вход ({user.Role})");
                return user;
            }

            Console.WriteLine("❌ Неверный логин или пароль");
            return null;
        }
    }
}
