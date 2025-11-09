using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Models;
using System.IO.Pipes;
using System.Net.WebSockets;

namespace StoreApp.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IDataManager _dataManager;
        
        
        public AdminUserService(IDataManager dataManager)
        {
            _dataManager = dataManager;
           
        }
           
        public bool ToggleBanUser(UserAccount user)
        {
            var users = _dataManager.LoadUsers();
            var _current = users.FirstOrDefault(u=>u.Username == user.Username);

            if(_current == null)
            {
                Console.WriteLine($"Пользователь {user.Username} не найден! ");
                Console.ReadKey();
                return true;
            }

            if(_current.Role =="admin")
            {
                Console.WriteLine("Нельзя блокировать администратора");
                return true;
            }

            _current.IsBanned = !_current.IsBanned;

            _dataManager.SaveUsers(users);
            Console.WriteLine(_current.IsBanned
                ? $"Пользователь {user.Username} Заблокирован!"
                : $"Пользователь {user.Username} Разблокирован!");
            Console.ReadKey();
          
            user.IsBanned = _current.IsBanned;
            return true;
        }
      

        public void DeleteUser(UserAccount user)
        {
            var users = _dataManager.LoadUsers();
            var existing = users.FirstOrDefault(u => u.Username == user.Username);

            if (existing == null)
            {
                Console.WriteLine($"Пользователь {user.Username} не найден ");
                return;
            }

            if (user.Role == "admin")
            {
                Console.WriteLine("Нельзя удалять администратора");
                Console.ReadKey();
                return;
            }

            if (UIHelper.Confirm($"Удалить пользователя {user.Username} ?"))
            {
                DeleteUser(user);
                Console.WriteLine($"Пользователь {user.Username} удалён.");
                Console.ReadKey();
            }

            users.Remove(existing);
            _dataManager.SaveUsers(users);

            Console.WriteLine($"Пользователь {user.Username} успешно удалён");

        }

        
        

    }
}
