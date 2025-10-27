using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Helpers;

namespace StoreApp.Models
{
    class Admin : UserAccount,IUserMenu
    {
        public override void ShowMenu()
        {
            while (true)
            {
                int choice = UIHelper.MenuSelect(new[]
                {
                    "Добавить:",
                    "Просмотреть список пользователей",
                    "Удалить:",
                    "Просмотреть список заказов",
                    "Выйти"
                }, "Меню администратора");

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Добавление пользователя...");
                        // вызвать метод из AuthService
                        Console.ReadKey();
                        break;
                    case 1:
                        Console.WriteLine("Список пользователей (позже добавим)");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Удаление пользователя...");
                        Console.ReadKey();
                        break;
                    case 3:

                        return; // выход из меню
                }
            }
        }
    }
}