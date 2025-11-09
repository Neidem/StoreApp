using StoreApp.Interface;
using StoreApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace StoreApp.Services.Auth
{
   public class AuthService : IAuthService
    {
        private readonly IDataManager _dataManager;
        private readonly List<UserAccount> _users;


        // private readonly List<UserAccount> users;
        private string usersFilePath;

        // Конструктор с аргументом
        //public AuthService(string usersFile)
        //{
        //    usersFilePath = usersFile;
        //    users = DataManager.LoadUsers();
        //}

        public AuthService(IDataManager dataManager)
        {
            //  Logger.Info("аутентификация");
            _dataManager = dataManager;
            _users = _dataManager.LoadUsers();
        }

        // Можно оставить пустой конструктор по умолчанию
        //public AuthService() : this("Data/users.json") { }

        //public static string GetMd5Hash(string input)
        //{
        //    using var md5 = MD5.Create();
        //    var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        //    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        //}

        //public void RegisterUser()
        //{
        //    ConsoleHelper.ClearUIArea(3);
        //    Console.WriteLine("=== Добавление нового пользователя ===");

        //    string login;
        //    do
        //    {
        //        Console.Write("Введите логин: ");
        //        login = Console.ReadLine()?.Trim();

        //        if (string.IsNullOrEmpty(login))
        //            Console.WriteLine(" Логин не может быть пустым!");
        //        else if (users.Any(u => u.Username.Equals(login, StringComparison.OrdinalIgnoreCase)))
        //            Console.WriteLine(" Такой пользователь уже существует!");
        //        else
        //            break;

        //    } while (true);

        //    string password;
        //    do
        //    {
        //        Console.Write("Введите пароль (мин. 4 символа): ");
        //        password = Console.ReadLine();

        //        if (string.IsNullOrEmpty(password) || password.Length < 4)
        //            Console.WriteLine(" Слишком короткий пароль!");
        //        else
        //            break;

        //    } while (true);

        //    string role;
        //    do
        //    {
        //        Console.Write("Введите роль (admin / customer): ");
        //        role = Console.ReadLine()?.ToLower().Trim();

        //        if (role == "admin" || role == "customer")
        //            break;
        //        else
        //            Console.WriteLine(" Некорректная роль!");
        //    } while (true);

        //    var newUser = new UserAccount
        //    {
        //        Username = login,
        //        PasswordHash= GetMd5Hash(password),
        //        Role = role
        //    };

        //    users.Add(newUser);

        //    // ✅ Сохраняем в реальный путь
        //    var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json");
        //    string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });

        //   // Console.WriteLine($"DEBUG PATH: {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json")}");
        //    Console.ReadKey();
        //    File.WriteAllText(dataPath, json);

        //    Console.WriteLine(" Пользователь успешно добавлен!");
        //    Console.ReadKey();
        //}

        //public UserAccount Authenticate(string username, string password)
        //{
        //    var user = users.FirstOrDefault(u => u.Username == username);

        //    if (user != null && user.PasswordHash == GetMd5Hash(password))
        //    {
        //        if (user.Role == "admin")
        //            return new Admin { Username = user.Username, Role = user.Role };
        //        else
        //            return new Customer { Username = user.Username, Role = user.Role };
        //    }

        //    return null;
        //}

        public AuthenticationResult AuthenticateUser(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.PasswordHash == GetMd5Hash(password));
           
            if (user == null)
            {
                return new AuthenticationResult { Succes = false };
            }

            if (user.IsBanned)
            {
                return new AuthenticationResult { Succes =  false, IsBanned = true };
            }

            IUserMenu menu = user.Role == "admin"
              ? new Admin { Username = user.Username, Role = user.Role }
              : new Customer { Username = user.Username, Role = user.Role };

            return new AuthenticationResult
            {
                Succes = true,
                User = user,
                UserMenu = menu
            };

        }

        public static string GetMd5Hash(string input)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();



            //using var md5 = System.Security.Cryptography.MD5.Create();
            //var bytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            //return Convert.ToHexString(bytes);
        }

        //public UserAccount Login()
        //{
        //    Console.Write("Логин: ");
        //    string login = Console.ReadLine();
        //    Console.Write("Пароль: ");
        //    string password = Console.ReadLine();

        //    string hash = GetMd5Hash(password);

        //    var user = users.Find(u => u.Username == login && u.PasswordHash == hash);

        //    if (user != null)
        //    {
        //        Console.WriteLine($" Успешный вход ({user.Role})");
        //        return user;
        //    }

        //    Console.WriteLine("❌ Неверный логин или пароль");
        //    Logger.Warning("Пользователь не найден");
        //    return null;
        //}





    }
}
