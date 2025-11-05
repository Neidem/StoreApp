using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Helpers;
using StoreApp.Interface;

namespace StoreApp.Models
{
    class Admin : UserAccount,IUserMenu
    {
        private readonly IAdminStoreService _adminStore;
        private readonly IAdminUserService _adminUser;

        public Admin()
        {
            _adminStore = DependencyContainer.GetAdminStoreService();
            _adminUser = DependencyContainer.GetAdminUserService();
        }
        public override void ShowMenu()
        {
            while (true)
            {
                int choice = UIHelper.MenuSelect(new[]
                {
                    "Действие с товаром:",
                    "Просмотреть список пользователей",
                    "Выйти"
                }, $"Меню администратора [{Username}]");

                switch (choice)
                {
                    case 0:
                        Console.Clear();
                        _adminStore.ShowProductMenu(this);
                        Console.ReadKey();
                        break;
                    case 1:
                        UIHelper.ShowLoadingAnimation(1.55); // имитация загрузки
                        _adminUser.ViewAllUsers(this);
                        Console.ReadKey();
                        break;
                    case 2:

                        return; // выход из меню
                }
            }
        }
    }
}