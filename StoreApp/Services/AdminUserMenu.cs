using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    public class AdminUserMenu : IAdminUserMenu
    {
        private readonly IDataManager _dataManager;
        private readonly IAdminUserService _adminUserService;
        private readonly IAdminUserViewer _viewer;

        public AdminUserMenu(IDataManager dataManager,IAdminUserService adminUserService, IAdminUserViewer viewer)
        {
            _dataManager = dataManager;
            _adminUserService = adminUserService;
            _viewer = viewer;
        }

        public bool ViewAllUsers(UserAccount user)
        {
            ConsoleHelper.ClearUIArea(3);
            Console.WriteLine($"\n Администратор: {user.Username}");
            Console.WriteLine("Загрузка списка пользователей... ");

            var users = _dataManager.LoadUsers();
            if (!users.Any())
            {
                Console.WriteLine("Пользователей нет");
                Console.ReadKey();
                return false;
            }

            while (true)
            {
                ConsoleHelper.ClearUIArea(3);
                string[] options = users
                    .Select(u =>
                   {
                       string role = u.Role.PadRight(8);
                       string status = u.IsBanned ?
                       "[Заблокирован]" : "Активен";

                       return $"{u.Username,-15} | {role} | {status}";
                   })
                    .Append("Назад")
                    .ToArray();
                
                int choice = UIHelper.MenuSelect(options,
                   
                    $"\n Всего пользователей {users.Count} \n "+
                    "Выберите пользователя:");

                if (choice == options.Length - 1)
                {
                    return false;
                }

                var selectedUser = users[choice];

                if (!ShowUserActions(selectedUser))
                {
                    return false;
                }
            }
        }


        private bool ShowUserActions(UserAccount user)
        {
            string status = user.IsBanned ? "Заблокирован" : "Активен";
            while (true)
            {
                ConsoleHelper.ClearUIArea(3);

                int choice = UIHelper.MenuSelect(new[]
                {
                    user.IsBanned ? "Разблокировать": "Заблокировать",
                    "Удалить пользователя",
                    "Заказы пользователя",
                    "Корзина пользователя",
                    "Назад"

               }, $"Пользователь: {user.Username} \n Статус {status}");

                switch (choice)
                {
                    case 0:
                       _adminUserService.ToggleBanUser(user);
                        break;
                    case 1:
                       _adminUserService.DeleteUser(user);
                        break;
                    case 2:
                        _viewer.ShowOrdersUser(user);
                        break;
                    case 3:
                        _viewer.ShowCartUser(user);
                        break;
                    case 4:
                        return true;

                }

            }

        }

       

    }
}
