using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Models;

namespace StoreApp.Services.Users
{
    class Admin : UserAccount,IUserMenu
    {
        private readonly IAdminStoreMenu _adminStoreMenu;
        private readonly IAdminUserMenu _adminUserMenu;


        public Admin()
        {
           _adminStoreMenu = DependencyContainer.GetAdminStoreMenu();
           _adminUserMenu = DependencyContainer.GetAdminUserMenu(); 
        }
        public override void ShowMenu()
        {
            while (true)
            {
                int choice = UIHelper.MenuSelect(new[]
                {
                    "Действие с товаром:",
                    "Список всех пользователей",
                    "Выйти"
                }, $"Меню администратора [{Username}]");

                switch (choice)
                {
                    case 0:
                        ConsoleHelper.ClearUIArea(3);
                        _adminStoreMenu.ShowProductMenu(this);
                        Console.ReadKey();
                        break;
                    case 1:
                       // UIHelper.ShowLoadingAnimation(1.55); // имитация загрузки
                        _adminUserMenu.ViewAllUsers(this);
                        Console.ReadKey();
                        break;
                    case 2:

                        return; // выход из меню
                }
            }
        }
    }
}